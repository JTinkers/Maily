using NUnit.Framework;
using Maily.API.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Maily.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using Maily.Data.Models;
using NuGet.Frameworks;
using System.Linq;

namespace Maily.API.Services.Tests
{
    [TestFixture()]
    public class TokenizerTests
    {
        private MailyContext context { get; set; }

        private Mock<IHttpContextAccessor> mockHttpContextAccessor { get; set; }

        private Tokenizer tokenizer { get; set; }

        [SetUp]
        public void Setup()
        {
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());

            var options = new DbContextOptionsBuilder<MailyContext>()
                .UseInMemoryDatabase("Maily")
                .Options;

            context = new MailyContext(options);
            context.Database.EnsureDeleted();

            tokenizer = new Tokenizer(context, mockHttpContextAccessor.Object, new Hasher());

            var user = context.Users.Add(new User()
            {
                Token = "sampletoken"
            }).Entity;

            context.SaveChanges();
        }

        [Test()]
        public void CreateTokenTest_PassNull()
        {
            Assert.IsNull(tokenizer.CreateToken(null));
        }

        [Test()]
        public void CreateTokenTest_PassString()
        {
            Assert.IsNotNull(tokenizer.CreateToken("sampledata"));
        }

        [Test()]
        public void CreateTokenTest_PassEmptyString()
        {
            Assert.IsNull(tokenizer.CreateToken(string.Empty));
        }

        [Test()]
        public void GetTokenTest_NoToken()
        {
            Assert.IsNull(tokenizer.GetToken());
        }

        [Test()]
        public void GetTokenTest_EmptyToken()
        {
            mockHttpContextAccessor.Object.HttpContext.Request.Headers ["Authorization"] = "";

            Assert.AreEqual(tokenizer.GetToken(), string.Empty);
        }

        [Test()]
        public void GetTokenTest_SampleToken()
        {
            mockHttpContextAccessor.Object.HttpContext.Request.Headers["Authorization"] = "sampletoken";

            Assert.IsNotNull(tokenizer.GetToken());
            Assert.AreEqual(tokenizer.GetToken(), "sampletoken");
        }

        [Test()]
        public void GetUserTest_NoToken()
        {
            Assert.IsNull(tokenizer.GetUser());
        }

        [Test()]
        public void GetUserTest_WrongToken()
        {
            mockHttpContextAccessor.Object.HttpContext.Request.Headers ["Authorization"] = "wrongtoken";

            Assert.IsNull(tokenizer.GetUser());
        }

        [Test()]
        public void GetUserTest_CorrectToken()
        {
            mockHttpContextAccessor.Object.HttpContext.Request.Headers["Authorization"] = "sampletoken";

            Assert.AreEqual(tokenizer.GetUser(), context.Users.First());
        }
    }
}