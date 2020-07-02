using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizManager.Authentication;
using QuizManager.Models;

namespace QuizManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ILoginUser _loginUser;

        public HomeController(ILogger<HomeController> logger, ILoginUser loginUser)
        {
            _logger = logger;
            _loginUser = loginUser;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("Username") == null)
            {
                return Redirect("/Home/Login");
            }



            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("Username") != null)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var isLoginSuccess = _loginUser.Login(loginModel, HttpContext.Session);

            if (isLoginSuccess)
            {
                return Redirect("/Home/Index");
            }

            return View("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
