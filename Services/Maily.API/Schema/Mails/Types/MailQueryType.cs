using HotChocolate.Types;
using Maily.API.Middleware.Authorization;
using Maily.API.Schema.Mails.Objects;

namespace Maily.API.Schema.Mails.Types
{
    public class MailQueryType : ObjectTypeExtension<MailQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<MailQuery> descriptor)
        {
            descriptor.Name("Query");

            descriptor.Field(x => x.GetAll())
                .UseAuthorization()
                .Name("mails");
        }
    }
}
