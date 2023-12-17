using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agencia.Models;

namespace Agencia.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Context _context;

        public UsuarioController(Context context)
        {
            _context = context;

            _context.usuarios
                .Include(u => u.misReservasHabitaciones)
                    .ThenInclude(rh => rh.miHabitacion)
                    .ThenInclude(h => h.hotel)
                .Include(u => u.misReservasVuelos)
                    .ThenInclude(rv => rv.miVuelo)
                    .ThenInclude(v => v.origen)
                .Include(u => u.habitacionesUsadas)
                .Include(u => u.vuelosTomados)
                    .ThenInclude(v => v.origen)
                    .ThenInclude(c => c.vuelos_destino)
                    .ThenInclude(v => v.misReservas)
                    .ThenInclude(r => r.miUsuario)
                    .ThenInclude(u => u.vuelosTomados)
                    .ThenInclude(v => v.destino)
                .Load();
        }

        public async Task<IActionResult> Perfil()
        {
            string usuarioLogeado = HttpContext.Session.GetString("UsuarioLogeado");
            string esAdminString = HttpContext.Session.GetString("esAdmin");
            string usuarioMail = HttpContext.Session.GetString("userMail");
            bool isAdmin = false;

            if (!string.IsNullOrEmpty(esAdminString))
            {
                bool.TryParse(esAdminString, out isAdmin);
            }

            if (usuarioLogeado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.mail == usuarioMail);

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            if (usuario != null)
            {
                return View(usuario);
            }

            return NotFound();
        }


        //------------CRUD------------


        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            string usuarioLogeado = HttpContext.Session.GetString("UsuarioLogeado");
            string esAdminString = HttpContext.Session.GetString("esAdmin");
            string usuarioMail = HttpContext.Session.GetString("userMail");
            bool isAdmin = false;

            if (!string.IsNullOrEmpty(esAdminString))
            {
                bool.TryParse(esAdminString, out isAdmin);
            }

            if (usuarioLogeado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(await _context.usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string usuarioLogeado = HttpContext.Session.GetString("UsuarioLogeado");
            string esAdminString = HttpContext.Session.GetString("esAdmin");
            string usuarioMail = HttpContext.Session.GetString("userMail");
            bool isAdmin = false;

            if (!string.IsNullOrEmpty(esAdminString))
            {
                bool.TryParse(esAdminString, out isAdmin);
            }

            if (usuarioLogeado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            string usuarioLogeado = HttpContext.Session.GetString("UsuarioLogeado");
            string esAdminString = HttpContext.Session.GetString("esAdmin");
            string usuarioMail = HttpContext.Session.GetString("userMail");
            bool isAdmin = false;

            if (!string.IsNullOrEmpty(esAdminString))
            {
                bool.TryParse(esAdminString, out isAdmin);
            }

            if (usuarioLogeado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,dni,nombre,apellido,mail,clave,intentosFallidos,bloqueado,credito,isAdmin")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                bool esInvalido = _context.usuarios.Any(u => u.dni == usuario.dni);

                if (esInvalido)
                {
                    Console.WriteLine("Ese dni ya esta en uso");

                    return RedirectToAction("Index");
                }

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string usuarioLogeado = HttpContext.Session.GetString("UsuarioLogeado");
            string esAdminString = HttpContext.Session.GetString("esAdmin");
            string usuarioMail = HttpContext.Session.GetString("userMail");
            bool isAdmin = false;

            if (!string.IsNullOrEmpty(esAdminString))
            {
                bool.TryParse(esAdminString, out isAdmin);
            }

            if (usuarioLogeado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,dni,nombre,apellido,mail,clave,intentosFallidos,bloqueado,credito,isAdmin")] Usuario usuario)
        {
            if (id != usuario.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool dniInvalido = await _context.usuarios.AnyAsync(u => u.dni == usuario.dni && u.id != usuario.id);

                if (dniInvalido)
                {
                    Console.WriteLine("Ese dni ya esta en uso");

                    return RedirectToAction("Index");
                }

                bool mailInvalido = await _context.usuarios.AnyAsync(u => u.mail == usuario.mail && u.id != usuario.id);

                if (mailInvalido)
                {
                    Console.WriteLine("Ese mail ya esta en uso");

                    return RedirectToAction("Index");
                }

                try
                {
                    var usuario_modificado = await _context.usuarios.FirstOrDefaultAsync(u => u.id == usuario.id);

                    usuario_modificado.dni = usuario.dni;
                    usuario_modificado.nombre = usuario.nombre;
                    usuario_modificado.apellido = usuario.apellido;
                    usuario_modificado.mail = usuario.mail;
                    usuario_modificado.clave = usuario.clave;
                    usuario_modificado.credito = usuario.credito;
                    usuario_modificado.intentosFallidos = usuario.intentosFallidos;
                    usuario_modificado.bloqueado = usuario.bloqueado;
                    usuario_modificado.isAdmin = usuario.isAdmin;

                    //_context.Update(usuario); <--- Esto esta roto
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string usuarioLogeado = HttpContext.Session.GetString("UsuarioLogeado");
            string esAdminString = HttpContext.Session.GetString("esAdmin");
            string usuarioMail = HttpContext.Session.GetString("userMail");
            bool isAdmin = false;

            if (!string.IsNullOrEmpty(esAdminString))
            {
                bool.TryParse(esAdminString, out isAdmin);
            }

            if (usuarioLogeado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usuarios == null)
            {
                return Problem("Entity set 'Context.usuarios'  is null.");
            }

            var usuario = await _context.usuarios.FindAsync(id);

            if (usuario != null)
            {
                usuario.misReservasHabitaciones.ForEach(r =>
                {
                    if (DateTime.Now < r.fechaDesde)
                    {
                        usuario.credito += r.pagado;

                        var usuario_habitacion = _context.usuarioHabitacion
                            .FirstOrDefault(usuario_habitacion =>
                                usuario_habitacion.usuario == r.miUsuario &&
                                usuario_habitacion.habitacion == r.miHabitacion);

                        if (usuario_habitacion != null)
                        {
                            if (usuario_habitacion.cantidad > 1)
                            {
                                usuario_habitacion.cantidad--;
                                _context.usuarioHabitacion.Update(usuario_habitacion);
                            }
                            else
                            {
                                _context.usuarioHabitacion.Remove(usuario_habitacion);
                            }
                        }

                        usuario.misReservasHabitaciones.Remove(r);
                        usuario.habitacionesUsadas.Remove(r.miHabitacion);

                        r.miHabitacion.misReservas.Remove(r);
                        r.miHabitacion.usuarios.Remove(r.miUsuario);

                        _context.habitaciones.Update(r.miHabitacion);
                        _context.reservasHabitacion.Remove(r);
                    }
                });

                usuario.misReservasVuelos.ForEach(r =>
                {
                    if (DateTime.Now < r.miVuelo.fecha)
                    {
                        usuario.credito += r.pagado;

                        var usuario_vuelo = _context.usuarioVuelo
                            .FirstOrDefault(usuario_vuelo =>
                                usuario_vuelo.usuario == r.miUsuario &&
                                usuario_vuelo.vuelo == r.miVuelo);

                        if (usuario_vuelo != null)
                        {
                            _context.usuarioVuelo.Remove(usuario_vuelo);
                        }

                        usuario.misReservasVuelos.Remove(r);
                        usuario.vuelosTomados.Remove(r.miVuelo);

                        r.miVuelo.misReservas.Remove(r);
                        r.miVuelo.pasajeros.Remove(r.miUsuario);

                        _context.vuelos.Update(r.miVuelo);
                        _context.reservasVuelo.Remove(r);
                    }
                });

                _context.usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.usuarios.Any(e => e.id == id);
        }
    }
}
