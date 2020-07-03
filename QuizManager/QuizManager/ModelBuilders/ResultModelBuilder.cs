using QuizManager.Interfaces;
using QuizManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.ModelBuilders
{
    public class ResultModelBuilder : IResultModelBuilder
    {
        private IQuizService _quizService;
        private IScoreService _scoreService;

        public ResultModelBuilder(IQuizService quizService, IScoreService scoreService)
        {
            _quizService = quizService;
            _scoreService = scoreService;
        }

        public ResultModel Build(AnswersModel answers)
        {
            var total = _quizService.GetTotatlQuizQuestions(answers.QuizId).GetAwaiter().GetResult();
            var score = _scoreService.GetScore(answers.AnswersToCheck).GetAwaiter().GetResult();
            
            return new ResultModel
            {
                QuizId = answers.QuizId,
                Total = total,
                Score = score
            };
        }
    }
}
