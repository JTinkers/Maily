using HotChocolate.Types;
using Maily.API.Schema.MailGroups.Objects;

namespace Maily.API.Schema.MailGroups.Types
{
    public class MailGroupUpdateInputType : InputObjectType<MailGroupUpdateInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MailGroupUpdateInput> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.Name);
        }
    }
}
