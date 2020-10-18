const query = `query($filter: MailGroupFilter) {
  mailGroups(where: $filter) {
    nodes {
      id
      name
      mailGroupMails {
        count: totalCount
      }
    }
  }
}`

export default query
