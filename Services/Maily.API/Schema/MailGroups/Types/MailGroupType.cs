using HotChocolate.Types;
using Maily.Data.Models;

namespace Maily.API.Schema.MailGroups.Types
{
    public class MailGroupType : ObjectType<MailGroup>
    {
        protected override void Configure(IObjectTypeDescriptor<MailGroup> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.UserId);
            descriptor.Field(x => x.Name);

            descriptor.Field(x => x.User);
            descriptor.Field(x => x.MailGroupMails);
        }
    }
}
