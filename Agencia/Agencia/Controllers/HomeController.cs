using Agencia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
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

        public IActionResult ResultadosDeLaBusqueda(string origin, string destination, DateTime start_date, DateTime end_date, int rooms, int people, string total_adults, string total_minors, string total_people_rooms)
        {
            ViewBag.origen = origin;
            ViewBag.destino = destination;
            ViewBag.fecha_desde = start_date;
            ViewBag.fecha_hasta = end_date;
            ViewBag.cantHabitaciones = rooms;
            ViewBag.cantPersonas = people;
            ViewBag.total_adults = total_adults;
            ViewBag.total_minors = total_minors;
            ViewBag.total_people_rooms = total_people_rooms;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}