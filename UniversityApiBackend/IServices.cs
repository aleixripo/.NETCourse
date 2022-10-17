using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend
{
    public interface IServices
    {
        User SearchUserEmail(string mail);
        IEnumerable<Student> OfAge();
        IEnumerable<Student> HaveCourses();
        IEnumerable<Course> OneStudentSinged(string level);
        IEnumerable<Course> Courses(string level, string category);
        
    }
}