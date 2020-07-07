using QuizManager.Interfaces;
using QuizManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.ModelBuilders
{
    public class QuizModelBuilder : IQuizModelBuilder
    {
        private IQuizService _quizService;

        public QuizModelBuilder(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public Quiz Build(int id)
        {
            var quizEntity = _quizService.GetQuiz(id).GetAwaiter().GetResult();
            var quiz = new Quiz
            {
                Id = quizEntity.Id,
                Title = quizEntity.Title,
                Questions = new List<Question>()
            };

            foreach (var question in quizEntity.Questions)
            {
                var newQuestion = new Question
                {
                    Id = question.Id,
                    QuestionText = question.QuestionString,
                    CorrectAnswer = question.CorrectAnswer.AnswerText,
                    WrongAnswers = question.WrongAnswers.Answers.Select(x => x.AnswerText).ToList()
                };
                quiz.Questions.Add(newQuestion);
            }

            return quiz;
        }

        public EditQuestionModel BuildEditQuestion(int quizId, int questionId)
        {
            var questionToEdit = _quizService.GetQuestion(questionId).GetAwaiter().GetResult();
            var editQuestionModel = new EditQuestionModel
            {
                QuizId = quizId,
                QuestionId = questionId,
                Question = new Question
                {
                    QuestionText = questionToEdit.QuestionString,
                    CorrectAnswer = questionToEdit.CorrectAnswer.AnswerText,
                    WrongAnswers = new List<string>()
                }
            };

            foreach (var wrongAnswer in questionToEdit.WrongAnswers.Answers)
            {
                editQuestionModel.Question.WrongAnswers.Add(wrongAnswer.AnswerText);
            }

            return editQuestionModel;
        }

        public string Create(NewQuizModel newQuiz)
        {
            var isCreatedQuiz = _quizService.CreateQuiz(newQuiz).GetAwaiter().GetResult();

            if(isCreatedQuiz)
            {
                return "Quiz Successfully Created";
            }

            return "Could not create quiz";
        }

        public bool CreateQuestion(NewQuestionModel newQuestion)
        {
            return _quizService.CreateQuestion(newQuestion).GetAwaiter().GetResult();
        }

        public bool UpdateQuestion(EditQuestionModel editQuestion)
        {
            return _quizService.UpdateQuestion(editQuestion.Question).GetAwaiter().GetResult();
        }
    }
}
