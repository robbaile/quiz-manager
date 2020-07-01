using Microsoft.EntityFrameworkCore;

namespace QuizManager.Data
{
    public class QuizManagerContext : DbContext
    {
        public QuizManagerContext(DbContextOptions<QuizManagerContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<WrongAnswers> WrongAnswers { get; set; }
    }
}
