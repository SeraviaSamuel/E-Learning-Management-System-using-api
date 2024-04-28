using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_Management_System.Models
{
    public class ApplicationUser : IdentityUser //Account
    {
        public string Address { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string AccountType { get; set; }

        [ForeignKey("Learner")]
        public int? LearnerId { get; set; }
        public Learner? Learner { get; set; }

        [ForeignKey("Instructor")]
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        [ForeignKey("Adminstrator")]
        public int? AdminstratorId { get; set; }
        public Adminstrator? Adminstrator { get; set; }
    }
}