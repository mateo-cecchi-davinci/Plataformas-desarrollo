using Agencia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Agencia.Controllers
{
    public class ReservaController : Controller
    {
        private readonly Context _context;

        public ReservaController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Proceso([FromBody] JObject requestData)
        {
            var hotel = requestData["hotel"].ToObject<Hotel>();
            var vuelo = requestData["flight"].ToObject<Vuelo>();
            var fechaDesde = DateTime.ParseExact(requestData["start_date"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var fechaHasta = DateTime.ParseExact(requestData["end_date"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var total = requestData["total"].ToObject<double>();
            var habitacionesChicas = requestData["sm_rooms"].ToObject<int>();
            var habitacionesMedianas = requestData["md_rooms"].ToObject<int>();
            var habitacionesGrandes = requestData["xl_rooms"].ToObject<int>();
            var totalPersonas = requestData["people"].ToObject<int>();

            //int cant_hab_chicas = 0;
            //int cant_hab_medianas = 0;
            //int cant_hab_grandes = 0;

            //foreach (var habitacion in hotel.habitaciones)
            //{
            //    if (habitacion.capacidad > 4)
            //    {
            //        cant_hab_grandes++;
            //    }
            //    else if (habitacion.capacidad > 2)
            //    {
            //        cant_hab_medianas++;
            //    }
            //    else
            //    {
            //        cant_hab_chicas++;
            //    }
            //}

            //cant_hab_chicas += habitacionesChicas;
            //cant_hab_medianas += habitacionesMedianas;
            //cant_hab_grandes += habitacionesGrandes;

            //foreach (var habitacion in hotel.habitaciones)
            //{
            //    foreach (var reserva in habitacion.misReservas)
            //    {
            //        if (reserva.fechaDesde <= fechaHasta && reserva.fechaHasta >= fechaDesde)
            //        {

            //        }
            //    }
            //}

            var habitacionesDisponibles = hotel.habitaciones
                .GroupBy(habitacion => habitacion.capacidad)
                .ToDictionary(group => group.Key, group => group.Count());

            bool hab_suficientes = habitacionesDisponibles.TryGetValue(2, out int disponiblesCapacidad2) &&
                                           habitacionesDisponibles.TryGetValue(4, out int disponiblesCapacidad4) &&
                                           habitacionesDisponibles.TryGetValue(8, out int disponiblesCapacidad8) &&
                                           disponiblesCapacidad2 >= habitacionesChicas &&
                                           disponiblesCapacidad4 >= habitacionesMedianas &&
                                           disponiblesCapacidad8 >= habitacionesGrandes;

            if (hab_suficientes)
            {
                List<Habitacion> hab_Chicas = new List<Habitacion>();
                List<Habitacion> hab_Medianas = new List<Habitacion>();
                List<Habitacion> hab_Grandes = new List<Habitacion>();

                foreach (var habitacion in hotel.habitaciones)
                {
                    if (!habitacion.misReservas.Any(reserva => reserva.fechaDesde >= fechaHasta && reserva.fechaHasta <= fechaDesde))
                    {
                        if (habitacion.capacidad == 2 && habitacionesChicas > 0)
                        {
                            hab_Chicas.Add(habitacion);
                            habitacionesChicas--;
                        }

                        if (habitacion.capacidad == 2 && habitacionesMedianas > 0)
                        {
                            hab_Medianas.Add(habitacion);
                            habitacionesMedianas--;
                        }

                        if (habitacion.capacidad == 2 && habitacionesGrandes > 0)
                        {
                            hab_Grandes.Add(habitacion);
                            habitacionesGrandes--;
                        }
                    }
                }

                if (habitacionesGrandes == 0 && habitacionesMedianas == 0 && habitacionesChicas == 0)
                {
                    //aca hay disponibilidad para hacer la reserva 
                    //Listas cargadas y c/u de esas habitaciones hay q reservalas
                }
                else
                {
                    //no se puede reservar
                }

                    //bool habitacionDisponible = !habitacion.misReservas.Any(reserva =>
                    //    reserva.fechaDesde <= fechaHasta && reserva.fechaHasta >= fechaDesde);

                    //if (habitacionDisponible)
                    //{
                        //AGREGAR LOGICA DEL USUARIO LOGGEADO QUE ESTA HACIENDO LA RESERVA

                        //var nuevaReserva = new ReservaHabitacion
                        //{
                        //    miHabitacion = habitacion,
                        //    miUsuario = usuario,
                        //    fechaDesde = fechaDesde,
                        //    fechaHasta = fechaHasta,
                        //    pagado = 0,
                        //    cantPersonas = habitacion.capacidad 
                        //};

                        //habitacion.misReservas.Add(nuevaReserva);
                        //usuario.misReservasHabitaciones.Add(nuevaReserva);

                        //_context.reservasHabitacion.Add(nuevaReserva);
                        //_context.SaveChanges();

                        //_context.Entry(habitacion).State = EntityState.Modified;
                        //_context.Entry(usuario).State = EntityState.Modified;
                        //_context.SaveChanges();


                    //}
                    //else
                    //{
                        // Lógica en caso de que no haya disponibilidad en alguna habitación
                    //}
                
            }

            return View();
        }
    }
}
