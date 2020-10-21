using Maily.Data.Models;

namespace Maily.API.Schema.MailGroups.Objects
{
    /// <summary>
    /// Proxy class used in deletion of <see cref="MailGroup"/>.
    /// </summary>
    public class MailGroupDeleteInput
    {
        public int Id { get; set; }
    }
}
