import createPersistedState from 'vuex-persistedstate'

export default ({store}) =>
{
	createPersistedState(
	{
		key: 'maily',
		paths:
		[
			'auth'
		]
	})(store)
}
