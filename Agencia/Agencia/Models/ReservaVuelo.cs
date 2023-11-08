using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agencia.Models
{
    public class ReservaVuelo
    {

        public int id {  get; set; }
        public Vuelo miVuelo { get; set; }

        [ForeignKey("usuarioRV_fk")]
        public Usuario miUsuario { get; set;}
        public double pagado { get; set;}
        public int cantPersonas { get; set;}
        public int vuelo_fk { get; set; }
        public int usuarioRV_fk { get; set;}

        public ReservaVuelo() { }

        public ReservaVuelo(Vuelo miVuelo, Usuario miUsuario, double pagado, int cantPersonas, int vuelo_fk, int usuario_fk)
        {
            this.miVuelo = miVuelo;
            this.miUsuario = miUsuario;
            this.pagado = pagado;
            this.cantPersonas = cantPersonas;
            this.vuelo_fk = vuelo_fk;
            this.usuarioRV_fk = usuario_fk;
        }

        public string[] ToString()
        {
            return new string[] { id.ToString(), miVuelo.origen.nombre, miVuelo.destino.nombre, miUsuario.id.ToString(), miUsuario.nombre, miVuelo.fecha.ToString(), cantPersonas.ToString(), pagado.ToString() };
        }

    }
}
