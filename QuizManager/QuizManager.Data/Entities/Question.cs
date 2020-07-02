using System.Collections.Generic;

namespace QuizManager.Data
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionString { get; set; }

        public Answer CorrectAnswer { get; set; }

        public WrongAnswers WrongAnswers { get; set; }
    }
}
