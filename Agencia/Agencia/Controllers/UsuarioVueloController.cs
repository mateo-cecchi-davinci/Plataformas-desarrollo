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
    public class UsuarioVueloController : Controller
    {
        private readonly Context _context;

        public UsuarioVueloController(Context context)
        {
            _context = context;
        }

        // GET: UsuarioVuelo
        public async Task<IActionResult> Index()
        {
            var context = _context.usuarioVuelo.Include(u => u.usuario).Include(u => u.vuelo);
            return View(await context.ToListAsync());
        }

        // GET: UsuarioVuelo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.usuarioVuelo == null)
            {
                return NotFound();
            }

            var usuarioVuelo = await _context.usuarioVuelo
                .Include(u => u.usuario)
                .Include(u => u.vuelo)
                .FirstOrDefaultAsync(m => m.usuario_fk == id);
            if (usuarioVuelo == null)
            {
                return NotFound();
            }

            return View(usuarioVuelo);
        }

        // GET: UsuarioVuelo/Create
        public IActionResult Create()
        {
            ViewData["usuario_fk"] = new SelectList(_context.usuarios, "id", "apellido");
            ViewData["vuelo_fk"] = new SelectList(_context.vuelos, "id", "aerolinea");
            return View();
        }

        // POST: UsuarioVuelo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("usuario_fk,vuelo_fk")] UsuarioVuelo usuarioVuelo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioVuelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["usuario_fk"] = new SelectList(_context.usuarios, "id", "apellido", usuarioVuelo.usuario_fk);
            ViewData["vuelo_fk"] = new SelectList(_context.vuelos, "id", "aerolinea", usuarioVuelo.vuelo_fk);
            return View(usuarioVuelo);
        }

        // GET: UsuarioVuelo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.usuarioVuelo == null)
            {
                return NotFound();
            }

            var usuarioVuelo = await _context.usuarioVuelo.FindAsync(id);
            if (usuarioVuelo == null)
            {
                return NotFound();
            }
            ViewData["usuario_fk"] = new SelectList(_context.usuarios, "id", "apellido", usuarioVuelo.usuario_fk);
            ViewData["vuelo_fk"] = new SelectList(_context.vuelos, "id", "aerolinea", usuarioVuelo.vuelo_fk);
            return View(usuarioVuelo);
        }

        // POST: UsuarioVuelo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("usuario_fk,vuelo_fk")] UsuarioVuelo usuarioVuelo)
        {
            if (id != usuarioVuelo.usuario_fk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioVuelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioVueloExists(usuarioVuelo.usuario_fk))
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
            ViewData["usuario_fk"] = new SelectList(_context.usuarios, "id", "apellido", usuarioVuelo.usuario_fk);
            ViewData["vuelo_fk"] = new SelectList(_context.vuelos, "id", "aerolinea", usuarioVuelo.vuelo_fk);
            return View(usuarioVuelo);
        }

        // GET: UsuarioVuelo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.usuarioVuelo == null)
            {
                return NotFound();
            }

            var usuarioVuelo = await _context.usuarioVuelo
                .Include(u => u.usuario)
                .Include(u => u.vuelo)
                .FirstOrDefaultAsync(m => m.usuario_fk == id);
            if (usuarioVuelo == null)
            {
                return NotFound();
            }

            return View(usuarioVuelo);
        }

        // POST: UsuarioVuelo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usuarioVuelo == null)
            {
                return Problem("Entity set 'Context.usuarioVuelo'  is null.");
            }
            var usuarioVuelo = await _context.usuarioVuelo.FindAsync(id);
            if (usuarioVuelo != null)
            {
                _context.usuarioVuelo.Remove(usuarioVuelo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioVueloExists(int id)
        {
          return _context.usuarioVuelo.Any(e => e.usuario_fk == id);
        }
    }
}
