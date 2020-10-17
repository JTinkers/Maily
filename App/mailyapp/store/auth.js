export const state = () =>
({
	user: null
})

export const mutations =
{
	SET_AUTH(state, user)
	{
		state.user = user
	}
}
