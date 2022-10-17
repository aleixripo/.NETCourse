using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend
{
    public class Services : IServices
    {
        private readonly DbContext _context;
        public Services(DbContext dbContext)
        {
            this._context = dbContext;
        }
        public User SearchUserEmail(string mail)
        {
            return _context.Set<User>().Single(user => user.Email.Equals(mail));
        }

        public IEnumerable<Student> OfAge()
        {
            return _context.Set<Student>().Where(student => student.Age >= 18);
        }

        public IEnumerable<Student> HaveCourses()
        {
            return _context.Set<Student>().Where(student =>student.Courses.Any());
        }

        public IEnumerable<Course> OneStudentSinged(Level level)
        {
            return _context.Set<Course>().Where(course => course.Level == level && course.Students.Any());
        }

        public IEnumerable<Course> Courses(Level level, Category category)
        {
            return _context.Set<Course>().Where(course => course.Level == level && course.Categories == category);
        }
    }
}
