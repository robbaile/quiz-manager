using QuizManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Interfaces
{
    public interface IQuizModelBuilder
    {
        public Quiz Build(int id);

        public EditQuestionModel BuildEditQuestion(int quizId, int questionId);

        public string Create(NewQuizModel newQuiz);

        public bool UpdateQuestion(EditQuestionModel editQuestion);

        public bool CreateQuestion(NewQuestionModel newQuestion);

        public bool Delete(int id);

        public string DeleteQuestion(int quizId, int id);
    }
}
