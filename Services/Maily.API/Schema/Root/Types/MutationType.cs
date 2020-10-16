using HotChocolate.Types;
using Maily.API.Schema.Root.Objects;

namespace Maily.API.Schema.Root.Types
{
    public class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            
        }
    }
}
