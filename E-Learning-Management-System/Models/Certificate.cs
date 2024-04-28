using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_Management_System.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; } = false;
        [ForeignKey("Quiz")]
        public int? QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        [ForeignKey("Learner")]
        public int? LearnerId { get; set; }
        public Learner? Learner { get; set; }

    }
}