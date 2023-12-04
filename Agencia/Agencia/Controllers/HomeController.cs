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
            string usuarioLogeado = HttpContext.Session.GetString("UsuarioLogeado");
            string esAdminString = HttpContext.Session.GetString("esAdmin");
            bool isAdmin = false;

            if (!string.IsNullOrEmpty(esAdminString))
            {

                bool.TryParse(esAdminString, out isAdmin);
            }

            if (usuarioLogeado == null) 
            {
                return RedirectToAction("Index", "Login");
            }
            var hoteles = _context.hoteles.Include(h => h.ubicacion).ToList();
            var vuelos = _context.vuelos.Include(v => v.origen).Include(v => v.destino).ToList();

            ViewData["hoteles"] = hoteles;
            ViewData["vuelos"] = vuelos;

            ViewBag.usuarioLogeado = usuarioLogeado;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Paquetes()
        {

            var hoteles = _context.hoteles
                .Include(h => h.ubicacion)
                .Include(h => h.habitaciones)
                .ToList();
            var vuelos = _context.vuelos
                .Include(v => v.origen)
                .Include(v => v.destino)
                .ToList();

            var habitacionesDoblesPorHotel = new Dictionary<int, List<Habitacion>>();

            foreach (var hotel in hoteles)
            {
                var habitacionesDobles = hotel.habitaciones.Where(h => h.capacidad == 2).ToList();

                habitacionesDoblesPorHotel.Add(hotel.id, habitacionesDobles);
            }

            ViewData["hoteles"] = hoteles;
            ViewData["vuelos"] = vuelos;
            ViewData["habitacionesDoblesPorHotel"] = habitacionesDoblesPorHotel;

            return View();
        }

        public IActionResult Base()
        {
            return View();
        }

        public IActionResult ResultadosDeLaBusqueda(string origin, string destination, DateTime start_date, DateTime end_date, int rooms, int people, string total_adults, string total_minors, string total_people_rooms)
        {
            var hoteles = _context.hoteles
                .Include(h => h.ubicacion)
                .Include(h => h.habitaciones)
                .Where(hotel => hotel.ubicacion.nombre == destination) //AGREGAR FILTROS ACA
                .ToList();
            var vuelos = _context.vuelos
                .Include(v => v.origen)
                .Include(v => v.destino)
                .Where(vuelo => vuelo.origen.nombre == origin && vuelo.destino.nombre == destination)
                .ToList();

            TimeSpan diferencia = end_date.Subtract(start_date);
            int cantDias = diferencia.Days;

            Dictionary<string, int> habitaciones = JsonSerializer.Deserialize<Dictionary<string, int>>(total_people_rooms);
            Dictionary<string, int> adultos = JsonSerializer.Deserialize<Dictionary<string, int>>(total_adults);
            Dictionary<string, int> menores = JsonSerializer.Deserialize<Dictionary<string, int>>(total_minors);

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

            var hotelesSeleccionados = hoteles
                .Where(h => h.habitaciones
                .Count(hab => hab.capacidad == 2) >= habitaciones_chicas && h.habitaciones
                .Count(hab => hab.capacidad == 4) >= habitaciones_medianas && h.habitaciones
                .Count(hab => hab.capacidad == 8) >= habitaciones_grandes)
                .ToList();

            var hotelesConCostoTotal = hotelesSeleccionados.Select(hotel =>
            {
                var costo = hotel.habitaciones
                    .Where(habitacion => habitacion.capacidad == 2 || habitacion.capacidad == 4 || habitacion.capacidad == 8)
                    .Take(habitaciones_chicas + habitaciones_medianas + habitaciones_grandes)
                    .Sum(habitacion => habitacion.costo);

                return new
                {
                    hotel,
                    costo
                };
            }).ToList();


            int total_adultos = adultos.Values.Sum();
            int total_menores = menores.Values.Sum();

            ViewBag.hotelesConCostoTotal = hotelesConCostoTotal;
            ViewBag.vuelos = vuelos;
            ViewBag.fecha_desde = start_date;
            ViewBag.fecha_hasta = end_date;
            ViewBag.cantDias = cantDias;
            ViewBag.total_adultos = total_adultos;
            ViewBag.total_menores = total_menores;
            ViewBag.total_personas = people;
            ViewBag.habitaciones_chicas = habitaciones_chicas;
            ViewBag.habitaciones_medianas = habitaciones_medianas;
            ViewBag.habitaciones_grandes = habitaciones_grandes;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}