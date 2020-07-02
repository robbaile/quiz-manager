using QuizManager.Interfaces;
using QuizManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.ModelBuilders
{
    public class AllQuizzesModelBuilder : IAllQuizzesModelBuilder
    {
        private IQuizService _quizService;

        public AllQuizzesModelBuilder(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public AllQuizzesModel Build()
        {
            throw new NotImplementedException();
        }
    }
}
