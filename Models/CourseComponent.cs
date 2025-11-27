using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoSystem.Models
{
    public class CourseComponent
    {
        [Key]
        public int ComponentId { get; set; }

        public int CourseId { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public CourseComponent? Parent { get; set; }

        public List<CourseComponent>? Children { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = null!;

        public string? Content { get; set; }

        public int? Position { get; set; }

        public int? DurationMinutes { get; set; }
    }
}
