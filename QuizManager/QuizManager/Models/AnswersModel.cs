using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Models
{
    public class AnswersModel
    {
        public int QuizId { get; set; }

        public List<AnswerCheck> AnswersToCheck { get; set; }
    }
}
