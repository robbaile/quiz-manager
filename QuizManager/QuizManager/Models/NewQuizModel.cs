using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Models
{
    public class NewQuizModel
    {
        public string Title { get; set; }

        public List<NewQuestion> NewQuestions { get; set; }
    }
}
