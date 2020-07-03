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
    }
}
