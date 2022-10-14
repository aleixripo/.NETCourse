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
    }
}
