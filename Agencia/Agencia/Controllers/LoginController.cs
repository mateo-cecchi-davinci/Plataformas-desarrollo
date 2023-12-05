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

        public IActionResult Registrar()
        {
            ViewData["ErrorMessage"] = TempData["ErrorMessage"];
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
                    HttpContext.Session.SetString("userMail", user.mail);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("userMail", user.mail);
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

        public IActionResult RegUser(string nombre, string apellido, int dni, string mail, string clave, string clave2)
        {
            if (clave!=clave2) 
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
