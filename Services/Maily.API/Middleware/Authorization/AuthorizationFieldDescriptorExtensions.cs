using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Types;
using Maily.API.Services;

namespace Maily.API.Middleware.Authorization
{
    public static class AuthorizationFieldDescriptorExtensions
    {
        public static IObjectFieldDescriptor UseAuthorization(this IObjectFieldDescriptor descriptor)
        {
            return descriptor.Use((services, next)
                => new Authorizator(next, services.GetRequiredService<TokenHelper>()));
        }
    }
}
