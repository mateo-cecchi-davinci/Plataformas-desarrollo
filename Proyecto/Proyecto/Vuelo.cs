using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    public class Vuelo
    {

        public int id { get; set; }
        public Ciudad origen { get; set; }
        public Ciudad destino { get; set; }
        public int capacidad { get; set; }
        public int vendido { get; set; }
        public List<Usuario> pasajeros { get; set; }
        public double costo { get; set; }
        public DateTime fecha { get; set; }
        public string aerolinea { get; set; }
        public string avion { get; set; }
        public int origen_fk { get; set; }
        public int destino_fk { get; set; }
        public List<ReservaVuelo> misReservas { get; set; } = new List<ReservaVuelo>();

        public Vuelo(int id, Ciudad origen, Ciudad destino, int capacidad,  double costo, DateTime fecha, string aerolinea, string avion)
        {
            this.id = id;
            this.origen = origen;
            this.destino = destino;
            this.capacidad = capacidad;
            this.costo = costo;
            this.fecha = fecha;
            this.aerolinea = aerolinea;
            this.avion = avion;
        }

        public Vuelo(int id, int capacidad, int vendido, double costo, DateTime fecha, string aerolinea, string avion, int origen_fk, int destino_fk)
        {
            this.id = id;
            this.capacidad = capacidad;
            this.vendido = vendido;
            this.costo = costo;
            this.fecha = fecha;
            this.aerolinea = aerolinea;
            this.avion = avion;
            this.origen_fk = origen_fk;
            this.destino_fk = destino_fk;
        }

        public string[] ToString()
        {
            return new string[] { id.ToString(), origen.nombre, destino.nombre, costo.ToString(), capacidad.ToString(), fecha.ToString(), aerolinea, avion };
        }
    }

}
