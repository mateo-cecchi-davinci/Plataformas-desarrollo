using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agencia.Models
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
        public ICollection<Vuelo> vuelosTomados { get; set; } = new List<Vuelo>();
        public List<UsuarioVuelo> usuario_vuelo { get; set; } = new List<UsuarioVuelo>();
        public Habitacion habitacion {  get; set; }
        public int habitacion_fk {  get; set; }

        //public ICollection<Hotel> hotelesVisitados { get; set; } = new List<Hotel>();
        //public List<UsuarioHotel> usuario_hotel { get; set; } = new List<UsuarioHotel>();

        public Usuario() { }

        public Usuario(int dni, string nombre, string apellido, string mail, string clave, int intentosFallidos, bool bloqueado, double credito, bool isAdmin, int habitacion_fk)
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
            this.habitacion_fk = habitacion_fk;
        }

        public string[] ToString()
        {
            return new string[] { id.ToString(), dni.ToString(), nombre, apellido, credito.ToString(), mail, intentosFallidos.ToString(), bloqueado.ToString() };
        }

    }
}
