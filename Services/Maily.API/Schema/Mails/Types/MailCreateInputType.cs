using HotChocolate.Types;
using Maily.API.Schema.Mails.Objects;

namespace Maily.API.Schema.Mails.Types
{
    public class MailCreateInputType : InputObjectType<MailCreateInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MailCreateInput> descriptor)
        {
            descriptor.Field(x => x.Value);
        }
    }
}
