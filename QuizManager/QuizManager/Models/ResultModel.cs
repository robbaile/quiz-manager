using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Models
{
    public class ResultModel
    {
        public int QuizId { get; set; }

        public int Score { get; set; }
        
        public int Total { get; set; }
    }
}
