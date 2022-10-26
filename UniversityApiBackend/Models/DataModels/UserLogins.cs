using Microsoft.Build.Framework;

namespace UniversityApiBackend.Models.DataModels
{
    public class UserLogins
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
