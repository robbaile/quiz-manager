using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using QuizManager.Interfaces;
using QuizManager.ModelBuilders;
using QuizManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizManager.Tests.ModelBuilderTests
{
    [TestFixture]
    public class AllQuizzesModelBuilderTests
    {
        private AllQuizzesModelBuilder _modelBuilder;
        private IQuizService _quizService;

        [SetUp]
        public void Setup()
        {
            _quizService = A.Fake<IQuizService>();
            _modelBuilder = new AllQuizzesModelBuilder(_quizService);
        }

        [Test]
        public void BuildReturnsAllQuizzesModelBuilder()
        {
            // Arrange
            var expected = new AllQuizzesModel
            {
                Quizzes = new List<QuizOverview>
                {
                    new QuizOverview
                    {
                        Id = 1,
                        Title = "Geography"
                    }
                }
            };
            A.CallTo(() => _quizService.GetAllQuizzes())
                .Returns(new List<Data.Quiz>
                {
                    new Data.Quiz
                    {
                        Id = 1,
                        Title = "Geography"
                    }
                });

            // Act
            var actual = _modelBuilder.Build();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
