using NUnit.Framework;
using Maily.API.Schema.MailGroups.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.AspNetCore.Http;
using Maily.API.Services;
using Maily.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Maily.Data.Models;
using System.Linq;

namespace Maily.API.Schema.MailGroups.Objects.Tests
{
    [TestFixture()]
    public class MailGroupQueryTests
    {
        private MailyContext context { get; set; }

        private Tokenizer tokenizer { get; set; }

        private Mock<IHttpContextAccessor> mockHttpContextAccessor { get; set; }

        private MailGroupQuery query { get; set; }

        [SetUp]
        public void Setup()
        {
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor
                .Setup(x => x.HttpContext)
                .Returns(new DefaultHttpContext());

            var options = new DbContextOptionsBuilder<MailyContext>()
                .UseInMemoryDatabase("Maily")
                .Options;

            context = new MailyContext(options);
            context.Database.EnsureDeleted();

            tokenizer = new Tokenizer(context, mockHttpContextAccessor.Object, new Hasher());
            mockHttpContextAccessor.Object.HttpContext.Request.Headers["Authorization"] = "sampletoken";
            query = new MailGroupQuery(context, tokenizer);

            var user = context.Users.Add(new User()
            {
                Token = "sampletoken"
            }).Entity;         
            
            context.MailGroups.Add(new MailGroup()
            {
                User = user,
                Name = "sample group 1"
            });            
            
            context.MailGroups.Add(new MailGroup()
            {
                User = user,
                Name = "sample group 2"
            });

            context.SaveChanges();
        }

        [Test()]
        public void GetAllTest_NotEmpty()
        {
            Assert.IsTrue(query.GetAll().Count() > 0);
        }

        [Test()]
        public void GetAllTest_WhenEmpty()
        {
            context.MailGroups.RemoveRange(context.MailGroups.ToList());
            context.SaveChanges();

            Assert.IsTrue(query.GetAll().Count() == 0);
        }
    }
}