using HotChocolate.Types;
using Maily.API.Schema.Mails.Objects;

namespace Maily.API.Schema.Mails.Types
{
    public class MailDeleteInputType : InputObjectType<MailDeleteInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MailDeleteInput> descriptor)
        {
            descriptor.Field(x => x.Id);
        }
    }
}
