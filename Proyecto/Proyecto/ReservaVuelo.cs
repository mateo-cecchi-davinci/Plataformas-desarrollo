using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    public class ReservaVuelo
    {

        public int id {  get; set; }
        public Vuelo miVuelo { get; set; }
        public Usuario miUsuario { get; set;}
        public double pagado { get; set;}
        public int cantPersonas { get; set;}

        public ReservaVuelo (int id, Vuelo miVuelo, Usuario miUsuario, double pagado, int cantPersonas)
        {
            this.id = id;
            this.miVuelo = miVuelo;
            this.miUsuario = miUsuario;
            this.pagado = pagado;
            this.cantPersonas = cantPersonas;
        }

        public ReservaVuelo(Vuelo miVuelo, Usuario miUsuario, double pagado, int cantPersonas)
        {
            this.miVuelo = miVuelo;
            this.miUsuario = miUsuario;
            this.pagado = pagado;
            this.cantPersonas = cantPersonas;
        }

        public string[] ToString()
        {
            return new string[] { id.ToString(), miVuelo.origen.nombre, miVuelo.destino.nombre, miUsuario.nombre, miVuelo.fecha.ToString(), cantPersonas.ToString(), pagado.ToString() };
        }

    }
}
