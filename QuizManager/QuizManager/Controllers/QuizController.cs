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
            if (HttpContext.Session.GetString("Username") == null || !ModelState.IsValid)
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Username") == null || HttpContext.Session.GetString("IsEditor") == "False")
            {
                return Redirect("/Home/Login");
            }

            var model = _quizModelBuilder.Build(id);

            return View(model);
        }

        [HttpGet]
        public IActionResult EditQuestion(int id, int questionId)
        {
            if (HttpContext.Session.GetString("Username") == null || HttpContext.Session.GetString("IsEditor") == "False")
            {
                return Redirect("/Home/Login");
            }

            var model = _quizModelBuilder.BuildEditQuestion(id, questionId);

            return View("EditQuestion", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditQuestion([FromBody]EditQuestionModel editQuestionModel)
        {
            if (HttpContext.Session.GetString("Username") == null 
                || HttpContext.Session.GetString("IsEditor") == "False" 
                || !ModelState.IsValid)
            {
                return Json("401: Unauthorized");
            }

            var model = _quizModelBuilder.UpdateQuestion(editQuestionModel);

            return Json(model);
        }

        [HttpGet]
        public IActionResult NewQuestion(int id)
        {
            ViewBag.UserId = id;
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateQuestion([FromBody]NewQuestionModel newQuestion)
        {
            if (HttpContext.Session.GetString("Username") == null 
                || HttpContext.Session.GetString("IsEditor") == "False" 
                || !ModelState.IsValid)
            {
                return Json("401: Unauthorized");    
            }

            var model = _quizModelBuilder.CreateQuestion(newQuestion);
            return Json(model);
        }

        [HttpGet]
        public IActionResult New()
        {
            if (HttpContext.Session.GetString("Username") == null || HttpContext.Session.GetString("IsEditor") == "False")
            {
                return Redirect("/Home/Login");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromBody]NewQuizModel newQuiz)
        {
            if (HttpContext.Session.GetString("Username") == null 
                || HttpContext.Session.GetString("IsEditor") == "False" 
                || !ModelState.IsValid)
            {
                return Json("401: Unauthorized");
            }

            var quizResponse = _quizModelBuilder.Create(newQuiz);

            return Json(quizResponse);
        }
    }
}