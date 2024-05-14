using E_Learning_Management_System.Models;

namespace E_Learning_Management_System.Repository
{
    public interface ICourseRepository : IRepository<Course>
    {
        public List<Course> GetAllIncludeInstructor();
    }
}