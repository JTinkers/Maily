using System.Linq;
using Microsoft.AspNetCore.Http;
using Maily.Data.Contexts;
using Maily.Data.Models;

namespace Maily.API.Services
{
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

        public string CreateToken(User user)
        {
            var data = user.Id + ";" + user.Username;

            return _hasher.CreateHash(data);
        }

        public string GetToken()
        {   
            var token = (string)_httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            return token;
        }

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
