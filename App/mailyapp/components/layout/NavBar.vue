<template>
	<fragment>
		<SignUpModal ref='signUpModal'/>
		<LogInModal ref='logInModal'/>
		<b-navbar id='navbar'>
			<template #start>
				<b-navbar-item tag='nuxt-link' to='/addresses'>
					<span v-text='"Addresses"'/>
				</b-navbar-item>
				<b-navbar-item tag='nuxt-link' to='/groups'>
					<span v-text='"Groups"'/>
				</b-navbar-item>
				<b-navbar-item tag='nuxt-link' to='/setup'>
					<span v-text='"Setup"'/>
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
						<b-dropdown-item @click='logOut' v-text='"Log Out"'/>
					</b-dropdown>
				</b-navbar-item>
			</template>
		</b-navbar>
	</fragment>
</template>

<script>
	export default
	{
		computed:
		{
			user()
			{
				return this.$store.state.auth.user
			}
		},
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
		}
	}
</script>

<style lang='scss' scoped>
	#navbar
	{
		border-bottom: 1px solid rgb(240, 240, 240);
	}

	#user
	{
		font-weight: bold;
		cursor: pointer;
	}
</style>
