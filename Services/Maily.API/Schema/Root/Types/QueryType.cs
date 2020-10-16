using HotChocolate.Types;
using Maily.API.Middleware.Authorization;
using Maily.API.Schema.Root.Objects;

namespace Maily.API.Schema.Root.Types
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(x => x.Mails()).UseAuthorization();
            descriptor.Field(x => x.MailGroups()).UseAuthorization();
            descriptor.Field(x => x.MailGroupMails()).UseAuthorization();
            descriptor.Field(x => x.Users()).UseAuthorization();
        }
    }
}
