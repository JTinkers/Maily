using Maily.Data.Models;

namespace Maily.API.Schema.Mails.Objects
{
    /// <summary>
    /// Proxy containing fields required in editting of <see cref="Mail"/>.
    /// </summary>
    public class MailUpdateInput
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }
}
