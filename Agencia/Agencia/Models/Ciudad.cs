using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agencia.Models
{
    public class Ciudad
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public List<Hotel> hoteles { get; set; } = new List<Hotel>();
        public List<Vuelo> vuelos { get; set; } = new List<Vuelo>();

        public Ciudad() { }

    }
}
