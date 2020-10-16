using HotChocolate.Types;
using Maily.Data.Models;

namespace Maily.API.Schema.MailGroupMails.Types
{
    public class MailGroupMailType : ObjectType<MailGroupMail>
    {
        protected override void Configure(IObjectTypeDescriptor<MailGroupMail> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.MailId);
            descriptor.Field(x => x.MailGroupId);

            descriptor.Field(x => x.Mail);
            descriptor.Field(x => x.MailGroup);
        }
    }
}
