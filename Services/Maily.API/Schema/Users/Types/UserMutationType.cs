using HotChocolate.Types;
using Maily.API.Schema.Users.Objects;

namespace Maily.API.Schema.Mails.Types
{
    public class UserMutationType : ObjectTypeExtension<UserMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<UserMutation> descriptor)
        {
            descriptor.Name("Mutation");

            descriptor.Field(x => x.SignUp(default, default, default, default)).Name("userSignUp");
            descriptor.Field(x => x.SignIn(default, default, default)).Name("userSignIn");
        }
    }
}
