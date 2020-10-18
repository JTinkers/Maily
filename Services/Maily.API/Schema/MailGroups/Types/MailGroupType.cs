using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Maily.API.Schema.MailGroupMails.Types;
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

            descriptor.Field(x => x.MailGroupMails)
                .UsePaging<MailGroupMailType>()
                .UseSelection();
        }
    }
}
