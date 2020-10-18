<template>
	<div class='columns'>
		<div class='column is-one-fifth'>
			<div class='panel'>
				<p class='panel-heading' v-text='"Manage"'/>
				<div class='panel-block'>
					<div id='manageform'>
						<b-field class='flex-grow' label='Group'>
							<b-input v-model='form.name'/>
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
						<b-input v-model='filter.name_contains' placeholder='Search...' icon='magnify' @input='fetch'/><!-- no debounce :(-->
					</div>
				</div>
				<div class='panel-block'>
					<b-table class='flex-grow' :data='mailGroups'>
						<b-table-column v-slot='props' field='id' label='Id' width='40'>
							{{ props.row.id }}
						</b-table-column>
						<b-table-column v-slot='props' field='name' label='Name'>
							{{ props.row.name }}
						</b-table-column>
						<b-table-column v-slot='props' field='groupCount' label='Uses <n> mails'>
							{{ props.row.mailCount }}
						</b-table-column>
						<b-table-column v-slot='props' width='180'>
							<a class='button is-primary' @click='edit(props.row.id)' v-text='"Edit"'/>
							<a class='button is-primary' @click='remove(props.row.id)' v-text='"Remove"'/>
						</b-table-column>
					</b-table>
				</div>
			</div>
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
				name: null
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
				var response = (await this.$api.post('/',
				{
					query: MailGroupListQuery,
					variables:
					{
						filter: this.filter
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

				this.mailGroups = response.data.mailGroups.nodes
				this.mailGroups.forEach(x => x.mailCount = x.mailGroupMails.count)
			},
			async add()
			{
				var response = (await this.$api.post('/',
				{
					query: MailGroupCreateMutation,
					variables:
					{
						input:
						{
							name: this.form.name
						}
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

				this.$buefy.toast.open(
				{
					message: 'Successfully added an address!',
					type: 'is-success'
				})

				response.data.createMailGroup.mailCount = 0
				this.mailGroups.push(response.data.createMailGroup)

				this.clear()
			},
			async save()
			{
				var response = (await this.$api.post('/',
				{
					query: MailGroupUpdateMutation,
					variables:
					{
						input:
						{
							id: this.form.id,
							name: this.form.name
						}
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

				this.$buefy.toast.open(
				{
					message: 'Successfully updated address!',
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
					name: null
				}
			},
			async remove(id)
			{
				var response = (await this.$api.post('/',
				{
					query: MailGroupDeleteMutation,
					variables:
					{
						input:
						{
							id: id
						}
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

				this.$buefy.toast.open(
				{
					message: 'Successfully removed the address!',
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
