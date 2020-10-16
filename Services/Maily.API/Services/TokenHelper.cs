using System.Linq;
using Microsoft.AspNetCore.Http;
using Maily.Data.Contexts;
using Maily.Data.Models;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Maily.API.Services
{
    public class TokenHelper
    {
        private MailyContext _context { get; }

        private IHttpContextAccessor _httpContextAccessor { get; }

        public TokenHelper(MailyContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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

            if (user == null)
                return null;

            return user;
        }
    }
}
