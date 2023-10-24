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
        public List<Usuario> huespedes { get; set; } = new List<Usuario>();
        public string nombre { get; set; }
        public int ciudad_fk { get; set; }
        public List<ReservaHotel> misReservas { get; set; } = new List<ReservaHotel>();
        public List<UsuarioHotel> hotel_usuario { get; set; } = new List<UsuarioHotel>();

        public Hotel(int id, Ciudad ubicacion, int capacidad, double costo, string nombre)
        {
            this.id = id;
            this.ubicacion = ubicacion;
            this.capacidad = capacidad;
            this.costo = costo;
            this.nombre = nombre;
        }

        public Hotel(int id, string nombre, int capacidad, double costo, int ciudad_fk)
        {
            this.id = id;
            this.nombre = nombre;
            this.capacidad = capacidad;
            this.costo = costo;
            this.ciudad_fk = ciudad_fk;
        }

        public string[] ToString()
        {
            return new string[] { id.ToString(), nombre, ubicacion.nombre, costo.ToString(), capacidad.ToString() };
        }

    }
}
