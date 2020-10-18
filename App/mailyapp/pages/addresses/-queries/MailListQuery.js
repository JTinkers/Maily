const query = `query($filter: MailFilter) {
  mails(where: $filter) {
    nodes {
      id
      value
      mailGroupMails {
        count: totalCount
      }
    }
  }
}`

export default query
