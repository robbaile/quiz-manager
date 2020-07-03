using Microsoft.EntityFrameworkCore;
using QuizManager.Data;
using QuizManager.Interfaces;
using QuizManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Services
{
    public class ScoreService : IScoreService
    {
        private readonly QuizManagerContext _quizManagerContext;
        public ScoreService(QuizManagerContext quizManagerContext)
        {
            _quizManagerContext = quizManagerContext;
        }

        public async Task<int> GetScore(List<AnswerCheck> answers)
        {
            var score = 0;

            foreach (var answer in answers)
            {
                var question = await _quizManagerContext.Questions
                    .Include(a => a.CorrectAnswer)
                    .FirstOrDefaultAsync(a => a.Id == Convert.ToInt32(answer.QuestionId));

                if(question.CorrectAnswer.AnswerText == answer.Answer)
                {
                    score += 1;
                }
            }

            return score;
        }
    }
}
