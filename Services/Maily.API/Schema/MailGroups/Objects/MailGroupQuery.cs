using System.Linq;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;

namespace Maily.API.Schema.MailGroups.Objects
{
    /// <summary>
    /// Class containing API functionality associated with <see cref="MailGroup"/>.
    /// </summary>
    public class MailGroupQuery
    {
        private MailyContext _context { get; set; }

        private Tokenizer _tokenizer { get; set; }

        public MailGroupQuery(MailyContext context, Tokenizer tokenizer)
        {
            _context = context;
            _tokenizer = tokenizer;
        }

        /// <summary>
        /// Fetch query resulting in a collection of mail groups.
        /// </summary>
        /// <returns>Query leading to mail groups.</returns>
        public IQueryable<MailGroup> GetAll()
        {
            var user = _tokenizer.GetUser();

            return _context
                .MailGroups
                .Where(x => x.UserId == user.Id);
        }
    }
}
