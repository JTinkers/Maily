using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Maily.API.Schema.MailGroupMails.Objects
{
    /// <summary>
    /// Class containing API functionality associated with <see cref="Mail"/> 
    /// to <see cref="MailGroup"/> attachments.
    /// </summary>
    public class MailGroupMailMutation
    {
        readonly MailyContext _context;

        readonly Tokenizer _tokenizer;

        public MailGroupMailMutation(MailyContext context, Tokenizer tokenizer)
        {
            _context = context;
            _tokenizer = tokenizer;
        }

        /// <summary>
        /// Attach mail to a mail group.
        /// </summary>
        /// <param name="mailId">Id of mail to attach.</param>
        /// <param name="mailGroupId">Id of group to attach to.</param>
        /// <returns>Associate entry of mail attached to mail group.</returns>
        public MailGroupMail AddMailToMailGroup(int mailId, int mailGroupId)
        {
            var mail = _context.Mails.SingleOrDefault(x => x.Id == mailId);         
            var mailGroup = _context.MailGroups.SingleOrDefault(x => x.Id == mailGroupId);

            if (!isRequestValid(mail, mailGroup))
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

        /// <summary>
        /// Remove attachment of mail to a mail group.
        /// </summary>
        /// <param name="id">Id of associate entry describing attachment.</param>
        /// <returns>Removed attachment.</returns>
        public MailGroupMail DeleteMailFromMailGroup(int id)
        {
            var mailGroupMail = _context.MailGroupMails
                .Include(x => x.Mail)
                .Include(x => x.MailGroup)
                .SingleOrDefault(x => x.Id == id);

            if (mailGroupMail == null)
                return null;

            if (!isRequestValid(mailGroupMail.Mail, mailGroupMail.MailGroup))
                return null;

            _context.Remove(mailGroupMail);
            _context.SaveChanges();

            return mailGroupMail;
        }

        private bool isRequestValid(Mail mail, MailGroup mailGroup)
        {
            var user = _tokenizer.GetUser();

            if (mail == null || mailGroup == null || user == null)
                return false;

            if (mail.UserId != user.Id || mailGroup.UserId != user.Id)
                return false;

            return true;
        }
    }
}
