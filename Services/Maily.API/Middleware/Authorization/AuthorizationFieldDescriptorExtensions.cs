using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Types;
using Maily.API.Services;

namespace Maily.API.Middleware.Authorization
{
    /// <summary>
    /// Extensions related to <see cref="Authorizator"/> API middleware.
    /// </summary>
    public static class AuthorizationFieldDescriptorExtensions
    {
        /// <summary>
        /// Attach authorizator to the field.
        /// </summary>
        /// <returns>Field descriptor with middleware attached.</returns>
        public static IObjectFieldDescriptor UseAuthorization(this IObjectFieldDescriptor descriptor)
        {
            return descriptor.Use((services, next)
                => new Authorizator(next, services
                    .CreateScope()
                    .ServiceProvider
                    .GetRequiredService<Tokenizer>()));
        }
    }
}
