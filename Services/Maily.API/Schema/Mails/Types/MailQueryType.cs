using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Maily.API.Middleware.Authorization;
using Maily.API.Schema.Mails.Objects;

namespace Maily.API.Schema.Mails.Types
{
    public class MailQueryType : ObjectTypeExtension<MailQuery>
    {
        public object MailFilterInputType { get; private set; }

        protected override void Configure(IObjectTypeDescriptor<MailQuery> descriptor)
        {
            descriptor.Name("Query");

            descriptor.Field(x => x.GetAll())
                .UseAuthorization()
                .UsePaging<MailType>()
                .UseFiltering<MailFilterInputType>()
                .UseSelection()
                .Name("mails");
        }
    }
}
