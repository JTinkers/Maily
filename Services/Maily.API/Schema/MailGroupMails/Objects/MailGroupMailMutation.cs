using HotChocolate;
using HotChocolate.Resolvers;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Maily.API.Schema.MailGroupMails.Objects
{
    public class MailGroupMailMutation
    {
        readonly MailyContext _context;

        readonly Tokenizer _tokenizer;

        public MailGroupMailMutation(MailyContext context, Tokenizer tokenizer)
        {
            _context = context;
            _tokenizer = tokenizer;
        }

        public MailGroupMail AddMailToMailGroup(int mailId, int mailGroupId)
        {
            var user = _tokenizer.GetUser();

            var mail = _context.Mails.SingleOrDefault(x => x.Id == mailId);
            var mailGroup = _context.MailGroups.SingleOrDefault(x => x.Id == mailGroupId);

            if (mail.UserId != user.Id)
                return null;

            if (mailGroup.UserId != user.Id)
                return null;

            var mailGroupMail = new MailGroupMail()
            {
                MailId = mailId,
                MailGroupId = mailGroupId
            };

            _context.Add(mailGroupMail);
            _context.SaveChanges();

            return mailGroupMail;
        }

        public MailGroupMail DeleteMailFromMailGroup(int id)
        {
            var user = _tokenizer.GetUser();

            var mailGroupMail = _context.MailGroupMails
                .Include(x => x.Mail)
                .SingleOrDefault(x => x.Id == id);

            if (mailGroupMail == null)
                return null;

            if (mailGroupMail.Mail.UserId != user.Id)
                return null;

            _context.Remove(mailGroupMail);
            _context.SaveChanges();

            return mailGroupMail;
        }
    }
}
