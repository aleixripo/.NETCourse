using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend
{
    public interface IServices
    {
        User SearchUserEmail(string mail);
        IEnumerable<Student> OfAge();
    }
}