using System.Linq;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;

namespace Maily.API.Schema.MailGroups.Objects
{
    public class MailGroupMutation
    {
        private MailyContext _context { get; set; }

        private Tokenizer _tokenizer { get; set; }

        public MailGroupMutation(MailyContext context, Tokenizer tokenizer)
        {
            _context = context;
            _tokenizer = tokenizer;
        }

        public MailGroup CreateMailGroup(MailGroupCreateInput input)
        {
            var user = _tokenizer.GetUser();

            var mailGroup = new MailGroup()
            {
                Name = input.Name,
                UserId = user.Id
            };

            _context.Add(mailGroup);
            _context.SaveChanges();

            return mailGroup;
        }

        public MailGroup UpdateMailGroup(MailGroupUpdateInput input)
        {
            var mailGroup = _context.MailGroups.SingleOrDefault(x => x.Id == input.Id);

            if (mailGroup == null)
                return null;

            var user = _tokenizer.GetUser();

            if (mailGroup.UserId != user.Id)
                return null;

            mailGroup.Name = input.Name;

            _context.Update(mailGroup);
            _context.SaveChanges();

            return mailGroup;
        }

        public MailGroup DeleteMailGroup(MailGroupDeleteInput input)
        {
            var mailGroup = _context.MailGroups.SingleOrDefault(x => x.Id == input.Id);

            if (mailGroup == null)
                return null;

            var user = _tokenizer.GetUser();

            if (mailGroup.UserId != user.Id)
                return null;

            _context.Remove(mailGroup);
            _context.SaveChanges();

            return mailGroup;
        }
    }
}
