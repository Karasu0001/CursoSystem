using CursoSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoSystem.Controllers
{
    public class LeccionesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LeccionesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Ver(int id)
        {
            var leccion = _db.Lecciones
                .Include(l => l.Evaluaciones)
                .ThenInclude(e => e.Preguntas)
                .ThenInclude(p => p.Opciones)
                .FirstOrDefault(l => l.IdLeccion == id);

            if (leccion == null) return NotFound();

            return View(leccion);
        }
    }
}
