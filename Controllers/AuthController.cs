using CursoSystem.Data;
using CursoSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AuthController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Agregar debug para ver qué está recibiendo
            Console.WriteLine($"Intento de login - Email: {email}, Password: {password}");

            var user = await _db.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                Console.WriteLine("Usuario no encontrado en la base de datos");
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }

            Console.WriteLine($"Usuario encontrado: {user.Nombre} {user.Apellido}");

            //  CORRECTO: Guardar "IdUsuario"
            HttpContext.Session.SetString("IdUsuario", user.IdUsuario.ToString());
            HttpContext.Session.SetString("Nombre", user.Nombre);

            // Verificar que la sesión se guardó
            var sessionId = HttpContext.Session.GetString("IdUsuario");
            Console.WriteLine($"Sesión guardada - IdUsuario: {sessionId}");

            return RedirectToAction("Index", "Cursos");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string Nombre, string Apellido, string Email, string Password)
        {
            // Validar si el usuario ya existe
            var usuarioExistente = await _db.Usuarios
                .FirstOrDefaultAsync(u => u.Email == Email);

            if (usuarioExistente != null)
            {
                ViewBag.Error = "El email ya está registrado.";
                return View();
            }

            // Crear nuevo usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = Nombre,
                Apellido = Apellido,
                Email = Email,
                Password = Password,
                FechaRegistro = DateTime.Now
            };

            _db.Usuarios.Add(nuevoUsuario);
            await _db.SaveChangesAsync();

            // Auto-login después del registro
            HttpContext.Session.SetString("IdUsuario", nuevoUsuario.IdUsuario.ToString());
            HttpContext.Session.SetString("Nombre", nuevoUsuario.Nombre);

            return RedirectToAction("Index", "Cursos");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // 🔍 Método para debuggear la sesión
        public IActionResult DebugSession()
        {
            var idUsuario = HttpContext.Session.GetString("IdUsuario");
            var nombre = HttpContext.Session.GetString("Nombre");

            var result = $"SESIÓN ACTUAL - IdUsuario: '{idUsuario}', Nombre: '{nombre}'";
            Console.WriteLine(result);

            return Content(result);
        }
    }
}