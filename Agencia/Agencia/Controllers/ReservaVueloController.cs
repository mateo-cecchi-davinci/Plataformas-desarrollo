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
    public class ReservaVueloController : Controller
    {
        private readonly Context _context;

        public ReservaVueloController(Context context)
        {
            _context = context;

            _context.reservasVuelo
                .Include(r => r.miUsuario)
                .Include(r => r.miVuelo)
                .Load();

            _context.usuarios
                .Include(u => u.misReservasVuelos)
                .Include(u => u.vuelosTomados)
                .Load();

            _context.vuelos
                .Include(v => v.misReservas)
                .Include(v => v.pasajeros)
                .Include(v => v.origen)
                .Include(v => v.destino)
                .Load();
        }

        // GET: ReservaVuelo
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

            var context = _context.reservasVuelo;

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(await context.ToListAsync());
        }

        // GET: ReservaVuelo/Details/5
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

            if (id == null || _context.reservasVuelo == null)
            {
                return NotFound();
            }

            var reservaVuelo = await _context.reservasVuelo.FirstOrDefaultAsync(m => m.id == id);

            if (reservaVuelo == null)
            {
                return NotFound();
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(reservaVuelo);
        }

        // GET: ReservaVuelo/Create
        public async Task<IActionResult> CreateAsync()
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

            var vuelos = await _context.vuelos.ToListAsync();

            var vuelos_con_origen_y_destino = vuelos.Select(v => new SelectListItem
            {
                Value = v.id.ToString(),
                Text = $"{v.origen.nombre} - {v.destino.nombre}"
            }).ToList();

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            ViewData["usuarioRV_fk"] = new SelectList(_context.usuarios.Select(u => new { u.id, nombre = u.nombre + " " + u.apellido }), "id", "nombre");
            ViewData["vuelo_fk"] = new SelectList(vuelos_con_origen_y_destino, "Value", "Text");

            return View();
        }

        // POST: ReservaVuelo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,pagado,cantPersonas,vuelo_fk,usuarioRV_fk")] ReservaVuelo reservaVuelo)
        {
            if (ModelState.IsValid)
            {
                var usuario_seleccionado = _context.usuarios.FirstOrDefault(u => u.id == reservaVuelo.usuarioRV_fk);

                var vuelo_seleccionado = _context.vuelos.FirstOrDefault(v => v.id == reservaVuelo.vuelo_fk);

                if (usuario_seleccionado == null)
                {
                    Console.WriteLine("Usuario no encontrado");
                    return RedirectToAction("Index");
                }

                if (vuelo_seleccionado == null)
                {
                    Console.WriteLine("Vuelo no encontrado");
                    return RedirectToAction("Index");
                }

                if (_context.reservasVuelo.Any(r => r.miUsuario == usuario_seleccionado && r.miVuelo == vuelo_seleccionado))
                {
                    Console.WriteLine("Ya reservó ese vuelo");
                    return RedirectToAction("Index");
                }

                double pago = reservaVuelo.cantPersonas * vuelo_seleccionado.costo;

                if (vuelo_seleccionado.vendido + reservaVuelo.cantPersonas < 0)
                {
                    Console.WriteLine("La cantidad de personas ingresada excede la capacidad del vuelo");
                    return RedirectToAction("Index");
                }

                if (pago > usuario_seleccionado.credito)
                {
                    Console.WriteLine("Crédito insuficiente");
                    return RedirectToAction("Index");
                }

                var usuario_vuelo = _context.usuarioVuelo.FirstOrDefault(uv => uv.usuario == usuario_seleccionado && uv.vuelo == vuelo_seleccionado);

                if (usuario_vuelo == null)
                {
                    usuario_vuelo = new UsuarioVuelo(usuario_seleccionado.id, vuelo_seleccionado.id);
                    _context.usuarioVuelo.Add(usuario_vuelo);
                }

                usuario_seleccionado.credito -= pago;
                usuario_seleccionado.misReservasVuelos.Add(reservaVuelo);
                vuelo_seleccionado.vendido += reservaVuelo.cantPersonas;
                vuelo_seleccionado.misReservas.Add(reservaVuelo);

                reservaVuelo.pagado = pago;

                _context.usuarios.Update(usuario_seleccionado);
                _context.vuelos.Update(vuelo_seleccionado);
                _context.Add(reservaVuelo);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["usuarioRV_fk"] = new SelectList(_context.usuarios, "id", "apellido", reservaVuelo.usuarioRV_fk);
            ViewData["vuelo_fk"] = new SelectList(_context.vuelos, "id", "aerolinea", reservaVuelo.vuelo_fk);

            return View(reservaVuelo);
        }

        // GET: ReservaVuelo/Edit/5
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

            if (id == null || _context.reservasVuelo == null)
            {
                return NotFound();
            }

            var reservaVuelo = await _context.reservasVuelo.FindAsync(id);

            if (reservaVuelo == null)
            {
                return NotFound();
            }

            var vuelos = await _context.vuelos.ToListAsync();

            var vuelos_con_origen_y_destino = vuelos.Select(v => new SelectListItem
            {
                Value = v.id.ToString(),
                Text = $"{v.origen.nombre} - {v.destino.nombre}"
            }).ToList();

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            ViewData["usuarioRV_fk"] = new SelectList(_context.usuarios.Select(u => new { u.id, nombre = u.nombre + " " + u.apellido }), "id", "nombre");
            ViewData["vuelo_fk"] = new SelectList(vuelos_con_origen_y_destino, "Value", "Text");

            return View(reservaVuelo);
        }

        // POST: ReservaVuelo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,pagado,cantPersonas,vuelo_fk,usuarioRV_fk")] ReservaVuelo reservaVuelo)
        {
            if (id != reservaVuelo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuario_seleccionado = _context.usuarios.FirstOrDefault(u => u.id == reservaVuelo.usuarioRV_fk);
                    var vuelo_seleccionado = _context.vuelos.FirstOrDefault(v => v.id == reservaVuelo.vuelo_fk);
                    var reserva_seleccionada = _context.reservasVuelo.FirstOrDefault(r => r.id == reservaVuelo.id);

                    if (usuario_seleccionado == null)
                    {
                        Console.WriteLine("Usuario no encontrado");
                        return RedirectToAction("Index");
                    }

                    if (vuelo_seleccionado == null)
                    {
                        Console.WriteLine("Vuelo no encontrado");
                        return RedirectToAction("Index");
                    }

                    if (reserva_seleccionada == null)
                    {
                        Console.WriteLine("Reserva no encontrada");
                        return RedirectToAction("Index");
                    }

                    if (_context.reservasVuelo.Any(r => r.miUsuario == usuario_seleccionado && r.miVuelo == vuelo_seleccionado && r.id != reservaVuelo.id))
                    {
                        Console.WriteLine("Ya reservó ese vuelo");
                        return RedirectToAction("Index");
                    }

                    if ((vuelo_seleccionado.vendido - reserva_seleccionada.cantPersonas) + reservaVuelo.cantPersonas > vuelo_seleccionado.capacidad)
                    {
                        Console.WriteLine("La cantidad de personas ingresada excede la capacidad del vuelo");
                        return RedirectToAction("Index");
                    }

                    double costo = reservaVuelo.cantPersonas * vuelo_seleccionado.costo;

                    if (usuario_seleccionado.credito < costo)
                    {
                        Console.WriteLine("Crédito insuficiente");
                        return RedirectToAction("Index");
                    }

                    var usuario_vuelo = _context.usuarioVuelo
                        .FirstOrDefault(uv => uv.usuario == usuario_seleccionado && uv.vuelo == vuelo_seleccionado);

                    if (usuario_vuelo == null)
                    {
                        usuario_vuelo = new UsuarioVuelo(usuario_seleccionado.id, vuelo_seleccionado.id);
                        _context.usuarioVuelo.Add(usuario_vuelo);
                    }

                    if (reserva_seleccionada.miUsuario.id != reservaVuelo.usuarioRV_fk)
                    {
                        if (DateTime.Now < reserva_seleccionada.miVuelo.fecha)
                        {
                            reserva_seleccionada.miUsuario.credito += reserva_seleccionada.pagado;
                        }

                        reserva_seleccionada.miUsuario.misReservasVuelos.Remove(reserva_seleccionada);
                        usuario_seleccionado.misReservasVuelos.Add(reserva_seleccionada);

                        _context.usuarios.Update(reserva_seleccionada.miUsuario);
                        
                        reserva_seleccionada.usuarioRV_fk = reservaVuelo.usuarioRV_fk;
                        reserva_seleccionada.miUsuario = usuario_seleccionado;
                    }

                    if (reserva_seleccionada.miVuelo.id != reservaVuelo.vuelo_fk)
                    {
                        reserva_seleccionada.miVuelo.vendido -= reserva_seleccionada.cantPersonas;

                        reserva_seleccionada.miVuelo.misReservas.Remove(reserva_seleccionada);
                        vuelo_seleccionado.misReservas.Add(reserva_seleccionada);

                        _context.vuelos.Update(reserva_seleccionada.miVuelo);

                        reserva_seleccionada.vuelo_fk = reservaVuelo.vuelo_fk;
                        reserva_seleccionada.miVuelo = vuelo_seleccionado;
                    }

                    usuario_seleccionado.credito += reserva_seleccionada.pagado;
                    usuario_seleccionado.credito -= costo;

                    if (reserva_seleccionada.cantPersonas != reservaVuelo.cantPersonas)
                    {
                        vuelo_seleccionado.vendido -= reserva_seleccionada.cantPersonas;
                        vuelo_seleccionado.vendido += reservaVuelo.cantPersonas;
                    }

                    reserva_seleccionada.pagado = costo;
                    reserva_seleccionada.cantPersonas = reservaVuelo.cantPersonas;

                    _context.usuarios.Update(usuario_seleccionado);
                    _context.vuelos.Update(vuelo_seleccionado);
                    
                    //_context.Update(reservaVuelo); <--- esto esta roto
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaVueloExists(reservaVuelo.id))
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

            ViewData["usuarioRV_fk"] = new SelectList(_context.usuarios, "id", "apellido", reservaVuelo.usuarioRV_fk);
            ViewData["vuelo_fk"] = new SelectList(_context.vuelos, "id", "aerolinea", reservaVuelo.vuelo_fk);

            return View(reservaVuelo);
        }

        // GET: ReservaVuelo/Delete/5
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

            if (id == null || _context.reservasVuelo == null)
            {
                return NotFound();
            }

            var reservaVuelo = await _context.reservasVuelo.FirstOrDefaultAsync(m => m.id == id);

            if (reservaVuelo == null)
            {
                return NotFound();
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(reservaVuelo);
        }

        // POST: ReservaVuelo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.reservasVuelo == null)
            {
                return Problem("Entity set 'Context.reservasVuelo'  is null.");
            }

            var reservaVuelo = await _context.reservasVuelo.FirstOrDefaultAsync(reserva_vuelo => reserva_vuelo.id == id);

            if (reservaVuelo != null)
            {
                if (DateTime.Now < reservaVuelo.miVuelo.fecha)
                {
                    reservaVuelo.miUsuario.credito += reservaVuelo.pagado;

                    var usuario_vuelo = _context.usuarioVuelo
                        .FirstOrDefault(usuario_vuelo => usuario_vuelo.usuario == reservaVuelo.miUsuario && usuario_vuelo.vuelo == reservaVuelo.miVuelo);

                    if (usuario_vuelo != null)
                    {
                        _context.usuarioVuelo.Remove(usuario_vuelo);
                    }

                    reservaVuelo.miUsuario.misReservasVuelos.Remove(reservaVuelo);
                    reservaVuelo.miVuelo.misReservas.Remove(reservaVuelo);

                    _context.usuarios.Update(reservaVuelo.miUsuario);
                    _context.vuelos.Update(reservaVuelo.miVuelo);
                }

                _context.reservasVuelo.Remove(reservaVuelo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaVueloExists(int id)
        {
            return _context.reservasVuelo.Any(e => e.id == id);
        }
    }
}
