using HotChocolate;
using HotChocolate.Resolvers;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.Data.Models;
using System.Linq;

namespace Maily.API.Schema.Users.Objects
{
    /// <summary>
    /// Class containing API functionality associated with <see cref="User"/> 
    /// </summary>
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

        /// <summary>
        /// Create and store an instance of <see cref="User"/>.
        /// </summary>
        /// <param name="nickname">Nickname of the user.</param>
        /// <param name="username">A unique username of the user.</param>
        /// <param name="password">An unhashed password.</param>
        /// <param name="resolverContext">Resolver context used in error reporting.</param>
        /// <returns>An instance of proxy class <see cref="UserSignUpPayload"/> containing some of user fields.</returns>
        public UserSignUpPayload SignUp(string nickname, string username, string password, IResolverContext resolverContext)
        {
            if (nickname == null || username == null || password == null)
                return null;

            if(_context.Users.Any(x => x.Username == username))
            {
                resolverContext.ReportError(ErrorBuilder.New()
                    .SetCode(UserSignUpErrorCode.UsernameNotUnique.ToString())
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

            // Id and Username to ensures unique hash
            user.Token = _tokenizer.CreateToken(user.Id + ";" + user.Username);

            _context.Update(user);
            _context.SaveChanges();

            return new UserSignUpPayload()
            {
                Id = user.Id,
                Nickname = user.Nickname,
                Token = user.Token
            };
        }

        /// <summary>
        /// Retrieve data related to user of given username and password.
        /// </summary>
        /// <param name="username">A unique username of the user.</param>
        /// <param name="password">An unhashed password.</param>
        /// <param name="resolverContext">Resolver context used in error reporting.</param>
        /// <returns>An instance of proxy class <see cref="UserSignInPayload"/> containing some of user fields.</returns>
        public UserSignInPayload SignIn(string username, string password, IResolverContext resolverContext)
        {
            if (username == null || password == null)
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            if (user == null)
            {
                resolverContext.ReportError(ErrorBuilder.New()
                    .SetCode(UserSignInErrorCode.UserNotFound.ToString())
                    .SetMessage("User of given username was not found!")
                    .Build());

                return null;
            }

            if (!_hasher.IsMatching(password, user.Password))
            {
                resolverContext.ReportError(ErrorBuilder.New()
                    .SetCode(UserSignInErrorCode.PasswordMismatch.ToString())
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
