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
    public class ReservaHabitacionController : Controller
    {
        private readonly Context _context;

        public ReservaHabitacionController(Context context)
        {
            _context = context;
        }

        // GET: ReservaHabitacions
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

            var context = _context.reservasHabitacion
                .Include(r => r.miHabitacion)
                    .ThenInclude(habitacion => habitacion.hotel)
                .Include(r => r.miUsuario);
            
            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(await context.ToListAsync());
        }

        // GET: ReservaHabitacions/Details/5
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

            if (id == null || _context.reservasHabitacion == null)
            {
                return NotFound();
            }

            var reservaHabitacion = await _context.reservasHabitacion
                .Include(r => r.miHabitacion)
                    .ThenInclude(habitacion => habitacion.hotel)
                .Include(r => r.miUsuario)
                .FirstOrDefaultAsync(m => m.id == id);

            if (reservaHabitacion == null)
            {
                return NotFound();
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(reservaHabitacion);
        }

        // GET: ReservaHabitacions/Create
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

            ViewData["habitacion_fk"] = new SelectList(_context.habitaciones, "id", "id");
            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios, "id", "apellido");
            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View();
        }

        // POST: ReservaHabitacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,fechaDesde,fechaHasta,pagado,cantPersonas,habitacion_fk,usuarioRH_fk")] ReservaHabitacion reservaHabitacion)
        {
            if (ModelState.IsValid)
            {
                var usuario_seleccionado = _context.usuarios.FirstOrDefault(u => u.id == reservaHabitacion.usuarioRH_fk);
                var habitacion_seleccionada = _context.habitaciones.FirstOrDefault(h => h.id == reservaHabitacion.habitacion_fk);

                TimeSpan diferencia = reservaHabitacion.fechaHasta - reservaHabitacion.fechaDesde;
                int cantidadDias = Math.Abs(diferencia.Days);

                double costo = reservaHabitacion.cantPersonas * habitacion_seleccionada.costo * cantidadDias;

                if (usuario_seleccionado.credito - costo < 0)
                {
                    Console.WriteLine("Crédito insuficiente");
                    return RedirectToAction("Index");
                }

                if (habitacion_seleccionada.capacidad < reservaHabitacion.cantPersonas)
                {
                    Console.WriteLine("Capacidad de la habitación insuficiente");
                    return RedirectToAction("Index");
                }

                if (habitacion_seleccionada.misReservas.Any(rh => rh.fechaDesde <= reservaHabitacion.fechaHasta && rh.fechaHasta >= reservaHabitacion.fechaDesde))
                {
                    Console.WriteLine("Error, la habitación esta ocupada");
                    return RedirectToAction("Index");
                }

                var usuario_habitacion = _context.usuarioHabitacion.FirstOrDefault(uh => uh.usuarios_fk == usuario_seleccionado.id && uh.habitaciones_fk == habitacion_seleccionada.id);

                if (usuario_habitacion == null)
                {
                    usuario_habitacion = new UsuarioHabitacion(usuario_seleccionado.id, habitacion_seleccionada.id, 1);

                    _context.usuarioHabitacion.Add(usuario_habitacion);
                }
                else
                {
                    usuario_habitacion.cantidad++;

                    _context.usuarioHabitacion.Update(usuario_habitacion);
                }

                usuario_seleccionado.credito -= costo;

                usuario_seleccionado.misReservasHabitaciones.Add(reservaHabitacion);
                habitacion_seleccionada.misReservas.Add(reservaHabitacion);

                _context.usuarios.Update(usuario_seleccionado);
                _context.habitaciones.Update(habitacion_seleccionada);
                _context.Add(reservaHabitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["habitacion_fk"] = new SelectList(_context.habitaciones, "id", "id", reservaHabitacion.habitacion_fk);
            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios, "id", "apellido", reservaHabitacion.usuarioRH_fk);

            return View(reservaHabitacion);
        }

        // GET: ReservaHabitacions/Edit/5
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

            if (id == null || _context.reservasHabitacion == null)
            {
                return NotFound();
            }

            var reservaHabitacion = await _context.reservasHabitacion.FindAsync(id);

            if (reservaHabitacion == null)
            {
                return NotFound();
            }

            ViewData["habitacion_fk"] = new SelectList(_context.habitaciones, "id", "id", reservaHabitacion.habitacion_fk);
            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios, "id", "apellido", reservaHabitacion.usuarioRH_fk);
            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(reservaHabitacion);
        }

        // POST: ReservaHabitacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,fechaDesde,fechaHasta,pagado,cantPersonas,habitacion_fk,usuarioRH_fk")] ReservaHabitacion reservaHabitacion)
        {
            if (id != reservaHabitacion.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuario_seleccionado = _context.usuarios
                        .Include(usuario => usuario.misReservasHabitaciones)
                        .FirstOrDefault(u => u.id == reservaHabitacion.usuarioRH_fk);

                    var habitacion_seleccionada = _context.habitaciones
                        .Include(vuelo => vuelo.misReservas)
                        .FirstOrDefault(h => h.id == reservaHabitacion.habitacion_fk);

                    if (usuario_seleccionado == null)
                    {
                        Console.WriteLine("Usuario no encontrado");
                        return RedirectToAction("Index");
                    }

                    if (habitacion_seleccionada == null)
                    {
                        Console.WriteLine("Habitacion no encontrada");
                        return RedirectToAction("Index");
                    }

                    if (habitacion_seleccionada.capacidad < reservaHabitacion.cantPersonas)
                    {
                        Console.WriteLine("Capacidad de la habitación insuficiente");
                        return RedirectToAction("Index");
                    }

                    if (habitacion_seleccionada.misReservas.Any(rh => rh.fechaDesde <= reservaHabitacion.fechaHasta && rh.fechaHasta >= reservaHabitacion.fechaDesde && rh.id != reservaHabitacion.id))
                    {
                        Console.WriteLine("Error, la habitación esta ocupada");
                        return RedirectToAction("Index");
                    }

                    TimeSpan diferencia = reservaHabitacion.fechaHasta - reservaHabitacion.fechaDesde;
                    int cantidadDias = Math.Abs(diferencia.Days);

                    double costo = reservaHabitacion.cantPersonas * habitacion_seleccionada.costo * cantidadDias;

                    if (usuario_seleccionado.credito - costo < 0)
                    {
                        Console.WriteLine("Crédito insuficiente");
                        return RedirectToAction("Index");
                    }

                    var usuario_habitacion = _context.usuarioHabitacion.FirstOrDefault(uh => uh.usuarios_fk == usuario_seleccionado.id && uh.habitaciones_fk == habitacion_seleccionada.id);

                    if (usuario_habitacion == null)
                    {
                        usuario_habitacion = new UsuarioHabitacion(usuario_seleccionado.id, habitacion_seleccionada.id, 1);

                        _context.usuarioHabitacion.Add(usuario_habitacion);
                    }
                    else
                    {
                        usuario_habitacion.cantidad++;

                        _context.usuarioHabitacion.Update(usuario_habitacion);
                    }

                    var reserva_anterior = _context.reservasHabitacion
                        .Include(reserva_anterior => reserva_anterior.miUsuario)
                            .ThenInclude(usuario => usuario.misReservasHabitaciones)
                        .Include(reserva_anterior => reserva_anterior.miHabitacion)
                            .ThenInclude(habitacion => habitacion.misReservas)
                        .FirstOrDefault(reserva_anterior => reserva_anterior.id == reservaHabitacion.id);

                    if (reserva_anterior.usuarioRH_fk != reservaHabitacion.usuarioRH_fk)
                    {
                        reserva_anterior.miUsuario.credito += reserva_anterior.pagado;
                        reserva_anterior.miUsuario.misReservasHabitaciones.Remove(reserva_anterior);

                        _context.usuarios.Update(reserva_anterior.miUsuario);
                    }

                    if (reserva_anterior.habitacion_fk != reservaHabitacion.habitacion_fk)
                    {
                        reserva_anterior.miHabitacion.misReservas.Remove(reserva_anterior);

                        _context.habitaciones.Update(reserva_anterior.miHabitacion);
                    }

                    usuario_seleccionado.credito += reserva_anterior.pagado;
                    usuario_seleccionado.credito -= costo;

                    usuario_seleccionado.misReservasHabitaciones.Remove(reserva_anterior);
                    habitacion_seleccionada.misReservas.Remove(reserva_anterior);
                    usuario_seleccionado.misReservasHabitaciones.Add(reservaHabitacion);
                    habitacion_seleccionada.misReservas.Add(reservaHabitacion);

                    _context.usuarios.Update(usuario_seleccionado);
                    _context.habitaciones.Update(habitacion_seleccionada);
                    _context.Update(reservaHabitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaHabitacionExists(reservaHabitacion.id))
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

            ViewData["habitacion_fk"] = new SelectList(_context.habitaciones, "id", "id", reservaHabitacion.habitacion_fk);
            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios, "id", "apellido", reservaHabitacion.usuarioRH_fk);

            return View(reservaHabitacion);
        }

        // GET: ReservaHabitacions/Delete/5
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

            if (id == null || _context.reservasHabitacion == null)
            {
                return NotFound();
            }

            var reservaHabitacion = await _context.reservasHabitacion
                .Include(r => r.miHabitacion)
                .Include(r => r.miUsuario)
                .FirstOrDefaultAsync(m => m.id == id);

            if (reservaHabitacion == null)
            {
                return NotFound();
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(reservaHabitacion);
        }

        // POST: ReservaHabitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.reservasHabitacion == null)
            {
                return Problem("Entity set 'Context.reservasHotel'  is null.");
            }

            var reservaHabitacion = await _context.reservasHabitacion
                .Include(reserva_habitacion => reserva_habitacion.miUsuario)
                    .ThenInclude(usuario => usuario.misReservasHabitaciones)
                .Include(reserva_habitacion => reserva_habitacion.miHabitacion)
                    .ThenInclude(habitacion => habitacion.misReservas)
                .FirstOrDefaultAsync(reserva_habitacion => reserva_habitacion.id == id);

            if (reservaHabitacion != null)
            {
                if (DateTime.Now < reservaHabitacion.fechaDesde)
                {
                    reservaHabitacion.miUsuario.credito += reservaHabitacion.pagado;

                    var usuario_habitacion = _context.usuarioHabitacion
                        .FirstOrDefault(usuario_habitacion => usuario_habitacion.usuario == reservaHabitacion.miUsuario && usuario_habitacion.habitacion == reservaHabitacion.miHabitacion);

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

                    reservaHabitacion.miUsuario.misReservasHabitaciones.Remove(reservaHabitacion);
                    reservaHabitacion.miHabitacion.misReservas.Remove(reservaHabitacion);

                    _context.usuarios.Update(reservaHabitacion.miUsuario);
                    _context.habitaciones.Update(reservaHabitacion.miHabitacion);
                }

                _context.reservasHabitacion.Remove(reservaHabitacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaHabitacionExists(int id)
        {
          return _context.reservasHabitacion.Any(e => e.id == id);
        }
    }
}
