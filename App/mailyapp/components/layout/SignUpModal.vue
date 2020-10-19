<template>
	<b-modal v-model='isActive' @close='clear'>
		<div id='signup' class='panel is-primary'>
			<p class='panel-heading' v-text='"Sign Up"'/>
			<div id='fields' class='panel-block'>
				<b-field label='Nickname'>
					<b-input v-model='form.nickname'/>
				</b-field>
				<b-field label='Username'>
					<b-input v-model='form.username'/>
				</b-field>
				<b-field label='Password'>
					<b-input v-model='form.password' type='password'/>
				</b-field>
				<a class='button is-primary' @click='signUp' v-text='"Submit"'/>
			</div>
		</div>
	</b-modal>
</template>

<script>
	export default
	{
		data: () =>
		({
			isActive: false,
			form:
			{
				nickname: null,
				username: null,
				password: null
			}
		}),
		methods:
		{
			open()
			{
				this.isActive = true
			},
			close()
			{
				this.isActive = false

				this.clear()
			},
			clear()
			{
				this.form =
				{
					username: null,
					password: null
				}
			},
			async signUp()
			{
				const query = `mutation($nickname: String!, $username: String!, $password: String!) {
				  userSignUp(nickname: $nickname, username: $username, password: $password) {
					id
					nickname
					token
				  }
				}`

				var response = await this.$api.query(query,
				{
					nickname: this.form.nickname,
					username: this.form.username,
					password: this.form.password
				})

				if(!response)
					return

				this.$store.commit('auth/SET_AUTH', response.data.userSignUp)

				this.$buefy.toast.open(
				{
                    message: 'Logged in successfully!',
                    type: 'is-success'
                })

				this.$api.refreshToken()

				this.$router.push('/')

				this.close()
			}
		}
	}
</script>

<style lang='scss' scoped>
	#signup
	{
		max-width: 320px;
		margin: auto;

		#fields
		{
			display: flex;
			flex-direction: column;

			.field, .button
			{
				flex-grow: 1;
				align-self: stretch;
			}
		}
	}
</style>
