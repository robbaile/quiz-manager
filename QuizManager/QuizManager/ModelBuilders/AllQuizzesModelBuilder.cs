using QuizManager.Interfaces;
using QuizManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.ModelBuilders
{
    public class AllQuizzesModelBuilder : IAllQuizzesModelBuilder
    {
        private IQuizService _quizService;

        public AllQuizzesModelBuilder(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public AllQuizzesModel Build()
        {
            var model = new AllQuizzesModel()
            {
                Quizzes = new List<QuizOverview>()
            };
            var quizzes = _quizService.GetAllQuizzes().GetAwaiter().GetResult();

            foreach (var quiz in quizzes)
            {
                var quizOverview = new QuizOverview
                {
                    Id = quiz.Id,
                    Title = quiz.Title
                };

                model.Quizzes.Add(quizOverview);
            }

            return model;
        }
    }
}
