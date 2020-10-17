using System.Linq;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;

namespace Maily.API.Schema.MailGroupMails.Objects
{
    public class MailGroupMailQuery
    {
        private MailyContext _context { get; set; }

        private Tokenizer _tokenizer { get; set; }

        public MailGroupMailQuery(MailyContext context, Tokenizer tokenizer)
        {
            _context = context;
            _tokenizer = tokenizer;
        }

        public IQueryable<MailGroupMail> GetAll()
        {
            var user = _tokenizer.GetUser();

            return _context.MailGroupMails
                .Where(x => x.MailGroup.UserId == user.Id);
        }
    }
}
