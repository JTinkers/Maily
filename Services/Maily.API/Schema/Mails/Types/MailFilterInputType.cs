using HotChocolate.Types.Filters;
using Maily.Data.Models;

namespace Maily.API.Schema.Mails.Types
{
    public class MailFilterInputType : FilterInputType<Mail>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Mail> descriptor)
        {
            descriptor.Filter(x => x.Value).BindFiltersImplicitly();
        }
    }
}