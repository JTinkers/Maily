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

namespace Maily.API.Schema.MailGroups.Objects.Tests
{
    [TestFixture()]
    public class MailGroupMutationTests
    {
        private MailyContext context { get; set; }

        private Tokenizer tokenizer { get; set; }

        private Mock<IHttpContextAccessor> mockHttpContextAccessor { get; set; }

        private MailGroupMutation mutation { get; set; }

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
            mutation = new MailGroupMutation(context, tokenizer);

            var user = context.Users.Add(new User()
            {
                Token = "sampletoken"
            }).Entity;

            context.Mails.Add(new Mail()
            {
                User = user,
                Value = "sample@mail.com"
            });

            context.MailGroups.Add(new MailGroup()
            {
                User = user,
                Name = "sample mail group1"
            });            
            
            context.MailGroups.Add(new MailGroup()
            {
                User = user,
                Name = "sample mail group2"
            });            
            
            context.MailGroups.Add(new MailGroup()
            {
                User = user,
                Name = "sample mail group3"
            });

            context.SaveChanges();
        }

        [Test()]
        public void CreateMailGroupTest_NullInput()
        {
            Assert.IsNull(mutation.CreateMailGroup(null));
        }

        [Test()]
        public void CreateMailGroupTest_EmptyFieldsInput()
        {
            var input = new MailGroupCreateInput();

            Assert.IsNull(mutation.CreateMailGroup(null));
        }

        [Test()]
        public void CreateMailGroupTest_ValidInput()
        {
            var input = new MailGroupCreateInput()
            {
                Name = "sample group"
            };

            Assert.IsNotNull(mutation.CreateMailGroup(input));
        }

        [Test()]
        public void UpdateMailGroupTest_NullInput()
        {
            Assert.IsNull(mutation.UpdateMailGroup(null));
        }

        [Test()]
        public void UpdateMailGroupTest_EmptyFieldsInput()
        {
            var input = new MailGroupUpdateInput();

            Assert.IsNull(mutation.UpdateMailGroup(input));
        }

        [Test()]
        public void UpdateMailGroupTest_ValidInput()
        {
            var input = new MailGroupUpdateInput()
            {
                Id = 1,
                Name = "updated name"
            };

            Assert.IsNotNull(mutation.UpdateMailGroup(input));
        }

        [Test()]
        public void DeleteMailGroupTest_NullInput()
        {
            Assert.IsNull(mutation.DeleteMailGroup(null));
        }

        [Test()]
        public void DeleteMailGroupTest_EmptyFieldsInput()
        {
            var input = new MailGroupDeleteInput();

            Assert.IsNull(mutation.DeleteMailGroup(input));
        }

        [Test()]
        public void DeleteMailGroupTest_ValidInput()
        {
            var input = new MailGroupDeleteInput()
            {
                Id = 1
            };

            Assert.IsNotNull(mutation.DeleteMailGroup(input));
        }
    }
}