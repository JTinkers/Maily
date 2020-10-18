using HotChocolate.Types.Filters;
using Maily.Data.Models;

namespace Maily.API.Schema.Mails.Types
{
    public class MailGroupMailFilterInputType : FilterInputType<MailGroupMail>
    {
        protected override void Configure(IFilterInputTypeDescriptor<MailGroupMail> descriptor)
        {
            descriptor.Filter(x => x.MailId).BindFiltersImplicitly();
            descriptor.Filter(x => x.MailGroupId).BindFiltersImplicitly();
        }
    }
}