using NUnit.Framework;
using Maily.API.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maily.API.Services.Tests
{
    [TestFixture()]
    public class HasherTests
    {
        private Hasher hasher { get; set; }

        private string hash { get; set; }

        [SetUp]
        public void Setup()
        {
            hasher = new Hasher();
            hash = hasher.CreateHash("sampleinput");
        }

        [Test()]
        public void CreateHashTest_PassNull()
        {
            Assert.IsNull(hasher.CreateHash(null));
        }

        [Test()]
        public void CreateHashTest_PassEmptyString()
        {
            Assert.IsNull(hasher.CreateHash(string.Empty));
        }

        [Test()]
        public void CreateHashTest_PassString()
        {
            Assert.IsNotNull(hasher.CreateHash("sample"));
        }

        [Test()]
        public void IsMatchingTest_NullInput()
        {
            Assert.IsFalse(hasher.IsMatching(null, hash));
        }

        [Test()]
        public void IsMatchingTest_NullHash()
        {
            Assert.IsFalse(hasher.IsMatching("sampleinput", null));
        }

        [Test()]
        public void IsMatchingTest_ProperInput()
        {
            Assert.IsTrue(hasher.IsMatching("sampleinput", hash));
        }

        [Test()]
        public void IsMatchingTest_HashAsInput()
        {
            Assert.IsFalse(hasher.IsMatching(hash, hash));
        }

        [Test()]
        public void IsMatchingTest_WrongInput()
        {
            Assert.IsFalse(hasher.IsMatching("sample", hash));
        }

        [Test()]
        public void IsMatchingTest_EmptyStringAsInput()
        {
            Assert.IsFalse(hasher.IsMatching(string.Empty, hash));
        }

        [Test()]
        public void IsMatchingTest_NullInputs()
        {
            Assert.IsFalse(hasher.IsMatching(null, null));
        }
    }
}