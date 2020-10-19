<template>
	<div id='content'>
		<no-ssr>
			<NavBar/>
		</no-ssr>
		<div id='view'>
			<nuxt/>
		</div>
	</div>
</template>

<style lang='scss'>
	html
	{
		overflow: hidden;
	}

	body
	{
		display: flex;
		background: rgb(245, 245, 245);
		min-height: 100vh;
	}

	.page-enter-active
	{
		animation: fadeIn 0.25s;
	}
	.page-leave-active
	{
		animation: fadeOut 0.25s;
	}

	#__nuxt, #__layout, #content
	{
		display: flex;
		flex-grow: 1;
	}

	#content
	{
		flex-direction: column;
	}

	#view
	{
		padding: 1.5rem;
	}
</style>
