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

            _context.hoteles
                .Include(h => h.habitaciones)
                    .ThenInclude(habitacion => habitacion.misReservas)
                    .ThenInclude(reserva => reserva.miUsuario)
                    .ThenInclude(usuario => usuario.misReservasHabitaciones)
                    .ThenInclude(r => r.miHabitacion)
                    .ThenInclude(habitacion => habitacion.misReservas)
                    .ThenInclude(reserva => reserva.miUsuario)
                    .ThenInclude(usuario => usuario.habitacionesUsadas)
                .Include(h => h.ubicacion)
                    .ThenInclude(c => c.hoteles)
                .Load();
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

            if (usuarioLogeado == null)
            {
                return RedirectToAction("Index", "Login");
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

            if (usuarioLogeado == null)
            {
                return RedirectToAction("Index", "Login");
            }

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

            var hoteles = _context.hoteles
                .Where(hotel => 
                    hotel.ubicacion.nombre == destination &&
                    hotel.habitaciones.Count(hab => hab.capacidad == 2) >= habitaciones_chicas &&
                    hotel.habitaciones.Count(hab => hab.capacidad == 4) >= habitaciones_medianas &&
                    hotel.habitaciones.Count(hab => hab.capacidad == 8) >= habitaciones_grandes)
                .ToList();

            if (hoteles == null || hoteles.Count == 0)
            {
                string error_message = "No hubo resultados";
                TempData["error"] = error_message;
                return RedirectToAction(nameof(Home));
            }

            var costo_habitaciones_chicas = _context.habitaciones
                .ToList()
                .Where(habitacion => hoteles.Any(h => h.id == habitacion.hotel_fk) && habitacion.capacidad == 2)
                .GroupBy(habitacion => habitacion.hotel_fk)
                .Select(group => group.First())
                .ToList();

            var costos_chicas = costo_habitaciones_chicas.Select(habitacion => habitacion.costo * habitaciones_chicas).ToList();

            var costo_habitaciones_medianas = _context.habitaciones
                .ToList()
                .Where(habitacion => hoteles.Any(h => h.id == habitacion.hotel_fk) && habitacion.capacidad == 4)
                .GroupBy(habitacion => habitacion.hotel_fk)
                .Select(group => group.First())
                .ToList();

            var costos_medianas = costo_habitaciones_medianas.Select(habitacion => habitacion.costo * habitaciones_medianas).ToList();

            var costo_habitaciones_grandes = _context.habitaciones
                .ToList()
                .Where(habitacion => hoteles.Any(h => h.id == habitacion.hotel_fk) && habitacion.capacidad == 8)
                .GroupBy(habitacion => habitacion.hotel_fk)
                .Select(group => group.First())
                .ToList();

            var costos_grandes = costo_habitaciones_grandes.Select(habitacion => habitacion.costo * habitaciones_grandes).ToList();

            var sumaCostosPorHotel = costos_chicas
                .Zip(costos_medianas, (chicas, medianas) => chicas + medianas)
                .Zip(costos_grandes, (subtotal, grandes) => subtotal + grandes)
                .ToList();

            var diccionarioCostosPorHotel = hoteles
                .Select((hotel, index) => new { hotel, costo = sumaCostosPorHotel[index] })
                .ToDictionary(x => x.hotel, x => x.costo);

            int total_adultos = adultos.Values.Sum();
            int total_menores = menores.Values.Sum();

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;
            ViewBag.hotelesSeleccionados = diccionarioCostosPorHotel;
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

                                var usuario_habitacion = _context.usuarioHabitacion
                                    .FirstOrDefault(uh => uh.usuarios_fk == usuario.id && uh.habitaciones_fk == hab.id);

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

                                var usuario_habitacion = _context.usuarioHabitacion
                                    .FirstOrDefault(uh => uh.usuarios_fk == usuario.id && uh.habitaciones_fk == hab.id);

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

                                var usuario_habitacion = _context.usuarioHabitacion
                                    .FirstOrDefault(uh => uh.usuarios_fk == usuario.id && uh.habitaciones_fk == hab.id);

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

                            string mensaje = "La compra se ha realizado con éxito";
                            TempData["compraExitosa"] = mensaje;

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

            if (usuarioLogeado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var context = _context.hoteles.Include(h => h.ubicacion);
            
            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;
            
            return View(await context.ToListAsync());
        }

        // GET: Hotels/Details/5
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

            if (id == null || _context.hoteles == null)
            {
                return NotFound();
            }

            var hotel = await _context.hoteles.FirstOrDefaultAsync(m => m.id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(hotel);
        }

        // GET: Hotels/Create
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

            ViewData["ciudad_fk"] = new SelectList(_context.ciudades, "id", "nombre");
            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

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
                var ciudad_seleccionada = _context.ciudades.FirstOrDefault(c => c.id == hotel.ciudad_fk);

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

                ciudad_seleccionada.hoteles.Add(hotel);

                _context.Update(ciudad_seleccionada);
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
            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

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
                    var hotel_modificado = await _context.hoteles.FirstOrDefaultAsync(h => h.id == hotel.id);

                    if (hotel_modificado == null)
                    {
                        Console.WriteLine("Hotel inválido");
                        return NotFound();
                    }

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

                        hotel_modificado.imagen = rutaCompleta.Replace("wwwroot", "").TrimStart('\\', '/');
                    }

                    if (hotel_modificado.ubicacion.id != hotel.ciudad_fk)
                    {
                        var ubicacion_nueva = await _context.ciudades.FirstOrDefaultAsync(c => c.id == hotel.ciudad_fk);

                        hotel_modificado.ubicacion.hoteles.Remove(hotel_modificado);
                        ubicacion_nueva.hoteles.Add(hotel_modificado);

                        _context.ciudades.Update(hotel_modificado.ubicacion);
                        _context.ciudades.Update(ubicacion_nueva);

                        hotel_modificado.ciudad_fk = hotel.ciudad_fk;
                        hotel_modificado.ubicacion = ubicacion_nueva;
                    }

                    hotel_modificado.nombre = hotel.nombre;
                    hotel_modificado.descripcion = hotel.descripcion;

                    //_context.Update(hotel_modificado); <--- Esto esta roto
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

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

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

            var hotel = await _context.hoteles.FirstOrDefaultAsync(h => h.id == id);

            if (hotel != null)
            {
                hotel.habitaciones.ForEach(habitacion =>
                {
                    habitacion.misReservas.ForEach(reserva_habitacion =>
                    {
                        if (DateTime.Now < reserva_habitacion.fechaDesde)
                        {
                            var usuario_habitacion = _context.usuarioHabitacion
                                .FirstOrDefault(uh => uh.usuario == reserva_habitacion.miUsuario && uh.habitacion == reserva_habitacion.miHabitacion);

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

                            reserva_habitacion.miUsuario.credito += reserva_habitacion.pagado;
                            reserva_habitacion.miUsuario.misReservasHabitaciones.Remove(reserva_habitacion);
                            reserva_habitacion.miUsuario.habitacionesUsadas.Remove(habitacion);

                            _context.usuarios.Update(reserva_habitacion.miUsuario);
                            _context.reservasHabitacion.Remove(reserva_habitacion);
                        }
                    });

                    _context.habitaciones.Remove(habitacion);
                });

                hotel.ubicacion.hoteles.Remove(hotel);

                _context.ciudades.Update(hotel.ubicacion);
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
