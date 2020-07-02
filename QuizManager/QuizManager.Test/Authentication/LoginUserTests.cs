using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using QuizManager.Authentication;
using QuizManager.Data;
using QuizManager.Interfaces;

namespace QuizManager.Tests.Authentication
{
    [TestFixture]
    public class LoginUserTests
    {
        private IUserService _userService;
        private LoginUser _loginUser;

        [SetUp]
        public void Setup()
        {
            _userService = A.Fake<IUserService>();
            _loginUser = new LoginUser(_userService);
        }

        [Test]
        public void LoginUserWithCorrectDetailsReturnsTrue()
        {
            // Arrange
            A.CallTo(() => _userService.Authenticate("rob", "rob"))
                .Returns(new User
                {
                    Id = 1,
                    Username = "rob",
                    IsEditor = true,
                    IsViewer = true,
                    IsRestricted = true
                });

            // Act 
            var actual = _loginUser.AuthenticateUser("rob", "rob");

            // assert 
            actual.Should().BeTrue();
        }
    }
}
