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
			{ rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' },
			{ rel: 'stylesheet', href: 'https://cdn.materialdesignicons.com/5.4.55/css/materialdesignicons.min.css' }
		]
	},
	css:
	[
		'@/assets/scss/defaults.scss',
		'@/assets/scss/bulma.scss'
	],
	plugins:
	[
		'@/plugins/axios',
		'@/plugins/vue-plugins.js',
		'@/plugins/extensions.js'
	],
	components: true,
	buildModules:
	[
		'@nuxtjs/eslint-module'
	],
	modules:
	[
		'nuxt-buefy',
		'@nuxtjs/axios',
		'@nuxtjs/proxy'
	],
	proxy:
	{
		'/api': 'http://localhost:5000/'
	},
	axios:
	{
		proxy: true
	},
	build:
	{
	}
}
