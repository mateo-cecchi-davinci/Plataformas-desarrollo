using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    public class ReservaVuelo
    {

        public Vuelo miVuelo { get; set; }
        public Usuario miUsuario { get; set;}
        public double pagado { get; set;}

        public ReservaVuelo (Vuelo miVuelo, Usuario miUsuario, double pagado)
        {
            this.miVuelo = miVuelo;
            this.miUsuario = miUsuario;
            this.pagado = pagado;
        }
    }
}
