using Maily.Data.Models;

namespace Maily.API.Schema.Mails.Objects
{
    /// <summary>
    /// Proxy containing fields required in deletion of <see cref="Mail"/>.
    /// </summary>
    public class MailDeleteInput
    {
        public int Id { get; set; }
    }
}
