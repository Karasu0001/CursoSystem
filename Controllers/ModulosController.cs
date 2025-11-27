using CursoSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoSystem.Controllers
{
    public class ModulosController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ModulosController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Detalle(int id)
        {
            var modulo = _db.Modulos
                .Include(m => m.Lecciones)
                .FirstOrDefault(m => m.IdModulo == id);

            if (modulo == null) return NotFound();

            return View(modulo);
        }
    }
}
