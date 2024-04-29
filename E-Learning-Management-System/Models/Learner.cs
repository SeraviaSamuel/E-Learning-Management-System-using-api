using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_Management_System.Models
{
    public class Learner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("Account")]
        public string? AccountId { get; set; }
        public ApplicationUser? Account { get; set; }
        [ForeignKey("Content")]
        public int? ContentId { get; set; }
        public Content? Content { get; set; }

        [ForeignKey("Quiz")]
        public int? QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        public ICollection<Certificate>? Certificates { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }
        public ICollection<Course>? Courses { get; set; }

    }
}
