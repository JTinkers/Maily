const query = `mutation($input: MailGroupUpdateInput) {
  updateMailGroup(input: $input) {
    id
    name
  }
}`

export default query
