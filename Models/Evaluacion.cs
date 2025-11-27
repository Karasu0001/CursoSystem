using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoSystem.Models
{
    public class Evaluacion
    {
        [Key]
        public int IdEval { get; set; }

        // ✅ CORREGIDO: Especificar la clave foránea
        [ForeignKey("Leccion")]
        public int IdLeccion { get; set; }

        public string Tipo { get; set; } = null!;
        public string? Texto { get; set; }

        public Leccion? Leccion { get; set; }
        public ICollection<Pregunta>? Preguntas { get; set; }
    }
}