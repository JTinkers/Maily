const query = `mutation($input: MailCreateInput) {
  createMail(input: $input) {
    id
	value
  }
}`

export default query
