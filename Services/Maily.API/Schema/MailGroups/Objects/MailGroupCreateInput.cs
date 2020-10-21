using Maily.Data.Models;

namespace Maily.API.Schema.MailGroups.Objects
{
    /// <summary>
    /// Proxy class used in creation of <see cref="MailGroup"/>.
    /// </summary>
    public class MailGroupCreateInput
    {
        public string Name { get; set; }
    }
}
