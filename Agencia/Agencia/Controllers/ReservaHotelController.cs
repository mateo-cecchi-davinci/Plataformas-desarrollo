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
    public class ReservaHotelController : Controller
    {
        private readonly Context _context;

        public ReservaHotelController(Context context)
        {
            _context = context;
        }

        // GET: ReservaHotel
        public async Task<IActionResult> Index()
        {
            var context = _context.reservasHotel.Include(r => r.miHotel).Include(r => r.miUsuario);
            return View(await context.ToListAsync());
        }

        // GET: ReservaHotel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.reservasHotel == null)
            {
                return NotFound();
            }

            var reservaHotel = await _context.reservasHotel
                .Include(r => r.miHotel)
                .Include(r => r.miUsuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (reservaHotel == null)
            {
                return NotFound();
            }

            return View(reservaHotel);
        }

        // GET: ReservaHotel/Create
        public IActionResult Create()
        {
            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre");
            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios, "id", "apellido");
            return View();
        }

        // POST: ReservaHotel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,fechaDesde,fechaHasta,pagado,cantPersonas,hotel_fk,usuarioRH_fk")] ReservaHotel reservaHotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservaHotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre", reservaHotel.hotel_fk);
            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios, "id", "apellido", reservaHotel.usuarioRH_fk);
            return View(reservaHotel);
        }

        // GET: ReservaHotel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.reservasHotel == null)
            {
                return NotFound();
            }

            var reservaHotel = await _context.reservasHotel.FindAsync(id);
            if (reservaHotel == null)
            {
                return NotFound();
            }
            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre", reservaHotel.hotel_fk);
            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios, "id", "apellido", reservaHotel.usuarioRH_fk);
            return View(reservaHotel);
        }

        // POST: ReservaHotel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,fechaDesde,fechaHasta,pagado,cantPersonas,hotel_fk,usuarioRH_fk")] ReservaHotel reservaHotel)
        {
            if (id != reservaHotel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservaHotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaHotelExists(reservaHotel.id))
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
            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre", reservaHotel.hotel_fk);
            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios, "id", "apellido", reservaHotel.usuarioRH_fk);
            return View(reservaHotel);
        }

        // GET: ReservaHotel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.reservasHotel == null)
            {
                return NotFound();
            }

            var reservaHotel = await _context.reservasHotel
                .Include(r => r.miHotel)
                .Include(r => r.miUsuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (reservaHotel == null)
            {
                return NotFound();
            }

            return View(reservaHotel);
        }

        // POST: ReservaHotel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.reservasHotel == null)
            {
                return Problem("Entity set 'Context.reservasHotel'  is null.");
            }
            var reservaHotel = await _context.reservasHotel.FindAsync(id);
            if (reservaHotel != null)
            {
                _context.reservasHotel.Remove(reservaHotel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaHotelExists(int id)
        {
          return _context.reservasHotel.Any(e => e.id == id);
        }
    }
}
