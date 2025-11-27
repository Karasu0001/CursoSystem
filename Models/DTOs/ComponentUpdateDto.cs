namespace CursoSystem.Models.DTOs
{
    public class ComponentUpdateDto
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Content { get; set; }
        public int? Position { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
