using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    public class Agencia
    {

        private List<Usuario> usuarios = new List<Usuario>();
        private List<Hotel> hoteles = new List<Hotel>();
        private List<Vuelo> vuelos = new List<Vuelo>();
        private List<Ciudad> ciudades = new List<Ciudad>();
        private List<ReservaHotel> reservasHotel = new List<ReservaHotel>();
        private List<ReservaVuelo> reservasVuelo = new List<ReservaVuelo>();
        private Usuario usuarioActual;
        private int cantUsuarios;

        public Agencia()
        {
            usuarios = new List<Usuario>();
            cantUsuarios =  0;

        }

        public bool agregarUsuario(int dni, string nombre, string apellido, string mail, string clave)
        {
            usuarios.Add(new Usuario(cantUsuarios, dni, nombre, apellido, mail, clave));
            cantUsuarios++;
            return true;
        }

        public bool iniciarSesion(string mail, string clave)
        {
            foreach (Usuario u in usuarios) {
                if (u.mail.Equals(mail) && u.clave.Equals(clave))
                {
                    usuarioActual = u;
                    return true;
                }
            }
            return false;
        }

        public bool modificarUsuario(int id, string dni, string nombre, string apellido, string mail)
        {
            foreach (Usuario u in usuarios)
            {
                if (u.id == id)
                {
                    u.dni = int.Parse(dni);
                    u.nombre = nombre;
                    u.apellido = apellido;
                    u.mail = mail;
                    return true;
                }
            }
            return false;
        }

        public bool eliminarUsuario(int id)
        {
            foreach (Usuario u in usuarios)
            {
                if (u.id == id)
                {
                    usuarios.Remove(u);
                    return true;
                }
            }
            return false;
        }

        public string nombreLogueado()
        {
            return usuarioActual.nombre;
        }

        public List<Usuario> obtenerUsuarios()
        {
            return usuarios.ToList();
        }

    }
}
