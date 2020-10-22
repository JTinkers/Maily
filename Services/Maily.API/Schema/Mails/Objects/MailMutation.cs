using System.Linq;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Maily.API.Schema.Mails.Objects
{
    /// <summary>
    /// Class containing API functionality associated with <see cref="Mail"/> 
    /// </summary>
    public class MailMutation
    {
        private MailyContext _context { get; set; }

        private Tokenizer _tokenizer { get; set; }

        public MailMutation(MailyContext context, Tokenizer tokenizer)
        {
            _context = context;
            _tokenizer = tokenizer;
        }

        /// <summary>
        /// Create and store an instance of <see cref="Mail"/>.
        /// </summary>
        /// <param name="input">Proxy containing required fields.</param>
        /// <returns>An instance of created <see cref="Mail"/>.</returns>
        public Mail CreateMail(MailCreateInput input)
        {
            if (input == null)
                return null;

            var user = _tokenizer.GetUser();

            var mail = new Mail()
            {
                Value = input.Value,
                UserId = user.Id
            };

            if (!isRequestValid(mail))
                return null;

            _context.Add(mail);
            _context.SaveChanges();

            return mail;
        }

        /// <summary>
        /// Update an instance of <see cref="Mail"/>.
        /// </summary>
        /// <param name="input">Proxy containing required fields.</param>
        /// <returns>An instance of updated <see cref="Mail"/>.</returns>
        public Mail UpdateMail(MailUpdateInput input)
        {
            if (input == null)
                return null;

            var mail = _context.Mails.SingleOrDefault(x => x.Id == input.Id);

            if (!isRequestValid(mail))
                return null;

            mail.Value = input.Value;

            _context.Update(mail);
            _context.SaveChanges();

            return mail;
        }

        /// <summary>
        /// Delete an instance of <see cref="Mail"/>.
        /// </summary>
        /// <param name="input">Proxy containing required fields.</param>
        /// <returns>An instance of deleted <see cref="Mail"/>.</returns>
        public Mail DeleteMail(MailDeleteInput input)
        {
            if (input == null)
                return null;

            var mail = _context.Mails.Include(x => x.MailGroupMails)
                .SingleOrDefault(x => x.Id == input.Id);

            if (!isRequestValid(mail))
                return null;

            _context.RemoveRange(mail.MailGroupMails);
            _context.Remove(mail);
            _context.SaveChanges();

            return mail;
        }

        private bool isRequestValid(Mail mail)
        {
            var user = _tokenizer.GetUser();

            if (mail == null || user == null)
                return false;

            if (mail.UserId != user.Id)
                return false;

            return true;
        }
    }
}
