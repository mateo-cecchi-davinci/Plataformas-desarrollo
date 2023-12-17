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
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Agencia.Controllers
{
    public class ReservaHabitacionController : Controller
    {
        private readonly Context _context;

        public ReservaHabitacionController(Context context)
        {
            _context = context;

            _context.reservasHabitacion
                .Include(reserva_habitacion => reserva_habitacion.miUsuario)
                    .ThenInclude(usuario => usuario.misReservasHabitaciones)
                    .ThenInclude(r => r.miHabitacion)
                    .ThenInclude(habitacion => habitacion.misReservas)
                    .ThenInclude(reserva => reserva.miUsuario)
                    .ThenInclude(usuario => usuario.habitacionesUsadas)
                    .ThenInclude(h => h.hotel)
                    .ThenInclude(h => h.habitaciones)
                .Load();

            _context.habitaciones
                .Include(h => h.hotel)
                .Load();
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

            var context = _context.reservasHabitacion;

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

            var reservaHabitacion = await _context.reservasHabitacion.FirstOrDefaultAsync(m => m.id == id);

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

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios.Select(u => new { u.id, nombre = u.nombre + " " + u.apellido }), "id", "nombre");
            ViewData["hoteles"] = new SelectList(_context.hoteles, "id", "nombre");

            return View();
        }

        // POST: ReservaHabitacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int hotel, string people, string total_people_rooms, int habitaciones_chicas, int habitaciones_medianas, int habitaciones_grandes, [Bind("id,fechaDesde,fechaHasta,pagado,cantPersonas,habitacion_fk,usuarioRH_fk")] ReservaHabitacion reservaHabitacion)
        {
            if (ModelState.IsValid)
            {
                var usuario_seleccionado = _context.usuarios.FirstOrDefault(u => u.id == reservaHabitacion.usuarioRH_fk);
                var hotel_seleccionado = _context.hoteles.FirstOrDefault(h => h.id == hotel);

                if (usuario_seleccionado == null)
                {
                    Console.WriteLine("Usuario inválido");
                    return RedirectToAction("Index");
                }

                if (hotel_seleccionado == null)
                {
                    Console.WriteLine("Hotel inválido");
                    return RedirectToAction("Index");
                }

                if (usuario_seleccionado.credito - reservaHabitacion.pagado < 0)
                {
                    Console.WriteLine("Crédito insuficiente");
                    return RedirectToAction("Index");
                }

                //Dictionary<string, int> personas_por_habitacion = JsonSerializer.Deserialize<Dictionary<string, int>>(total_people_rooms);
                List<Habitacion> hab_Chicas = new List<Habitacion>();
                List<Habitacion> hab_Medianas = new List<Habitacion>();
                List<Habitacion> hab_Grandes = new List<Habitacion>();
                //List<int> cantPersonas = new List<int>();

                //foreach (var item in personas_por_habitacion.Values)
                //{
                //    cantPersonas.Add(item);
                //}

                foreach (var habitacion in hotel_seleccionado.habitaciones)
                {
                    if (!habitacion.misReservas.Any(reservaIterada => reservaIterada.fechaDesde <= reservaHabitacion.fechaHasta && reservaIterada.fechaHasta >= reservaHabitacion.fechaDesde))
                    {
                        if (habitacion.capacidad == 2 && habitaciones_chicas > 0)
                        {
                            hab_Chicas.Add(habitacion);
                            habitaciones_chicas--;
                        }

                        if (habitacion.capacidad == 4 && habitaciones_medianas > 0)
                        {
                            hab_Medianas.Add(habitacion);
                            habitaciones_medianas--;
                        }

                        if (habitacion.capacidad == 8 && habitaciones_grandes > 0)
                        {
                            hab_Grandes.Add(habitacion);
                            habitaciones_grandes--;
                        }
                    }
                }

                // Hay que agregarle a c/ reserva la cantidad de personas de esa habitacion y no la cantidad total

                if (habitaciones_grandes == 0 && habitaciones_medianas == 0 && habitaciones_chicas == 0)
                {
                    foreach (var hab in hab_Chicas)
                    {
                        var nuevaReserva = new ReservaHabitacion(
                            hab,
                            usuario_seleccionado,
                            reservaHabitacion.fechaDesde,
                            reservaHabitacion.fechaHasta,
                            hab.costo,
                            int.Parse(people),
                            hab.id,
                            usuario_seleccionado.id
                        );

                        usuario_seleccionado.misReservasHabitaciones.Add(nuevaReserva);
                        hab.misReservas.Add(nuevaReserva);

                        var usuario_habitacion = _context.usuarioHabitacion
                            .FirstOrDefault(uh => uh.usuarios_fk == usuario_seleccionado.id && uh.habitaciones_fk == hab.id);

                        if (usuario_habitacion == null)
                        {
                            usuario_habitacion = new UsuarioHabitacion(usuario_seleccionado.id, hab.id, 1);

                            _context.usuarioHabitacion.Add(usuario_habitacion);
                        }
                        else
                        {
                            usuario_habitacion.cantidad++;

                            _context.usuarioHabitacion.Update(usuario_habitacion);
                        }

                        _context.habitaciones.Update(hab);
                        _context.reservasHabitacion.Add(nuevaReserva);
                    }

                    foreach (var hab in hab_Medianas)
                    {
                        var nuevaReserva = new ReservaHabitacion(
                            hab,
                            usuario_seleccionado,
                            reservaHabitacion.fechaDesde,
                            reservaHabitacion.fechaHasta,
                            hab.costo,
                            int.Parse(people),
                            hab.id,
                            usuario_seleccionado.id
                        );

                        usuario_seleccionado.misReservasHabitaciones.Add(nuevaReserva);
                        hab.misReservas.Add(nuevaReserva);

                        var usuario_habitacion = _context.usuarioHabitacion
                            .FirstOrDefault(uh => uh.usuarios_fk == usuario_seleccionado.id && uh.habitaciones_fk == hab.id);

                        if (usuario_habitacion == null)
                        {
                            usuario_habitacion = new UsuarioHabitacion(usuario_seleccionado.id, hab.id, 1);

                            _context.usuarioHabitacion.Add(usuario_habitacion);
                        }
                        else
                        {
                            usuario_habitacion.cantidad++;

                            _context.usuarioHabitacion.Update(usuario_habitacion);
                        }

                        _context.habitaciones.Update(hab);
                        _context.reservasHabitacion.Add(nuevaReserva);
                    }

                    foreach (var hab in hab_Grandes)
                    {
                        var nuevaReserva = new ReservaHabitacion(
                            hab,
                            usuario_seleccionado,
                            reservaHabitacion.fechaDesde,
                            reservaHabitacion.fechaHasta,
                            hab.costo,
                            int.Parse(people),
                            hab.id,
                            usuario_seleccionado.id
                        );

                        usuario_seleccionado.misReservasHabitaciones.Add(nuevaReserva);
                        hab.misReservas.Add(nuevaReserva);

                        var usuario_habitacion = _context.usuarioHabitacion
                            .FirstOrDefault(uh => uh.usuarios_fk == usuario_seleccionado.id && uh.habitaciones_fk == hab.id);

                        if (usuario_habitacion == null)
                        {
                            usuario_habitacion = new UsuarioHabitacion(usuario_seleccionado.id, hab.id, 1);

                            _context.usuarioHabitacion.Add(usuario_habitacion);
                        }
                        else
                        {
                            usuario_habitacion.cantidad++;

                            _context.usuarioHabitacion.Update(usuario_habitacion);
                        }

                        _context.habitaciones.Update(hab);
                        _context.reservasHabitacion.Add(nuevaReserva);
                    }

                    usuario_seleccionado.credito -= reservaHabitacion.pagado;

                    _context.usuarios.Update(usuario_seleccionado);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
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

            var habitaciones = await _context.habitaciones.ToListAsync();

            var habitaciones_con_hotel = habitaciones.Select(h =>
            {
                string capacidadText;

                switch (h.capacidad)
                {
                    case 2:
                        capacidadText = "para dos personas";
                        break;
                    case 4:
                        capacidadText = "para cuatro personas";
                        break;
                    case 8:
                        capacidadText = "para ocho personas";
                        break;
                    default:
                        capacidadText = "habitación inválida";
                        break;
                }

                return new SelectListItem
                {
                    Value = h.id.ToString(),
                    Text = $"{h.hotel.nombre} - {capacidadText}"
                };
            }).ToList();

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            ViewData["habitacion_fk"] = new SelectList(habitaciones_con_hotel, "Value", "Text");
            ViewData["usuarioRV_fk"] = new SelectList(_context.usuarios.Select(u => new { u.id, nombre = u.nombre + " " + u.apellido }), "id", "nombre");

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
                    var habitacion_seleccionada = _context.habitaciones.FirstOrDefault(h => h.id == reservaHabitacion.habitacion_fk);
                    var reserva_seleccionada = _context.reservasHabitacion.FirstOrDefault(r => r.id == reservaHabitacion.id);

                    if (usuario_seleccionado == null)
                    {
                        Console.WriteLine("Usuario invalidó");
                        return RedirectToAction("Index");
                    }

                    if (habitacion_seleccionada == null)
                    {
                        Console.WriteLine("Habitación inválida");
                        return RedirectToAction("Index");
                    }

                    if (reserva_seleccionada == null)
                    {
                        Console.WriteLine("Reserva inválida");
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

                    if (reserva_seleccionada.miUsuario.id != reservaHabitacion.usuarioRH_fk)
                    {
                        if (DateTime.Now < reservaHabitacion.fechaDesde)
                        {
                            reserva_seleccionada.miUsuario.credito += reserva_seleccionada.pagado;
                        }

                        reserva_seleccionada.miUsuario.misReservasHabitaciones.Remove(reserva_seleccionada);
                        usuario_seleccionado.misReservasHabitaciones.Add(reserva_seleccionada);

                        _context.usuarios.Update(reserva_seleccionada.miUsuario);

                        reserva_seleccionada.usuarioRH_fk = reservaHabitacion.usuarioRH_fk;
                        reserva_seleccionada.miUsuario = usuario_seleccionado;
                    }

                    if (reserva_seleccionada.miHabitacion.id != reservaHabitacion.habitacion_fk)
                    {
                        reserva_seleccionada.miHabitacion.misReservas.Remove(reserva_seleccionada);
                        habitacion_seleccionada.misReservas.Add(reserva_seleccionada);

                        _context.habitaciones.Update(reserva_seleccionada.miHabitacion);
                        _context.habitaciones.Update(habitacion_seleccionada);

                        reserva_seleccionada.habitacion_fk = reservaHabitacion.habitacion_fk;
                        reserva_seleccionada.miHabitacion = habitacion_seleccionada;
                    }

                    usuario_seleccionado.credito += reserva_seleccionada.pagado;
                    usuario_seleccionado.credito -= costo;

                    reserva_seleccionada.fechaDesde = reservaHabitacion.fechaDesde;
                    reserva_seleccionada.fechaHasta = reservaHabitacion.fechaHasta;
                    reserva_seleccionada.pagado = reservaHabitacion.pagado;
                    reserva_seleccionada.cantPersonas = reservaHabitacion.cantPersonas;

                    _context.usuarios.Update(usuario_seleccionado);

                    //_context.Update(reservaHabitacion); <--- esto esta roto
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

            var reservaHabitacion = await _context.reservasHabitacion.FirstOrDefaultAsync(m => m.id == id);

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

            var reservaHabitacion = await _context.reservasHabitacion.FirstOrDefaultAsync(reserva_habitacion => reserva_habitacion.id == id);

            if (reservaHabitacion != null)
            {
                if (DateTime.Now < reservaHabitacion.fechaDesde)
                {
                    reservaHabitacion.miUsuario.credito += reservaHabitacion.pagado;

                    var usuario_habitacion = _context.usuarioHabitacion
                        .FirstOrDefault(usuario_habitacion =>
                            usuario_habitacion.usuario == reservaHabitacion.miUsuario &&
                            usuario_habitacion.habitacion == reservaHabitacion.miHabitacion);

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
        public IActionResult ObtenerCosto(int id, string totalPeopleRoomsString, int diferenciaEnDias, string fechaInicio, string fechaFin)
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
                    int habitaciones_chicas_seleccionadas = habitaciones_chicas;
                    int habitaciones_medianas_seleccionadas = habitaciones_medianas;
                    int habitaciones_grandes_seleccionadas = habitaciones_grandes;
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

                    var costo_habitaciones = new CostoHabitacionesJson
                    {
                        costo = total,
                        habitacionesChicasSeleccionadas = habitaciones_chicas_seleccionadas,
                        habitacionesMedianasSeleccionadas = habitaciones_medianas_seleccionadas,
                        habitacionesGrandesSeleccionadas = habitaciones_grandes_seleccionadas
                    };

                    var json = JsonConvert.SerializeObject(costo_habitaciones);
                    return new ContentResult
                    {
                        Content = json,
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                }
                else
                {
                    return StatusCode(404, "No hay disponibilidad");
                }
            }
            else
            {
                return StatusCode(400, "Fechas inválidas");
            }
        }
    }
}
