const query = `mutation($input: MailUpdateInput) {
  updateMail(input: $input) {
    id
    value
  }
}`

export default query
