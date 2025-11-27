using CursoSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Leccion> Lecciones { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<OpcionPregunta> OpcionesPregunta { get; set; }
        public DbSet<CourseComponent> CourseComponents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ CONFIGURAR RELACIONES EXPLÍCITAMENTE

            // Curso -> Modulos
            modelBuilder.Entity<Modulo>()
                .HasOne(m => m.Curso)
                .WithMany(c => c.Modulos)
                .HasForeignKey(m => m.IdCurso);

            // Modulo -> Lecciones
            modelBuilder.Entity<Leccion>()
                .HasOne(l => l.Modulo)
                .WithMany(m => m.Lecciones)
                .HasForeignKey(l => l.IdModulo);

            // Leccion -> Evaluaciones
            modelBuilder.Entity<Evaluacion>()
                .HasOne(e => e.Leccion)
                .WithMany(l => l.Evaluaciones)
                .HasForeignKey(e => e.IdLeccion);

            // Evaluacion -> Preguntas
            modelBuilder.Entity<Pregunta>()
                .HasOne(p => p.Evaluacion)
                .WithMany(e => e.Preguntas)
                .HasForeignKey(p => p.IdEval);

            // Pregunta -> Opciones
            modelBuilder.Entity<OpcionPregunta>()
                .HasOne(o => o.Pregunta)
                .WithMany(p => p.Opciones)
                .HasForeignKey(o => o.IdPregunta);
        }
    }
}