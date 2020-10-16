export default
{
	head:
	{
		title: 'mailyapp',
		meta:
		[
			{ charset: 'utf-8' },
			{ name: 'viewport', content: 'width=device-width, initial-scale=1' },
			{ hid: 'description', name: 'description', content: '' }
		],
		link:
		[
			{ rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }
		]
	},
	css:
	[
		'~/assets/scss/defaults.scss',
		'vuesax/dist/vuesax.css'
	],
	plugins:
	[
		'@/plugins/vuesax'
	],
	components: true,
	buildModules:
	[
		'@nuxtjs/eslint-module',
	],
	modules:
	[
		'@nuxtjs/axios',
	],
	axios: {},
	build: {}
}
