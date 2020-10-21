using HotChocolate.Types;
using Maily.API.Schema.Mails.Objects;

namespace Maily.API.Schema.Mails.Types
{
    /// <summary>
    /// Type describing fields available in <see cref="MailUpdateInput"/>.
    /// </summary>
    public class MailUpdateInputType : InputObjectType<MailUpdateInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MailUpdateInput> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.Value);
        }
    }
}
