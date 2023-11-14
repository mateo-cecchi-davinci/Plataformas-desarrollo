using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agencia.Models
{
    public class Hotel
    {

        public int id { get; set; }
        public Ciudad ubicacion { get; set; }
        public int capacidad { get; set; }
        public double costo { get; set; }
        public string nombre { get; set; }
        public int ciudad_fk { get; set; }

        [NotMapped]
        public IFormFile archivoImagen { get; set; }
        public string imagen { get; set; }
        public ICollection<Usuario> huespedes { get; set; } = new List<Usuario>();
        public List<ReservaHotel> misReservas { get; set; } = new List<ReservaHotel>();
        public List<UsuarioHotel> hotel_usuario { get; set; } = new List<UsuarioHotel>();

        public Hotel() { }

        public Hotel(string nombre, int capacidad, double costo, int ciudad_fk, Ciudad ubicacion)
        {
            this.nombre = nombre;
            this.capacidad = capacidad;
            this.costo = costo;
            this.ciudad_fk = ciudad_fk;
            this.ubicacion = ubicacion;
        }

        public string[] ToString()
        {
            return new string[] { id.ToString(), nombre, ubicacion.nombre, costo.ToString(), capacidad.ToString() };
        }

    }
}
