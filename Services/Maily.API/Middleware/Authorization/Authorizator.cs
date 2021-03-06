﻿using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Resolvers;
using Maily.API.Services;

namespace Maily.API.Middleware.Authorization
{
    /// <summary>
    /// Class responsible for token authorization over HTTP protocol.
    /// </summary>
    public class Authorizator
    {
        private FieldDelegate _next { get; set; }

        private Tokenizer _tokenizer { get; set; }

        public Authorizator(FieldDelegate next, Tokenizer tokenizer)
        {
            _next = next;
            _tokenizer = tokenizer;
        }

        /// <summary>
        /// Verify whether token stored inside HTTP request header is valid and leads to an authorized user.
        /// </summary>
        /// <param name="context">Middleware context used in resolving.</param>
        /// <returns>Task result of the request.</returns>
        public async Task InvokeAsync(IMiddlewareContext context)
        {
            var token = _tokenizer.GetToken();
            var user = _tokenizer.GetUser();

            if (token == null)
            {
                context.ReportError(ErrorBuilder.New()
                    .SetCode(AuthorizationErrorCodes.NoToken.ToString())
                    .SetMessage("No authorization token provided!")
                    .Build());

                return;
            }

            if (user == null)
            {
                context.ReportError(ErrorBuilder.New()
                    .SetCode(AuthorizationErrorCodes.NoAssociatedUser.ToString())
                    .SetMessage("Provided authorization token isn't associated with a valid user!")
                    .Build());

                return;
            }

            await _next(context);
        }
    }
}
