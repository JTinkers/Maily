using HotChocolate.Types;
using Maily.API.Schema.MailGroups.Objects;

namespace Maily.API.Schema.MailGroups.Types
{
    public class MailGroupCreateInputType : InputObjectType<MailGroupCreateInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MailGroupCreateInput> descriptor)
        {
            descriptor.Field(x => x.Name);
        }
    }
}
