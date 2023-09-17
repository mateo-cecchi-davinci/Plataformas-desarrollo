using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    internal class Agencia
    {

        public List<Usuario> usuarios { get; set; }
        public List<Hotel> hoteles { get; set; }
        public List<Vuelo> vuelos { get; set; }
        public List<Ciudad> ciudades { get; set; }
        public List<ReservaHotel> reservasHotel { get; set; }
        public List<ReservaVuelo> reservasVuelo { get; set; }
        public Usuario usuarioActual { get; set; }

        private int cantHoteles;

        public Agencia()
        {
            hoteles = new List<Hotel>();
            cantHoteles = 0;
        }

        public bool agregarHoteles(Ciudad ubicacion, int capacidad, double costo, string nombre)
        {
            hoteles.Add(new Hotel(cantHoteles,ubicacion,capacidad,costo, nombre));
            cantHoteles++;
            return true;
        }

        public List<Hotel> obtenerHoteles()
        {
            return hoteles.ToList();        
        }
    }
}
