using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    public class Ciudad
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public List<Vuelo> vuelosOrigen { get; set; }
        public List<Vuelo> vuelosDestino{ get; set; }
        public List<Hotel> hoteles { get; set; }

        public Ciudad (int id, string nombre, List<Vuelo> vuelosOrigen, List<Vuelo> vuelosDestino, List<Hotel> hoteles)
        {
            this.id = id;
            this.nombre = nombre;
            this.vuelosOrigen = vuelosOrigen;
            this.vuelosDestino = vuelosDestino;
            this.hoteles = hoteles;
        }

        public Ciudad(string nombre)
        {
            this.nombre = nombre;
        }

        public string[] ToString()
        {
            return new string[] { nombre };
        }

    }
}
