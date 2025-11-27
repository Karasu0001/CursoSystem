using CursoSystem.Data;
using CursoSystem.Models;
using CursoSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoSystem.Controllers
{
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ICourseComponentService _componentService;

        public CursosController(ApplicationDbContext db, ICourseComponentService componentService)
        {
            _db = db;
            _componentService = componentService;
        }

        // ===========================
        // 🔐 Validación de Sesión
        // ===========================
        private bool UsuarioNoLogueado()
        {
            var idUsuario = HttpContext.Session.GetString("IdUsuario");
            return idUsuario == null;
        }

        private int GetUserId()
        {
            var idUsuario = HttpContext.Session.GetString("IdUsuario");
            return idUsuario != null ? int.Parse(idUsuario) : 0;
        }

        // ===========================
        // 📋 LISTA DE CURSOS
        // ===========================
        public IActionResult Index()
        {
            if (UsuarioNoLogueado())
                return RedirectToAction("Login", "Auth");

            var cursos = _db.Cursos.ToList();
            var userId = GetUserId();
            var inscripciones = _db.Inscripciones
                .Where(i => i.IdUsuario == userId)
                .ToList();

            ViewBag.Inscripciones = inscripciones;
            return View(cursos);
        }

        // ===========================
        // 📖 DETALLE DEL CURSO
        // ===========================
        public IActionResult Detalle(int id)
        {
            if (UsuarioNoLogueado())
                return RedirectToAction("Login", "Auth");

            var curso = _db.Cursos.FirstOrDefault(c => c.IdCurso == id);
            if (curso == null) return NotFound();

            // Cargar módulos y lecciones
            var modulos = _db.Modulos.Where(m => m.IdCurso == id).ToList();
            foreach (var modulo in modulos)
            {
                modulo.Lecciones = _db.Lecciones.Where(l => l.IdModulo == modulo.IdModulo).ToList();
            }
            curso.Modulos = modulos;

            // Verificar inscripción
            var userId = GetUserId();
            var estaInscrito = _db.Inscripciones
                .Any(i => i.IdUsuario == userId && i.IdCurso == id);

            ViewBag.EstaInscrito = estaInscrito;
            ViewBag.CursoId = id;

            return View(curso);
        }

        // ===========================
        // 📝 INSCRIBIRSE AL CURSO
        // ===========================
        [HttpPost]
        [ValidateAntiForgeryToken] // ✅ AGREGAR ESTE ATRIBUTO
        public IActionResult Inscribirse(int cursoId)
        {
            Console.WriteLine($"🎯 MÉTODO Inscribirse EJECUTADO - CursoID: {cursoId}");

            if (UsuarioNoLogueado())
            {
                Console.WriteLine("❌ USUARIO NO LOGUEADO");
                return RedirectToAction("Login", "Auth");
            }

            var userId = GetUserId();
            Console.WriteLine($"👤 Usuario ID: {userId}, Curso ID: {cursoId}");

            try
            {
                // Verificar si ya está inscrito
                var yaInscrito = _db.Inscripciones
                    .Any(i => i.IdUsuario == userId && i.IdCurso == cursoId);

                if (yaInscrito)
                {
                    Console.WriteLine("❌ YA INSCRITO");
                    TempData["Error"] = "Ya estás inscrito en este curso.";
                    return RedirectToAction("Detalle", new { id = cursoId });
                }

                // Crear nueva inscripción
                var inscripcion = new Inscripcion
                {
                    IdUsuario = userId,
                    IdCurso = cursoId,
                    Progreso = 0,
                    Estado = "EnCurso",
                    FechaInicio = DateTime.Now,
                    FechaFin = null
                };

                Console.WriteLine($"📝 Creando inscripción...");

                _db.Inscripciones.Add(inscripcion);
                _db.SaveChanges();

                Console.WriteLine("✅ INSCRIPCIÓN EXITOSA");
                TempData["Success"] = "¡Te has inscrito correctamente al curso!";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 ERROR: {ex.Message}");
                TempData["Error"] = "Error al inscribirse en el curso.";
            }

            return RedirectToAction("Detalle", new { id = cursoId });
        }

        // ===========================
        // 📚 MIS CURSOS
        // ===========================
        public IActionResult MisCursos()
        {
            if (UsuarioNoLogueado())
                return RedirectToAction("Login", "Auth");

            var userId = GetUserId();
            var misCursos = _db.Inscripciones
                .Where(i => i.IdUsuario == userId)
                .Include(i => i.Curso)
                .ToList();

            return View(misCursos);
        }

        // ===========================
        // 📖 LECCIÓN
        // ===========================
        public IActionResult Leccion(int id)
        {
            if (UsuarioNoLogueado())
                return RedirectToAction("Login", "Auth");

            var leccion = _db.Lecciones
                .Include(l => l.Evaluaciones)
                .ThenInclude(e => e.Preguntas)
                .ThenInclude(p => p.Opciones)
                .FirstOrDefault(l => l.IdLeccion == id);

            if (leccion == null) return NotFound();

            return View(leccion);
        }

        // ===========================
        // 🎓 MARCAR LECCIÓN COMPLETADA
        // ===========================
        [HttpPost]
        public IActionResult MarcarLeccionCompletada(int cursoId, int leccionId)
        {
            if (UsuarioNoLogueado())
                return RedirectToAction("Login", "Auth");

            TempData["Success"] = "Lección marcada como completada";
            return RedirectToAction("Detalle", new { id = cursoId });
        }
    }
}