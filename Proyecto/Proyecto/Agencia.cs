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
        private int cantHoteles;
        private int cantVuelos;

        public Agencia()
        {
            usuarios = new List<Usuario>();
            hoteles = new List<Hotel>();
            vuelos = new List<Vuelo> ();
            cantUsuarios =  0;
            cantHoteles = 0;
            cantVuelos = 0;

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

        //Seccion Hoteles

        public bool agregarHotel(Ciudad ubicacion, int capacidad, double costo, string nombre)
        {
            hoteles.Add(new Hotel(cantHoteles, ubicacion, capacidad, costo, nombre));
            cantHoteles++;
            return true;
        }

        public bool elminarHotel(int id) 
        {
            foreach (Hotel h in hoteles) { 
                if (h.id == id)
                {
                    hoteles.Remove(h);
                    return true;
                }
            }
                return false;
        }

        public bool modificarHoteles (int id, Ciudad ubicacion, string capacidad, string costo, string nombre)
        {
            foreach (Hotel h in hoteles) {
                if (h.id == id) { 
                    h.ubicacion = ubicacion;
                    h.capacidad = int.Parse(capacidad);
                    h.costo = double.Parse(costo);
                    h.nombre = nombre;
                    return true;
                }
            }
            return false;
        }

        public List<Hotel> obtenerHoteles()
        {
            return hoteles.ToList();
        }

        //CRUD Vuelos
        public bool agregarVuelos(Ciudad origen, Ciudad destino, double costo, DateTime fecha, string aerolinea, string avion)
        {
            vuelos.Add(new Vuelo(cantVuelos, origen,destino, costo,  fecha,  aerolinea, avion));
            cantVuelos++;
            return true;
        }

        public List<Vuelo> obtenerVuelos()
        {
            return vuelos.ToList();
        }



    }
}
