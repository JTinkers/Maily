const query = `query {
  mails {
    nodes {
      id
      value
    }
  }

  mailGroups {
    nodes {
      id
      name
      mailGroupMails {
        nodes {
          id
          mailId
          mailGroupId
        }
      }
    }
  }
}`

export default query
