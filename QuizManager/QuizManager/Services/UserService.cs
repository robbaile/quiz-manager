using Microsoft.EntityFrameworkCore;
using QuizManager.Data;
using QuizManager.Data.Passwords;
using QuizManager.Interfaces;
using System.Threading.Tasks;

namespace QuizManager.Services
{
    public class UserService : IUserService
    {
        private readonly QuizManagerContext _quizManagerContext;
        public UserService(QuizManagerContext quizManagerContext)
        {
            _quizManagerContext = quizManagerContext;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _quizManagerContext.Users.FirstOrDefaultAsync(user => user.Username == username);

            if(SecurePasswordHasher.Verify(password, user.Password))
            {
                return new User
                {
                    Id = user.Id,
                    Username = username,
                    IsEditor = user.IsEditor,
                    IsViewer = user.IsViewer,
                    IsRestricted = user.IsRestricted
                };
            }

            return new User();
            
        }
    }
}
