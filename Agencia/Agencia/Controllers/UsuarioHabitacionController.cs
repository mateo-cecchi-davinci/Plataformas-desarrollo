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
    public class UsuarioHabitacionController : Controller
    {
        private readonly Context _context;

        public UsuarioHabitacionController(Context context)
        {
            _context = context;
        }

        // GET: UsuarioHabitacions
        public async Task<IActionResult> Index()
        {
            var context = _context.usuarioHabitacion.Include(u => u.habitacion).Include(u => u.usuario);
            return View(await context.ToListAsync());
        }

        // GET: UsuarioHabitacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.usuarioHabitacion == null)
            {
                return NotFound();
            }

            var usuarioHabitacion = await _context.usuarioHabitacion
                .Include(u => u.habitacion)
                .Include(u => u.usuario)
                .FirstOrDefaultAsync(m => m.usuarios_fk == id);
            if (usuarioHabitacion == null)
            {
                return NotFound();
            }

            return View(usuarioHabitacion);
        }

        // GET: UsuarioHabitacions/Create
        public IActionResult Create()
        {
            ViewData["habitaciones_fk"] = new SelectList(_context.Habitacion, "id", "id");
            ViewData["usuarios_fk"] = new SelectList(_context.usuarios, "id", "apellido");
            return View();
        }

        // POST: UsuarioHabitacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("usuarios_fk,habitaciones_fk,cantidad")] UsuarioHabitacion usuarioHabitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioHabitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["habitaciones_fk"] = new SelectList(_context.Habitacion, "id", "id", usuarioHabitacion.habitaciones_fk);
            ViewData["usuarios_fk"] = new SelectList(_context.usuarios, "id", "apellido", usuarioHabitacion.usuarios_fk);
            return View(usuarioHabitacion);
        }

        // GET: UsuarioHabitacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.usuarioHabitacion == null)
            {
                return NotFound();
            }

            var usuarioHabitacion = await _context.usuarioHabitacion.FindAsync(id);
            if (usuarioHabitacion == null)
            {
                return NotFound();
            }
            ViewData["habitaciones_fk"] = new SelectList(_context.Habitacion, "id", "id", usuarioHabitacion.habitaciones_fk);
            ViewData["usuarios_fk"] = new SelectList(_context.usuarios, "id", "apellido", usuarioHabitacion.usuarios_fk);
            return View(usuarioHabitacion);
        }

        // POST: UsuarioHabitacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("usuarios_fk,habitaciones_fk,cantidad")] UsuarioHabitacion usuarioHabitacion)
        {
            if (id != usuarioHabitacion.usuarios_fk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioHabitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioHabitacionExists(usuarioHabitacion.usuarios_fk))
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
            ViewData["habitaciones_fk"] = new SelectList(_context.Habitacion, "id", "id", usuarioHabitacion.habitaciones_fk);
            ViewData["usuarios_fk"] = new SelectList(_context.usuarios, "id", "apellido", usuarioHabitacion.usuarios_fk);
            return View(usuarioHabitacion);
        }

        // GET: UsuarioHabitacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.usuarioHabitacion == null)
            {
                return NotFound();
            }

            var usuarioHabitacion = await _context.usuarioHabitacion
                .Include(u => u.habitacion)
                .Include(u => u.usuario)
                .FirstOrDefaultAsync(m => m.usuarios_fk == id);
            if (usuarioHabitacion == null)
            {
                return NotFound();
            }

            return View(usuarioHabitacion);
        }

        // POST: UsuarioHabitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usuarioHabitacion == null)
            {
                return Problem("Entity set 'Context.usuarioHabitacion'  is null.");
            }
            var usuarioHabitacion = await _context.usuarioHabitacion.FindAsync(id);
            if (usuarioHabitacion != null)
            {
                _context.usuarioHabitacion.Remove(usuarioHabitacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioHabitacionExists(int id)
        {
          return _context.usuarioHabitacion.Any(e => e.usuarios_fk == id);
        }
    }
}
