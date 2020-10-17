using Maily.API.Schema.MailGroups.Objects;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;
using System.Linq;

namespace Maily.API.Schema.Root.Objects
{
    public class Mutation
    {
        private MailyContext _context { get; set; }

        private TokenHelper _tokenHelper { get; set; }

        public Mutation(MailyContext context, TokenHelper tokenHelper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
        }

        public MailGroup CreateMailGroup(MailGroupCreateInput input)
        {
            var mailGroup = new MailGroup()
            {
                Name = input.Name
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

            var user = _tokenHelper.GetUser();

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

            var user = _tokenHelper.GetUser();

            if (mailGroup.UserId != user.Id)
                return null;

            _context.Remove(mailGroup);
            _context.SaveChanges();

            return mailGroup;
        }
    }
}
