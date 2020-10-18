<template>
	<div id='content'>
		<NavBar/>
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
