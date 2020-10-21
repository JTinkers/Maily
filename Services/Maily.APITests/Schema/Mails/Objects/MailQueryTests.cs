using NUnit.Framework;
using Maily.API.Schema.Mails.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using Maily.Data.Models;
using Maily.API.Services;
using Maily.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Maily.API.Schema.Mails.Objects.Tests
{
    [TestFixture()]
    public class MailQueryTests
    {
        private MailyContext context { get; set; }

        private Tokenizer tokenizer { get; set; }

        private Mock<IHttpContextAccessor> mockHttpContextAccessor { get; set; }

        private MailQuery query { get; set; }

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
            query = new MailQuery(context, tokenizer);

            var user = context.Users.Add(new User()
            {
                Token = "sampletoken"
            }).Entity;

            context.Mails.Add(new Mail()
            {
                User = user,
                Value = "sample1@mail.com"
            });

            context.Mails.Add(new Mail()
            {
                User = user,
                Value = "sample2@mail.com"
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
            context.Mails.RemoveRange(context.Mails.ToList());
            context.SaveChanges();

            Assert.IsTrue(query.GetAll().Count() == 0);
        }
    }
}