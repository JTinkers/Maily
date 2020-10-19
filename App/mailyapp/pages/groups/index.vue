<template>
	<div class='columns'>
		<div class='column is-one-fifth'>
			<Panel class='is-primary' header='Manage'>
				<PanelBlock>
					<div id='manageform'>
						<b-field class='flex-grow' label='Group'>
							<b-input v-model='form.name'/>
						</b-field>
						<b-button v-if='!form.id' class='button is-primary' @click='add' v-text='"Add"'/>
						<b-button v-else class='button is-primary' @click='save' v-text='"Save"'/>
					</div>
				</PanelBlock>
			</Panel>
		</div>
		<div class='column'>
			<Panel>
				<template #heading>
					<div id='listheader'>
						<span v-text='"List"'/>
						<b-input v-model='filter.name_contains' placeholder='Search...' icon='magnify' @input='fetch'/><!-- no debounce :(-->
					</div>
				</template>
				<PanelBlock>
					<b-table class='flex-grow' :data='mailGroups'>
						<b-table-column v-slot='props' field='id' label='Id' width='40'>
							<span v-text='props.row.id'/>
						</b-table-column>
						<b-table-column v-slot='props' field='name' label='Name'>
							<span v-text='props.row.name'/>
						</b-table-column>
						<b-table-column v-slot='props' field='groupCount' label='Uses <n> mails'>
							<span v-text='props.row.mailCount'/>
						</b-table-column>
						<b-table-column v-slot='props' width='180'>
							<b-button class='is-info' type='is-info' size='is-small' outlined rounded @click='edit(props.row.id)' v-text='"Edit"'/>
							<b-button class='is-danger' type='is-info' size='is-small' outlined rounded @click='remove(props.row.id)' v-text='"Remove"'/>
						</b-table-column>
					</b-table>
				</PanelBlock>
			</Panel>
		</div>
	</div>
</template>

<script>
	import MailGroupListQuery from './-queries/MailGroupListQuery'
	import MailGroupCreateMutation from './-queries/MailGroupCreateMutation'
	import MailGroupUpdateMutation from './-queries/MailGroupUpdateMutation'
	import MailGroupDeleteMutation from './-queries/MailGroupDeleteMutation'

	export default
	{
		data: () =>
		({
			mailGroups: [],
			filter:
			{
				name_contains: ''
			},
			form:
			{
				id: null,
				name: ''
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
				var response = await this.$api.query(MailGroupListQuery, { filter: this.filter })

				if(!response)
					return

				this.mailGroups = response.data.mailGroups.nodes
				this.mailGroups.forEach(x => x.mailCount = x.mailGroupMails.count)
			},
			async add()
			{
				var response = await this.$api.query(MailGroupCreateMutation, {	input: { name: this.form.name } })

				if(!response)
					return

				this.$buefy.toast.open(
				{
					message: 'Successfully added an group!',
					type: 'is-success'
				})

				response.data.createMailGroup.mailCount = 0
				this.mailGroups.push(response.data.createMailGroup)

				this.clear()
			},
			async save()
			{
				var response = await this.$api.query(MailGroupUpdateMutation, {	input: { id: this.form.id, name: this.form.name	} })

				if(!response)
					return

				this.$buefy.toast.open(
				{
					message: 'Successfully updated group!',
					type: 'is-success'
				})

				this.mailGroups.find(x => x.id == this.form.id).name = response.data.updateMailGroup.name

				this.clear()
			},
			edit(id)
			{
				var mail = this.mailGroups.find(x => x.id == id)

				this.form = Object.assign({}, mail)
			},
			clear()
			{
				this.form =
				{
					id: null,
					name: ''
				}
			},
			async remove(id)
			{
				var response = await this.$api.query(MailGroupDeleteMutation, {	input: { id: id	} })

				if(!response)
					return

				this.$buefy.toast.open(
				{
					message: 'Successfully removed the group!',
					type: 'is-success'
				})

				this.mailGroups = this.mailGroups.filter(x => x.id != id)
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
