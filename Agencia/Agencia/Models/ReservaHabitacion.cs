using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agencia.Models
{
    public class ReservaHabitacion
    {

        public int id { get; set; }
        public Habitacion miHabitacion { get; set; }

        [ForeignKey("usuarioRH_fk")]
        public Usuario miUsuario { get; set; }
        public DateTime fechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }
        public double pagado { get; set; }
        public int cantPersonas { get; set; }
        public int habitacion_fk { get; set; }
        public int usuarioRH_fk { get; set; }

        public ReservaHabitacion() { }

        public ReservaHabitacion (Habitacion miHabitacion, Usuario miUsuario, DateTime fechaDesde, DateTime fechaHasta, double pagado, int cantPersonas, int habitacion_fk, int usuario_fk)
        {
            this.miHabitacion = miHabitacion;
            this.miUsuario = miUsuario;
            this.fechaDesde = fechaDesde;
            this.fechaHasta = fechaHasta;
            this.pagado = pagado;
            this.cantPersonas = cantPersonas;
            this.habitacion_fk = habitacion_fk;
            this.usuarioRH_fk = usuario_fk;
        }

        public string[] ToString()
        {
            return new string[] { id.ToString(), miHabitacion.hotel.nombre, miUsuario.id.ToString(), miUsuario.nombre, fechaDesde.ToString(), fechaHasta.ToString(), pagado.ToString(), cantPersonas.ToString() };
        }
    }
}
