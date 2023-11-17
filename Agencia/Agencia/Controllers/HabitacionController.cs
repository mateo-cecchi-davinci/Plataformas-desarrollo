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
        }

        // GET: Habitacion
        public async Task<IActionResult> Index()
        {
            var context = _context.Habitacion.Include(h => h.hotel);
            return View(await context.ToListAsync());
        }

        // GET: Habitacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Habitacion == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacion
                .Include(h => h.hotel)
                .FirstOrDefaultAsync(m => m.id == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // GET: Habitacion/Create
        public IActionResult Create()
        {
            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre");
            return View();
        }

        // POST: Habitacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,capacidad,costo,hotel_fk")] Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre", habitacion.hotel_fk);
            return View(habitacion);
        }

        // GET: Habitacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Habitacion == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacion.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre", habitacion.hotel_fk);
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
                    _context.Update(habitacion);
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
            if (id == null || _context.Habitacion == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacion
                .Include(h => h.hotel)
                .FirstOrDefaultAsync(m => m.id == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // POST: Habitacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Habitacion == null)
            {
                return Problem("Entity set 'Context.Habitacion'  is null.");
            }
            var habitacion = await _context.Habitacion.FindAsync(id);
            if (habitacion != null)
            {
                _context.Habitacion.Remove(habitacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitacionExists(int id)
        {
          return _context.Habitacion.Any(e => e.id == id);
        }
    }
}
