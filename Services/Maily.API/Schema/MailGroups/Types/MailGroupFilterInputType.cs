using HotChocolate.Types.Filters;
using Maily.Data.Models;

namespace Maily.API.Schema.MailGroups.Types
{
    /// <summary>
    /// Type describing filterable fields of <see cref="MailGroup"/>.
    /// </summary>
    public class MailGroupFilterInputType : FilterInputType<MailGroup>
    {
        protected override void Configure(IFilterInputTypeDescriptor<MailGroup> descriptor)
        {
            descriptor.Filter(x => x.Name).BindFiltersImplicitly();
        }
    }
}