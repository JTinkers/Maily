<template>
	<div class='columns'>
		<div class='column is-one-quarter'>
			<div class='panel is-primary' @drop='(e) => dropDraggable(e, null)' @dragover.prevent @dragenter.prevent>
				<p class='panel-heading' v-text='"Addresses"'/>
				<div v-for='mail in mails' class='address panel-block' draggable @dragstart='(e) => startDragging(e, mail, null)'>
					<span v-text='mail.value'/>
					<i class='mdi mdi-drag'/>
				</div>
			</div>
		</div>
		<div class='columns column is-multiline'>
			<div v-for='mailGroup in mailGroups' :key='mailGroup.id' class='column is-one-quarter'>
				<div class='panel' @drop='(e) => dropDraggable(e, mailGroup)' @dragover.prevent @dragenter.prevent>
					<p class='panel-heading' v-text='mailGroup.name'/>
					<div v-for='mail in mailGroup.mailGroupMails.nodes.map(x => x.mail)' :key='mail.id' class='address panel-block' draggable @dragstart='(e) => startDragging(e, mail, mailGroup)'>
						<span v-text='mail.value'/>
						<i class='mdi mdi-drag'/>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	import DataQuery from './-queries/DataQuery'
	import AddMailToMailGroupMutation from './-queries/AddMailToMailGroupMutation'
	import DeleteMailFromMailGroupMutation from './-queries/DeleteMailFromMailGroupMutation'

	export default
	{
		data: () =>
		({
			mails: [],
			mailGroups: []
		}),
		mounted()
		{
			this.fetch()
		},
		methods:
		{
			async fetch()
			{
				var response = (await this.$api.post('/',
				{
					query: DataQuery
				})).data

				if(response.errors)
				{
					this.$buefy.toast.open(
					{
						message: response.errors[0].message,
						type: 'is-danger'
					})

					return
				}

				this.mails = response.data.mails.nodes
				this.mailGroups = response.data.mailGroups.nodes

				// sort by least emails to make columns align nicely
				this.mailGroups.sort((a, b) => a.mailGroupMails.nodes.length - b.mailGroupMails.nodes.length)

				// add easy navigation property
				this.mailGroups.forEach(x => x.mailGroupMails.nodes.forEach(y =>
				{
					y.mail = this.mails.find(z => z.id == y.mailId)
				}))
			},
			startDragging(e, mail, sourceGroup)
			{
				e.dataTransfer.dropEffect = 'move'
				e.dataTransfer.effectAllowed = 'move'
				e.dataTransfer.setData('mailId', mail.id)
				e.dataTransfer.setData('sourceGroupId', sourceGroup && sourceGroup.id)
			},
			dropDraggable(e, targetGroup)
			{
				var mailId = e.dataTransfer.getData('mailId')
				var sourceGroupId = e.dataTransfer.getData('sourceGroupId')

				var mail = this.mails.find(x => x.id == mailId)
				var sourceGroup = this.mailGroups.find(x => x.id == sourceGroupId)

				// if it's the same group - do nothing
				if(targetGroup == sourceGroup)
					return

				// if target group is null/undefined - remove from source group
				if(!targetGroup)
				{
					this.removeFromMailGroup(mail, sourceGroup)

					return
				}

				// if source group is null/undefined - add to target group
				if(!sourceGroup)
				{
					this.addToMailGroup(mail, targetGroup)

					return
				}

				// if source group and target group aren't null/undefined - copy over via addition
				if(sourceGroup && targetGroup)
				{
					this.addToMailGroup(mail, targetGroup)

					return
				}
			},
			async addToMailGroup(mail, mailGroup)
			{
				var mailGroupMailIndex = mailGroup.mailGroupMails.nodes.map(x => x.mailId).indexOf(mail.id)

				// prevent duplicates
				if(mailGroupMailIndex > -1)
				{
					this.$buefy.toast.open(
					{
						message: 'The address is already in that group!',
						type: 'is-info'
					})

					return
				}

				var response = (await this.$api.post('/',
				{
					query: AddMailToMailGroupMutation,
					variables:
					{
						mailId: mail.id,
						mailGroupId: mailGroup.id
					}
				})).data

				if(response.errors)
				{
					this.$buefy.toast.open(
					{
						message: response.errors[0].message,
						type: 'is-danger'
					})

					return
				}

				// add missing navigation property along with mailgroupmail data
				mailGroup.mailGroupMails.nodes.push({ mail: mail, ...response.data.addMailToMailGroup })
			},
			async removeFromMailGroup(mail, mailGroup)
			{
				var mailGroupMailIndex = mailGroup.mailGroupMails.nodes.map(x => x.mailId).indexOf(mail.id)
				var mailGroupMail = mailGroup.mailGroupMails.nodes[mailGroupMailIndex]

				var response = (await this.$api.post('/',
				{
					query: DeleteMailFromMailGroupMutation,
					variables:
					{
						id: mailGroupMail.id
					}
				})).data

				if(response.errors)
				{
					this.$buefy.toast.open(
					{
						message: response.errors[0].message,
						type: 'is-danger'
					})

					return
				}

				// update list, remove removed element
				mailGroup.mailGroupMails.nodes.removeAt(mailGroupMailIndex)
			}
		}
	}
</script>

<style lang='scss' scoped>
	.address
	{
		> :nth-child(2)
		{
			margin-left: auto;
		}

		cursor: grab;
	}
</style>
