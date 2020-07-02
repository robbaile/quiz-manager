using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using QuizManager.Authentication;
using QuizManager.Data;
using QuizManager.Interfaces;
using QuizManager.Models;

namespace QuizManager.Tests.Authentication
{
    [TestFixture]
    public class LoginUserTests
    {
        private IUserService _userService;
        private ISession _session;
        private LoginUser _loginUser;


        [SetUp]
        public void Setup()
        {
            _userService = A.Fake<IUserService>();
            _session = A.Fake<ISession>();
            _loginUser = new LoginUser(_userService);
        }

        [Test]
        public void LoginUserWithCorrectDetailsReturnsTrue()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Username = "rob",
                Password = "rob"
            };
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
            var actual = _loginUser.Login(loginModel, _session);

            // assert 
            actual.Should().BeTrue();
        }
    }
}
