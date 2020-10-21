using HotChocolate.Types;
using Maily.API.Schema.Users.Objects;

namespace Maily.API.Schema.Users.Types
{
    /// <summary>
    /// Type describing fields available in <see cref="UserSignInPayload"/>.
    /// </summary>
    public class UserSignInPayloadType : ObjectType<UserSignInPayload>
    {
        protected override void Configure(IObjectTypeDescriptor<UserSignInPayload> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.Nickname);
            descriptor.Field(x => x.Token);
        }
    }
}
