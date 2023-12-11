using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agencia.Models;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Agencia.Controllers
{
    public class HotelController : Controller
    {
        private readonly Context _context;

        public HotelController(Context context)
        {
            _context = context;
        }

        public IActionResult Home()
        {
            string usuarioLogeado = HttpContext.Session.GetString("UsuarioLogeado");
            string esAdminString = HttpContext.Session.GetString("esAdmin");
            string usuarioMail = HttpContext.Session.GetString("userMail");
            bool isAdmin = false;

            if (!string.IsNullOrEmpty(esAdminString))
            {

                bool.TryParse(esAdminString, out isAdmin);
            }

            var hoteles = _context.hoteles.Include(h => h.ubicacion).ToList();

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;
            ViewData["hoteles"] = hoteles;

            return View();
        }

        public IActionResult ResultadosDeLaBusqueda(string origin, string destination, DateTime start_date, DateTime end_date, int rooms, int people, string total_adults, string total_minors, string total_people_rooms)
        {
            string usuarioLogeado = HttpContext.Session.GetString("UsuarioLogeado");
            string esAdminString = HttpContext.Session.GetString("esAdmin");
            string usuarioMail = HttpContext.Session.GetString("userMail");
            bool isAdmin = false;

            if (!string.IsNullOrEmpty(esAdminString))
            {

                bool.TryParse(esAdminString, out isAdmin);
            }

            var hoteles = _context.hoteles
                .Include(h => h.ubicacion)
                .Include(h => h.habitaciones)
                .Where(hotel => hotel.ubicacion.nombre == destination) //AGREGAR FILTROS ACA
                .ToList();

            TimeSpan diferencia = end_date.Subtract(start_date);
            int cantDias = diferencia.Days;

            Dictionary<string, int> habitaciones = JsonSerializer.Deserialize<Dictionary<string, int>>(total_people_rooms);
            Dictionary<string, int> adultos = JsonSerializer.Deserialize<Dictionary<string, int>>(total_adults);
            Dictionary<string, int> menores = JsonSerializer.Deserialize<Dictionary<string, int>>(total_minors);

            int habitaciones_chicas = 0;
            int habitaciones_medianas = 0;
            int habitaciones_grandes = 0;

            foreach (var personas_por_hab in habitaciones.Values)
            {
                if (personas_por_hab > 4)
                {
                    habitaciones_grandes++;
                }
                else if (personas_por_hab > 2)
                {
                    habitaciones_medianas++;
                }
                else
                {
                    habitaciones_chicas++;
                }
            }

            var hotelesSeleccionados = hoteles
                .Where(h => h.habitaciones.Count(hab => hab.capacidad == 2) >= habitaciones_chicas &&
                    h.habitaciones.Count(hab => hab.capacidad == 4) >= habitaciones_medianas &&
                    h.habitaciones.Count(hab => hab.capacidad == 8) >= habitaciones_grandes)
                .Select(hotel => new
                {
                    hotel.id,
                    hotel.nombre,
                    hotel.descripcion,
                    hotel.imagen,
                    costo = hotel.habitaciones
                        .Where(hab => hab.capacidad == 2 || hab.capacidad == 4 || hab.capacidad == 8)
                        .Take(habitaciones_chicas + habitaciones_medianas + habitaciones_grandes)
                        .Sum(hab => hab.costo)
                })
            .ToList();


            int total_adultos = adultos.Values.Sum();
            int total_menores = menores.Values.Sum();

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;
            ViewBag.hotelesSeleccionados = hotelesSeleccionados;
            ViewBag.fecha_desde = start_date;
            ViewBag.fecha_hasta = end_date;
            ViewBag.cantDias = cantDias;
            ViewBag.total_adultos = total_adultos;
            ViewBag.total_menores = total_menores;
            ViewBag.total_personas = people;
            ViewBag.habitaciones_chicas = habitaciones_chicas;
            ViewBag.habitaciones_medianas = habitaciones_medianas;
            ViewBag.habitaciones_grandes = habitaciones_grandes;

            return View();
        }

        [HttpPost]
        public IActionResult Reservar([FromBody] ReservaHabitacionJson reserva)
        {
            string usuarioMail = HttpContext.Session.GetString("userMail");
            var usuario = _context.usuarios.FirstOrDefault(u => u.mail == usuarioMail);

            if (usuario.credito < reserva.total)
            {
                return Json(new { message = "Error, credito insuficiente" });
            }

            var hotel_id = reserva.hotel_id;
            var fechaDesde = reserva.start_date;
            var fechaHasta = reserva.end_date;
            var total = reserva.total;
            var habitacionesChicas = reserva.sm_rooms;
            var habitacionesMedianas = reserva.md_rooms;
            var habitacionesGrandes = reserva.xl_rooms;
            var totalPersonas = reserva.people;

            var hotel = _context.hoteles.Include(h => h.habitaciones).FirstOrDefault(h => h.id == hotel_id);

            if (hotel != null)
            {
                var habitacionesDisponibles = hotel.habitaciones
                .GroupBy(habitacion => habitacion.capacidad)
                .ToDictionary(group => group.Key, group => group.Count());

                bool hab_suficientes =
                    habitacionesDisponibles.TryGetValue(2, out int disponiblesCapacidad2) &&
                    habitacionesDisponibles.TryGetValue(4, out int disponiblesCapacidad4) &&
                    habitacionesDisponibles.TryGetValue(8, out int disponiblesCapacidad8) &&
                    disponiblesCapacidad2 >= habitacionesChicas &&
                    disponiblesCapacidad4 >= habitacionesMedianas &&
                    disponiblesCapacidad8 >= habitacionesGrandes;

                if (hab_suficientes)
                {
                    List<Habitacion> hab_Chicas = new List<Habitacion>();
                    List<Habitacion> hab_Medianas = new List<Habitacion>();
                    List<Habitacion> hab_Grandes = new List<Habitacion>();

                    foreach (var habitacion in hotel.habitaciones)
                    {
                        if (!habitacion.misReservas.Any(reservaIterada => reservaIterada.fechaDesde <= fechaHasta && reservaIterada.fechaHasta >= fechaDesde))
                        {
                            if (habitacion.capacidad == 2 && habitacionesChicas > 0)
                            {
                                hab_Chicas.Add(habitacion);
                                habitacionesChicas--;
                            }

                            if (habitacion.capacidad == 4 && habitacionesMedianas > 0)
                            {
                                hab_Medianas.Add(habitacion);
                                habitacionesMedianas--;
                            }

                            if (habitacion.capacidad == 8 && habitacionesGrandes > 0)
                            {
                                hab_Grandes.Add(habitacion);
                                habitacionesGrandes--;
                            }
                        }
                    }

                    if (habitacionesGrandes == 0 && habitacionesMedianas == 0 && habitacionesChicas == 0)
                    {

                        if (usuario != null)
                        {
                            foreach (var hab in hab_Chicas)
                            {
                                var nuevaReserva = new ReservaHabitacion(hab, usuario, fechaDesde, fechaHasta, total, totalPersonas, hab.id, usuario.id);

                                usuario.misReservasHabitaciones.Add(nuevaReserva);
                                hab.misReservas.Add(nuevaReserva);

                                var usuario_habitacion = _context.usuarioHabitacion.FirstOrDefault(uh => uh.usuarios_fk == usuario.id && uh.habitaciones_fk == hab.id);

                                if (usuario_habitacion == null)
                                {
                                    usuario_habitacion = new UsuarioHabitacion(usuario.id, hab.id, 1);

                                    _context.usuarioHabitacion.Add(usuario_habitacion);
                                }
                                else
                                {
                                    usuario_habitacion.cantidad++;

                                    _context.usuarioHabitacion.Update(usuario_habitacion);
                                }

                                _context.reservasHabitacion.Add(nuevaReserva);
                                _context.Update(hab);
                            }

                            foreach (var hab in hab_Medianas)
                            {
                                var nuevaReserva = new ReservaHabitacion(hab, usuario, fechaDesde, fechaHasta, total, totalPersonas, hab.id, usuario.id);

                                usuario.misReservasHabitaciones.Add(nuevaReserva);
                                hab.misReservas.Add(nuevaReserva);

                                var usuario_habitacion = _context.usuarioHabitacion.FirstOrDefault(uh => uh.usuarios_fk == usuario.id && uh.habitaciones_fk == hab.id);

                                if (usuario_habitacion == null)
                                {
                                    usuario_habitacion = new UsuarioHabitacion(usuario.id, hab.id, 1);

                                    _context.usuarioHabitacion.Add(usuario_habitacion);
                                }
                                else
                                {
                                    usuario_habitacion.cantidad++;

                                    _context.usuarioHabitacion.Update(usuario_habitacion);
                                }

                                _context.reservasHabitacion.Add(nuevaReserva);
                                _context.Update(hab);
                            }

                            foreach (var hab in hab_Grandes)
                            {
                                var nuevaReserva = new ReservaHabitacion(hab, usuario, fechaDesde, fechaHasta, total, totalPersonas, hab.id, usuario.id);

                                usuario.misReservasHabitaciones.Add(nuevaReserva);
                                hab.misReservas.Add(nuevaReserva);

                                var usuario_habitacion = _context.usuarioHabitacion.FirstOrDefault(uh => uh.usuarios_fk == usuario.id && uh.habitaciones_fk == hab.id);

                                if (usuario_habitacion == null)
                                {
                                    usuario_habitacion = new UsuarioHabitacion(usuario.id, hab.id, 1);

                                    _context.usuarioHabitacion.Add(usuario_habitacion);
                                }
                                else
                                {
                                    usuario_habitacion.cantidad++;

                                    _context.usuarioHabitacion.Update(usuario_habitacion);
                                }

                                _context.reservasHabitacion.Add(nuevaReserva);
                                _context.Update(hab);
                            }

                            usuario.credito -= total;

                            _context.Update(usuario);
                            _context.SaveChanges();

                            return Json(new { message = "Solicitud recibida con éxito" });
                        }
                    }
                    else
                    {
                        return Json(new { message = "Error, no se puede reservar" });
                    }
                }

                return Json(new { message = "Error, cantidad de habitaciones insuficiente" });
            }

            return Json(new { message = "Hubo un error" });
        }

        //------------CRUD------------


        // GET: Hotels
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
            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;
            var context = _context.hoteles.Include(h => h.ubicacion);
            return View(await context.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.hoteles == null)
            {
                return NotFound();
            }

            var hotel = await _context.hoteles
                .Include(h => h.ubicacion)
                .FirstOrDefaultAsync(m => m.id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // GET: Hotels/Create
        public IActionResult Create()
        {
            ViewData["ciudad_fk"] = new SelectList(_context.ciudades, "id", "nombre");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id, nombre, ciudad_fk, archivoImagen, descripcion")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                if (hotel.archivoImagen != null)
                {
                    string directorio = "wwwroot/images/hotel/";
                    string nombreArchivo = Guid.NewGuid().ToString() + "_" + hotel.archivoImagen.FileName;
                    string rutaCompleta = Path.Combine(directorio, nombreArchivo);

                    Directory.CreateDirectory(Path.GetDirectoryName(rutaCompleta));

                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        await hotel.archivoImagen.CopyToAsync(stream);
                    }

                    hotel.imagen = rutaCompleta.Replace("wwwroot", "").TrimStart('\\', '/');
                }

                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ciudad_fk"] = new SelectList(_context.ciudades, "id", "nombre", hotel.ciudad_fk);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.hoteles == null)
            {
                return NotFound();
            }

            var hotel = await _context.hoteles.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            ViewData["ciudad_fk"] = new SelectList(_context.ciudades, "id", "nombre", hotel.ciudad_fk);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id, nombre, ciudad_fk, archivoImagen, descripcion")] Hotel hotel)
        {
            if (id != hotel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (hotel.archivoImagen != null)
                    {
                        string directorio = "wwwroot/images/hotel/";
                        string nombreArchivo = Guid.NewGuid().ToString() + "_" + hotel.archivoImagen.FileName;
                        string rutaCompleta = Path.Combine(directorio, nombreArchivo);

                        Directory.CreateDirectory(Path.GetDirectoryName(rutaCompleta));

                        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                        {
                            await hotel.archivoImagen.CopyToAsync(stream);
                        }

                        hotel.imagen = rutaCompleta.Replace("wwwroot", "").TrimStart('\\', '/');
                    }

                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.id))
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
            ViewData["ciudad_fk"] = new SelectList(_context.ciudades, "id", "nombre", hotel.ciudad_fk);
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.hoteles == null)
            {
                return NotFound();
            }

            var hotel = await _context.hoteles
                .Include(h => h.ubicacion)
                .FirstOrDefaultAsync(m => m.id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.hoteles == null)
            {
                return Problem("Entity set 'Context.hoteles'  is null.");
            }
            var hotel = await _context.hoteles.FindAsync(id);
            if (hotel != null)
            {
                _context.hoteles.Remove(hotel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
            return _context.hoteles.Any(e => e.id == id);
        }
    }
}
