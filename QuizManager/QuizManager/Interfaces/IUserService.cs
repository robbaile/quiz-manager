using QuizManager.Data;
using System.Threading.Tasks;

namespace QuizManager.Interfaces
{
    public interface IUserService
    {
        public Task<User> Authenticate(string username, string password);
    }
}
