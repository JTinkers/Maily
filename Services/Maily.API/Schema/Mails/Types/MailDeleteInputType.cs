using HotChocolate.Types;
using Maily.API.Schema.Mails.Objects;

namespace Maily.API.Schema.Mails.Types
{
    /// <summary>
    /// Type describing fields available in <see cref="MailDeleteInput"/>.
    /// </summary>
    public class MailDeleteInputType : InputObjectType<MailDeleteInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MailDeleteInput> descriptor)
        {
            descriptor.Field(x => x.Id);
        }
    }
}
