using Microsoft.EntityFrameworkCore;
using QuizManager.Data;
using QuizManager.Interfaces;
using QuizManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Services
{
    public class QuizService : IQuizService
    {
        private readonly QuizManagerContext _quizManagerContext;
        public QuizService(QuizManagerContext quizManagerContext)
        {
            _quizManagerContext = quizManagerContext;
        }

        public async Task<List<Data.Quiz>> GetAllQuizzes()
        {
            return await _quizManagerContext.Quizzes.ToListAsync();
        }

        public async Task<Data.Quiz> GetQuiz(int id)
        {
            return await _quizManagerContext.Quizzes
                .Include(a => a.Questions)
                .ThenInclude(b => b.CorrectAnswer)
                .Include(c => c.Questions)
                .ThenInclude(d => d.WrongAnswers)
                .ThenInclude(e => e.Answers)
                .FirstOrDefaultAsync(quiz => quiz.Id == id);
        }

        public async Task<int> GetTotatlQuizQuestions(int id)
        {
            var quiz = await _quizManagerContext.Quizzes
                .Include(a => a.Questions)
                .FirstOrDefaultAsync(quiz => quiz.Id == id);

            return quiz.Questions.Count();
        }

        public async Task<bool> CreateQuiz(Models.NewQuizModel newQuiz)
        {
            var quiz = new Data.Quiz
            {
                Title = newQuiz.Title,
                Questions = new List<Data.Question>()
            };

            foreach (var question in newQuiz.NewQuestions)
            {
                var questionToAdd = new Data.Question
                {
                    QuestionString = question.Question,
                    CorrectAnswer = new Answer { AnswerText = question.CorrectAnswer },
                    WrongAnswers = new WrongAnswers()
                    {
                        Answers = new List<Answer>()
                    }
                };

                foreach (var wrongAnswer in question.WrongAnswers)
                {
                    var wrongAnswerToAdd = new Answer
                    {
                        AnswerText = wrongAnswer
                    };

                    questionToAdd.WrongAnswers.Answers.Add(wrongAnswerToAdd);
                };

                quiz.Questions.Add(questionToAdd);
            };

           
            try
            {
                await _quizManagerContext.Quizzes.AddAsync(quiz);
                await _quizManagerContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Data.Question> GetQuestion(int questionId)
        {
            return await _quizManagerContext.Questions
                .Include(x => x.CorrectAnswer)
                .Include(y => y.WrongAnswers)
                .ThenInclude(z => z.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId);
        }

        public async Task<bool> UpdateQuestion(Models.Question question)
        {
            var currentQuestion = await _quizManagerContext.Questions.Include(x => x.CorrectAnswer)
                .Include(y => y.WrongAnswers)
                .ThenInclude(z => z.Answers)
                .FirstOrDefaultAsync(q => q.Id == question.Id);

            if(currentQuestion != null)
            {
                currentQuestion.QuestionString = question.QuestionText;
                currentQuestion.CorrectAnswer = new Answer { AnswerText = question.CorrectAnswer };
                var newWrongAnswers = new WrongAnswers
                {
                    Answers = new List<Answer>()
                };

                foreach (var answer in question.WrongAnswers)
                {
                    var newWrongAnswer = new Answer
                    {
                        AnswerText = answer
                    };

                    newWrongAnswers.Answers.Add(newWrongAnswer);
                }

                currentQuestion.WrongAnswers = newWrongAnswers;

                try
                {
                    await _quizManagerContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }

            return false;
        }

        public async Task<bool> CreateQuestion(NewQuestionModel question)
        {
            var questionToInsert = new Data.Question
            {
                QuestionString = question.Question,
                CorrectAnswer = new Answer
                {
                    AnswerText = question.CorrectAnswer
                },
                WrongAnswers = new WrongAnswers
                {
                    Answers = new List<Answer>()
                }
            };

            foreach (var wrongAnswer in question.WrongAnswers)
            {
                var wrongAnswerToAdd = new Answer
                {
                    AnswerText = wrongAnswer
                };

                questionToInsert.WrongAnswers.Answers.Add(wrongAnswerToAdd);
            };

            try
            {
                var quiz = await _quizManagerContext.Quizzes
                                    .Include(a => a.Questions)
                                    .ThenInclude(b => b.CorrectAnswer)
                                    .Include(c => c.Questions)
                                    .ThenInclude(d => d.WrongAnswers)
                                    .ThenInclude(e => e.Answers)
                                    .FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(question.QuizId));
                quiz.Questions.Add(questionToInsert);
                await _quizManagerContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
