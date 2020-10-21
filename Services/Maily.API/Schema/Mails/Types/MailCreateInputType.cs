using HotChocolate.Types;
using Maily.API.Schema.Mails.Objects;

namespace Maily.API.Schema.Mails.Types
{
    /// <summary>
    /// Type describing fields available in <see cref="MailCreateInput"/>.
    /// </summary>
    public class MailCreateInputType : InputObjectType<MailCreateInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MailCreateInput> descriptor)
        {
            descriptor.Field(x => x.Value);
        }
    }
}
