<template>
	<fragment>
		<SignUpModal ref='signUpModal'/>
		<LogInModal ref='logInModal'/>
		<b-navbar id='navbar'>
			<template #start>
				<b-navbar-item v-for='route in routes' :key='route.route' tag='nuxt-link' :to='route.route'>
					<span v-text='route.label'/>
				</b-navbar-item>
			</template>
			<template #end>
				<b-navbar-item v-if='!user' tag='div'>
	                <div class='buttons'>
	                    <b-button class='is-primary' rounded @click='openSignUpModal' v-text='"Sign up"'/>
	                    <b-button class='is-light' rounded @click='openLogInModal' v-text='"Log in"'/>
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
		data: () =>
		({
			routes:
			[
				{
					label: 'Addresses',
					route: '/addresses'
				},
				{
					label: 'Groups',
					route: '/groups'
				},
				{
					label: 'Setup',
					route: '/setup'
				}
			]
		}),
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

				this.$api.refreshToken()

				this.$buefy.toast.open(
				{
					message: 'Logged out successfully!',
					type: 'is-success'
				})
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
