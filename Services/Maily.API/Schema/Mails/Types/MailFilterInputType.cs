using HotChocolate.Types.Filters;
using Maily.Data.Models;

namespace Maily.API.Schema.Mails.Types
{
    /// <summary>
    /// Type describing filterable fields of <see cref="Mail"/>.
    /// </summary>
    public class MailFilterInputType : FilterInputType<Mail>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Mail> descriptor)
        {
            descriptor.Filter(x => x.Value).BindFiltersImplicitly();
        }
    }
}