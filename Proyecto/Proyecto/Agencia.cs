using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Proyecto
{
    public class Agencia
    {

        private int cantidadPersonasRH = 0;
        private int cantidadPersonasRV = 0;
        private Usuario? usuarioActual;

        private Context contexto;

        public Agencia()
        {
            inicializarAtributos();
        }

        private void inicializarAtributos()
        {
            try
            {
                contexto = new Context();

                contexto.usuarios
                    .Include(u => u.misReservasHoteles)
                    .Include(u => u.misReservasVuelos)
                    .Include(u => u.hotelesVisitados)
                    .Include(u => u.vuelosTomados)
                    .Include(u => u.usuario_hotel)
                    .Include(u => u.usuario_vuelo)
                    .Load();
                contexto.hoteles
                    .Include(h => h.huespedes)
                    .Include(h => h.misReservas)
                    .Include(h => h.hotel_usuario)
                    .Load();
                contexto.vuelos
                    .Include(v => v.pasajeros)
                    .Include(v => v.misReservas)
                    .Include(v => v.vuelo_usuario)
                    .Load();
                contexto.ciudades
                    .Include(c => c.hoteles)
                    .Include(c => c.vuelos)
                    .Load();
                contexto.reservasHotel.Load();
                contexto.reservasVuelo.Load();
                contexto.usuarioHotel.Load();
                contexto.usuarioVuelo.Load();
            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error.");
            }
        }

        public bool agregarUsuario(int dni, string nombre, string apellido, string mail, string clave, double credito)
        {

            bool esValido = !contexto.usuarios.Any(u => u.dni == dni);

            if (esValido)
            {
                Usuario nuevo = new Usuario(dni, nombre, apellido, mail, clave, 0, false, credito, false);
                contexto.usuarios.Add(nuevo);
                contexto.SaveChanges();
                return true;
            }
            else
            {
                MessageBox.Show("Error, ese dni ya esta usado.");
                return false;
            }
        }

        public bool iniciarSesion(string mail, string clave)
        {
            var usuario = contexto.usuarios.FirstOrDefault(u => u.mail.Equals(mail));

            if (usuario != null)
            {
                if (usuario.bloqueado)
                {
                    MessageBox.Show("El usuario está bloqueado. Contáctese con el administrador.");
                    return false;
                }

                if (usuario.clave.Equals(clave))
                {
                    usuarioActual = usuario;
                    usuario.intentosFallidos = 0;
                    return true;
                }
                else
                {
                    MessageBox.Show("Error, mail o contraseña incorrectos");
                    usuario.intentosFallidos++;
                    contexto.usuarios.Update(usuario);
                    contexto.SaveChanges();

                    if (usuario.intentosFallidos >= 3)
                    {
                        usuario.bloqueado = true;
                        contexto.usuarios.Update(usuario);
                        contexto.SaveChanges();
                        MessageBox.Show("El usuario ha sido bloqueado debido a múltiples intentos fallidos.");
                    }
                    return false;
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
            var usuario = contexto.usuarios.FirstOrDefault(u => u.id.Equals(id));

            if (usuario != null)
            {
                usuario.dni = dni;
                usuario.nombre = nombre;
                usuario.apellido = apellido;
                usuario.mail = mail;
                usuario.credito = credito;
                usuario.intentosFallidos = intentosFallidos;
                usuario.bloqueado = bloqueado;
                contexto.usuarios.Update(usuario);
                contexto.SaveChanges();
                return true;
            }

            return false;
        }

        public bool eliminarUsuario(int id)
        {
            var usuario = contexto.usuarios.FirstOrDefault(u => u.id.Equals(id));

            if (usuario != null)
            {
                try
                {
                    contexto.usuarios.Remove(usuario);
                    contexto.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
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
            return contexto.usuarios.ToList();
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
            return contexto.hoteles.ToList();
        }

        public bool agregarHotel(string ciudad, int capacidad, double costo, string nombre)
        {
            Ciudad ubicacion = contexto.ciudades.FirstOrDefault(c => c.nombre == ciudad);

            Hotel nuevo = new Hotel(nombre, capacidad, costo, ubicacion.id, ubicacion);

            ubicacion.hoteles.Add(nuevo);

            contexto.hoteles.Add(nuevo);
            contexto.ciudades.Update(ubicacion);
            contexto.SaveChanges();

            return true;
        }

        public bool modificarHotel(int id, string ciudad, int capacidad, double costo, string nombre)
        {
            Ciudad nuevaUbicacion = contexto.ciudades.FirstOrDefault(c => c.nombre == ciudad);

            if (nuevaUbicacion == null)
            {
                MessageBox.Show("La ciudad no se encuentra disponible");
                return false;
            }

            var hotel = contexto.hoteles.FirstOrDefault(h => h.id.Equals(id));

            if (hotel != null)
            {
                if (capacidad < hotel.capacidad)
                {
                    try
                    {
                        ReservaHotel reserva = hotel.misReservas.First(r => DateTime.Now < r.fechaDesde);

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

                try
                {
                    if (hotel.ubicacion != nuevaUbicacion)
                    {
                        hotel.ubicacion.hoteles.Remove(hotel);
                        contexto.ciudades.Update(hotel.ubicacion);
                    }
                    else
                    {
                        hotel.ubicacion.hoteles.Add(hotel);
                        contexto.ciudades.Update(hotel.ubicacion);
                    }

                    hotel.ubicacion = nuevaUbicacion;
                    hotel.capacidad = capacidad;
                    hotel.costo = costo;
                    hotel.nombre = nombre;

                    contexto.hoteles.Update(hotel);
                    contexto.SaveChanges();

                    MessageBox.Show("Modificado con exito");
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }

            return false;
        }

        public bool elminarHotel(int id)
        {
            var hotel = contexto.hoteles.FirstOrDefault(h => h.id.Equals(id));

            if (hotel != null)
            {
                try
                {
                    hotel.misReservas.ForEach(rh =>
                    {
                        if (DateTime.Now < rh.fechaDesde)
                        {
                            rh.miUsuario.credito += rh.pagado;
                            rh.miUsuario.misReservasHoteles.Remove(rh);
                        }

                        rh.miUsuario.hotelesVisitados.Remove(hotel);
                        contexto.usuarios.Update(rh.miUsuario);

                        hotel.ubicacion.hoteles.Remove(hotel);
                        contexto.ciudades.Update(hotel.ubicacion);
                    });

                    contexto.hoteles.Remove(hotel);
                    contexto.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        //CRUD Vuelos
        public bool agregarVuelo(string origen, string destino, int capacidad, double costo, DateTime fecha, string aerolinea, string avion)
        {

            Ciudad ciudadO = contexto.ciudades.FirstOrDefault(c => c.nombre == origen);
            Ciudad ciudadD = contexto.ciudades.FirstOrDefault(c => c.nombre == destino);

            if (ciudadO == null || ciudadD == null)
            {
                MessageBox.Show("La ciudad no se encuentra disponible");
                return false;
            }

            Vuelo nuevo = new Vuelo(ciudadO, ciudadD, capacidad, 0, costo, fecha, aerolinea, avion, ciudadO.id, ciudadD.id);

            ciudadO.vuelos.Add(nuevo);
            ciudadD.vuelos.Add(nuevo);

            contexto.ciudades.Update(ciudadO);
            contexto.ciudades.Update(ciudadD);
            contexto.vuelos.Add(nuevo);
            contexto.SaveChanges();

            return true;
        }

        public List<Vuelo> obtenerVuelos()
        {
            return contexto.vuelos.ToList();
        }

        public bool modificarVuelo(int id, string origen, string destino, int capacidad, double costo, DateTime fecha, string aerolinea, string avion)
        {

            Ciudad ciudadO = contexto.ciudades.FirstOrDefault(c => c.nombre == origen);
            Ciudad ciudadD = contexto.ciudades.FirstOrDefault(c => c.nombre == destino);

            if (ciudadO == null || ciudadD == null)
            {
                MessageBox.Show("La ciudad no se encuentra disponible");
                return false;
            }

            var vuelo = contexto.vuelos.FirstOrDefault(v => v.id.Equals(id));

            if (vuelo != null)
            {
                if (capacidad >= vuelo.capacidad)
                {
                    vuelo.misReservas.ForEach(r =>
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
                        if (vuelo.origen != ciudadO)
                        {
                            vuelo.origen.vuelos.Remove(vuelo);
                            contexto.ciudades.Update(vuelo.origen);
                        }
                        else
                        {
                            ciudadO.vuelos.Add(vuelo);
                            contexto.ciudades.Update(ciudadO);
                        }

                        if (vuelo.destino != ciudadD)
                        {
                            vuelo.destino.vuelos.Remove(vuelo);
                            contexto.ciudades.Update(vuelo.destino);
                        }
                        else
                        {
                            ciudadD.vuelos.Add(vuelo);
                            contexto.ciudades.Update(ciudadD);
                        }

                        vuelo.origen = ciudadO;
                        vuelo.destino = ciudadD;
                        vuelo.capacidad = capacidad;
                        vuelo.costo = costo;
                        vuelo.fecha = fecha;
                        vuelo.aerolinea = aerolinea;
                        vuelo.avion = avion;

                        contexto.vuelos.Update(vuelo);
                        contexto.SaveChanges();

                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("La nueva capacidad excede a la anterior");
                    return false;
                }
            }

            MessageBox.Show("Vuelo no registrado");
            return false;
        }

        public bool eliminarVuelo(int id)
        {
            try
            {
                var vuelo = contexto.vuelos.FirstOrDefault(v => v.id.Equals(id));

                if (vuelo != null)
                {
                    vuelo.misReservas.ForEach(rv =>
                    {
                        if (DateTime.Now < vuelo.fecha)
                        {
                            rv.miUsuario.credito += rv.pagado;
                            rv.miUsuario.misReservasVuelos.Remove(rv);
                        }

                        rv.miUsuario.vuelosTomados.Remove(vuelo);
                        contexto.usuarios.Update(rv.miUsuario);

                        vuelo.origen.vuelos.Remove(vuelo);
                        contexto.ciudades.Update(vuelo.origen);

                        vuelo.destino.vuelos.Remove(vuelo);
                        contexto.ciudades.Update(vuelo.destino);
                    });

                    contexto.vuelos.Remove(vuelo);
                    contexto.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Crud ReservaHotel

        public bool agregarReservaHotel(string hotel, string usuario, DateTime fechaDesde, DateTime fechaHasta, int cantPersonas)
        {

            string[] partes = usuario.Split(new string[] { ". " }, StringSplitOptions.None);
            int usuario_id = int.Parse(partes[0]);

            Usuario usuarioSeleccionado = contexto.usuarios.FirstOrDefault(u => u.id == usuario_id);
            Hotel hotelSeleccionado = contexto.hoteles.FirstOrDefault(h => h.nombre == hotel);

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

                UsuarioHotel usuario_hotel = contexto.usuarioHotel.Where(uh => uh.usuario_fk == usuario_id && uh.hotel_fk == hotelSeleccionado.id).FirstOrDefault();

                if (usuario_hotel == null)
                {
                    usuario_hotel = new UsuarioHotel(usuario_id, hotelSeleccionado.id, 1);

                    contexto.usuarioHotel.Add(usuario_hotel);
                }
                else
                {
                    usuario_hotel.cantidad++;

                    contexto.usuarioHotel.Update(usuario_hotel);
                }

                usuarioSeleccionado.credito -= pago;

                ReservaHotel nuevaReserva = new ReservaHotel(hotelSeleccionado, usuarioSeleccionado, fechaDesde, fechaHasta, pago, cantPersonas, hotelSeleccionado.id, usuarioSeleccionado.id);

                usuarioSeleccionado.misReservasHoteles.Add(nuevaReserva);
                contexto.usuarios.Update(usuarioSeleccionado);

                hotelSeleccionado.misReservas.Add(nuevaReserva);
                contexto.hoteles.Update(hotelSeleccionado);

                contexto.reservasHotel.Add(nuevaReserva);
                contexto.SaveChanges();

                return true;
            }
        }

        public List<ReservaHotel> obtenerReservaHotel()
        {
            return contexto.reservasHotel.ToList();
        }

        public bool modificarReservaHotel(int id, string hotel, string usuario, DateTime fechaDesde, DateTime fechaHasta, int cantidad)
        {

            string[] partes = usuario.Split(new string[] { ". " }, StringSplitOptions.None);
            int usuario_id = int.Parse(partes[0]);

            Usuario usuarioSeleccionado = contexto.usuarios.FirstOrDefault(u => u.id == usuario_id);
            Hotel hotelSeleccionado = contexto.hoteles.FirstOrDefault(h => h.nombre == hotel);

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
                if (r.fechaDesde <= fechaHasta && r.fechaHasta >= fechaDesde && r.id != id)
                {
                    cantidadPersonasRH += r.cantPersonas;
                }
            });

            cantidadPersonasRH += cantidad;

            if (hotelSeleccionado.capacidad < cantidadPersonasRH)
            {
                MessageBox.Show("El hotel no tiene disponibilidad para esa cantidad de personas");
                return false;
            }

            var reservaHotel = contexto.reservasHotel.FirstOrDefault(rh => rh.id.Equals(id));

            if (reservaHotel != null)
            {
                cantidadPersonasRH -= reservaHotel.cantPersonas;
                cantidadPersonasRH += cantidad;

                if (hotelSeleccionado.capacidad >= cantidadPersonasRH)
                {
                    UsuarioHotel usuario_hotel = contexto.usuarioHotel.Where(uh => uh.usuario_fk == usuario_id && uh.hotel_fk == hotelSeleccionado.id).FirstOrDefault();

                    if (usuario_hotel == null)
                    {
                        usuario_hotel = new UsuarioHotel(usuario_id, hotelSeleccionado.id, 1);

                        contexto.usuarioHotel.Add(usuario_hotel);
                    }
                    else
                    {
                        usuario_hotel.cantidad++;

                        contexto.usuarioHotel.Update(usuario_hotel);
                    }

                    if (hotelSeleccionado != reservaHotel.miHotel)
                    {
                        reservaHotel.miHotel.misReservas.Remove(reservaHotel);

                        contexto.hoteles.Update(reservaHotel.miHotel);
                    }

                    if (usuarioSeleccionado != reservaHotel.miUsuario)
                    {
                        reservaHotel.miUsuario.credito += reservaHotel.pagado;
                        reservaHotel.miUsuario.misReservasHoteles.Remove(reservaHotel);

                        contexto.usuarios.Update(reservaHotel.miUsuario);
                    }

                    usuarioSeleccionado.credito += reservaHotel.pagado;
                    usuarioSeleccionado.credito -= pago;

                    hotelSeleccionado.misReservas.Remove(reservaHotel);
                    usuarioSeleccionado.misReservasHoteles.Remove(reservaHotel);

                    reservaHotel.miHotel = hotelSeleccionado;
                    reservaHotel.miUsuario = usuarioSeleccionado;
                    reservaHotel.fechaDesde = fechaDesde;
                    reservaHotel.fechaHasta = fechaHasta;
                    reservaHotel.pagado = pago;
                    reservaHotel.cantPersonas = cantidad;

                    hotelSeleccionado.misReservas.Add(reservaHotel);
                    contexto.hoteles.Update(hotelSeleccionado);

                    usuarioSeleccionado.misReservasHoteles.Add(reservaHotel);
                    contexto.usuarios.Update(usuarioSeleccionado);

                    contexto.reservasHotel.Update(reservaHotel);
                    contexto.SaveChanges();

                    MessageBox.Show("Modificada con exito");

                    return true;
                }
                else
                {
                    MessageBox.Show("El hotel no tiene disponibilidad para esa cantidad de personas");
                    return false;
                }
            }

            return false;
        }

        public bool eliminarReservaHotel(int id)
        {
            var reservaHotel = contexto.reservasHotel.FirstOrDefault(rh => rh.id.Equals(id));

            if (reservaHotel != null)
            {
                if (DateTime.Now < reservaHotel.fechaDesde)
                {
                    reservaHotel.miUsuario.credito += reservaHotel.pagado;
                }

                var usuarioHotel = contexto.usuarioHotel.FirstOrDefault(uh => uh.usuario == reservaHotel.miUsuario && uh.hotel == reservaHotel.miHotel);

                if (usuarioHotel != null)
                {
                    if (usuarioHotel.cantidad > 1)
                    {
                        usuarioHotel.cantidad--;
                        contexto.usuarioHotel.Update(usuarioHotel);
                    }
                    else
                    {
                        contexto.usuarioHotel.Remove(usuarioHotel);
                    }
                }

                reservaHotel.miUsuario.misReservasHoteles.Remove(reservaHotel);
                contexto.usuarios.Update(reservaHotel.miUsuario);

                reservaHotel.miHotel.misReservas.Remove(reservaHotel);
                contexto.hoteles.Update(reservaHotel.miHotel);

                contexto.reservasHotel.Remove(reservaHotel);
                contexto.SaveChanges();

                return true;
            }

            return false;
        }

        //Crud ReservaVuelo

        public bool agregarReservaVuelo(string origen, string destino, string usuario, int cantPersonas, DateTime fecha)
        {
            string[] partes = usuario.Split(new string[] { ". " }, StringSplitOptions.None);
            int usuario_id = int.Parse(partes[0]);

            Usuario usuarioSeleccionado = contexto.usuarios.FirstOrDefault(u => u.id == usuario_id);
            Vuelo vueloSeleccionado = contexto.vuelos.FirstOrDefault(v => v.origen.nombre == origen && v.destino.nombre == destino && v.fecha == fecha);

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
                UsuarioVuelo usuario_vuelo = contexto.usuarioVuelo.First(uv => uv.usuario_fk == usuario_id && uv.vuelo_fk == vueloSeleccionado.id);

                if (usuario_vuelo == null)
                {
                    usuario_vuelo = new UsuarioVuelo(usuario_id, vueloSeleccionado.id);

                    contexto.usuarioVuelo.Add(usuario_vuelo);
                }

                vueloSeleccionado.vendido += cantPersonas;
                usuarioSeleccionado.credito -= pago;

                ReservaVuelo rv = new ReservaVuelo(vueloSeleccionado, usuarioSeleccionado, pago, cantPersonas, vueloSeleccionado.id, usuarioSeleccionado.id);

                usuarioSeleccionado.misReservasVuelos.Add(rv);
                contexto.usuarios.Update(usuarioSeleccionado);

                vueloSeleccionado.misReservas.Add(rv);
                contexto.vuelos.Update(vueloSeleccionado);

                contexto.reservasVuelo.Add(rv);
                contexto.SaveChanges();

                return true;
            }
        }

        public List<ReservaVuelo> obtenerReservaVuelo()
        {
            return contexto.reservasVuelo.ToList();
        }

        public bool modificarReservaVuelo(int id, string origen, string destino, string usuario, int cantPersonas, DateTime fecha)
        {
            string[] partes = usuario.Split(new string[] { ". " }, StringSplitOptions.None);
            int usuario_id = int.Parse(partes[0]);

            try
            {
                Usuario usuarioSeleccionado = contexto.usuarios.First(u => u.id == usuario_id);
                Vuelo vueloSeleccionado = contexto.vuelos.First(v => v.origen.nombre == origen && v.destino.nombre == destino && v.fecha == fecha);

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
                    var reservaVuelo = contexto.reservasVuelo.FirstOrDefault(rv => rv.id.Equals(id));

                    if (reservaVuelo != null)
                    {
                        UsuarioVuelo usuario_vuelo = contexto.usuarioVuelo.First(uh => uh.usuario_fk == usuario_id && uh.vuelo_fk == vueloSeleccionado.id);

                        if (usuario_vuelo == null)
                        {
                            usuario_vuelo = new UsuarioVuelo(usuario_id, vueloSeleccionado.id);

                            contexto.usuarioVuelo.Add(usuario_vuelo);
                        }

                        if (vueloSeleccionado != reservaVuelo.miVuelo)
                        {
                            reservaVuelo.miVuelo.capacidad += reservaVuelo.cantPersonas;
                            reservaVuelo.miVuelo.misReservas.Remove(reservaVuelo);

                            contexto.vuelos.Update(reservaVuelo.miVuelo);
                        }

                        if (usuarioSeleccionado != reservaVuelo.miUsuario)
                        {
                            reservaVuelo.miUsuario.credito += reservaVuelo.pagado;
                            reservaVuelo.miUsuario.misReservasVuelos.Remove(reservaVuelo);

                            contexto.usuarios.Update(reservaVuelo.miUsuario);
                        }

                        vueloSeleccionado.vendido += reservaVuelo.cantPersonas;
                        vueloSeleccionado.vendido -= cantPersonas;

                        usuarioSeleccionado.credito += reservaVuelo.pagado;
                        usuarioSeleccionado.credito -= pago;

                        usuarioSeleccionado.misReservasVuelos.Remove(reservaVuelo);
                        vueloSeleccionado.misReservas.Remove(reservaVuelo);

                        reservaVuelo.miVuelo = vueloSeleccionado;
                        reservaVuelo.miUsuario = usuarioSeleccionado;
                        reservaVuelo.pagado = pago;
                        reservaVuelo.cantPersonas = cantPersonas;

                        usuarioSeleccionado.misReservasVuelos.Add(reservaVuelo);
                        contexto.usuarios.Update(usuarioSeleccionado);

                        vueloSeleccionado.misReservas.Add(reservaVuelo);
                        contexto.vuelos.Update(vueloSeleccionado);

                        contexto.reservasVuelo.Update(reservaVuelo);
                        contexto.SaveChanges();

                        MessageBox.Show("Modificada con exito");

                        return true;
                    }
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("El vuelo ingresado no existe");
                return false;
            }

            return false;
        }

        public bool eliminarReservaVuelo(int id)
        {
            var reservaVuelo = contexto.reservasVuelo.FirstOrDefault(rv => rv.id.Equals(id));

            if (reservaVuelo != null)
            {
                if (DateTime.Now < reservaVuelo.miVuelo.fecha)
                {
                    reservaVuelo.miUsuario.credito += reservaVuelo.pagado;
                }

                var usuarioVuelo = contexto.usuarioVuelo.FirstOrDefault( uv => uv.usuario == reservaVuelo.miUsuario && uv.vuelo == reservaVuelo.miVuelo);

                if (usuarioVuelo != null)
                {
                    contexto.usuarioVuelo.Remove(usuarioVuelo);
                }

                reservaVuelo.miUsuario.misReservasVuelos.Remove(reservaVuelo);
                contexto.usuarios.Update(reservaVuelo.miUsuario);

                reservaVuelo.miVuelo.misReservas.Remove(reservaVuelo);
                contexto.vuelos.Update(reservaVuelo.miVuelo);

                contexto.reservasVuelo.Remove(reservaVuelo);
                contexto.SaveChanges();

                return true;
            }

            return false;
        }

        public List<UsuarioHotel> obtenerUsuariosHoteles()
        {
            return contexto.usuarioHotel.ToList();
        }

        public List<UsuarioVuelo> obtenerUsuariosVuelos()
        {
            return contexto.usuarioVuelo.ToList();
        }

        //Ciudades

        public List<Ciudad> obtenerCiudades()
        {
            return contexto.ciudades.ToList();
        }

        public void cerrar()
        {
            contexto.Dispose();
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
