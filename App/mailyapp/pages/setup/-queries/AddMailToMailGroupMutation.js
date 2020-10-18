const query = `mutation($mailId: Int!, $mailGroupId: Int!) {
  addMailToMailGroup(mailId: $mailId, mailGroupId: $mailGroupId) {
    id
    mailId
    mailGroupId
  }
}`

export default query
