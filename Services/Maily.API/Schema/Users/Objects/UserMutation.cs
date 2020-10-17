using HotChocolate;
using HotChocolate.Resolvers;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;
using System.Linq;

namespace Maily.API.Schema.Users.Objects
{
    public class UserMutation
    {
        readonly MailyContext _context;

        readonly Tokenizer _tokenizer;

        readonly Hasher _hasher;

        public UserMutation(MailyContext context, Tokenizer tokenizer, Hasher hasher)
        {
            _context = context;
            _tokenizer = tokenizer;
            _hasher = hasher;
        }

        public UserSignUpPayload SignUp(string nickname, string username, string password, IResolverContext resolverContext)
        {
            if(_context.Users.Any(x => x.Username == username))
            {
                resolverContext.ReportError(ErrorBuilder.New()
                    .SetCode(UserSignUpResult.UsernameNotUnique.ToString())
                    .SetMessage("Username is not unique!")
                    .Build());

                return null;
            }

            var user = new User()
            {
                Nickname = nickname,
                Username = username,
                Password = _hasher.CreateHash(password)
            };

            _context.Add(user);
            _context.SaveChanges();

            user.Token = _tokenizer.CreateToken(user);

            _context.Update(user);
            _context.SaveChanges();

            return new UserSignUpPayload()
            {
                Id = user.Id,
                Nickname = user.Nickname,
                Token = user.Token
            };
        }

        public UserSignInPayload SignIn(string username, string password, IResolverContext resolverContext)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            if (user == null)
            {
                resolverContext.ReportError(ErrorBuilder.New()
                    .SetCode(UserSignInResult.UserNotFound.ToString())
                    .SetMessage("User of given username was not found!")
                    .Build());

                return null;
            }

            if (!_hasher.IsMatching(password, user.Password))
            {
                resolverContext.ReportError(ErrorBuilder.New()
                    .SetCode(UserSignInResult.PasswordMismatch.ToString())
                    .SetMessage("Wrong password!")
                    .Build());

                return null;
            }

            return new UserSignInPayload()
            {
                Id = user.Id,
                Nickname = user.Nickname,
                Token = user.Token
            };
        }
    }
}
