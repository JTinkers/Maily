using HotChocolate.Types;
using Maily.API.Middleware.Authorization;
using Maily.API.Schema.MailGroups.Objects;

namespace Maily.API.Schema.MailGroups.Types
{
    public class MailGroupQueryType : ObjectTypeExtension<MailGroupQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<MailGroupQuery> descriptor)
        {
            descriptor.Name("Query");

            descriptor.Field(x => x.GetAll())
                .UseAuthorization()
                .Name("mailGroups");
        }
    }
}
