using Maily.Data.Models;

namespace Maily.API.Schema.Mails.Objects
{
    /// <summary>
    /// Proxy containing fields required in creation of <see cref="Mail"/>.
    /// </summary>
    public class MailCreateInput
    {
        public string Value { get; set; }
    }
}
