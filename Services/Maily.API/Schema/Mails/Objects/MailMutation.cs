using System.Linq;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;

namespace Maily.API.Schema.Mails.Objects
{
    public class MailMutation
    {
        private MailyContext _context { get; set; }

        private Tokenizer _tokenizer { get; set; }

        public MailMutation(MailyContext context, Tokenizer tokenizer)
        {
            _context = context;
            _tokenizer = tokenizer;
        }

        public Mail CreateMail(MailCreateInput input)
        {
            var user = _tokenizer.GetUser();

            var mail = new Mail()
            {
                Value = input.Value,
                UserId = user.Id
            };

            _context.Add(mail);
            _context.SaveChanges();

            return mail;
        }

        public Mail UpdateMail(MailUpdateInput input)
        {
            var mail = _context.Mails.SingleOrDefault(x => x.Id == input.Id);

            if (mail == null)
                return null;

            var user = _tokenizer.GetUser();

            if (mail.UserId != user.Id)
                return null;

            mail.Value = input.Value;

            _context.Update(mail);
            _context.SaveChanges();

            return mail;
        }

        public Mail DeleteMail(MailDeleteInput input)
        {
            var mail = _context.Mails.SingleOrDefault(x => x.Id == input.Id);

            if (mail == null)
                return null;

            var user = _tokenizer.GetUser();

            if (mail.UserId != user.Id)
                return null;

            _context.RemoveRange(_context.MailGroupMails.Where(x => x.MailId == mail.Id));
            _context.Remove(mail);
            _context.SaveChanges();

            return mail;
        }
    }
}
