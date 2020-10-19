<template>
	<div class='columns'>
		<div class='column is-one-fifth'>
			<div class='panel'>
				<p class='panel-heading' v-text='"Manage"'/>
				<div class='panel-block'>
					<div id='manageform'>
						<b-field class='flex-grow' label='Address'>
							<b-input v-model='form.value'/>
						</b-field>
						<a v-if='!form.id' class='button is-primary' @click='add' v-text='"Add"'/>
						<a v-else class='button is-primary' @click='save' v-text='"Save"'/>
					</div>
				</div>
			</div>
		</div>
		<div class='column'>
			<div class='panel'>
				<div class='panel-heading'>
					<div id='listheader'>
						<span v-text='"List"'/>
						<b-input v-model='filter.value_contains' placeholder='Search...' icon='magnify' @input='fetch'/><!-- no debounce :(-->
					</div>
				</div>
				<div class='panel-block'>
					<b-table class='flex-grow' :data='mails'>
						<b-table-column v-slot='props' field='id' label='Id' width='40'>
							{{ props.row.id }}
						</b-table-column>
						<b-table-column v-slot='props' field='value' label='Address'>
							{{ props.row.value }}
						</b-table-column>
						<b-table-column v-slot='props' field='groupCount' label='Used in <n> groups'>
							{{ props.row.groupCount }}
						</b-table-column>
						<b-table-column v-slot='props' width='180'>
							<b-button class='is-info' type='is-info' size='is-small' outlined rounded @click='edit(props.row.id)' v-text='"Edit"'/>
							<b-button class='is-danger' type='is-info' size='is-small' outlined rounded @click='remove(props.row.id)' v-text='"Remove"'/>
						</b-table-column>
					</b-table>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	import MailListQuery from './-queries/MailListQuery'
	import MailCreateMutation from './-queries/MailCreateMutation'
	import MailUpdateMutation from './-queries/MailUpdateMutation'
	import MailDeleteMutation from './-queries/MailDeleteMutation'

	export default
	{
		data: () =>
		({
			mails: [],
			filter:
			{
				value_contains: ''
			},
			form:
			{
				id: null,
				value: ''
			}
		}),
		mounted()
		{
			this.fetch()
		},
		methods:
		{
			async fetch()
			{
				var response = await this.$api.query(MailListQuery, { filter: this.filter })

				if(!response)
					return

				this.mails = response.data.mails.nodes
				this.mails.forEach(x => x.groupCount = x.mailGroupMails.count)
			},
			async add()
			{
				var response = await this.$api.query(MailCreateMutation, { input: {	value: this.form.value } })

				if(!response)
					return

				this.$buefy.toast.open(
				{
					message: 'Successfully added an address!',
					type: 'is-success'
				})

				response.data.createMail.groupCount = 0
				this.mails.push(response.data.createMail)

				this.clear()
			},
			async save()
			{
				var response = await this.$api.query(MailUpdateMutation, { input: {	id: this.form.id, value: this.form.value } })

				if(!response)
					return

				this.$buefy.toast.open(
				{
					message: 'Successfully updated address!',
					type: 'is-success'
				})

				this.mails.find(x => x.id == this.form.id).value = response.data.updateMail.value

				this.clear()
			},
			edit(id)
			{
				var mail = this.mails.find(x => x.id == id)

				this.form = Object.assign({}, mail)
			},
			clear()
			{
				this.form =
				{
					id: null,
					value: ''
				}
			},
			async remove(id)
			{
				var response = await this.$api.query(MailDeleteMutation, { input: {	id: id } })

				if(!response)
					return

				this.$buefy.toast.open(
				{
					message: 'Successfully removed the address!',
					type: 'is-success'
				})

				this.mails = this.mails.filter(x => x.id != id)
			}
		}
	}
</script>

<style lang='scss' scoped>
	#listheader
	{
		display: flex;
		align-items: center;

		> :nth-child(2)
		{
			margin-left: auto;
		}
	}

	#manageform
	{
		display: flex;
		flex-grow: 1;
		flex-direction: column;
		align-items: stretch;
	}

	.flex-grow
	{
		flex-grow: 1;
	}
</style>
