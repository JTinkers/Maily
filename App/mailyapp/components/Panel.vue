<template>
	<div class='panel' v-on='$listeners'>
		<div class='panel-heading'>
			<slot name='heading'>
				<span v-text='header'/>
			</slot>
		</div>
		<slot/>
	</div>
</template>

<script>
	export default
	{
		props:
		{
			header:
			{
				type: String,
				default: () => '<header>'
			}
		}
	}
</script>
