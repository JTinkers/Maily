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
        }
    }
}
