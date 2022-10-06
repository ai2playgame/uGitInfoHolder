using NUnit.Framework;

namespace UGitVersionHolder.Runtime.Tests
{
    public class TestGitVersionClass
    {
        [Test]
        public void GetHashTest()
        {
            Assert.That(!string.IsNullOrEmpty(GitVersion.GetHash()));
        }
        [Test]
        public void GetDateTest()
        {
            Assert.That(!string.IsNullOrEmpty(GitVersion.GetDate()));
        }
    }
}