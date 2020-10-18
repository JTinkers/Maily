const query = `mutation($id: Int!) {
  deleteMailFromMailGroup(id: $id) {
    id
    mailId
    mailGroupId
  }
}`

export default query
