using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Course : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, MinLength(280)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string LongDescription { get; set; } = string.Empty;

        [Required]
        public string TargetPublic { get; set; } = string.Empty;

        [Required]
        public string Targets { get; set; } = string.Empty;

        [Required]
        public string Requeriments { get; set; } = string.Empty;

        public enum Level
        {
            Basic,
            Medium,
            Advanced,
            Expert
        }

        public Level level { get; set; } = Level.Basic;
    }
}
