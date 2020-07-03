using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizManager.Authentication;
using QuizManager.Interfaces;
using QuizManager.Models;

namespace QuizManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ILoginUser _loginUser;
        private IAllQuizzesModelBuilder _allQuizzesModelBuilder;

        public HomeController(ILogger<HomeController> logger, ILoginUser loginUser, IAllQuizzesModelBuilder allQuizzesModelBuilder)
        {
            _logger = logger;
            _loginUser = loginUser;
            _allQuizzesModelBuilder = allQuizzesModelBuilder;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("Username") == null)
            {
                return Redirect("/Home/Login");
            }

            var model = _allQuizzesModelBuilder.Build();

            return View(model);
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

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return Redirect("/Home/Login");
        }

        [HttpGet]
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
