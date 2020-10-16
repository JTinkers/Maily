using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maily.Data.Models
{
    public class MailGroup
    {
        public int Id { get; set; }

        [MinLength(1)]
        [MaxLength(512)]
        public string Name { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<MailGroupMail> MailGroupMails { get; set; }
    }
}
