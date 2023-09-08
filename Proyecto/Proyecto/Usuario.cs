using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    internal class Usuario
    {

        public int id { get; set; }
        public int dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public int intentosFallidos { get; set; }
        public bool bloqueado { get; set; }
        public List<ReservaHotel> misReservasHoteles { get; set; }
        public List<ReservaVuelo> misReservasVuelos { get; set; }
        public double credito { get; set; }
        public bool isAdmin { get; set; }
        public List<Hotel> hotelesVisitados { get; set; }
        public List<Vuelo> vuelosTomados { get; set; }

        public Usuario (int id, int dni, string nombre, string apellido, string mail, string  password)
        {
            this.id = id;
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.mail = mail;
            this.password = password;
        }



    }
}
