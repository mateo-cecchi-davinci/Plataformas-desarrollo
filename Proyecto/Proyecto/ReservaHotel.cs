using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    internal class ReservaHotel
    {

        public Hotel miHotel { get; set; }
        public Usuario miUsuario { get; set; }
        public DateTime fechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }
        public double pagado { get; set; }

        public ReservaHotel (Hotel miHotel, Usuario miUsuario, DateTime fechaDesde, DateTime fechaHasta, double pagado)
        {
            this.miHotel = miHotel;
            this.miUsuario = miUsuario;
            this.fechaDesde = fechaDesde;
            this.fechaHasta = fechaHasta;
            this.pagado = pagado;
        }
    }
}
