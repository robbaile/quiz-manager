using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Models
{
    public class EditQuestionModel
    {
        public int QuizId { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
