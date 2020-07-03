using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizManager.Interfaces;
using QuizManager.Models;

namespace QuizManager.Controllers
{
    public class QuizController : Controller
    {
        private IQuizModelBuilder _quizModelBuilder;
        private IResultModelBuilder _resultModelBuilder;

        public QuizController(IQuizModelBuilder quizModelBuilder, IResultModelBuilder resultModelBuilder)
        {
            _quizModelBuilder = quizModelBuilder;
            _resultModelBuilder = resultModelBuilder;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return Redirect("/Home/Login");
            }

            var model = _quizModelBuilder.Build(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Results([FromBody]AnswersModel answers)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return Redirect("/Home/Login");
            }

            var model = _resultModelBuilder.Build(answers);
            return Json(model);
        }

        [HttpGet]
        public IActionResult Viewer(int id)
        {
            if (HttpContext.Session.GetString("Username") == null || HttpContext.Session.GetString("IsViewer") == "False")
            {
                return Redirect("/Home/Login");
            }

            var model = _quizModelBuilder.Build(id);

            return View(model);
        }
    }
}