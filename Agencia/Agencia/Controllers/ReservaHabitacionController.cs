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
    public class ReservaHabitacionController : Controller
    {
        private readonly Context _context;

        public ReservaHabitacionController(Context context)
        {
            _context = context;
        }

        // GET: ReservaHabitacions
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
            var context = _context.reservasHabitacion.Include(r => r.miHabitacion).Include(r => r.miUsuario);
            return View(await context.ToListAsync());
        }

        // GET: ReservaHabitacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.reservasHabitacion == null)
            {
                return NotFound();
            }

            var reservaHabitacion = await _context.reservasHabitacion
                .Include(r => r.miHabitacion)
                .Include(r => r.miUsuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (reservaHabitacion == null)
            {
                return NotFound();
            }

            return View(reservaHabitacion);
        }

        // GET: ReservaHabitacions/Create
        public IActionResult Create()
        {
            ViewData["habitacion_fk"] = new SelectList(_context.habitaciones, "id", "id");
            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios, "id", "apellido");
            return View();
        }

        // POST: ReservaHabitacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,fechaDesde,fechaHasta,pagado,cantPersonas,habitacion_fk,usuarioRH_fk")] ReservaHabitacion reservaHabitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservaHabitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["habitacion_fk"] = new SelectList(_context.habitaciones, "id", "id", reservaHabitacion.habitacion_fk);
            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios, "id", "apellido", reservaHabitacion.usuarioRH_fk);
            return View(reservaHabitacion);
        }

        // GET: ReservaHabitacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.reservasHabitacion == null)
            {
                return NotFound();
            }

            var reservaHabitacion = await _context.reservasHabitacion.FindAsync(id);
            if (reservaHabitacion == null)
            {
                return NotFound();
            }
            ViewData["habitacion_fk"] = new SelectList(_context.habitaciones, "id", "id", reservaHabitacion.habitacion_fk);
            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios, "id", "apellido", reservaHabitacion.usuarioRH_fk);
            return View(reservaHabitacion);
        }

        // POST: ReservaHabitacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,fechaDesde,fechaHasta,pagado,cantPersonas,habitacion_fk,usuarioRH_fk")] ReservaHabitacion reservaHabitacion)
        {
            if (id != reservaHabitacion.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservaHabitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaHabitacionExists(reservaHabitacion.id))
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
            ViewData["habitacion_fk"] = new SelectList(_context.habitaciones, "id", "id", reservaHabitacion.habitacion_fk);
            ViewData["usuarioRH_fk"] = new SelectList(_context.usuarios, "id", "apellido", reservaHabitacion.usuarioRH_fk);
            return View(reservaHabitacion);
        }

        // GET: ReservaHabitacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.reservasHabitacion == null)
            {
                return NotFound();
            }

            var reservaHabitacion = await _context.reservasHabitacion
                .Include(r => r.miHabitacion)
                .Include(r => r.miUsuario)
                .FirstOrDefaultAsync(m => m.id == id);
            if (reservaHabitacion == null)
            {
                return NotFound();
            }

            return View(reservaHabitacion);
        }

        // POST: ReservaHabitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.reservasHabitacion == null)
            {
                return Problem("Entity set 'Context.reservasHotel'  is null.");
            }
            var reservaHabitacion = await _context.reservasHabitacion.FindAsync(id);
            if (reservaHabitacion != null)
            {
                _context.reservasHabitacion.Remove(reservaHabitacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaHabitacionExists(int id)
        {
          return _context.reservasHabitacion.Any(e => e.id == id);
        }
    }
}
