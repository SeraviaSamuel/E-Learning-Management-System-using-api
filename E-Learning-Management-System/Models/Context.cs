using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_Management_System.Models
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Adminstrator> Adminstrator { get; set; }
        public DbSet<Certificate> Certificate { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Learner> Learner { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<TheQuizzes> TheQuizzes { get; set; }
        public Context() { }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

    }
}
