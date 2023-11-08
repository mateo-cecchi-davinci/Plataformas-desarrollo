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
    public class UsuarioHotelController : Controller
    {
        private readonly Context _context;

        public UsuarioHotelController(Context context)
        {
            _context = context;
        }

        // GET: UsuarioHotel
        public async Task<IActionResult> Index()
        {
            var context = _context.usuarioHotel.Include(u => u.hotel).Include(u => u.usuario);
            return View(await context.ToListAsync());
        }

        // GET: UsuarioHotel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.usuarioHotel == null)
            {
                return NotFound();
            }

            var usuarioHotel = await _context.usuarioHotel
                .Include(u => u.hotel)
                .Include(u => u.usuario)
                .FirstOrDefaultAsync(m => m.usuario_fk == id);
            if (usuarioHotel == null)
            {
                return NotFound();
            }

            return View(usuarioHotel);
        }

        // GET: UsuarioHotel/Create
        public IActionResult Create()
        {
            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre");
            ViewData["usuario_fk"] = new SelectList(_context.usuarios, "id", "apellido");
            return View();
        }

        // POST: UsuarioHotel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("usuario_fk,hotel_fk,cantidad")] UsuarioHotel usuarioHotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioHotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre", usuarioHotel.hotel_fk);
            ViewData["usuario_fk"] = new SelectList(_context.usuarios, "id", "apellido", usuarioHotel.usuario_fk);
            return View(usuarioHotel);
        }

        // GET: UsuarioHotel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.usuarioHotel == null)
            {
                return NotFound();
            }

            var usuarioHotel = await _context.usuarioHotel.FindAsync(id);
            if (usuarioHotel == null)
            {
                return NotFound();
            }
            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre", usuarioHotel.hotel_fk);
            ViewData["usuario_fk"] = new SelectList(_context.usuarios, "id", "apellido", usuarioHotel.usuario_fk);
            return View(usuarioHotel);
        }

        // POST: UsuarioHotel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("usuario_fk,hotel_fk,cantidad")] UsuarioHotel usuarioHotel)
        {
            if (id != usuarioHotel.usuario_fk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioHotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioHotelExists(usuarioHotel.usuario_fk))
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
            ViewData["hotel_fk"] = new SelectList(_context.hoteles, "id", "nombre", usuarioHotel.hotel_fk);
            ViewData["usuario_fk"] = new SelectList(_context.usuarios, "id", "apellido", usuarioHotel.usuario_fk);
            return View(usuarioHotel);
        }

        // GET: UsuarioHotel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.usuarioHotel == null)
            {
                return NotFound();
            }

            var usuarioHotel = await _context.usuarioHotel
                .Include(u => u.hotel)
                .Include(u => u.usuario)
                .FirstOrDefaultAsync(m => m.usuario_fk == id);
            if (usuarioHotel == null)
            {
                return NotFound();
            }

            return View(usuarioHotel);
        }

        // POST: UsuarioHotel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usuarioHotel == null)
            {
                return Problem("Entity set 'Context.usuarioHotel'  is null.");
            }
            var usuarioHotel = await _context.usuarioHotel.FindAsync(id);
            if (usuarioHotel != null)
            {
                _context.usuarioHotel.Remove(usuarioHotel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioHotelExists(int id)
        {
          return _context.usuarioHotel.Any(e => e.usuario_fk == id);
        }
    }
}
