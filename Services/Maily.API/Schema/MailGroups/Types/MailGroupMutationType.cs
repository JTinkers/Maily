using HotChocolate.Types;
using Maily.API.Middleware.Authorization;
using Maily.API.Schema.MailGroups.Objects;

namespace Maily.API.Schema.MailGroups.Types
{
    /// <summary>
    /// Type describing available functionality of <see cref="MailGroupMutation"/>.
    /// </summary>
    public class MailGroupMutationType : ObjectTypeExtension<MailGroupMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<MailGroupMutation> descriptor)
        {
            descriptor.Name("Mutation");

            descriptor.Field(x => x.CreateMailGroup(default)).UseAuthorization().Name("createMailGroup");
            descriptor.Field(x => x.UpdateMailGroup(default)).UseAuthorization().Name("updateMailGroup");
            descriptor.Field(x => x.DeleteMailGroup(default)).UseAuthorization().Name("deleteMailGroup");
        }
    }
}
