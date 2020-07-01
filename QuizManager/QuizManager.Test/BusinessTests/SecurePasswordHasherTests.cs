using FluentAssertions;
using NUnit.Framework;
using QuizManager.Business;

namespace QuizManager.Tests.BusinessTests
{

    [TestFixture]
    public class SecurePasswordHasherTests
    {
        [Test]
        public void SecurePasswordHasherHashShouldReturnHashedPassword()
        {
            // arrange
            var expected = "$MYHASH$V1$10000$+soXGnoX+HMdZu4wlyoArKITiXrIz1rPJ1Mdnc8b9N1HwGru";

            // act
            var actual = SecurePasswordHasher.Hash("rob");

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void SecurePasswordHasherVerifyWithCorrectPasswordReturnsTrue()
        {
            // act
            var actual = SecurePasswordHasher.Verify("rob", "$MYHASH$V1$10000$+soXGnoX+HMdZu4wlyoArKITiXrIz1rPJ1Mdnc8b9N1HwGru");

            // assert
            Assert.IsTrue(actual);
        }
    }
}
