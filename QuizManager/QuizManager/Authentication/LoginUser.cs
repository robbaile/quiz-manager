using Microsoft.AspNetCore.Http;
using QuizManager.Interfaces;
using QuizManager.Models;
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

        public bool Login(LoginModel loginModel, ISession session)
        {
            var user = _userService.Authenticate(loginModel.Username, loginModel.Password).GetAwaiter().GetResult();

            if(user.Username == null)
            {
                return false;
            }

            session.SetString("Username", user.Username);
            session.SetString("UserId", user.Id.ToString());

            return true;
        }
    }
}
