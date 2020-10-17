using System.Linq;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;

namespace Maily.API.Schema.MailGroups.Objects
{
    public class MailGroupQuery
    {
        private MailyContext _context { get; set; }

        private Tokenizer _tokenizer { get; set; }

        public MailGroupQuery(MailyContext context, Tokenizer tokenizer)
        {
            _context = context;
            _tokenizer = tokenizer;
        }

        public IQueryable<MailGroup> GetAll()
        {
            var user = _tokenizer.GetUser();

            return _context.MailGroups
                .Where(x => x.UserId == user.Id);
        }
    }
}
