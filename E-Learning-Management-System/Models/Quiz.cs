using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_Management_System.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public double Mark { get; set; }
        public bool IsDeleted { get; set; } = false;
        [ForeignKey("Certificate")]
        public int? CertificateId { get; set; }
        public Certificate? Certificate { get; set; }
        [ForeignKey("Learner")]
        public int? LearnerId { get; set; }
        public Learner? Learner { get; set; }
        [ForeignKey("Feedback")]
        public int? FeedbackId { get; set; }
        public Feedback? Feedback { get; set; }
        [ForeignKey("TheQuizzes")]
        public int? TheQuizzesId { get; set; }
        public TheQuizzes? TheQuizzes { get; set; }


    }
}