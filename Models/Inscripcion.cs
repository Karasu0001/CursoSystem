using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoSystem.Models
{
    public class Inscripcion
    {
        [Key]
        public int IdInscripcion { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("Curso")]
        public int IdCurso { get; set; }

        public decimal Progreso { get; set; }
        public string Estado { get; set; } = "EnCurso";
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public Usuario? Usuario { get; set; }
        public Curso? Curso { get; set; }
    }
}