using E_Learning_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_Management_System.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(Context context) : base(context)
        {

        }
        public List<Course> GetAllIncludeInstructor()
        {
            return context.Course
                .Include(course => course.Instructor)
                .Where(item => item.IsDeleted == false).ToList();
        }

    }
}
