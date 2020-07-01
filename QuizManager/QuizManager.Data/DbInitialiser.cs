using QuizManager.Business;
using System.Collections.Generic;
using System.Linq;

namespace QuizManager.Data
{
    public class DbInitialiser
    {
        public static void Initialise(QuizManagerContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            // Creating users for the database 
            var users = new User[]
            {
                new User { Username = "robbaile", Password = SecurePasswordHasher.Hash("rob"), IsEditor = true, IsRestricted = true, IsViewer = true },
                new User { Username = "alan", Password = SecurePasswordHasher.Hash("alan"), IsEditor = true, IsRestricted = true, IsViewer = true },
                new User { Username = "steve", Password = SecurePasswordHasher.Hash("steve"), IsEditor = true, IsRestricted = true, IsViewer = true },
                new User { Username = "test", Password = SecurePasswordHasher.Hash("password"), IsEditor = false, IsRestricted = true, IsViewer = false }
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();

            var wrongAnswers = new WrongAnswers 
            {
                Answers = new List<Answer>
                {
                    new Answer { AnswerText = "Belfast" },
                    new Answer { AnswerText = "Dublin" },
                    new Answer { AnswerText = "Edinburgh" }
                }
            };

            //Creating Quiz Questions
            var questions = new List<Question>
            {
                new Question
                {
                    QuestionString = "What is the capital of England",
                    CorrectAnswer = new Answer { AnswerText = "London" },
                    WrongAnswers = wrongAnswers
                },
                new Question
                {
                    QuestionString = "What is the capital of Estonia",
                    CorrectAnswer = new Answer { AnswerText = "Tallinn" },
                    WrongAnswers = wrongAnswers
                },
                new Question
                {
                    QuestionString = "What is the capital of Brazil",
                    CorrectAnswer = new Answer { AnswerText = "Sao Paulo" },
                    WrongAnswers = wrongAnswers
                }
            };

            var quizzes = new Quiz[]
            {
                new Quiz
                {
                    Title = "Geography",
                    Questions = questions
                }
            };

            foreach (var quiz in quizzes)
            {
                context.Quizzes.Add(quiz);
            }

            context.SaveChanges();
        }
    }
}
