using QuizManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Interfaces
{
    public interface IQuizService
    {
        public Task<List<Quiz>> GetAllQuizzes(); 
    }
}
