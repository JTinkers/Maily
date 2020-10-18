Array.prototype.remove = function(element)
{
	const index = this.indexOf(element)

	this.splice(index, 1)
}

Array.prototype.removeAt = function(index)
{
	this.splice(index, 1)
}
