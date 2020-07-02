using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using QuizManager.Authentication;
using QuizManager.Controllers;
using QuizManager.Interfaces;
using QuizManager.Models;

namespace QuizManager.Tests.ControllerTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController _controller;
        private ILogger<HomeController> _logger;
        private ISession _session;
        private ILoginUser _loginUser;
        private IAllQuizzesModelBuilder _allQuizzesModelBuilder;

        [SetUp]
        public void Setup()
        {
            var context = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };
            _loginUser = A.Fake<ILoginUser>();
            _logger = A.Fake<ILogger<HomeController>>();
            _allQuizzesModelBuilder = A.Fake<IAllQuizzesModelBuilder>();
            _session = A.Fake<ISession>();
            _controller = new HomeController(_logger, _loginUser, _allQuizzesModelBuilder);
            context.HttpContext.Session = _session;
            _controller.ControllerContext = context;
        }

        [Test]
        public void LoginWithCorrectModelRedirectsToIndex()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Username = "robbaile",
                Password = "rob"
            };
            A.CallTo(() => _loginUser.Login(loginModel, A<ISession>.Ignored)).Returns(true);

            // Act
            var actual = (RedirectResult)_controller.Login(loginModel);

            // Assert
            Assert.IsInstanceOf<RedirectResult>(actual);
            Assert.AreEqual("/Home/Index", actual.Url);
        }

        [Test]
        public void LoginWithInvalidModelReturnsModelToLogin()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Username = "r",
                Password = ""
            };
            A.CallTo(() => _loginUser.Login(loginModel, A<ISession>.Ignored)).Returns(false);
            
            // Act
            var actual = (ViewResult)_controller.Login(loginModel);

            // Assert
            Assert.IsInstanceOf<ActionResult>(actual);
            Assert.AreEqual("Login", actual.ViewName);
        }
    }
}
