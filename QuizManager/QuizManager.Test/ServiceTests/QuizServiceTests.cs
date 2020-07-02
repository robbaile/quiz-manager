using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using QuizManager.Data;
using QuizManager.Interfaces;
using QuizManager.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizManager.Tests.ServiceTests
{
    [TestFixture]
    public class QuizServiceTests
    {
        private IQuizService _quizService;
        private DbContextOptions<QuizManagerContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<QuizManagerContext>()
            .UseInMemoryDatabase(databaseName: "QuizManager")
            .Options;

            using (var context = new QuizManagerContext(_options))
            {
                context.Quizzes.Add(new Quiz { Title = "Geography" });
                context.SaveChanges();
            };
        }

        [Test]
        public void QuizServiceGetAllQuizzesReturnsAllQuizzes()
        {
            // Arrange
            var expected = new List<Quiz>
            {
                new Quiz { Id = 1, Title = "Geography" }
            }; 
             

            using (var context = new QuizManagerContext(_options))
            {
                _quizService = new QuizService(context);

                // Act
                var actual = _quizService.GetAllQuizzes().GetAwaiter().GetResult();
                // Assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}
