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
        public List<Vuelo> vuelos { get; set; } = new List<Vuelo>();
        public List<Hotel> hoteles { get; set; } = new List<Hotel>();

        public Ciudad(string nombre)
        {
            this.nombre = nombre;
        }

        public Ciudad(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

    }
}
