namespace Maily.Data.Models
{
    public class MailGroupMail
    {
        public int Id { get; set; }

        public int MailId { get; set; }

        public int MailGroupId { get; set; }

        public virtual Mail Mail { get; set; }

        public virtual MailGroup MailGroup { get; set; }
    }
}
