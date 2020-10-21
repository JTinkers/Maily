using NUnit.Framework;
using Maily.API.Schema.Mails.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using Maily.Data.Contexts;
using Maily.API.Services;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Maily.Data.Models;

namespace Maily.API.Schema.Mails.Objects.Tests
{
    [TestFixture()]
    public class MailMutationTests
    {
        private MailyContext context { get; set; }

        private Tokenizer tokenizer { get; set; }

        private Mock<IHttpContextAccessor> mockHttpContextAccessor { get; set; }

        private MailMutation mutation { get; set; }

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
            mutation = new MailMutation(context, tokenizer);

            var user = context.Users.Add(new User()
            {
                Token = "sampletoken"
            }).Entity;

            var mail = context.Mails.Add(new Mail()
            {
                User = user,
                Value = "sample@mail.com"
            });

            context.SaveChanges();
        }

        [Test()]
        public void CreateMailTest_NullInput()
        {
            Assert.IsNull(mutation.CreateMail(null));
        }

        [Test()]
        public void CreateMailTest_EmptyFieldsInput()
        {
            var input = new MailCreateInput();

            Assert.IsNull(mutation.CreateMail(null));
        }

        [Test()]
        public void CreateMailTest_ValidInput()
        {
            var input = new MailCreateInput()
            {
                Value = "sample@mail.com"
            };

            Assert.IsNotNull(mutation.CreateMail(input));
        }

        [Test()]
        public void UpdateMailTest_NullInput()
        {
            Assert.IsNull(mutation.UpdateMail(null));
        }

        [Test()]
        public void UpdateMailTest_EmptyFieldsInput()
        {
            var input = new MailUpdateInput();

            Assert.IsNull(mutation.UpdateMail(input));
        }

        [Test()]
        public void UpdateMailTest_ValidInput()
        {
            var input = new MailUpdateInput()
            {
                Id = 1,
                Value = "updated@mail.com"
            };

            Assert.IsNotNull(mutation.UpdateMail(input));
        }

        [Test()]
        public void DeleteMailTest_NullInput()
        {
            Assert.IsNull(mutation.DeleteMail(null));
        }

        [Test()]
        public void DeleteMailTest_EmptyFieldsInput()
        {
            var input = new MailDeleteInput();

            Assert.IsNull(mutation.DeleteMail(input));
        }

        [Test()]
        public void DeleteMailTest_ValidInput()
        {
            var input = new MailDeleteInput()
            {
                Id = 1
            };

            Assert.IsNotNull(mutation.DeleteMail(input));
        }
    }
}