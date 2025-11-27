using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoSystem.Models
{
    public class Leccion
    {
        [Key]
        public int IdLeccion { get; set; }

        // ✅ CORREGIDO: Especificar la clave foránea
        [ForeignKey("Modulo")]
        public int IdModulo { get; set; }

        public string? Contenido { get; set; }
        public int? Orden { get; set; }

        public Modulo? Modulo { get; set; }
        public ICollection<Evaluacion>? Evaluaciones { get; set; }
    }
}