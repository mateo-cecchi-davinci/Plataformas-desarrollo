using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    public class Usuario
    {

        public int id { get; set; }
        public int dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string mail { get; set; }
        public string clave { get; set; }
        public int intentosFallidos { get; set; }
        public bool bloqueado { get; set; }
        public List<ReservaHotel> misReservasHoteles { get; set; } = new List<ReservaHotel>();
        public List<ReservaVuelo> misReservasVuelos { get; set; } = new List<ReservaVuelo>();
        public double credito { get; set; }
        public bool isAdmin { get; set; }
        public ICollection<Hotel> hotelesVisitados { get; set; } = new List<Hotel>();
        public ICollection<Vuelo> vuelosTomados { get; set; } = new List<Vuelo>();
        public List<UsuarioHotel> usuario_hotel { get; set; } = new List<UsuarioHotel>();
        public List<UsuarioVuelo> usuario_vuelo { get; set; } = new List<UsuarioVuelo>();

        public Usuario() { }

        public Usuario(int dni, string nombre, string apellido, string mail, string clave, int intentosFallidos, bool bloqueado, double credito, bool isAdmin)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.mail = mail;
            this.clave = clave;
            this.intentosFallidos = intentosFallidos;
            this.bloqueado = bloqueado;
            this.credito = credito;
            this.isAdmin = isAdmin;
        }

        public string[] ToString()
        {
            return new string[] { id.ToString(), dni.ToString(), nombre, apellido, credito.ToString(), mail, intentosFallidos.ToString(), bloqueado.ToString() };
        }

    }
}
