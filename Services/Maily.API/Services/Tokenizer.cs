using System.Linq;
using Microsoft.AspNetCore.Http;
using Maily.Data.Contexts;
using Maily.Data.Models;

namespace Maily.API.Services
{
    /// <summary>
    /// Helper service used in generating hash tokens and associated <see cref="User"/>.
    /// </summary>
    public class Tokenizer
    {
        readonly MailyContext _context;

        readonly IHttpContextAccessor _httpContextAccessor;

        readonly Hasher _hasher;

        public Tokenizer(MailyContext context, IHttpContextAccessor httpContextAccessor, Hasher hasher)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _hasher = hasher;
        }

        /// <summary>
        /// Create hash token from input data.
        /// </summary>
        /// <param name="data">Unique data to create hash token from.</param>
        /// <returns>Generated token.</returns>
        public string CreateToken(string data)
        {
            return _hasher.CreateHash(data);
        }
        
        /// <summary>
        /// Retrieve token form HTTP request header.
        /// </summary>
        /// <returns>Token stored in HTTP request header.</returns>
        public string GetToken()
        {   
            var token = (string)_httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            return token;
        }

        /// <summary>
        /// Get <see cref="User"/> associated with the token.
        /// </summary>
        /// <returns><see cref="User"/> associated with the token.</returns>
        public User GetUser()
        {
            var token = GetToken();

            if (string.IsNullOrEmpty(token))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Token == token);

            return user;
        }
    }
}
