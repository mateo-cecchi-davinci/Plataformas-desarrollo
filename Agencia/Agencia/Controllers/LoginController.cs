using Agencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agencia.Controllers
{
    public class LoginController : Controller
    {
        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            string usuarioLogeado = HttpContext.Session.GetString("UsuarioLogeado");
            string esAdminString = HttpContext.Session.GetString("esAdmin");
            bool isAdmin = false;

            if (!string.IsNullOrEmpty(esAdminString))
            {

                bool.TryParse(esAdminString, out isAdmin);
            }

            ViewBag.UsuarioLogueado = usuarioLogeado;
            return View("Index");
        }

        public IActionResult Registrar()
        {
            return View();
        }

        public ActionResult Login(string email, string password)
        {
            var user = _context.usuarios.FirstOrDefault(u => u.mail == email && u.clave == password);

            if (user != null)
            {
                if (user.isAdmin)
                {
                    HttpContext.Session.SetString("UsuarioLogeado", user.nombre);
                    HttpContext.Session.SetString("esAdmin", user.isAdmin.ToString());

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("UsuarioLogeado", user.nombre);
                    return RedirectToAction("Index", "Home");

                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email y contraseña incorrectas!");
                return View("Index");
            }
        }

        public IActionResult RegUser(string nombre, string apellido, int dni, string mail, string clave)
        {
            var nuevoUsuario = new Usuario
            {
                nombre = nombre,
                apellido = apellido,
                dni = dni,
                mail = mail,
                clave = clave,
                intentosFallidos = 0,
                bloqueado = false,
                credito = 0.0,
                isAdmin = false,
            };

            _context.usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
