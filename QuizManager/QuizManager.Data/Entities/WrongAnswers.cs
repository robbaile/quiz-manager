using System.Collections.Generic;

namespace QuizManager.Data
{
    public class WrongAnswers
    {
        public int Id { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
