using HotChocolate.Types;
using Maily.API.Middleware.Authorization;
using Maily.API.Schema.Mails.Objects;

namespace Maily.API.Schema.Mails.Types
{
    /// <summary>
    /// Type describing available methods of the <see cref="MailMutation"/>.
    /// </summary>
    public class MailMutationType : ObjectTypeExtension<MailMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<MailMutation> descriptor)
        {
            descriptor.Name("Mutation");

            descriptor.Field(x => x.CreateMail(default)).UseAuthorization().Name("createMail");
            descriptor.Field(x => x.UpdateMail(default)).UseAuthorization().Name("updateMail");
            descriptor.Field(x => x.DeleteMail(default)).UseAuthorization().Name("deleteMail");
        }
    }
}
