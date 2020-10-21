using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Maily.API.Schema.MailGroupMails.Types;
using Maily.Data.Models;

namespace Maily.API.Schema.Mails.Types
{
    /// <summary>
    /// Type describing available fields of <see cref="Mail"/>.
    /// </summary>
    public class MailType : ObjectType<Mail>
    {
        protected override void Configure(IObjectTypeDescriptor<Mail> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.UserId);
            descriptor.Field(x => x.Value);

            descriptor.Field(x => x.MailGroupMails)
                .UsePaging<MailGroupMailType>()
                .UseSelection();
        }
    }
}
