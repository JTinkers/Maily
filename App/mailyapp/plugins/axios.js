export default function ({ $axios, store }, inject)
{
	const api = $axios.create()

	api.setBaseURL('/api')

	api.refreshToken = function()
	{
		if(process.client && store.state.auth && store.state.auth.user)
			api.setToken(store.state.auth.user.token)
	}

	api.refreshToken()

	inject('api', api)
}
