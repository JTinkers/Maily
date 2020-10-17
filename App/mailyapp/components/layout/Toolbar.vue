<template>
	<fragment>
		<SignUpModal ref='signUpModal'/>
		<LogInModal ref='logInModal'/>
		<b-navbar>
			<template #start>
				<b-navbar-item tag='nuxt-link' to='/addresses'>
					<span v-text='"Addresses"'/>
				</b-navbar-item>
				<b-navbar-item tag='nuxt-link' to='/groups'>
					<span v-text='"Groups"'/>
				</b-navbar-item>
				<b-navbar-item tag='nuxt-link' to='/manage'>
					<span v-text='"Manage"'/>
				</b-navbar-item>
			</template>
			<template #end>
				<b-navbar-item v-if='!user' tag='div'>
	                <div class='buttons'>
	                    <a class='button is-primary' @click='openSignUpModal' v-text='"Sign up"'/>
	                    <a class='button is-light' @click='openLogInModal' v-text='"Log in"'/>
	                </div>
	            </b-navbar-item>
				<b-navbar-item v-else tag='div'>
					<b-dropdown position='is-bottom-left'>
						<span id='user' slot='trigger' v-text='user.nickname'/>
						<b-dropdown-item v-text='"Log Out"' @click='logOut'/>
					</b-dropdown>
				</b-navbar-item>
			</template>
		</b-navbar>
	</fragment>
</template>

<script>
	export default
	{
		methods:
		{
			openSignUpModal()
			{
				this.$refs.signUpModal.open()
			},
			openLogInModal()
			{
				this.$refs.logInModal.open()
			},
			logOut()
			{
				this.$store.commit('auth/SET_AUTH', null)

				this.$buefy.toast.open(
				{
					message: 'Logged out successfully!',
					type: 'is-success'
				})

				this.$api.refreshToken()
			}
		},
		computed:
		{
			user()
			{
				return this.$store.state.auth.user
			}
		}
	}
</script>

<style lang='scss' scoped>
	#user
	{
		font-weight: bold;
		cursor: pointer;
	}
</style>
