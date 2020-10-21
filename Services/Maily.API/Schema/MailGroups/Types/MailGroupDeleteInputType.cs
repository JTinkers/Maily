using HotChocolate.Types;
using Maily.API.Schema.MailGroups.Objects;

namespace Maily.API.Schema.MailGroups.Types
{
    /// <summary>
    /// Type describing fields available in <see cref="MailGroupDeleteInput"/>.
    /// </summary>
    public class MailGroupDeleteInputType : InputObjectType<MailGroupDeleteInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MailGroupDeleteInput> descriptor)
        {
            descriptor.Field(x => x.Id);
        }
    }
}
