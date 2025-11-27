namespace CursoSystem.Models.DTOs
{
    public class ComponentCreateDto
    {
        public int CourseId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Content { get; set; }
        public int? Position { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
