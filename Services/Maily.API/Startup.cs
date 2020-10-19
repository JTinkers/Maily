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
using Maily.API.Schema.Users.Types;
using Maily.API.Services;
using Maily.Data.Contexts;
using Maily.API.Schema.Users;
using Maily.API.Middleware.Authorization;

namespace Maily.API
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get;  }

        public IConfiguration Configuration { get; }

        private ISchema _schema { get; set; }

        private QueryExecutionOptions _queryExecutionOptions { get; set; }

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
                ForceSerialExecution = true
            };

            _schema = SchemaBuilder.New()
                .BindClrType<int, IntType>()
                .BindClrType<string, StringType>()
                .BindClrType<bool, BooleanType>()
                .AddQueryType(x => x.Name("Query"))
                .AddMutationType(x => x.Name("Mutation"))
                .AddEnumType<AuthorizationErrorCodes>(x => x.BindValuesImplicitly())
                .AddEnumType<UserSignUpErrorCode>(x => x.BindValuesImplicitly())
                .AddEnumType<UserSignInErrorCode>(x => x.BindValuesImplicitly())
                .AddType(new PaginationAmountType(64))
                .AddType<MailType>()
                .AddType<MailQueryType>()
                .AddType<MailMutationType>()
                .AddType<MailFilterInputType>()
                .AddType<MailCreateInputType>()
                .AddType<MailUpdateInputType>()
                .AddType<MailDeleteInputType>()
                .AddType<UserMutationType>()
                .AddType<UserSignUpPayloadType>()
                .AddType<UserSignInPayloadType>()
                .AddType<MailGroupType>()
                .AddType<MailGroupQueryType>()
                .AddType<MailGroupMutationType>()
                .AddType<MailGroupCreateInputType>()
                .AddType<MailGroupUpdateInputType>()
                .AddType<MailGroupDeleteInputType>()
                .AddType<MailGroupMailType>()
                .AddType<MailGroupMailMutationType>()
                .ModifyOptions(x => x.DefaultBindingBehavior = BindingBehavior.Explicit)
                .Create();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<MailyContext>(options 
                => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddGraphQL(_schema, _queryExecutionOptions);
            services.AddHttpContextAccessor();
            services.AddScoped<Hasher>();
            services.AddScoped<Tokenizer>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UsePlayground("/api");
            }

            app.UseGraphQL("/api");
        }
    }
}
