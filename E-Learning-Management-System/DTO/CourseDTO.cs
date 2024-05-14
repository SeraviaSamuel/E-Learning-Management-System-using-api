using E_Learning_Management_System.Models;

namespace E_Learning_Management_System.DTO
{
    public class CourseDTO
    {
        public String Name { get; set; }
        public string ImgPath { get; set; }
        public int DurationInHours { get; set; }

        public int instructorId { get; set; }

    }
}
