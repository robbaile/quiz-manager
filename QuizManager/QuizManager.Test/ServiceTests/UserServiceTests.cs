using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using QuizManager.Data;
using QuizManager.Data.Passwords;
using QuizManager.Interfaces;
using QuizManager.Web.Services;

namespace QuizManager.Tests.ServiceTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _userService;
        private DbContextOptions<QuizManagerContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<QuizManagerContext>()
            .UseInMemoryDatabase(databaseName: "QuizManager")
            .Options;

            using (var context = new QuizManagerContext(_options))
            {
                context.Users.Add(new User { Username = "robbaile", Password = SecurePasswordHasher.Hash("rob"), IsEditor = true, IsRestricted = true, IsViewer = true });
                context.Users.Add(new User { Username = "alan", Password = SecurePasswordHasher.Hash("alan"), IsEditor = true, IsRestricted = true, IsViewer = true });
                context.Users.Add(new User { Username = "steve", Password = SecurePasswordHasher.Hash("steve"), IsEditor = true, IsRestricted = true, IsViewer = true });
                context.Users.Add(new User { Username = "test", Password = SecurePasswordHasher.Hash("password"), IsEditor = false, IsRestricted = true, IsViewer = false });
                context.SaveChanges();
            };
        }

        [Test]
        public void UserServiceAuthenticateReturnsUserWhenPasswordsMatch()
        {
            // Arrange
            var username = "robbaile";
            var password = "rob";
            var expected = new User
            {
                Id = 1,
                Username = "robbaile",
                IsRestricted = true,
                IsEditor = true,
                IsViewer = true
            };

            using (var context = new QuizManagerContext(_options))
            {
                _userService = new UserService(context);

                // Act
                var actual =  _userService.Authenticate(username, password).GetAwaiter().GetResult();

                // Assert
                actual.Should().BeEquivalentTo(expected);
            }
        }

        [Test]
        public void UserServiceAuthenticateReturnsEmptyUserWhenPasswordsDoNotMatch()
        {
            // Arrange
            var username = "robbaile";
            var password = "notrob";
            var expected = new User();

            using (var context = new QuizManagerContext(_options))
            {
                _userService = new UserService(context);

                // Act
                var actual = _userService.Authenticate(username, password).GetAwaiter().GetResult();

                // Assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}
