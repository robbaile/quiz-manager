using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var model = _quizModelBuilder.Build(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Results([FromBody]AnswersModel answers)
        {
            var model = _resultModelBuilder.Build(answers);
            return Json(model);
        }
    }
}