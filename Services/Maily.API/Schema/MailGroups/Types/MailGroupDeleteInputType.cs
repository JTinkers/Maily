using HotChocolate.Types;
using Maily.API.Schema.MailGroups.Objects;

namespace Maily.API.Schema.MailGroups.Types
{
    public class MailGroupDeleteInputType : InputObjectType<MailGroupDeleteInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MailGroupDeleteInput> descriptor)
        {
            descriptor.Field(x => x.Id);
        }
    }
}
