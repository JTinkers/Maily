using HotChocolate.Types;
using Maily.API.Schema.Users.Objects;

namespace Maily.API.Schema.Users.Types
{
    public class UserSignUpPayloadType : ObjectType<UserSignUpPayload>
    {
        protected override void Configure(IObjectTypeDescriptor<UserSignUpPayload> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.Nickname);
            descriptor.Field(x => x.Token);
        }
    }
}
