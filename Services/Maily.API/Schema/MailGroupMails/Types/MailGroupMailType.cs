using HotChocolate.Types;
using Maily.Data.Models;

namespace Maily.API.Schema.MailGroupMails.Types
{
    /// <summary>
    /// Type describing fields available in <see cref="MailGroupMail"/>.
    /// </summary>
    public class MailGroupMailType : ObjectType<MailGroupMail>
    {
        protected override void Configure(IObjectTypeDescriptor<MailGroupMail> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.MailId);
            descriptor.Field(x => x.MailGroupId);

            descriptor.Field(x => x.Mail).UseSelection();
            descriptor.Field(x => x.MailGroup).UseSelection();
        }
    }
}
