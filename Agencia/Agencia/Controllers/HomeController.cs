using Agencia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;

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
            var hoteles = _context.hoteles.Where(hotel => hotel.ubicacion.nombre == destination).ToList();
            var vuelos = _context.vuelos.Where(vuelo => vuelo.origen.nombre == origin && vuelo.destino.nombre == destination);

            TimeSpan diferencia = end_date.Subtract(start_date);
            int cantDias = diferencia.Days;

            Dictionary<string, int> habitaciones = JsonSerializer.Deserialize<Dictionary<string, int>>(total_people_rooms);
            List<int> adultos = JsonSerializer.Deserialize<List<int>>(total_adults);
            List<int> menores = JsonSerializer.Deserialize<List<int>>(total_minors);

            int habitaciones_chicas = 0;
            int habitaciones_medianas = 0;
            int habitaciones_grandes = 0;

            foreach (var personas_por_hab in habitaciones.Values)
            {
                if (personas_por_hab > 4)
                {
                    habitaciones_grandes++;
                }
                else if (personas_por_hab > 2)
                {
                    habitaciones_medianas++;
                }
                else
                {
                    habitaciones_chicas++;
                }
            }

            //Small Rooms: 2
            //Medium Rooms: 1
            //Large Rooms: 0
            //Total Adults: { "room-1":2,"room-2":1,"room-3":1}
            //Total Minors: { "room-1":1,"room-2":1,"room-3":1}
            //Total People: 7

            int total_adultos = adultos.Sum();
            int total_menores = menores.Sum();

            ViewBag.hoteles = hoteles;
            ViewBag.vuelos = vuelos;
            ViewBag.fecha_desde = start_date;
            ViewBag.fecha_hasta = end_date;
            ViewBag.cantDias = cantDias;
            ViewBag.habitaciones_chicas = habitaciones_chicas;
            ViewBag.habitaciones_medianas = habitaciones_medianas;
            ViewBag.habitaciones_grandes = habitaciones_grandes;
            ViewBag.total_adultos = total_adultos;
            ViewBag.total_menores = total_menores;
            ViewBag.total_personas = people;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}