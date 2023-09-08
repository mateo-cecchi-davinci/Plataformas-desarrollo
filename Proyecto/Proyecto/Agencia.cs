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

        

    }
}
