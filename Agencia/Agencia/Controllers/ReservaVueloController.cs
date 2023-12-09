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
            ViewBag.usuarioMail = usuarioMail;
            ViewBag.usuarioLogeado = usuarioLogeado;
            ViewBag.isAdmin = isAdmin;
            var context = _context.reservasVuelo.Include(r => r.miUsuario).Include(r => r.miVuelo);
            return View(await context.ToListAsync());
        }

        // GET: ReservaVuelo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.reservasVuelo == null)
            {
                return NotFound();
            }

            var reservaVuelo = await _context.reservasVuelo
                .Include(r => r.miUsuario)
                .Include(r => r.miVuelo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (reservaVuelo == null)
            {
                return NotFound();
            }

            return View(reservaVuelo);
        }

        // GET: ReservaVuelo/Create
        public IActionResult Create()
        {
            ViewData["usuarioRV_fk"] = new SelectList(_context.usuarios, "id", "apellido");
            ViewData["vuelo_fk"] = new SelectList(_context.vuelos, "id", "aerolinea");
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
            if (id == null || _context.reservasVuelo == null)
            {
                return NotFound();
            }

            var reservaVuelo = await _context.reservasVuelo.FindAsync(id);
            if (reservaVuelo == null)
            {
                return NotFound();
            }
            ViewData["usuarioRV_fk"] = new SelectList(_context.usuarios, "id", "apellido", reservaVuelo.usuarioRV_fk);
            ViewData["vuelo_fk"] = new SelectList(_context.vuelos, "id", "aerolinea", reservaVuelo.vuelo_fk);
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
                    _context.Update(reservaVuelo);
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
            if (id == null || _context.reservasVuelo == null)
            {
                return NotFound();
            }

            var reservaVuelo = await _context.reservasVuelo
                .Include(r => r.miUsuario)
                .Include(r => r.miVuelo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (reservaVuelo == null)
            {
                return NotFound();
            }

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
            var reservaVuelo = await _context.reservasVuelo.FindAsync(id);
            if (reservaVuelo != null)
            {
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
