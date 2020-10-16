using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Maily.API.Schema.MailGroupMails.Types;
using Maily.API.Schema.MailGroups.Types;
using Maily.API.Schema.Mails.Types;
using Maily.API.Schema.Root.Types;
using Maily.API.Schema.Users.Types;
using Maily.API.Services;
using Maily.Data.Contexts;
using Microsoft.AspNetCore.Http;

namespace Maily.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            _queryExecutionOptions = new QueryExecutionOptions
            {
                MaxExecutionDepth = 32,
                MaxOperationComplexity = 256,
                IncludeExceptionDetails = Environment.IsDevelopment(),
                ExecutionTimeout = new TimeSpan(0, 0, 10),
                ForceSerialExecution = false
            };

            _schema = SchemaBuilder.New()
                .AddQueryType<QueryType>()
                //.AddMutationType<MutationType>()
                .AddType<MailType>()
                .AddType<UserType>()
                .AddType<MailGroupType>()
                .AddType<MailGroupMailType>()
                .ModifyOptions(x => x.DefaultBindingBehavior = BindingBehavior.Explicit)
                .Create();
        }

        public IWebHostEnvironment Environment { get;  }

        public IConfiguration Configuration { get; }

        private ISchema _schema { get; set; }

        private QueryExecutionOptions _queryExecutionOptions { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<MailyContext>(options 
                => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddHttpContextAccessor();
            services.AddGraphQL(_schema, _queryExecutionOptions);
            services.AddScoped<TokenHelper>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UsePlayground("/api");
            }

            app.UseHttpsRedirection();
            app.UseGraphQL("/api");
        }
    }
}
