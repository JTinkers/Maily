using HotChocolate.Types;
using Maily.Data.Models;

namespace Maily.API.Schema.Users.Types
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.Nickname);

            descriptor.Field(x => x.Mails);
            descriptor.Field(x => x.MailGroups);
        }
    }
}
