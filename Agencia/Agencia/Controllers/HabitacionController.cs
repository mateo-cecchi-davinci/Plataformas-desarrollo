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
    public class HabitacionController : Controller
    {
        private readonly Context _context;

        public HabitacionController(Context context)
        {
            _context = context;

            _context.habitaciones
                .Include(h => h.hotel)
                .Include(h => h.misReservas)
                .Include(h => h.usuarios)
                    .ThenInclude(u => u.habitacionesUsadas)
                .Include(h => h.habitacion_usuario)
                .Load();
        }

        // GET: Habitacion
        public async Task<IActionResult> Index(int pg = 1)
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

            var context = _context.habitaciones;

            const int pageSize = 10;

            if (pg < 1)
                pg = 1;

            int recsCount = context.Count();

            var pager = new Paginador(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = await context.Skip(recSkip).Take(pageSize).ToListAsync();

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;
            ViewBag.pager = pager;

            return View(data);
        }

        // GET: Habitacion/Details/5
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

            if (id == null || _context.habitaciones == null)
            {
                return NotFound();
            }

            var habitacion = await _context.habitaciones.FirstOrDefaultAsync(m => m.id == id);

            if (habitacion == null)
            {
                return NotFound();
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(habitacion);
        }

        // GET: Habitacion/Create
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

            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre");
            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View();
        }

        // POST: Habitacion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,capacidad,costo,hotel_fk")] Habitacion habitacion, int cantidad)
        {
            if (ModelState.IsValid)
            {
                if (habitacion.capacidad != 2 && habitacion.capacidad != 4 && habitacion.capacidad != 8)
                {
                    Console.WriteLine("Capacidad inválida");
                    return RedirectToAction("Index");
                }

                var hotel_seleccionado = _context.hoteles.FirstOrDefault(h => h.id == habitacion.hotel_fk);

                if (hotel_seleccionado == null)
                {
                    Console.WriteLine("Hotel no encontrado");
                    return RedirectToAction("Index");
                }

                for (int i = 0; i < cantidad; i++)
                {
                    var nuevaHabitacion = new Habitacion
                    {
                        capacidad = habitacion.capacidad,
                        costo = habitacion.costo,
                        hotel_fk = habitacion.hotel_fk
                    };

                    hotel_seleccionado.habitaciones.Add(nuevaHabitacion);

                    _context.hoteles.Update(hotel_seleccionado);
                    _context.Add(nuevaHabitacion);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre", habitacion.hotel_fk);

            return View(habitacion);
        }

        // GET: Habitacion/Edit/5
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

            if (id == null || _context.habitaciones == null)
            {
                return NotFound();
            }

            var habitacion = await _context.habitaciones.FindAsync(id);

            if (habitacion == null)
            {
                return NotFound();
            }

            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre", habitacion.hotel_fk);
            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(habitacion);
        }

        // POST: Habitacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,capacidad,costo,hotel_fk")] Habitacion habitacion)
        {
            if (id != habitacion.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var habitacion_modificada = await _context.habitaciones.FirstOrDefaultAsync(h => h.id == habitacion.id);

                    if (habitacion_modificada == null)
                    {
                        Console.WriteLine("Habitación inválida");
                        return NotFound();
                    }

                    if (habitacion_modificada.capacidad < habitacion.capacidad)
                    {
                        var error = habitacion_modificada.misReservas.Any(r => DateTime.Now < r.fechaDesde);

                        if (error)
                        {
                            Console.WriteLine("No se puede realizar la modificación porque la capacidad ingresada es menor a la anterior y la habitación tiene reservas a futuro que pueden causar problemas con la disponibilidad.");
                            return RedirectToAction("Index");
                        }
                    }

                    if (habitacion.capacidad != 2 && habitacion.capacidad != 4 && habitacion.capacidad != 8)
                    {
                        Console.WriteLine("Capacidad inválida");
                        return RedirectToAction("Index");
                    }

                    if (habitacion_modificada.hotel_fk != habitacion.hotel_fk)
                    {
                        var hotel_nuevo = await _context.hoteles.FirstOrDefaultAsync(h => h.id == habitacion.hotel_fk);

                        habitacion_modificada.hotel.habitaciones.Remove(habitacion_modificada);
                        hotel_nuevo.habitaciones.Add(habitacion_modificada);

                        _context.hoteles.Update(habitacion_modificada.hotel);
                        _context.hoteles.Update(hotel_nuevo);

                        habitacion_modificada.hotel_fk = habitacion.hotel_fk;
                        habitacion_modificada.hotel = hotel_nuevo;
                    }

                    habitacion_modificada.capacidad = habitacion.capacidad;
                    habitacion_modificada.costo = habitacion.costo;

                    //_context.Update(habitacion_modificada); <--- esto esta roto
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitacionExists(habitacion.id))
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

            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre", habitacion.hotel_fk);

            return View(habitacion);
        }


        // GET: Habitacion/Delete/5
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

            if (id == null || _context.habitaciones == null)
            {
                return NotFound();
            }

            var habitacion = await _context.habitaciones.FirstOrDefaultAsync(m => m.id == id);

            if (habitacion == null)
            {
                return NotFound();
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(habitacion);
        }

        // POST: Habitacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.habitaciones == null)
            {
                return Problem("Entity set 'Context.Habitacion'  is null.");
            }

            var habitacion = await _context.habitaciones.FirstOrDefaultAsync(habitacion => habitacion.id == id);

            if (habitacion != null)
            {
                habitacion.misReservas.ForEach(reserva_habitacion =>
                {
                    if (DateTime.Now < reserva_habitacion.fechaDesde)
                    {
                        reserva_habitacion.miUsuario.credito += reserva_habitacion.pagado;

                        var usuario_habitacion = _context.usuarioHabitacion
                            .FirstOrDefault(usuario_habitacion => 
                                usuario_habitacion.usuario == reserva_habitacion.miUsuario && 
                                usuario_habitacion.habitacion == reserva_habitacion.miHabitacion);

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

                        reserva_habitacion.miUsuario.misReservasHabitaciones.Remove(reserva_habitacion);
                        reserva_habitacion.miUsuario.habitacionesUsadas.Remove(habitacion);

                        _context.usuarios.Update(reserva_habitacion.miUsuario);
                        _context.reservasHabitacion.Remove(reserva_habitacion);
                    }
                });

                habitacion.hotel.habitaciones.Remove(habitacion);

                _context.hoteles.Update(habitacion.hotel);
                _context.habitaciones.Remove(habitacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitacionExists(int id)
        {
            return _context.habitaciones.Any(e => e.id == id);
        }
    }
}
