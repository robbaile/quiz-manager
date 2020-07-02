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
    }
}
