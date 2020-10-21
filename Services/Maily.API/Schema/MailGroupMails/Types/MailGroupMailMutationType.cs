using HotChocolate.Types;
using Maily.API.Middleware.Authorization;
using Maily.API.Schema.MailGroupMails.Objects;

namespace Maily.API.Schema.MailGroupMails.Types
{
    /// <summary>
    /// Type describing available functionality of <see cref="MailGroupMailMutation"/>.
    /// </summary>
    public class MailGroupMailMutationType : ObjectTypeExtension<MailGroupMailMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<MailGroupMailMutation> descriptor)
        {
            descriptor.Name("Mutation");

            descriptor.Field(x => x.AddMailToMailGroup(default, default))
                .UseAuthorization()
                .Name("addMailToMailGroup");

            descriptor.Field(x => x.DeleteMailFromMailGroup(default))
                .UseAuthorization()
                .Name("deleteMailFromMailGroup");
        }
    }
}
