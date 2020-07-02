using System;
using System.Collections.Generic;
using System.Text;

namespace QuizManager.Data
{
    public class Quiz
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<Question> Questions { get; set; }
    }
}
