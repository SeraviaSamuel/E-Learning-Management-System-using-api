using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace E_Learning_Management_System.DTO
{
    public class TheQuizzesDTO
    {
        public int Id { get; set; }
        public int? CertificateId { get; set; }
        public int? InstructorId { get; set; }
    }
}
