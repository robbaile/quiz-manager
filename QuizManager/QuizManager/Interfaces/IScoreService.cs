using QuizManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizManager.Interfaces
{
    public interface IScoreService
    {
        public Task<int> GetScore(List<AnswerCheck> answers);
    }
}
