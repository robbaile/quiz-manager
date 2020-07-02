using Microsoft.AspNetCore.Http;
using QuizManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Authentication
{
    public interface ILoginUser
    {
        public bool Login(LoginModel loginModel, ISession session);
    }
}
