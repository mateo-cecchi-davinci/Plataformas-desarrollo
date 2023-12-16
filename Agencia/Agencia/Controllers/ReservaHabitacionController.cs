using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agencia.Models;
using System.Text.Json;
using System.Globalization;

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

            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios.Select(u => new { u.id, nombre = u.nombre + " " + u.apellido }), "id", "nombre");
            ViewData["hoteles"] = new SelectList(_context.hoteles, "id", "nombre");
            //ViewData["habitacion_fk"] = new SelectList(_context.habitaciones, "id", "id");
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
                    var usuario_seleccionado = _context.usuarios.FirstOrDefault(u => u.id == reservaHabitacion.usuarioRH_fk);

                    var habitacion_seleccionada = _context.habitaciones
                        .Include(habitacion => habitacion.misReservas)
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

                    var usuario_viejo = _context.reservasHabitacion.Where(r => r.id == reservaHabitacion.id).Select(r => r.miUsuario).FirstOrDefault();
                    var habitacion_vieja = _context.reservasHabitacion.Where(r => r.id == reservaHabitacion.id).Select(r => r.miHabitacion).FirstOrDefault();
                    var pago_viejo = _context.reservasHabitacion.Where(r => r.id == reservaHabitacion.id).Select(r => r.pagado).FirstOrDefault();

                    if (usuario_viejo.id != reservaHabitacion.usuarioRH_fk)
                    {
                        if (DateTime.Now < reservaHabitacion.fechaDesde)
                        {
                            usuario_viejo.credito += pago_viejo;
                        }

                        usuario_viejo.misReservasHabitaciones.Remove(reservaHabitacion);
                        usuario_seleccionado.misReservasHabitaciones.Add(reservaHabitacion);

                        _context.usuarios.Update(usuario_viejo);
                    }

                    if (habitacion_vieja.id != reservaHabitacion.habitacion_fk)
                    {
                        habitacion_vieja.misReservas.Remove(reservaHabitacion);
                        habitacion_seleccionada.misReservas.Add(reservaHabitacion);

                        _context.habitaciones.Update(habitacion_vieja);
                    }

                    usuario_seleccionado.credito += pago_viejo;
                    usuario_seleccionado.credito -= costo;

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
                    .ThenInclude(h => h.hotel)
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
                    .ThenInclude(r => r.miHabitacion)
                    .ThenInclude(habitacion => habitacion.misReservas)
                    .ThenInclude(reserva => reserva.miUsuario)
                    .ThenInclude(usuario => usuario.habitacionesUsadas)
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
                    reservaHabitacion.miUsuario.habitacionesUsadas.Remove(reservaHabitacion.miHabitacion);
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

        [HttpGet]
        public ActionResult<double> ObtenerCosto(int id, string totalPeopleRoomsString, int diferenciaEnDias, string fechaInicio, string fechaFin)
        {
            if (DateTime.TryParseExact(
                    fechaInicio,
                    "ddd MMM dd yyyy HH:mm:ss 'GMT'K '(Argentina Standard Time)'",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime fechaDesde) &&
                DateTime.TryParseExact(
                    fechaFin,
                    "ddd MMM dd yyyy HH:mm:ss 'GMT'K '(Argentina Standard Time)'",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime fechaHasta))
            {
                var hotel_seleccionado = _context.hoteles.Include(h => h.habitaciones).ThenInclude(h => h.misReservas).FirstOrDefault(h => h.id == id);
                Dictionary<string, int> habitaciones = JsonSerializer.Deserialize<Dictionary<string, int>>(totalPeopleRoomsString);

                int habitaciones_chicas = 0;
                int habitaciones_medianas = 0;
                int habitaciones_grandes = 0;
                int cant_personas_en_hab_chicas = 0;
                int cant_personas_en_hab_medianas = 0;
                int cant_personas_en_hab_grandes = 0;

                foreach (var personas_por_hab in habitaciones.Values)
                {
                    if (personas_por_hab > 4)
                    {
                        habitaciones_grandes++;
                        cant_personas_en_hab_grandes += personas_por_hab;
                    }
                    else if (personas_por_hab > 2)
                    {
                        habitaciones_medianas++;
                        cant_personas_en_hab_medianas += personas_por_hab;
                    }
                    else
                    {
                        habitaciones_chicas++;
                        cant_personas_en_hab_chicas += personas_por_hab;
                    }
                }

                var habitaciones_chicas_disponibles = hotel_seleccionado.habitaciones
                    .Where(h => h.capacidad == 2 && !h.misReservas.Any(r => r.fechaDesde <= fechaDesde && r.fechaHasta >= fechaHasta)).ToList();

                var habitaciones_medianas_disponibles = hotel_seleccionado.habitaciones
                    .Where(h => h.capacidad == 4 && !h.misReservas.Any(r => r.fechaDesde <= fechaDesde && r.fechaHasta >= fechaHasta)).ToList();

                var habitaciones_grandes_disponibles = hotel_seleccionado.habitaciones
                    .Where(h => h.capacidad == 8 && !h.misReservas.Any(r => r.fechaDesde <= fechaDesde && r.fechaHasta >= fechaHasta)).ToList();

                if (habitaciones_chicas <= habitaciones_chicas_disponibles.Count() &&
                    habitaciones_medianas <= habitaciones_medianas_disponibles.Count() &&
                    habitaciones_grandes <= habitaciones_grandes_disponibles.Count())
                {
                    double costo_hab_chicas = 0;
                    double costo_hab_medianas = 0;
                    double costo_hab_grandes = 0;
                    double total = 0;

                    foreach (var hab in habitaciones_chicas_disponibles)
                    {
                        if (habitaciones_chicas > 0)
                        {
                            costo_hab_chicas += hab.costo * diferenciaEnDias * cant_personas_en_hab_chicas;
                            habitaciones_chicas--;
                        }
                    }

                    foreach (var hab in habitaciones_medianas_disponibles)
                    {
                        if (habitaciones_medianas > 0)
                        {
                            costo_hab_medianas += hab.costo * diferenciaEnDias * cant_personas_en_hab_medianas;
                            habitaciones_medianas--;
                        }
                    }

                    foreach (var hab in habitaciones_grandes_disponibles)
                    {
                        if (habitaciones_grandes > 0)
                        {
                            costo_hab_grandes += hab.costo * diferenciaEnDias * cant_personas_en_hab_grandes;
                            habitaciones_grandes--;
                        }
                    }

                    total = costo_hab_chicas + costo_hab_medianas + costo_hab_grandes;

                    return total;
                }
                else
                {
                    // no hay disponibilidad
                    return 0.2;
                }
            }
            else
            {
                // estan mal las fechas
                return 0.1;
            }
        }
    }
}
