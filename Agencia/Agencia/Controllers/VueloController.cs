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
    public class VueloController : Controller
    {
        private readonly Context _context;

        public VueloController(Context context)
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

            var vuelos = _context.vuelos.Include(v => v.origen).Include(v => v.destino).ToList();

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;
            ViewData["vuelos"] = vuelos;

            return View();
        }

        public IActionResult ResultadosDeLaBusqueda(string origin, string destination, DateTime? start_date, int people)
        {
            string usuarioLogeado = HttpContext.Session.GetString("UsuarioLogeado");
            string esAdminString = HttpContext.Session.GetString("esAdmin");
            string usuarioMail = HttpContext.Session.GetString("userMail");
            bool isAdmin = false;

            if (!string.IsNullOrEmpty(esAdminString))
            {

                bool.TryParse(esAdminString, out isAdmin);
            }

            var vuelos = _context.vuelos.Include(v => v.origen).Include(v => v.destino).ToList();

            if (!string.IsNullOrEmpty(origin))
            {
                vuelos = vuelos.Where(v => v.origen.nombre.ToLower().Contains(origin.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(destination))
            {
                vuelos = vuelos.Where(v => v.destino.nombre.ToLower().Contains(destination.ToLower())).ToList();
            }

            if (start_date != null)
            {
                vuelos = vuelos.Where(v => v.fecha.Date == start_date.Value.Date).ToList();
            }

            vuelos = vuelos.Where(v => v.capacidad - v.vendido >= people).ToList();

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;
            ViewBag.people = people;
            ViewData["vuelos"] = vuelos;

            return View("Home");
        }

        [HttpPost]
        public IActionResult Reservar(int id, int people)
        {
            string usuarioMail = HttpContext.Session.GetString("userMail");

            var usuario = _context.usuarios.FirstOrDefault(u => u.mail == usuarioMail);

            if (usuario != null)
            {
                var vuelo = _context.vuelos.FirstOrDefault(v => v.id == id);

                if (vuelo != null)
                {
                    if (people == 0)
                    {
                        people = 1;
                    }

                    double pago = vuelo.costo * people;

                    if (pago <= usuario.credito)
                    {
                        var usuario_vuelo = _context.usuarioVuelo.FirstOrDefault(uv => uv.usuario_fk == usuario.id && uv.vuelo_fk == vuelo.id);

                        if (usuario_vuelo == null)
                        {
                            usuario_vuelo = new UsuarioVuelo(usuario.id, vuelo.id);

                            _context.usuarioVuelo.Add(usuario_vuelo);
                        }

                        vuelo.vendido += people;
                        usuario.credito -= pago;

                        var nuevaReserva = new ReservaVuelo(vuelo, usuario, pago, people, vuelo.id, usuario.id);

                        usuario.misReservasVuelos.Add(nuevaReserva);
                        vuelo.misReservas.Add(nuevaReserva);

                        _context.reservasVuelo.Add(nuevaReserva);
                        _context.usuarios.Update(usuario);
                        _context.vuelos.Update(vuelo);
                        _context.SaveChanges();

                        return Ok();
                    }
                }
            }

            return NotFound();
        }


            //------------CRUD------------


            // GET: Vuelo
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
            var context = _context.vuelos.Include(v => v.destino).Include(v => v.origen);
            return View(await context.ToListAsync());
        }

        // GET: Vuelo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.vuelos == null)
            {
                return NotFound();
            }

            var vuelo = await _context.vuelos
                .Include(v => v.destino)
                .Include(v => v.origen)
                .FirstOrDefaultAsync(m => m.id == id);
            if (vuelo == null)
            {
                return NotFound();
            }

            return View(vuelo);
        }

        // GET: Vuelo/Create
        public IActionResult Create()
        {
            ViewData["destino_fk"] = new SelectList(_context.ciudades, "id", "nombre");
            ViewData["origen_fk"] = new SelectList(_context.ciudades, "id", "nombre");
            return View();
        }

        // POST: Vuelo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,capacidad,vendido,costo,fecha,aerolinea,avion,origen_fk,destino_fk")] Vuelo vuelo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vuelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["destino_fk"] = new SelectList(_context.ciudades, "id", "nombre", vuelo.destino_fk);
            ViewData["origen_fk"] = new SelectList(_context.ciudades, "id", "nombre", vuelo.origen_fk);
            return View(vuelo);
        }

        // GET: Vuelo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.vuelos == null)
            {
                return NotFound();
            }

            var vuelo = await _context.vuelos.FindAsync(id);
            if (vuelo == null)
            {
                return NotFound();
            }
            ViewData["destino_fk"] = new SelectList(_context.ciudades, "id", "nombre", vuelo.destino_fk);
            ViewData["origen_fk"] = new SelectList(_context.ciudades, "id", "nombre", vuelo.origen_fk);
            return View(vuelo);
        }

        // POST: Vuelo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,capacidad,vendido,costo,fecha,aerolinea,avion,origen_fk,destino_fk")] Vuelo vuelo)
        {
            if (id != vuelo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vuelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VueloExists(vuelo.id))
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
            ViewData["destino_fk"] = new SelectList(_context.ciudades, "id", "nombre", vuelo.destino_fk);
            ViewData["origen_fk"] = new SelectList(_context.ciudades, "id", "nombre", vuelo.origen_fk);
            return View(vuelo);
        }

        // GET: Vuelo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.vuelos == null)
            {
                return NotFound();
            }

            var vuelo = await _context.vuelos
                .Include(v => v.destino)
                .Include(v => v.origen)
                .FirstOrDefaultAsync(m => m.id == id);
            if (vuelo == null)
            {
                return NotFound();
            }

            return View(vuelo);
        }

        // POST: Vuelo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.vuelos == null)
            {
                return Problem("Entity set 'Context.vuelos'  is null.");
            }
            var vuelo = await _context.vuelos.FindAsync(id);
            if (vuelo != null)
            {
                _context.vuelos.Remove(vuelo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VueloExists(int id)
        {
          return _context.vuelos.Any(e => e.id == id);
        }
    }
}
