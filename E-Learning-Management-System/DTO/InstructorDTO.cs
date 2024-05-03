using System.ComponentModel.DataAnnotations;

namespace E_Learning_Management_System.DTO
{
    public class InstructorDTO
    {
        [Required(ErrorMessage = " Name Is Required")]
        [MinLength(3, ErrorMessage = "Name must be 3 char at least")]
        public string Name { get; set; }
    }
}
