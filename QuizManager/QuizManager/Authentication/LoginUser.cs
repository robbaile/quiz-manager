using QuizManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Authentication
{
    public class LoginUser : ILoginUser
    {
        private IUserService _userService;

        public LoginUser(IUserService userService)
        {
            _userService = userService;
        }

        public bool AuthenticateUser(string username, string password)
        {
            var user = _userService.Authenticate(username, password).GetAwaiter().GetResult();

            if(user.Username == null)
            {
                return false;
            }

            // setup session

            return true;
        }
    }
}
