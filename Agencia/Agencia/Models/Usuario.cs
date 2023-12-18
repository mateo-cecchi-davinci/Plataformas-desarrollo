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
        public List<ReservaHabitacion> misReservasHabitaciones { get; set; } = new List<ReservaHabitacion>();
        public List<ReservaVuelo> misReservasVuelos { get; set; } = new List<ReservaVuelo>();
        public double credito { get; set; }
        public bool isAdmin { get; set; }
        public ICollection<Vuelo> vuelosTomados { get; set; } = new List<Vuelo>();
        public List<UsuarioVuelo> usuario_vuelo { get; set; } = new List<UsuarioVuelo>();
        public ICollection<Habitacion> habitacionesUsadas { get; set; } = new List<Habitacion>();
        public List<UsuarioHabitacion> usuario_habitacion { get; set; } = new List<UsuarioHabitacion>();

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
