using NUnit.Framework;
using Maily.API.Schema.Users.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using Maily.Data.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using Maily.API.Services;
using Maily.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Configuration.Bindings;
using HotChocolate.Resolvers;

namespace Maily.API.Schema.Users.Objects.Tests
{
    [TestFixture()]
    public class UserMutationTests
    {
        private MailyContext context { get; set; }

        private Tokenizer tokenizer { get; set; }

        private Mock<IHttpContextAccessor> mockHttpContextAccessor { get; set; }

        private UserMutation mutation { get; set; }

        private Mock<IResolverContext> mockResolverContext { get; set; }

        [SetUp]
        public void Setup()
        {
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor
                .Setup(x => x.HttpContext)
                .Returns(new DefaultHttpContext());

            mockResolverContext = new Mock<IResolverContext>();

            var options = new DbContextOptionsBuilder<MailyContext>()
                .UseInMemoryDatabase("Maily")
                .Options;

            context = new MailyContext(options);
            context.Database.EnsureDeleted();

            var hasher = new Hasher();
            tokenizer = new Tokenizer(context, mockHttpContextAccessor.Object, hasher);
            mockHttpContextAccessor.Object.HttpContext.Request.Headers["Authorization"] = "sampletoken";
            mutation = new UserMutation(context, tokenizer, new Hasher());

            var user = context.Users.Add(new User()
            {
                Username = "sampleusername",
                Password = hasher.CreateHash("samplepassword"),
                Token = "sampletoken"
            }).Entity;

            context.SaveChanges();
        }

        [Test()]
        public void SignUpTest_NullNickname()
        {
            var result = mutation.SignUp(null, "sampleusername", "samplepassword", mockResolverContext.Object);

            Assert.IsNull(result);
        }

        [Test()]
        public void SignUpTest_NullUsername()
        {
            var result = mutation.SignUp("samplenickname", null, "samplepassword", mockResolverContext.Object);

            Assert.IsNull(result);
        }

        [Test()]
        public void SignUpTest_NullPassword()
        {
            var result = mutation.SignUp("samplenickname", "sampleusername", null, mockResolverContext.Object);

            Assert.IsNull(result);
        }

        [Test()]
        public void SignUpTest_ValidInputs()
        {
            var result = mutation.SignUp("samplenickname2", "sampleusername2", "samplepassword2", mockResolverContext.Object);

            Assert.IsNotNull(result);
        }

        [Test()]
        public void SignInTest_NullUsername()
        {
            var result = mutation.SignIn(null, "samplepassword", mockResolverContext.Object);

            Assert.IsNull(result);
        }

        [Test()]
        public void SignInTest_NullPassword()
        {
            var result = mutation.SignIn("sampleusername", null, mockResolverContext.Object);

            Assert.IsNull(result);
        }

        [Test()]
        public void SignInTest_ValidInputs()
        {
            var result = mutation.SignIn("sampleusername", "samplepassword", mockResolverContext.Object);

            Assert.IsNotNull(result);
        }
    }
}