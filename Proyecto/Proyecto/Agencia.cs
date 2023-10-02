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
        private int cantReservasHoteles;
        private int cantReservasVuelos;

        public Agencia()
        {
            usuarios = new List<Usuario>();
            hoteles = new List<Hotel>();
            vuelos = new List<Vuelo>();
            reservasHotel = new List<ReservaHotel>();
            reservasVuelo = new List<ReservaVuelo>();
            ciudades = new List<Ciudad>();
            cantUsuarios = 0;
            cantHoteles = 0;
            cantVuelos = 0;
            cantReservasHoteles = 0;
            cantReservasVuelos = 0;
        }

        public bool agregarUsuario(int dni, string nombre, string apellido, string mail, string clave, bool isAdmin, double credito)
        {
            usuarios.Add(new Usuario(cantUsuarios, dni, nombre, apellido, mail, clave, isAdmin, credito));
            cantUsuarios++;
            return true;
        }

        public bool iniciarSesion(string mail, string clave)
        {
            foreach (Usuario u in usuarios)
            {
                if (u.mail.Equals(mail) && u.clave.Equals(clave))
                {
                    usuarioActual = u;
                    return true;
                }
            }
            return false;
        }

        public void cerrarSesion()
        {
            usuarioActual = null;
        }

        public bool modificarUsuario(int id, int dni, string nombre, string apellido, string mail, double credito)
        {
            foreach (Usuario u in usuarios)
            {
                if (u.id == id)
                {
                    u.dni = dni;
                    u.nombre = nombre;
                    u.apellido = apellido;
                    u.mail = mail;
                    u.credito = credito;
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

        public double mostrarCredito()
        {
            return usuarioActual.credito;
        }

        public Usuario obtenerUsuarioActual()
        {
            return usuarioActual;
        }

        //Seccion Hoteles

        public bool agregarHotel(Ciudad ubicacion, int capacidad, double costo, string nombre)
        {
            hoteles.Add(new Hotel(cantHoteles, ubicacion, capacidad, costo, nombre));
            
            if(ubicacion.hoteles == null)
            {
                ubicacion.hoteles = new List<Hotel>();
            }
            ubicacion.hoteles.Add(hoteles.Last());
            cantHoteles++;
            return true;
        }

        public bool elminarHotel(int id)
        {
            foreach (Hotel h in hoteles)
            {
                if (h.id == id)
                {
                    h.ubicacion.hoteles.Remove(h);
                    hoteles.Remove(h);
                    return true;
                }
            }
            return false;
        }

        public bool modificarHoteles(int id, Ciudad ubicacion, int capacidad, double costo, string nombre)
        {
            foreach (Hotel h in hoteles)
            {
                if (h.id == id)
                {
                    if (ubicacion.hoteles == null)
                    {
                        ubicacion.hoteles = new List<Hotel>();
                    }

                    if (h.ubicacion != ubicacion)
                    {
                        h.ubicacion.hoteles.Remove(h);
                    }

                    h.ubicacion = ubicacion;
                    h.capacidad = capacidad;
                    h.costo = costo;
                    h.nombre = nombre;

                    if (!ubicacion.hoteles.Contains(h))
                    {
                        ubicacion.hoteles.Add(h);
                    }
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
        public bool agregarVuelos(Ciudad origen, Ciudad destino, int capacidad, double costo, DateTime fecha, string aerolinea, string avion)
        {

            if (origen.vuelosOrigen == null)
            {
                origen.vuelosOrigen = new List<Vuelo>();
            }

            if (destino.vuelosDestino == null)
            {
                destino.vuelosDestino = new List<Vuelo>();
            }

            vuelos.Add(new Vuelo(cantVuelos, origen, destino, capacidad, costo, fecha, aerolinea, avion));
            origen.vuelosOrigen.Add(vuelos.Last());
            destino.vuelosDestino.Add(vuelos.Last());
            cantVuelos++;
            return true;
        }

        public List<Vuelo> obtenerVuelos()
        {
            return vuelos.ToList();
        }

        public bool modificarVuelos(int id, Ciudad origen, Ciudad destino, int capacidad, double costo, string fecha, string aerolinea, string avion)
        {
            foreach (Vuelo v in vuelos)
            {
                if (v.id == id)
                {

                    if (origen.vuelosOrigen == null)
                    {
                        origen.vuelosOrigen = new List<Vuelo>();
                    }

                    if (destino.vuelosDestino == null)
                    {
                        destino.vuelosDestino = new List<Vuelo>();
                    }

                    if (v.origen != origen)
                    {
                        v.origen.vuelosOrigen.Remove(v);
                    }
                    if (v.destino != destino)
                    {
                        v.destino.vuelosDestino.Remove(v);
                    }

                    v.origen = origen;
                    v.destino = destino;
                    v.capacidad = capacidad;
                    v.costo = costo;
                    v.fecha = DateTime.Parse(fecha);
                    v.aerolinea = aerolinea;
                    v.avion = avion;

                    if (!origen.vuelosOrigen.Contains(v))
                    {
                        origen.vuelosOrigen.Add(v);
                    }
                    if (!destino.vuelosDestino.Contains(v))
                    {
                        destino.vuelosDestino.Add(v);
                    }

                    return true;
                }
            }
            return false;
        }

        public bool modificarFechaVuelo(int id, DateTime fecha)
        {
            foreach (Vuelo v in vuelos)
            {
                if(v.id == id)
                {
                    v.fecha = fecha;
                    return true;
                }
            }
            return false;
        }

        public bool elminarVuelo(int id)
        {
            foreach (Vuelo v in vuelos)
            {
                if (v.id == id)
                {
                    v.origen.vuelosOrigen.Remove(v);
                    v.origen.vuelosDestino.Remove(v);
                    vuelos.Remove(v);
                    return true;
                }
            }
            return false;
        }

        //Crud ReservaHotel

        public bool agregarReservaHotel(Hotel hotel, Usuario usuario, DateTime fechaDesde, DateTime fechaHasta, double pagado, int cantPersonas)
        {
            reservasHotel.Add(new ReservaHotel(cantReservasHoteles, hotel, usuario, fechaDesde, fechaHasta, pagado, cantPersonas));

            if (usuario.misReservasHoteles == null)
            {
                usuario.misReservasHoteles = new List<ReservaHotel>();
            }

            usuario.misReservasHoteles.Add(new ReservaHotel(hotel, usuario, fechaDesde, fechaHasta, hotel.costo, cantPersonas));

            cantReservasHoteles++;
            return true;
        }

        public List<ReservaHotel> obtenerReservaHotel()
        {
            return reservasHotel.ToList();
        }

        public bool modificarReservaHotel(int id, Hotel hotel, Usuario usuario, string fechaDesde, string fechaHasta, double pagado, int cantParseadas)
        {
            foreach (ReservaHotel rh in reservasHotel)
            {
                if (rh.id == id)
                {

                    if (usuario.misReservasHoteles == null)
                    {
                        usuario.misReservasHoteles = new List<ReservaHotel>();
                    }

                    usuario.misReservasHoteles.Remove(rh);

                    rh.miHotel = hotel;
                    rh.miUsuario = usuario;
                    rh.fechaDesde = DateTime.Parse(fechaDesde);
                    rh.fechaHasta = DateTime.Parse(fechaHasta);
                    rh.pagado = pagado;
                    rh.cantPersonas = cantParseadas;

                    usuario.misReservasHoteles.Add(rh);

                    return true;
                }
            }
            return false;
        }

        public bool elminarReservaHotel(int id)
        {
            foreach (ReservaHotel rh in reservasHotel)
            {
                if (rh.id == id)
                {
                    rh.miUsuario.misReservasHoteles.Remove(rh);
                    reservasHotel.Remove(rh);
                    return true;
                }
            }
            return false;
        }

        //public void validarCapacidad(DateTime fechaDesde, DateTime fechaHasta)
        //{
        //    foreach (Hotel hotel in hoteles)
        //    {
        //        foreach (ReservaHotel rh in reservasHotel)
        //        {
        //            bool haySuperposicion = hoteles.Any(hotel => hotel.id == rh.miHotel.id && (fechaDesde >= rh.fechaDesde && fechaDesde <= rh.fechaHasta) || (fechaHasta >= rh.fechaDesde && fechaHasta <= rh.fechaHasta));

        //            if (haySuperposicion)
        //            {
                        
        //            }
        //        }
        //    }
        //}

        //Crud ReservaVuelo

        public bool agregarReservaVuelo(Vuelo vuelo, Usuario usuario, double pagado, int cantPersonas)
        {
            reservasVuelo.Add(new ReservaVuelo(cantReservasVuelos, vuelo, usuario, pagado, cantPersonas));

            if (usuario.misReservasVuelos == null)
            {
                usuario.misReservasVuelos = new List<ReservaVuelo>();
            }

            usuario.misReservasVuelos.Add(new ReservaVuelo(vuelo, usuario, vuelo.costo, cantPersonas));

            cantReservasVuelos++;
            return true;
        }

        public List<ReservaVuelo> obtenerReservaVuelo()
        {
            return reservasVuelo.ToList();
        }

        public bool modificarReservaVuelo(int id, Vuelo vuelo, Usuario usuario, double pagado, int cantPersonas)
        {
            foreach (ReservaVuelo rv in reservasVuelo)
            {
                if (rv.id == id)
                {

                    if (usuario.misReservasVuelos == null)
                    {
                        usuario.misReservasVuelos = new List<ReservaVuelo>();
                    }

                    usuario.misReservasVuelos.Remove(rv);

                    rv.miVuelo = vuelo;
                    rv.miUsuario = usuario;
                    rv.pagado = pagado;
                    rv.cantPersonas = cantPersonas;

                    usuario.misReservasVuelos.Add(rv);

                    return true;
                }
            }
            return false;
        }

        public bool elminarReservaVuelo(int id)
        {
            foreach (ReservaVuelo rv in reservasVuelo)
            {
                if (rv.id == id)
                {
                    rv.miUsuario.misReservasVuelos.Remove(rv);
                    reservasVuelo.Remove(rv);
                    return true;
                }
            }
            return false;
        }

        //Ciudades

        public List<Ciudad> obtenerCiudad()
        {
            return ciudades.ToList();
        }

        public bool agregarCiudad(string nombre)
        {
            ciudades.Add(new Ciudad(nombre));
            return true;
        }

    }
}
