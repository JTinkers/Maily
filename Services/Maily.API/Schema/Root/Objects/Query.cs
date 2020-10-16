using System.Linq;
using Maily.Data.Contexts;
using Maily.Data.Models;

namespace Maily.API.Schema.Root.Objects
{
    public class Query
    {
        private MailyContext _context { get; set; }

        public Query(MailyContext context)
        {
            _context = context;
        }

        public IQueryable<Mail> Mails() => _context.Mails;

        public IQueryable<MailGroup> MailGroups() => _context.MailGroups;

        public IQueryable<MailGroupMail> MailGroupMails() => _context.MailGroupMails;

        public IQueryable<User> Users() => _context.Users;
    }
}
