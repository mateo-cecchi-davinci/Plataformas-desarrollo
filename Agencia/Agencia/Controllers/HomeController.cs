using Agencia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Agencia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            var hoteles = _context.hoteles.Include(h => h.ubicacion).ToList();
            var vuelos = _context.vuelos.Include(v => v.origen).Include(v => v.destino).ToList();

            ViewData["hoteles"] = hoteles;
            ViewData["vuelos"] = vuelos;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Paquetes()
        {

            var hoteles = _context.hoteles.Include(h => h.ubicacion).ToList();
            var vuelos = _context.vuelos.Include(v => v.origen).Include(v => v.destino).ToList();

            ViewData["hoteles"] = hoteles;
            ViewData["vuelos"] = vuelos;

            return View();
        }

        public IActionResult Base()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}