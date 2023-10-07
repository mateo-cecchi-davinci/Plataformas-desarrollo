using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    public class Hotel
    {

        private List<Usuario> huesped;

        public int id { get; set; }
        public Ciudad ubicacion { get; set; }
        public int capacidad { get; set; }
        public double costo { get; set; }
        public List<Usuario> huespedes { get => huesped.ToList(); }
        public string nombre { get; set; }
        public List<ReservaHotel> misReservas { get; set; }

        public Hotel(int id, Ciudad ubicacion, int capacidad, double costo, string nombre)
        {
            this.id = id;
            this.ubicacion = ubicacion;
            this.capacidad = capacidad;
            this.costo = costo;
            this.nombre = nombre;
            misReservas = new List<ReservaHotel>();
        }

        public string[] ToString()
        {
            return new string[] { id.ToString(), nombre, ubicacion.nombre, costo.ToString(), capacidad.ToString() };
        }

    }
}
