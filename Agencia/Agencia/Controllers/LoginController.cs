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

            return View("Index");
        }

        public ActionResult Login(string email, string password)
        {
            var user = _context.usuarios.FirstOrDefault(u => u.mail.Equals(email));

            if (user != null)
            {
                if (user.bloqueado)
                {
                    ModelState.AddModelError(string.Empty, "El usuario está bloqueado. Contáctese con un administrador.");

                    return View("Index");
                }

                if (user.clave.Equals(password))
                {
                    if (user.isAdmin)
                    {
                        HttpContext.Session.SetString("esAdmin", user.isAdmin.ToString());
                    }

                    HttpContext.Session.SetString("userMail", user.mail);
                    HttpContext.Session.SetString("UsuarioLogeado", user.nombre);

                    user.intentosFallidos = 0;

                    _context.usuarios.Update(user);
                    _context.SaveChanges();

                    return RedirectToAction("Home", "Hotel");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error, mail o contraseña incorrectos");

                    user.intentosFallidos++;

                    _context.usuarios.Update(user);
                    _context.SaveChanges();

                    if (user.intentosFallidos >= 3)
                    {
                        user.bloqueado = true;

                        _context.usuarios.Update(user);
                        _context.SaveChanges();

                        ModelState.AddModelError(string.Empty, "El usuario ha sido bloqueado debido a múltiples intentos fallidos.");
                    }

                    return View("Index");
                }
            }
            
            ModelState.AddModelError(string.Empty, "Mail no registrado");
            return View("Index");
        }

        public IActionResult Registrar()
        {
            ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            return View();
        }

        public IActionResult RegUser(string nombre, string apellido, int dni, string mail, string clave, string repetirClave)
        {
            if (clave != repetirClave)
            {
                TempData["ErrorMessage"] = "Las contraseñas no coinciden.";
                return RedirectToAction("Registrar");
            }

            var MailRegistrado = _context.usuarios.FirstOrDefault(u => u.mail == mail);

            if (MailRegistrado != null)
            {

                TempData["ErrorMessage"] = "Email ya registrado";
                return RedirectToAction("Registrar");
            }

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
