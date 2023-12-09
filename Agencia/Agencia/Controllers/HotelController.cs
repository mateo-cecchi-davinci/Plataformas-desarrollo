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

        // GET: Hotels
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
            var context = _context.hoteles.Include(h => h.ubicacion);
            return View(await context.ToListAsync());
        }

        // GET: Hotels/Details/5
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

        // GET: Hotels/Create
        public IActionResult Create()
        {
            ViewData["ciudad_fk"] = new SelectList(_context.ciudades, "id", "nombre");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id, nombre, ciudad_fk, archivoImagen, descripcion")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                if (hotel.archivoImagen != null)
                {
                    string directorio = "wwwroot/images/hotel/";
                    string nombreArchivo = Guid.NewGuid().ToString() + "_" + hotel.archivoImagen.FileName;
                    string rutaCompleta = Path.Combine(directorio, nombreArchivo);

                    Directory.CreateDirectory(Path.GetDirectoryName(rutaCompleta));

                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        await hotel.archivoImagen.CopyToAsync(stream);
                    }

                    hotel.imagen = rutaCompleta.Replace("wwwroot", "").TrimStart('\\', '/');
                }

                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ciudad_fk"] = new SelectList(_context.ciudades, "id", "nombre", hotel.ciudad_fk);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
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

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id, nombre, ciudad_fk, archivoImagen, descripcion")] Hotel hotel)
        {
            if (id != hotel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (hotel.archivoImagen != null)
                    {
                        string directorio = "wwwroot/images/hotel/";
                        string nombreArchivo = Guid.NewGuid().ToString() + "_" + hotel.archivoImagen.FileName;
                        string rutaCompleta = Path.Combine(directorio, nombreArchivo);

                        Directory.CreateDirectory(Path.GetDirectoryName(rutaCompleta));

                        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                        {
                            await hotel.archivoImagen.CopyToAsync(stream);
                        }

                        hotel.imagen = rutaCompleta.Replace("wwwroot", "").TrimStart('\\', '/');
                    }

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

        // GET: Hotels/Delete/5
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

        // POST: Hotels/Delete/5
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
