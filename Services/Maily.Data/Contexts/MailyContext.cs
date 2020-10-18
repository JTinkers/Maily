using Microsoft.EntityFrameworkCore;
using Maily.Data.Models;

namespace Maily.Data.Contexts
{
    public class MailyContext : DbContext
    {
        public DbSet<Mail> Mails { get; set; }

        public DbSet<MailGroup> MailGroups { get; set; }

        public DbSet<MailGroupMail> MailGroupMails { get; set; }

        public DbSet<User> Users { get; set; }

        public MailyContext(DbContextOptions<MailyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MailGroupMail>()
                .HasOne(x => x.Mail)
                .WithMany(x => x.MailGroupMails)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MailGroupMail>()
                .HasOne(x => x.MailGroup)
                .WithMany(x => x.MailGroupMails)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Token)
                .IsUnique();

            modelBuilder.Entity<Mail>()
                .HasIndex(x => new { x.UserId, x.Value })
                .IsUnique();            
            
            modelBuilder.Entity<MailGroup>()
                .HasIndex(x => new { x.UserId, x.Name })
                .IsUnique();

            modelBuilder.Entity<MailGroupMail>()
                .HasIndex(x => new { x.MailId, x.MailGroupId})
                .IsUnique();
        }
    }
}
