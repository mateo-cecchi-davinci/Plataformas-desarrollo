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

            _context.vuelos
                .Include(vuelo => vuelo.misReservas)
                    .ThenInclude(reserva_vuelo => reserva_vuelo.miUsuario)
                    .ThenInclude(usuario => usuario.misReservasVuelos)
                    .ThenInclude(reserva_vuelo => reserva_vuelo.miVuelo)
                    .ThenInclude(vuelo => vuelo.misReservas)
                    .ThenInclude(reserva_vuelo => reserva_vuelo.miUsuario)
                    .ThenInclude(usuario => usuario.vuelosTomados)
                .Include(vuelo => vuelo.origen)
                    .ThenInclude(origen => origen.vuelos_origen)
                .Include(vuelo => vuelo.destino)
                    .ThenInclude(destino => destino.vuelos_destino)
                .Load();
        }

        public IActionResult Home(int pg = 1)
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

            var vuelos = _context.vuelos;

            const int pageSize = 10;

            if (pg < 1)
                pg = 1;

            int recsCount = vuelos.Count();

            var pager = new Paginador(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = vuelos.Skip(recSkip).Take(pageSize).ToList();

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;
            ViewBag.pager = pager;
            ViewData["vuelos"] = data;

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

            if (usuarioLogeado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var vuelos = _context.vuelos.ToList();

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

            int pg = 1;

            const int pageSize = 10;

            if (pg < 1)
                pg = 1;

            int recsCount = vuelos.Count();

            var pager = new Paginador(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = vuelos.Skip(recSkip).Take(pageSize).ToList();

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;
            ViewBag.people = people;
            ViewBag.pager = pager;
            ViewData["vuelos"] = data;

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

            var context = _context.vuelos;

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
            ViewBag.data = data;

            return View(data);
        }

        // GET: Vuelo/Details/5
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

            if (id == null || _context.vuelos == null)
            {
                return NotFound();
            }

            var vuelo = await _context.vuelos.FirstOrDefaultAsync(m => m.id == id);

            if (vuelo == null)
            {
                return NotFound();
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

            return View(vuelo);
        }

        // GET: Vuelo/Create
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

            ViewData["destino_fk"] = new SelectList(_context.ciudades, "id", "nombre");
            ViewData["origen_fk"] = new SelectList(_context.ciudades, "id", "nombre");
            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

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
                var origen = _context.ciudades.FirstOrDefault(c => c.id == vuelo.origen_fk);
                var destino = _context.ciudades.FirstOrDefault(c => c.id == vuelo.destino_fk);

                if (origen == null)
                {
                    Console.WriteLine("El origen ingresado no se encuentra disponible");
                    return RedirectToAction("Index");
                }

                if (destino == null)
                {
                    Console.WriteLine("El destino ingresado no se encuentra disponible");
                    return RedirectToAction("Index");
                }

                origen.vuelos_origen.Add(vuelo);
                destino.vuelos_destino.Add(vuelo);

                _context.ciudades.Update(origen);
                _context.ciudades.Update(destino);

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

            if (id == null || _context.vuelos == null)
            {
                return NotFound();
            }

            var vuelo = await _context.vuelos.FindAsync(id);

            if (vuelo == null)
            {
                return NotFound();
            }

            ViewData["origen_fk"] = new SelectList(_context.ciudades, "id", "nombre", vuelo.origen_fk);
            ViewData["destino_fk"] = new SelectList(_context.ciudades, "id", "nombre", vuelo.destino_fk);
            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

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
                    var vuelo_seleccionado = _context.vuelos.FirstOrDefault(v => v.id == vuelo.id);

                    if (vuelo.capacidad < vuelo.vendido)
                    {
                        Console.WriteLine("No se puede realizar la modificacion debido a que la capacidad ingresada no es suficiente para satisfacer la cantidad de pasajeros que reservaron");
                        return RedirectToAction("Index");
                    }

                    if (vuelo_seleccionado.origen.id != vuelo.origen_fk)
                    {
                        var origen_nuevo = _context.ciudades.FirstOrDefault(c => c.id == vuelo.origen_fk);

                        vuelo_seleccionado.origen.vuelos_origen.Remove(vuelo_seleccionado);
                        origen_nuevo.vuelos_origen.Add(vuelo_seleccionado);

                        _context.ciudades.Update(vuelo_seleccionado.origen);
                        _context.ciudades.Update(origen_nuevo);

                        vuelo_seleccionado.origen_fk = vuelo.origen_fk;
                        vuelo_seleccionado.origen = origen_nuevo;
                    }

                    if (vuelo_seleccionado.destino.id != vuelo.destino_fk)
                    {
                        var destino_nuevo = _context.ciudades.FirstOrDefault(c => c.id == vuelo.destino_fk);

                        vuelo_seleccionado.destino.vuelos_destino.Remove(vuelo_seleccionado);
                        destino_nuevo.vuelos_destino.Add(vuelo_seleccionado);

                        _context.ciudades.Update(vuelo_seleccionado.destino);
                        _context.ciudades.Update(destino_nuevo);

                        vuelo_seleccionado.destino_fk = vuelo.destino_fk;
                        vuelo_seleccionado.destino = destino_nuevo;
                    }

                    vuelo_seleccionado.capacidad = vuelo.capacidad;
                    vuelo_seleccionado.vendido = vuelo.vendido;
                    vuelo_seleccionado.costo = vuelo.costo;
                    vuelo_seleccionado.fecha = vuelo.fecha;
                    vuelo_seleccionado.aerolinea = vuelo.aerolinea;
                    vuelo_seleccionado.avion = vuelo.avion;

                    //_context.Update(vuelo); <--- esto esta roto
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

            if (id == null || _context.vuelos == null)
            {
                return NotFound();
            }

            var vuelo = await _context.vuelos.FirstOrDefaultAsync(m => m.id == id);

            if (vuelo == null)
            {
                return NotFound();
            }

            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;

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

            var vuelo = await _context.vuelos.FirstOrDefaultAsync(vuelo => vuelo.id == id);

            if (vuelo != null)
            {
                vuelo.misReservas.ForEach(reserva_vuelo =>
                {
                    if (DateTime.Now < vuelo.fecha)
                    {
                        reserva_vuelo.miUsuario.credito += reserva_vuelo.pagado;
                        reserva_vuelo.miUsuario.misReservasVuelos.Remove(reserva_vuelo);

                        var usuario_vuelo = _context.usuarioVuelo
                            .FirstOrDefault(usuario_vuelo => usuario_vuelo.usuario == reserva_vuelo.miUsuario && usuario_vuelo.vuelo == reserva_vuelo.miVuelo);

                        if (usuario_vuelo != null)
                        {
                            _context.usuarioVuelo.Remove(usuario_vuelo);
                        }

                        reserva_vuelo.miUsuario.vuelosTomados.Remove(vuelo);

                        _context.usuarios.Update(reserva_vuelo.miUsuario);
                        _context.reservasVuelo.Remove(reserva_vuelo);
                    }
                });

                vuelo.origen.vuelos_origen.Remove(vuelo);
                vuelo.destino.vuelos_origen.Remove(vuelo);

                _context.ciudades.Update(vuelo.origen);
                _context.ciudades.Update(vuelo.destino);
                _context.vuelos.Remove(vuelo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult<double> ObtenerCostoVuelo(int id)
        {
            var costoVuelo = _context.vuelos.FirstOrDefault(v => v.id == id)?.costo;

            if (costoVuelo != null)
            {
                return costoVuelo;
            }

            return NotFound();
        }

        private bool VueloExists(int id)
        {
            return _context.vuelos.Any(e => e.id == id);
        }
    }
}
