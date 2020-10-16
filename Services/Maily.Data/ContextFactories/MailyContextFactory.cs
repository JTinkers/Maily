using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Maily.Data.Contexts;

namespace Maily.Data.ContextFactories
{
    public class MailyContextFactory : IDesignTimeDbContextFactory<MailyContext>
    {
        public MailyContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MailyContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DatabaseConnection"));

            return new MailyContext(optionsBuilder.Options);
        }
    }
}
