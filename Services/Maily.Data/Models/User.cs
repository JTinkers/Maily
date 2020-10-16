using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maily.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [MinLength(1)]
        [MaxLength(64)]
        public string Nickname { get; set; }

        [MinLength(8)]
        [MaxLength(128)]
        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public virtual ICollection<Mail> Mails { get; set; }

        public virtual ICollection<MailGroup> MailGroups { get; set; }
    }
}
