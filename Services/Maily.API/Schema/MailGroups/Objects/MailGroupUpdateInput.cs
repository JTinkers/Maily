using Maily.Data.Models;

namespace Maily.API.Schema.MailGroups.Objects
{
    /// <summary>
    /// Proxy class used in editting of <see cref="MailGroup"/>.
    /// </summary>
    public class MailGroupUpdateInput
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
