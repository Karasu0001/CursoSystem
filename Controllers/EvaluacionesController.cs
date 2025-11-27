using CursoSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoSystem.Controllers
{
    public class EvaluacionesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EvaluacionesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // ===========================
        // 🔐 Validación de Sesión
        // ===========================
        private bool UsuarioNoLogueado()
        {
            return HttpContext.Session.GetString("IdUsuario") == null;
        }

        public IActionResult Resolver(int id)
        {
            if (UsuarioNoLogueado())
                return RedirectToAction("Login", "Auth");

            var eval = _db.Evaluaciones
                .Include(e => e.Preguntas)
                .ThenInclude(p => p.Opciones)
                .FirstOrDefault(e => e.IdEval == id);

            if (eval == null) return NotFound();

            return View(eval);
        }

        [HttpPost]
        public IActionResult Resolver(int id, IFormCollection form)
        {
            if (UsuarioNoLogueado())
                return RedirectToAction("Login", "Auth");

            var eval = _db.Evaluaciones
                .Include(e => e.Preguntas)
                .ThenInclude(p => p.Opciones)
                .FirstOrDefault(e => e.IdEval == id);

            if (eval == null) return NotFound();

            int correctas = 0;
            int total = eval.Preguntas.Count;

            foreach (var p in eval.Preguntas)
            {
                string key = $"pregunta_{p.IdPregunta}";
                if (form.ContainsKey(key))
                {
                    int idOpcion = int.Parse(form[key]);
                    var opcion = p.Opciones.First(o => o.IdOpcion == idOpcion);
                    if (opcion.EsCorrecta) correctas++;
                }
            }

            ViewBag.Puntaje = correctas;
            ViewBag.Total = total;
            return View("Resultado");
        }
    }
}