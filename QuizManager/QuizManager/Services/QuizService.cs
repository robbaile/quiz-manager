using Microsoft.EntityFrameworkCore;
using QuizManager.Data;
using QuizManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Services
{
    public class QuizService : IQuizService
    {
        private readonly QuizManagerContext _quizManagerContext;
        public QuizService(QuizManagerContext quizManagerContext)
        {
            _quizManagerContext = quizManagerContext;
        }

        public async Task<List<Quiz>> GetAllQuizzes()
        {
            return await _quizManagerContext.Quizzes.ToListAsync();
        }

        public async Task<Quiz> GetQuiz(int id)
        {
            return await _quizManagerContext.Quizzes
                .Include(a => a.Questions)
                .ThenInclude(b => b.CorrectAnswer)
                .Include(c => c.Questions)
                .ThenInclude(d => d.WrongAnswers)
                .ThenInclude(e => e.Answers)
                .FirstOrDefaultAsync(quiz => quiz.Id == id);
        }

        public async Task<int> GetTotatlQuizQuestions(int id)
        {
            var quiz = await _quizManagerContext.Quizzes
                .Include(a => a.Questions)
                .FirstOrDefaultAsync(quiz => quiz.Id == id);

            return quiz.Questions.Count();
        }
    }
}
