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
