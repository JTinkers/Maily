import Vue from 'vue'

export default function ({ $axios, store }, inject)
{
	const api = $axios.create()

	api.setBaseURL('/api')

	api.refreshToken = function()
	{
		if(process.client && store.state.auth && store.state.auth.user)
			api.setToken(store.state.auth.user.token)
	}

	api.query = async function(query, variables)
	{
		var response = (await api.post('/',
		{
			query: query,
			variables: variables
		})).data

		if(response.errors)
		{
			Vue.prototype.$buefy.toast.open(
			{
				message: response.errors[0].message,
				type: 'is-danger'
			})

			return null
		}

		return response
	}

	api.refreshToken()

	inject('api', api)
}
