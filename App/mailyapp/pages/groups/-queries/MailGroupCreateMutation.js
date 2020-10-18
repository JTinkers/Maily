const query = `mutation($input: MailGroupCreateInput) {
  createMailGroup(input: $input) {
    id
    name
  }
}`

export default query
