using HotChocolate.Types;
using Maily.API.Schema.Users.Objects;

namespace Maily.API.Schema.Users.Types
{
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
