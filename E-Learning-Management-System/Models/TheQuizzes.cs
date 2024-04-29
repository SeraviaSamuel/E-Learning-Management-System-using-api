using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_Management_System.Models
{
    public class TheQuizzes
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        [ForeignKey("Certificate")]
        public int? CertificateId { get; set; }
        public Certificate? Certificate { get; set; }

        [ForeignKey("Instructor")]
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }

        public ICollection<Quiz>? Quizzes { get; set; }
    }
}