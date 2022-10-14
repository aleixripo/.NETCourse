using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }

    public class Course : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, MinLength(280)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string LongDescription { get; set; } = string.Empty;

        [Required]
        public string TargetPublic { get; set; } = string.Empty;

        [Required]
        public string Targets { get; set; } = string.Empty;

        [Required]
        public string Requeriments { get; set; } = string.Empty;

        public Level Level { get; set; } = Level.Basic;

        [Required]
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public virtual Chapter Chapter { get; set; } = new Chapter();

        [Required]
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
