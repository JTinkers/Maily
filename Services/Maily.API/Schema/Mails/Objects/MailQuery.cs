using System.Linq;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;

namespace Maily.API.Schema.Mails.Objects
{
    /// <summary>
    /// Class containing API functionality associated with <see cref="Mail"/>.
    /// </summary>
    public class MailQuery
    {
        private MailyContext _context { get; set; }

        private Tokenizer _tokenizer { get; set; }

        public MailQuery(MailyContext context, Tokenizer tokenizer)
        {
            _context = context;
            _tokenizer = tokenizer;
        }

        /// <summary>
        /// Fetch query resulting in a collection of mails.
        /// </summary>
        /// <returns>Query leading to mails.</returns>
        public IQueryable<Mail> GetAll()
        {
            var user = _tokenizer.GetUser();

            return _context.Mails
                .Where(x => x.UserId == user.Id);
        }
    }
}
