using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public string CorrectAnswer { get; set; }

        public List<string> WrongAnswers { get; set; }
    }
}
