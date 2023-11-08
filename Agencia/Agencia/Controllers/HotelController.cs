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
    public class HotelController : Controller
    {
        private readonly Context _context;

        public HotelController(Context context)
        {
            _context = context;
        }

        // GET: Hotel
        public async Task<IActionResult> Index()
        {
            var context = _context.hoteles.Include(h => h.ubicacion);
            return View(await context.ToListAsync());
        }

        // GET: Hotel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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

            return View(hotel);
        }

        // GET: Hotel/Create
        public IActionResult Create()
        {
            ViewData["ciudad_fk"] = new SelectList(_context.ciudades, "id", "nombre");
            return View();
        }

        // POST: Hotel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,capacidad,costo,nombre,ciudad_fk")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ciudad_fk"] = new SelectList(_context.ciudades, "id", "nombre", hotel.ciudad_fk);
            return View(hotel);
        }

        // GET: Hotel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
            return View(hotel);
        }

        // POST: Hotel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,capacidad,costo,nombre,ciudad_fk")] Hotel hotel)
        {
            if (id != hotel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
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

        // GET: Hotel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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

            return View(hotel);
        }

        // POST: Hotel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.hoteles == null)
            {
                return Problem("Entity set 'Context.hoteles'  is null.");
            }
            var hotel = await _context.hoteles.FindAsync(id);
            if (hotel != null)
            {
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
