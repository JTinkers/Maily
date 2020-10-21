using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.EntityFrameworkCore;
using Maily.Data.Contexts;
using Maily.API.Services;
using Maily.Data.Models;

namespace Maily.API.Schema.MailGroupMails.Objects.Tests
{
    [TestFixture()]
    public class MailGroupMailMutationTests
    {
        private MailyContext context { get; set; }

        private Tokenizer tokenizer { get; set; }

        private Mock<IHttpContextAccessor> mockHttpContextAccessor { get; set; }

        private MailGroupMailMutation mutation { get; set; }

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
            mutation = new MailGroupMailMutation(context, tokenizer);

            var user = context.Users.Add(new User()
            {
                Token = "sampletoken"
            }).Entity;

            var mail = context.Mails.Add(new Mail()
            {
                User = user,
                Value = "sample@mail.com"
            });

            var mailGroup = context.MailGroups.Add(new MailGroup()
            {
                User = user,
                Name = "sample mail group"
            });

            context.SaveChanges();
        }

        [Test()]
        public void AddMailToMailGroupTest_InvalidMailId()
        {
            Assert.IsNull(mutation.AddMailToMailGroup(0, 1));
        }

        [Test()]
        public void AddMailToMailGroupTest_InvalidMailGroupId()
        {
            Assert.IsNull(mutation.AddMailToMailGroup(1, 0));
        }

        [Test()]
        public void AddMailToMailGroupTest_InvalidIds()
        {
            Assert.IsNull(mutation.AddMailToMailGroup(0, 0));
        }

        [Test()]
        public void AddMailToMailGroupTest_ValidIds()
        {
            Assert.IsNotNull(mutation.AddMailToMailGroup(1, 1));
            Assert.IsTrue(mutation.AddMailToMailGroup(1, 1).MailId == 1);
            Assert.IsTrue(mutation.AddMailToMailGroup(1, 1).MailGroupId == 1);
        }

        [Test()]
        public void AddMailToMailGroupTest_InvalidUserToken()
        {
            mockHttpContextAccessor.Object.HttpContext.Request.Headers["Authorization"] = "invalidtoken";

            Assert.IsNull(mutation.AddMailToMailGroup(1, 1));
        }

        [Test()]
        public void DeleteMailFromMailGroupTest_InvalidId()
        {
            Assert.IsNull(mutation.DeleteMailFromMailGroup(0));
        }        
        
        [Test()]
        public void DeleteMailFromMailGroupTest_ValidId()
        {
            mutation.AddMailToMailGroup(1, 1);

            var deleted = mutation.DeleteMailFromMailGroup(1);

            Assert.IsNotNull(deleted);
            Assert.IsTrue(deleted.Id == 1);
        }

        [Test()]
        public void DeleteMailFromMailGroupTest_InvalidUserToken()
        {
            mockHttpContextAccessor.Object.HttpContext.Request.Headers["Authorization"] = "invalidtoken";

            Assert.IsNull(mutation.DeleteMailFromMailGroup(1));
        }
    }
}