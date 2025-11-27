using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoSystem.Models
{
    public class Modulo
    {
        [Key]
        public int IdModulo { get; set; }

        // ✅ CORREGIDO: Especificar la clave foránea
        [ForeignKey("Curso")]
        public int IdCurso { get; set; }

        public string Titulo { get; set; } = null!;
        public int? Orden { get; set; }

        public Curso? Curso { get; set; }

        // ✅ CORREGIDO: Especificar la clave foránea
        public ICollection<Leccion>? Lecciones { get; set; }
    }
}