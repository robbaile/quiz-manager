using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Models
{
    public class NewQuestionModel
    {
        public string QuizId { get; set; }

        public string Question { get; set; }

        public string CorrectAnswer { get; set; }

        public List<string> WrongAnswers { get; set; }
    }
}
