using QuizManager.Interfaces;
using QuizManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.ModelBuilders
{
    public class QuizModelBuilder : IQuizModelBuilder
    {
        private IQuizService _quizService;

        public QuizModelBuilder(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public Quiz Build(int id)
        {
            var quizEntity = _quizService.GetQuiz(id).GetAwaiter().GetResult();
            var quiz = new Quiz
            {
                Id = quizEntity.Id,
                Title = quizEntity.Title,
                Questions = new List<Question>()
            };

            foreach (var question in quizEntity.Questions)
            {
                var newQuestion = new Question
                {
                    Id = question.Id,
                    QuestionText = question.QuestionString,
                    CorrectAnswer = question.CorrectAnswer.AnswerText,
                    WrongAnswers = question.WrongAnswers.Answers.Select(x => x.AnswerText).ToList()
                };
                quiz.Questions.Add(newQuestion);
            }

            return quiz;
        }
    }
}
