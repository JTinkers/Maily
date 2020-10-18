const query = `mutation($input: MailDeleteInput) {
  deleteMail(input: $input) {
    id
  }
}`

export default query
