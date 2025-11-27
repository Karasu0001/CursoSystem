using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CursoSystem.Models
{
    public class Pregunta
    {
        [Key]
        public int IdPregunta { get; set; }
        public int IdEval { get; set; }
        public string Texto { get; set; } = null!;

        public Evaluacion? Evaluacion { get; set; }
        public ICollection<OpcionPregunta>? Opciones { get; set; }
    }
}
