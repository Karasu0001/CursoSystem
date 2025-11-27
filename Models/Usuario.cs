using System;
using System.ComponentModel.DataAnnotations;

namespace CursoSystem.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Apellido { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
    }
}
