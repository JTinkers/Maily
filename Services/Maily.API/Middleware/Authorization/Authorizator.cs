using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Resolvers;
using Maily.API.Services;

namespace Maily.API.Middleware.Authorization
{
    public class Authorizator
    {
        private FieldDelegate _next { get; set; }

        private TokenHelper _tokenHelper { get; set; }

        public Authorizator(FieldDelegate next, TokenHelper tokenHelper)
        {
            _next = next;
            _tokenHelper = tokenHelper;
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            var token = _tokenHelper.GetToken();
            var user = _tokenHelper.GetUser();

            if (token == null)
            {
                context.ReportError(ErrorBuilder.New()
                    .SetCode(AuthorizationResult.NoToken.ToString())
                    .SetMessage("No authorization token provided!")
                    .Build());

                return;
            }

            if (user == null)
            {
                context.ReportError(ErrorBuilder.New()
                    .SetCode(AuthorizationResult.NoAssociatedUser.ToString())
                    .SetMessage("Provided authorization token isn't associated with a valid user!")
                    .Build());

                return;
            }

            await _next(context);
        }
    }
}
