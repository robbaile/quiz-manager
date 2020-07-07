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

        public Task<Quiz> GetQuiz(int id);

        public Task<Question> GetQuestion(int questionId);

        public Task<int> GetTotatlQuizQuestions(int id);

        public Task<bool> CreateQuiz(Models.NewQuizModel newQuiz);

        public Task<bool> UpdateQuestion(Models.Question question);

        public Task<bool> CreateQuestion(Models.NewQuestionModel question);
    }
}
