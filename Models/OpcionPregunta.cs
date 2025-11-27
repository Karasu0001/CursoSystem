using System.ComponentModel.DataAnnotations;

namespace CursoSystem.Models
{
    public class OpcionPregunta
    {
        [Key]
        public int IdOpcion { get; set; }
        public int IdPregunta { get; set; }
        public string Texto { get; set; } = null!;
        public bool EsCorrecta { get; set; }

        public Pregunta? Pregunta { get; set; }
    }
}
