const query = `mutation($input: MailGroupDeleteInput) {
  deleteMailGroup(input: $input) {
    id
  }
}`

export default query
