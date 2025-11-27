using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoSystem.Models
{
    public class Curso
    {
        [Key]
        public int IdCurso { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Nivel { get; set; }
        public string? Duracion { get; set; }

        // ✅ CORREGIDO: Especificar la clave foránea
        public ICollection<Modulo>? Modulos { get; set; }
    }
}