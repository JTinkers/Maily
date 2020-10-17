using HotChocolate.Types;
using Maily.API.Middleware.Authorization;
using Maily.API.Schema.Root.Objects;

namespace Maily.API.Schema.Root.Types
{
    public class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field(x => x.CreateMailGroup(default)).UseAuthorization();
            descriptor.Field(x => x.UpdateMailGroup(default)).UseAuthorization();
            descriptor.Field(x => x.DeleteMailGroup(default)).UseAuthorization();
        }
    }
}
