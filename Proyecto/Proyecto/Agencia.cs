﻿using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private List<UsuarioHotel> usuarioHotel = new List<UsuarioHotel>();
        private List<UsuarioVuelo> usuarioVuelo = new List<UsuarioVuelo>();
        private int cantidadPersonasRH = 0;
        private int cantidadPersonasRV = 0;
        private Usuario? usuarioActual;
        private DAL DB;

        public Agencia()
        {
            DB = new DAL();
            inicializarAtributos();
        }

        private void inicializarAtributos()
        {
            usuarios = DB.inicializarUsuarios();
            hoteles = DB.inicializarHoteles();
            vuelos = DB.inicializarVuelos();
            reservasHotel = DB.inicializarReservasH();
            reservasVuelo = DB.inicializarReservasV();
            ciudades = DB.inicializarCiudades();
            usuarioHotel = DB.inicializarUsuarioHotel();
            usuarioVuelo = DB.inicializarUsuarioVuelo();

            foreach (Hotel h in hoteles)
            {
                foreach (Ciudad c in ciudades)
                {
                    if (c.id == h.ciudad_fk)
                    {
                        c.hoteles.Add(h);
                        h.ubicacion = c;
                    }
                }
            }

            foreach (Vuelo v in vuelos)
            {
                foreach (Ciudad c in ciudades)
                {
                    if (c.id == v.origen_fk)
                    {
                        c.vuelos.Add(v);
                        v.origen = c;
                    }

                    if (c.id == v.destino_fk)
                    {
                        c.vuelos.Add(v);
                        v.destino = c;
                    }
                }
            }

            foreach (ReservaHotel rh in reservasHotel)
            {
                foreach (Hotel h in hoteles)
                {
                    if (h.id == rh.hotel_fk)
                    {
                        h.misReservas.Add(rh);
                        rh.miHotel = h;
                    }
                }

                foreach (Usuario u in usuarios)
                {
                    if (u.id == rh.usuario_fk)
                    {
                        u.misReservasHoteles.Add(rh);
                        rh.miUsuario = u;
                    }
                }
            }

            foreach (ReservaVuelo rv in reservasVuelo)
            {
                foreach (Vuelo v in vuelos)
                {
                    if (v.id == rv.vuelo_fk)
                    {
                        v.misReservas.Add(rv);
                        rv.miVuelo = v;
                    }
                }

                foreach (Usuario u in usuarios)
                {
                    if (u.id == rv.usuario_fk)
                    {
                        u.misReservasVuelos.Add(rv);
                        rv.miUsuario = u;
                    }
                }
            }

            foreach (UsuarioHotel uh in usuarioHotel)
            {
                foreach (Hotel h in hoteles)
                {
                    foreach (Usuario u in usuarios)
                    {
                        if (uh.usuario_fk == u.id && uh.hotel_fk == h.id)
                        {
                            uh.usuario = u;
                            uh.hotel = h;
                            u.hotelesVisitados.Add(h);
                            u.usuario_hotel.Add(uh);
                            h.huespedes.Add(u);
                            h.hotel_usuario.Add(uh);
                        }
                    }
                }
            }

            foreach (UsuarioVuelo uv in usuarioVuelo)
            {
                foreach (Vuelo v in vuelos)
                {
                    foreach (Usuario u in usuarios)
                    {
                        if (uv.usuario_fk == u.id && uv.vuelo_fk == v.id)
                        {
                            uv.usuario = u;
                            uv.vuelo = v;
                            u.vuelosTomados.Add(v);
                            u.usuario_vuelo.Add(uv);
                            v.pasajeros.Add(u);
                            v.vuelo_usuario.Add(uv);
                        }
                    }
                }
            }

        }

        public bool agregarUsuario(int dni, string nombre, string apellido, string mail, string clave, double credito)
        {

            bool esValido = true;

            foreach (Usuario u in usuarios)
            {
                if (u.dni == dni)
                    esValido = false;
            }

            if (esValido)
            {
                int idNuevoUsuario;
                idNuevoUsuario = DB.agregarUsuario(dni, nombre, apellido, mail, clave, 0, false, false, credito);

                if (idNuevoUsuario != -1)
                {
                    Usuario nuevo = new Usuario(idNuevoUsuario, dni, nombre, apellido, mail, clave, false, credito);
                    usuarios.Add(nuevo);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool iniciarSesion(string mail, string clave)
        {
            foreach (Usuario u in usuarios)
            {
                if (u.mail.Equals(mail))
                {
                    if (u.bloqueado)
                    {
                        MessageBox.Show("El usuario está bloqueado. Contáctese con el administrador.");
                        return false;
                    }

                    if (u.clave.Equals(clave))
                    {
                        usuarioActual = u;
                        u.intentosFallidos = 0;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error, mail o contraseña incorrectos");
                        u.intentosFallidos++;
                        DB.modificarUsuario(u.id, u.dni, u.nombre, u.apellido, u.mail, u.credito, u.intentosFallidos, u.bloqueado);

                        if (u.intentosFallidos >= 3)
                        {
                            u.bloqueado = true;
                            DB.modificarUsuario(u.id, u.dni, u.nombre, u.apellido, u.mail, u.credito, u.intentosFallidos, u.bloqueado);
                            MessageBox.Show("El usuario ha sido bloqueado debido a múltiples intentos fallidos.");
                        }
                        return false;
                    }
                }
            }
            MessageBox.Show("Mail no registrado");
            return false;
        }

        public void cerrarSesion()
        {
            usuarioActual = null;
        }

        public bool modificarUsuario(int id, int dni, string nombre, string apellido, string mail, double credito, int intentosFallidos, bool bloqueado)
        {
            if (DB.modificarUsuario(id, dni, nombre, apellido, mail, credito, intentosFallidos, bloqueado) == 1)
            {
                try
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
                            u.intentosFallidos = intentosFallidos;
                            u.bloqueado = bloqueado;
                        }
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool eliminarUsuario(int id)
        {
            foreach (Usuario u in usuarios)
                if (u.id == id)
                {
                    if (DB.eliminarUsuario(id) == 1)
                    {
                        try
                        {
                            //ACA DEBERIA ELIMINAR SUS RESERVAS EN CASO DE QUE SEAN A FUTURO?
                            usuarios.Remove(u);
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
            return false;
        }

        public string nombreLogueado()
        {
            return usuarioActual.id.ToString() + ". " + usuarioActual.nombre;
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

        //Hoteles

        public List<Hotel> obtenerHoteles()
        {
            return hoteles.ToList();
        }

        public bool agregarHotel(string ciudad, int capacidad, double costo, string nombre)
        {
            Ciudad ubicacion = ciudades.FirstOrDefault(c => c.nombre == ciudad);

            int idNuevoHotel = DB.agregarHotel(ubicacion.id, capacidad, costo, nombre);

            if (idNuevoHotel != -1)
            {
                Hotel nuevo = new Hotel(idNuevoHotel, ubicacion, capacidad, costo, nombre);
                hoteles.Add(nuevo);
                ubicacion.hoteles.Add(nuevo);
                return true;
            }
            else
                return false;
        }

        public bool modificarHotel(int id, string ciudad, int capacidad, double costo, string nombre)
        {
            Ciudad nuevaUbicacion = ciudades.FirstOrDefault(c => c.nombre == ciudad);

            if (nuevaUbicacion == null)
            {
                MessageBox.Show("La ciudad no se encuentra disponible");
                return false;
            }

            foreach (Hotel h in hoteles)
            {
                if (h.id == id)
                {
                    if (capacidad < h.capacidad)
                    {
                        try
                        {
                            ReservaHotel reserva = h.misReservas.First(r => DateTime.Now < r.fechaDesde);

                            if (reserva != null)
                            {
                                MessageBox.Show("No se puede realizar la modificación porque\" & vbCrLf & \"" +
                                    "la capacidad ingresada es menor a la anterior\" & vbCrLf & \"" +
                                    "y el hotel tiene reservas a futuro que pueden\" & vbCrLf & \"" +
                                    "causar problemas con la disponibilidad.");
                                return false;
                            }
                        }
                        catch (InvalidOperationException) { }
                    }

                    if (DB.modificarHotel(id, nuevaUbicacion.id, capacidad, costo, nombre) == 1)
                    {
                        try
                        {
                            if (h.ubicacion != nuevaUbicacion)
                            {
                                h.ubicacion.hoteles.Remove(h);
                            }
                            else
                            {
                                h.ubicacion.hoteles.Add(h);
                            }

                            h.ubicacion = nuevaUbicacion;
                            h.capacidad = capacidad;
                            h.costo = costo;
                            h.nombre = nombre;

                            MessageBox.Show("Modificado con exito");
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
            }

            return false;
        }

        public bool elminarHotel(int id)
        {
            foreach (Hotel h in hoteles)
            {
                if (h.id == id)
                {
                    if (DB.eliminarHotel(id) == 1)
                    {
                        try
                        {
                            h.misReservas.ForEach(rh =>
                            {

                                if (DateTime.Now < rh.fechaDesde)
                                {
                                    rh.miUsuario.credito += rh.pagado;
                                    rh.miUsuario.misReservasHoteles.Remove(rh);
                                }
                                rh.miUsuario.hotelesVisitados.Remove(h);
                                h.ubicacion.hoteles.Remove(h);

                            });

                            hoteles.Remove(h);
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        //CRUD Vuelos
        public bool agregarVuelo(string origen, string destino, int capacidad, double costo, DateTime fecha, string aerolinea, string avion)
        {

            Ciudad ciudadO = ciudades.FirstOrDefault(c => c.nombre == origen);
            Ciudad ciudadD = ciudades.FirstOrDefault(c => c.nombre == destino);

            if (ciudadO == null || ciudadD == null)
            {
                MessageBox.Show("La ciudad no se encuentra disponible");
                return false;
            }

            int idNuevoVuelo = DB.agregarVuelo(ciudadO.id, ciudadD.id, capacidad, 0, costo, fecha, aerolinea, avion);

            if (idNuevoVuelo != -1)
            {
                Vuelo nuevo = new Vuelo(idNuevoVuelo, ciudadO, ciudadD, capacidad, costo, fecha, aerolinea, avion);
                vuelos.Add(nuevo);
                ciudadO.vuelos.Add(nuevo);
                ciudadD.vuelos.Add(nuevo);
                return true;
            }
            return false;
        }

        public List<Vuelo> obtenerVuelos()
        {
            return vuelos.ToList();
        }

        public bool modificarVuelo(int id, string origen, string destino, int capacidad, double costo, DateTime fecha, string aerolinea, string avion)
        {

            Ciudad ciudadO = ciudades.FirstOrDefault(c => c.nombre == origen);
            Ciudad ciudadD = ciudades.FirstOrDefault(c => c.nombre == destino);

            if (ciudadO == null || ciudadD == null)
            {
                MessageBox.Show("La ciudad no se encuentra disponible");
                return false;
            }

            foreach (Vuelo v in vuelos)
            {
                if (v.id == id)
                {
                    if (capacidad >= v.capacidad)
                    {
                        v.misReservas.ForEach(r =>
                        {

                            cantidadPersonasRV += r.cantPersonas;

                        });

                        if (capacidad < cantidadPersonasRV)
                        {
                            MessageBox.Show("No se puede realizar la modificacion\" & vbCrLf & \"" +
                                "debido a que la capacidad ingresada\" & vbCrLf & \"" +
                                "no es suficiente para satisfacer la\" & vbCrLf & \"" +
                                "cantidad de pasajeros que reservaron");
                            return false;
                        }
                        else
                        {

                            if (DB.modificarVuelo(id, capacidad, costo, fecha, aerolinea, avion, ciudadO.id, ciudadD.id) == 1)
                            {
                                if (v.origen != ciudadO)
                                {
                                    v.origen.vuelos.Remove(v);
                                }
                                else
                                {
                                    ciudadO.vuelos.Add(v);
                                }

                                if (v.destino != ciudadD)
                                {
                                    v.destino.vuelos.Remove(v);
                                }
                                else
                                {
                                    ciudadD.vuelos.Add(v);
                                }

                                v.origen = ciudadO;
                                v.destino = ciudadD;
                                v.capacidad = capacidad;
                                v.costo = costo;
                                v.fecha = fecha;
                                v.aerolinea = aerolinea;
                                v.avion = avion;

                                return true;
                            }

                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("La nueva capacidad excede a la anterior");
                        return false;
                    }
                }
            }
            MessageBox.Show("Vuelo no registrado");
            return false;

        }

        public bool elminarVuelo(int id)
        {
            if (DB.eliminarVuelo(id) == 1)
            {
                try
                {
                    foreach (Vuelo v in vuelos)
                    {
                        if (v.id == id)
                        {
                            v.misReservas.ForEach(rv =>
                            {

                                if (DateTime.Now < v.fecha)
                                {
                                    rv.miUsuario.credito += rv.pagado;
                                    rv.miUsuario.misReservasVuelos.Remove(rv);
                                }

                                rv.miUsuario.vuelosTomados.Remove(v);
                                v.origen.vuelos.Remove(v);

                            });

                            vuelos.Remove(v);
                            return true;
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        //Crud ReservaHotel

        public bool agregarReservaHotel(string hotel, string usuario, DateTime fechaDesde, DateTime fechaHasta, int cantPersonas)
        {

            string[] partes = usuario.Split(new string[] { ". " }, StringSplitOptions.None);
            int usuario_id = int.Parse(partes[0]);

            Usuario usuarioSeleccionado = usuarios.FirstOrDefault(u => u.id == usuario_id);
            Hotel hotelSeleccionado = hoteles.FirstOrDefault(h => h.nombre == hotel);

            TimeSpan diferencia = fechaHasta - fechaDesde;
            int cantidadDias = Math.Abs(diferencia.Days);

            double pago = cantPersonas * hotelSeleccionado.costo * cantidadDias;

            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("El usuario no se encuentra disponible");
                return false;
            }

            if (hotelSeleccionado == null)
            {
                MessageBox.Show("El hotel no se encuentra disponible");
                return false;
            }

            if (cantPersonas <= 0 || cantPersonas > 10)
            {
                MessageBox.Show("Cantidad de personas inválida");
                return false;
            }
            else if (usuarioSeleccionado.credito - pago < 0)
            {
                MessageBox.Show("Crédito insuficiente");
                return false;
            }
            else
            {
                hotelSeleccionado.misReservas.ForEach(r =>
                {
                    if (r.fechaDesde <= fechaHasta && r.fechaHasta >= fechaDesde)
                    {
                        cantidadPersonasRH += r.cantPersonas;
                    }
                });

                cantidadPersonasRH += cantPersonas;

                if (hotelSeleccionado.capacidad < cantidadPersonasRH)
                {
                    MessageBox.Show("El hotel no tiene disponibilidad para esa cantidad de personas");
                    return false;
                }

                int idNuevaReservaHotel = DB.agregarReservaHotel(fechaDesde, fechaHasta, pago, cantPersonas, hotelSeleccionado.id, usuario_id);

                if (idNuevaReservaHotel != -1)
                {
                    UsuarioHotel usuario_hotel = usuarioHotel.Where(uh => uh.usuario_fk == usuario_id && uh.hotel_fk == hotelSeleccionado.id).FirstOrDefault();

                    if (usuario_hotel == null)
                    {
                        UsuarioHotel usuarioH = new UsuarioHotel(usuario_id, hotelSeleccionado.id, 1);

                        usuarioHotel.Add(usuarioH);
                    }
                    else
                    {
                        usuario_hotel.cantidad++;
                    }

                    usuarioSeleccionado.credito -= pago;

                    ReservaHotel nuevaReserva = new ReservaHotel(idNuevaReservaHotel, hotelSeleccionado, usuarioSeleccionado, fechaDesde, fechaHasta, pago, cantPersonas);

                    reservasHotel.Add(nuevaReserva);
                    usuarioSeleccionado.misReservasHoteles.Add(nuevaReserva);
                    hotelSeleccionado.misReservas.Add(nuevaReserva);

                    return true;
                }

                return false;
            }
        }

        public List<ReservaHotel> obtenerReservaHotel()
        {
            return reservasHotel.ToList();
        }

        public bool modificarReservaHotel(int id, string hotel, string usuario, DateTime fechaDesde, DateTime fechaHasta, int cantidad)
        {

            string[] partes = usuario.Split(new string[] { ". " }, StringSplitOptions.None);
            int usuario_id = int.Parse(partes[0]);

            Usuario usuarioSeleccionado = usuarios.FirstOrDefault(u => u.id == usuario_id);
            Hotel hotelSeleccionado = hoteles.FirstOrDefault(h => h.nombre == hotel);

            TimeSpan diferencia = fechaHasta - fechaDesde;
            int cantidadDias = Math.Abs(diferencia.Days);

            double pago = cantidad * hotelSeleccionado.costo * cantidadDias;

            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("El usuario no se encuentra disponible");
                return false;
            }

            if (hotelSeleccionado == null)
            {
                MessageBox.Show("El hotel no se encuentra disponible");
                return false;
            }

            if (cantidad <= 0 || cantidad > 10)
            {
                MessageBox.Show("Cantidad de personas inválida");
                return false;
            }

            if (usuarioSeleccionado.credito - pago < 0)
            {
                MessageBox.Show("Crédito insuficiente");
                return false;
            }

            hotelSeleccionado.misReservas.ForEach(r =>
            {
                if (r.fechaDesde <= fechaHasta && r.fechaHasta >= fechaDesde)
                {
                    cantidadPersonasRH += r.cantPersonas;
                }
            });

            foreach (ReservaHotel rh in reservasHotel)
            {
                if (rh.id == id)
                {
                    cantidadPersonasRH -= rh.cantPersonas;
                    cantidadPersonasRH += cantidad;

                    if (hotelSeleccionado.capacidad >= cantidadPersonasRH)
                    {
                        if (DB.modificarReservaHotel(id, fechaDesde, fechaHasta, pago, cantidad, hotelSeleccionado.id, usuarioSeleccionado.id) == 1)
                        {
                            UsuarioHotel usuario_hotel = usuarioHotel.Where(uh => uh.usuario_fk == usuario_id && uh.hotel_fk == hotelSeleccionado.id).FirstOrDefault();

                            if (usuario_hotel == null)
                            {
                                if (DB.agregarUsuarioHotel(usuario_id, hotelSeleccionado.id, 1) == 1)
                                {
                                    usuario_hotel = new UsuarioHotel(usuario_id, hotelSeleccionado.id, 1);

                                    usuarioHotel.Add(usuario_hotel);
                                }
                            }
                            else
                            {
                                if (DB.modificarUsuarioHotel(usuario_id, hotelSeleccionado.id, usuario_hotel.cantidad) == 1)
                                {
                                    usuario_hotel.cantidad++;
                                }
                            }

                            if (hotelSeleccionado != rh.miHotel)
                            {
                                rh.miHotel.misReservas.Remove(rh);
                            }

                            if (usuarioSeleccionado != rh.miUsuario)
                            {
                                rh.miUsuario.credito += rh.pagado;
                                rh.miUsuario.misReservasHoteles.Remove(rh);
                            }

                            usuarioSeleccionado.credito += rh.pagado;
                            usuarioSeleccionado.credito -= pago;

                            hotelSeleccionado.misReservas.Remove(rh);
                            usuarioSeleccionado.misReservasHoteles.Remove(rh);

                            rh.miHotel = hotelSeleccionado;
                            rh.miUsuario = usuarioSeleccionado;
                            rh.fechaDesde = fechaDesde;
                            rh.fechaHasta = fechaHasta;
                            rh.pagado = pago;
                            rh.cantPersonas = cantidad;

                            hotelSeleccionado.misReservas.Add(rh);
                            usuarioSeleccionado.misReservasHoteles.Add(rh);
                            MessageBox.Show("Modificada con exito");

                            return true;
                        }

                        return false;
                    }
                    else
                    {
                        MessageBox.Show("El hotel no tiene disponibilidad para esa cantidad de personas");
                        return false;
                    }
                }
            }

            return false;
        }

        public bool elminarReservaHotel(int id)
        {
            if (DB.eliminarReservaHotel(id) == 1)
            {
                foreach (ReservaHotel rh in reservasHotel)
                {
                    if (rh.id == id)
                    {
                        if (DateTime.Now < rh.fechaDesde)
                        {
                            rh.miUsuario.credito += rh.pagado;
                        }

                        usuarioHotel.ForEach(uh =>
                        {
                            if (uh.usuario == rh.miUsuario && uh.hotel == rh.miHotel && uh.cantidad > 1)
                            {
                                uh.cantidad--;
                                DB.modificarUsuarioHotel(uh.usuario_fk, uh.hotel_fk, uh.cantidad);
                            }
                            else
                            {
                                if (DB.eliminarUsuarioHotel(uh.usuario_fk, uh.hotel_fk) == 1)
                                {
                                    usuarioHotel.Remove(uh);
                                }
                            }
                        });

                        rh.miUsuario.misReservasHoteles.Remove(rh);
                        rh.miHotel.misReservas.Remove(rh);
                        reservasHotel.Remove(rh);
                        return true;
                    }
                }
            }
            return false;
        }

        //Crud ReservaVuelo

        public bool agregarReservaVuelo(string origen, string destino, string usuario, int cantPersonas, DateTime fecha)
        {
            string[] partes = usuario.Split(new string[] { ". " }, StringSplitOptions.None);
            int usuario_id = int.Parse(partes[0]);

            Usuario usuarioSeleccionado = usuarios.FirstOrDefault(u => u.id == usuario_id);
            Vuelo vueloSeleccionado = vuelos.FirstOrDefault(v => v.origen.nombre == origen && v.destino.nombre == destino && v.fecha == fecha);

            double pago = cantPersonas * vueloSeleccionado.costo;

            if (vueloSeleccionado == null)
            {
                MessageBox.Show("El vuelo no se encuentra disponible");
                return false;
            }

            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("El usuario no se encuentra disponible");
                return false;
            }

            if (vueloSeleccionado.capacidad - (vueloSeleccionado.vendido + cantPersonas) < 0)
            {
                MessageBox.Show("La cantidad de personas ingresada excede la capacidad del vuelo");
                return false;
            }
            else if (cantPersonas <= 0 || cantPersonas > 10)
            {
                MessageBox.Show("Cantidad de personas inválida");
                return false;
            }
            else if (pago > usuarioSeleccionado.credito)
            {
                MessageBox.Show("Crédito insuficiente");
                return false;
            }
            else
            {
                int idNuevaReservaVuelo = DB.agregarReservaVuelo(pago, cantPersonas, vueloSeleccionado.id, usuario_id);

                if (idNuevaReservaVuelo != -1)
                {
                    UsuarioVuelo usuario_vuelo = usuarioVuelo.First(uv => uv.usuario_fk == usuario_id && uv.vuelo_fk == vueloSeleccionado.id);

                    if (usuario_vuelo == null)
                    {
                        if (DB.agregarUsuarioVuelo(usuario_id, vueloSeleccionado.id) == 1)
                        {
                            usuario_vuelo = new UsuarioVuelo(usuario_id, vueloSeleccionado.id);

                            usuarioVuelo.Add(usuario_vuelo);
                        }
                    }

                    vueloSeleccionado.vendido += cantPersonas;
                    usuarioSeleccionado.credito -= pago;

                    ReservaVuelo rv = new ReservaVuelo(idNuevaReservaVuelo, vueloSeleccionado, usuarioSeleccionado, pago, cantPersonas);

                    reservasVuelo.Add(rv);
                    usuarioSeleccionado.misReservasVuelos.Add(rv);
                    vueloSeleccionado.misReservas.Add(rv);

                    return true;
                }

                return false;
            }
        }

        public List<ReservaVuelo> obtenerReservaVuelo()
        {
            return reservasVuelo.ToList();
        }

        public bool modificarReservaVuelo(int id, string origen, string destino, string usuario, int cantPersonas, DateTime fecha)
        {
            foreach (ReservaVuelo rv in reservasVuelo)
            {
                if (rv.id == id)
                {
                    string[] partes = usuario.Split(new string[] { ". " }, StringSplitOptions.None);
                    int usuario_id = int.Parse(partes[0]);

                    try
                    {
                        Usuario usuarioSeleccionado = usuarios.First(u => u.id == usuario_id);
                        Vuelo vueloSeleccionado = vuelos.First(v => v.origen.nombre == origen && v.destino.nombre == destino && v.fecha == fecha);

                        double pago = vueloSeleccionado.costo * cantPersonas;

                        if (vueloSeleccionado == null)
                        {
                            MessageBox.Show("El vuelo no se encuentra disponible");
                            return false;
                        }
                        else if (usuarioSeleccionado == null)
                        {
                            MessageBox.Show("El usuario no se encuentra disponible");
                            return false;
                        }
                        else if (cantPersonas <= 0 || cantPersonas > 10)
                        {
                            MessageBox.Show("Cantidad de personas inválida");
                            return false;
                        }
                        else if (pago > usuarioSeleccionado.credito)
                        {
                            MessageBox.Show("Crédito insuficiente");
                            return false;
                        }
                        else if (vueloSeleccionado.capacidad - (vueloSeleccionado.vendido + cantPersonas) < 0)
                        {
                            MessageBox.Show("La cantidad de personas ingresada excede la capacidad del vuelo");
                            return false;
                        }
                        else
                        {
                            if (DB.modificarReservaVuelo(id, pago, cantPersonas, vueloSeleccionado.id, usuario_id) == 1)
                            {
                                UsuarioVuelo usuario_vuelo = usuarioVuelo.First(uh => uh.usuario_fk == usuario_id && uh.vuelo_fk == vueloSeleccionado.id);

                                if (usuario_vuelo == null)
                                {
                                    if (DB.agregarUsuarioVuelo(usuario_id, vueloSeleccionado.id) == 1)
                                    {
                                        usuario_vuelo = new UsuarioVuelo(usuario_id, vueloSeleccionado.id);

                                        usuarioVuelo.Add(usuario_vuelo);
                                    }
                                }

                                if (vueloSeleccionado != rv.miVuelo)
                                {
                                    rv.miVuelo.capacidad += rv.cantPersonas;
                                    rv.miVuelo.misReservas.Remove(rv);
                                }

                                if (usuarioSeleccionado != rv.miUsuario)
                                {
                                    rv.miUsuario.credito += rv.pagado;
                                    rv.miUsuario.misReservasVuelos.Remove(rv);
                                }

                                vueloSeleccionado.vendido += rv.cantPersonas;
                                vueloSeleccionado.vendido -= cantPersonas;

                                usuarioSeleccionado.credito += rv.pagado;
                                usuarioSeleccionado.credito -= pago;

                                usuarioSeleccionado.misReservasVuelos.Remove(rv);
                                vueloSeleccionado.misReservas.Remove(rv);

                                rv.miVuelo = vueloSeleccionado;
                                rv.miUsuario = usuarioSeleccionado;
                                rv.pagado = pago;
                                rv.cantPersonas = cantPersonas;

                                usuarioSeleccionado.misReservasVuelos.Add(rv);
                                vueloSeleccionado.misReservas.Add(rv);

                                MessageBox.Show("Modificada con exito");

                                return true;
                            }
                            return false;
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show("El vuelo ingresado no existe");
                        return false;
                    }
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
                    if (DB.eliminarReservaVuelo(id) == 1)
                    {
                        if (DateTime.Now < rv.miVuelo.fecha)
                        {
                            rv.miUsuario.credito += rv.pagado;
                        }

                        usuarioVuelo.ForEach(uv =>
                        {
                            if (uv.usuario == rv.miUsuario && uv.vuelo == rv.miVuelo)
                            {
                                if (DB.eliminarUsuarioVuelo(uv.usuario_fk, uv.vuelo_fk) == 1)
                                {
                                    usuarioVuelo.Remove(uv);
                                }
                            }
                        });

                        rv.miUsuario.misReservasVuelos.Remove(rv);
                        rv.miVuelo.misReservas.Remove(rv);
                        reservasVuelo.Remove(rv);
                        return true;
                    }
                }
            }
            return false;
        }

        public List<UsuarioHotel> obtenerUsuariosHoteles()
        {
            return usuarioHotel;
        }

        public List<UsuarioVuelo> obtenerUsuariosVuelos()
        {
            return usuarioVuelo;
        }

        //Ciudades

        public List<Ciudad> obtenerCiudades()
        {
            return ciudades;
        }

        //  ***Esto puede servir despues :)***
        //public int obtenerCiudadId(string ciudad)
        //{
        //    Ciudad ciudadEncontrada = ciudades.FirstOrDefault(c => c.nombre == ciudad);

        //    if (ciudadEncontrada != null)
        //    {
        //        return ciudadEncontrada.id;
        //    }

        //    return -1;
        //}

        public bool agregarCiudad(string nombre)
        {
            ciudades.Add(new Ciudad(nombre));
            return true;
        }


        //ESTO ERA PARA QUE CUANDO SE ELIGE UN ORIGEN EN RESERVA VUELO,
        //LOS DESTINOS QUE SE MUESTREN EN EL COMBO BOX DE AL LADO SEAN EN FUNCION
        //DE VUELOS QUE ESTAN ASOCIADOS AL ORIGEN INGRESADO Y VICEVERSA

        //public List<string> obtenerDestinosAsociadosAlOrigen(string origen)
        //{

        //    if (origen == "")
        //    {
        //        List<string> listaCiudades = new List<string>();
        //        listaCiudades.Add("");

        //        foreach (Ciudad c in ciudades)
        //        {
        //            listaCiudades.Add(c.nombre);
        //        }
        //        return listaCiudades;
        //    }

        //    List<string> destinos = new List<string>();

        //    foreach (Vuelo vuelo in vuelos)
        //    {
        //        if (vuelo.origen.nombre == origen)
        //        {
        //            destinos.Add(vuelo.destino.nombre);
        //        }
        //    }

        //    return destinos;
        //}

        //public List<string> obtenerOrigenesAsociadosAlDestino(string destino)
        //{

        //    if (destino == "")
        //    {
        //        List<string> listaCiudades = new List<string>();
        //        listaCiudades.Add("");

        //        foreach (Ciudad c in ciudades)
        //        {
        //            listaCiudades.Add(c.nombre);
        //        }
        //        return listaCiudades;
        //    }

        //    List<string> origenes = new List<string>();

        //    foreach (Vuelo vuelo in vuelos)
        //    {
        //        if (vuelo.destino.nombre == destino)
        //        {
        //            origenes.Add(vuelo.origen.nombre);
        //        }
        //    }

        //    return origenes;
        //}

    }
}
