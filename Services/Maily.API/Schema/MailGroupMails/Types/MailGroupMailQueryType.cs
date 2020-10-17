using HotChocolate.Types;
using Maily.API.Middleware.Authorization;
using Maily.API.Schema.MailGroupMails.Objects;

namespace Maily.API.Schema.MailGroupMails.Types
{
    public class MailGroupMailQueryType : ObjectTypeExtension<MailGroupMailQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<MailGroupMailQuery> descriptor)
        {
            descriptor.Name("Query");

            descriptor.Field(x => x.GetAll())
                .UseAuthorization()
                .Name("mailGroupMails");
        }
    }
}
