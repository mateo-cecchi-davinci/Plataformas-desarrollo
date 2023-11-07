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

            bool esValido = true;

            foreach (Usuario u in contexto.usuarios)
            {
                if (u.dni == dni)
                    esValido = false;
            }

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
            foreach (Usuario u in contexto.usuarios)
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
                        contexto.usuarios.Update(u);
                        contexto.SaveChanges();

                        if (u.intentosFallidos >= 3)
                        {
                            u.bloqueado = true;
                            contexto.usuarios.Update(u);
                            contexto.SaveChanges();
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
            try
            {
                foreach (Usuario u in contexto.usuarios)
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
                        contexto.usuarios.Update(u);
                        contexto.SaveChanges();
                    }
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool eliminarUsuario(int id)
        {
            foreach (Usuario u in contexto.usuarios)
                if (u.id == id)
                {
                    try
                    {
                        contexto.usuarios.Remove(u);
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

            foreach (Hotel h in contexto.hoteles)
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

                    try
                    {
                        if (h.ubicacion != nuevaUbicacion)
                        {
                            h.ubicacion.hoteles.Remove(h);
                            contexto.ciudades.Update(h.ubicacion);
                        }
                        else
                        {
                            h.ubicacion.hoteles.Add(h);
                            contexto.ciudades.Update(h.ubicacion);
                        }

                        h.ubicacion = nuevaUbicacion;
                        h.capacidad = capacidad;
                        h.costo = costo;
                        h.nombre = nombre;

                        contexto.hoteles.Update(h);
                        contexto.SaveChanges();

                        MessageBox.Show("Modificado con exito");
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

        public bool elminarHotel(int id)
        {
            foreach (Hotel h in contexto.hoteles)
            {
                if (h.id == id)
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
                            contexto.usuarios.Update(rh.miUsuario);

                            h.ubicacion.hoteles.Remove(h);
                            contexto.ciudades.Update(h.ubicacion);
                        });

                        contexto.hoteles.Remove(h);
                        contexto.SaveChanges();

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

            foreach (Vuelo v in contexto.vuelos)
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
                            if (v.origen != ciudadO)
                            {
                                v.origen.vuelos.Remove(v);
                                contexto.ciudades.Update(v.origen);
                            }
                            else
                            {
                                ciudadO.vuelos.Add(v);
                                contexto.ciudades.Update(ciudadO);
                            }

                            if (v.destino != ciudadD)
                            {
                                v.destino.vuelos.Remove(v);
                                contexto.ciudades.Update(v.destino);
                            }
                            else
                            {
                                ciudadD.vuelos.Add(v);
                                contexto.ciudades.Update(ciudadD);
                            }

                            v.origen = ciudadO;
                            v.destino = ciudadD;
                            v.capacidad = capacidad;
                            v.costo = costo;
                            v.fecha = fecha;
                            v.aerolinea = aerolinea;
                            v.avion = avion;

                            contexto.vuelos.Update(v);
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
            }
            MessageBox.Show("Vuelo no registrado");
            return false;

        }

        public bool elminarVuelo(int id)
        {
            try
            {
                foreach (Vuelo v in contexto.vuelos)
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
                            contexto.usuarios.Update(rv.miUsuario);

                            v.origen.vuelos.Remove(v);
                            contexto.ciudades.Update(v.origen);
                        });

                        contexto.vuelos.Remove(v);
                        contexto.SaveChanges();

                        return true;
                    }
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
            //Esto esa mal
            hotelSeleccionado.misReservas.ForEach(r =>
            {
                if (r.fechaDesde <= fechaHasta && r.fechaHasta >= fechaDesde)
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

            foreach (ReservaHotel rh in contexto.reservasHotel)
            {
                if (rh.id == id)
                {
                    cantidadPersonasRH -= rh.cantPersonas;
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

                        if (hotelSeleccionado != rh.miHotel)
                        {
                            rh.miHotel.misReservas.Remove(rh);

                            contexto.hoteles.Update(rh.miHotel);
                        }

                        if (usuarioSeleccionado != rh.miUsuario)
                        {
                            rh.miUsuario.credito += rh.pagado;
                            rh.miUsuario.misReservasHoteles.Remove(rh);

                            contexto.usuarios.Update(rh.miUsuario);
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
                        contexto.hoteles.Update(hotelSeleccionado);

                        usuarioSeleccionado.misReservasHoteles.Add(rh);
                        contexto.usuarios.Update(usuarioSeleccionado);

                        contexto.reservasHotel.Update(rh);
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
            }

            return false;
        }

        public bool elminarReservaHotel(int id)
        {
            foreach (ReservaHotel rh in contexto.reservasHotel)
            {
                if (rh.id == id)
                {
                    if (DateTime.Now < rh.fechaDesde)
                    {
                        rh.miUsuario.credito += rh.pagado;
                    }

                    contexto.usuarioHotel.ForEachAsync(uh =>
                    {
                        if (uh.usuario == rh.miUsuario && uh.hotel == rh.miHotel && uh.cantidad > 1)
                        {
                            uh.cantidad--;
                            contexto.usuarioHotel.Update(uh);
                        }
                        else
                        {
                            contexto.usuarioHotel.Remove(uh);
                        }
                    });

                    rh.miUsuario.misReservasHoteles.Remove(rh);
                    contexto.usuarios.Update(rh.miUsuario);

                    rh.miHotel.misReservas.Remove(rh);
                    contexto.hoteles.Update(rh.miHotel);

                    contexto.reservasHotel.Remove(rh);
                    contexto.SaveChanges();

                    return true;
                }
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
            foreach (ReservaVuelo rv in contexto.reservasVuelo)
            {
                if (rv.id == id)
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
                            UsuarioVuelo usuario_vuelo = contexto.usuarioVuelo.First(uh => uh.usuario_fk == usuario_id && uh.vuelo_fk == vueloSeleccionado.id);

                            if (usuario_vuelo == null)
                            {
                                usuario_vuelo = new UsuarioVuelo(usuario_id, vueloSeleccionado.id);

                                contexto.usuarioVuelo.Add(usuario_vuelo);
                            }

                            if (vueloSeleccionado != rv.miVuelo)
                            {
                                rv.miVuelo.capacidad += rv.cantPersonas;
                                rv.miVuelo.misReservas.Remove(rv);

                                contexto.vuelos.Update(rv.miVuelo);
                            }

                            if (usuarioSeleccionado != rv.miUsuario)
                            {
                                rv.miUsuario.credito += rv.pagado;
                                rv.miUsuario.misReservasVuelos.Remove(rv);

                                contexto.usuarios.Update(rv.miUsuario);
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
                            contexto.usuarios.Update(usuarioSeleccionado);

                            vueloSeleccionado.misReservas.Add(rv);
                            contexto.vuelos.Update(vueloSeleccionado);

                            contexto.reservasVuelo.Update(rv);
                            contexto.SaveChanges();

                            MessageBox.Show("Modificada con exito");

                            return true;
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
            foreach (ReservaVuelo rv in contexto.reservasVuelo)
            {
                if (rv.id == id)
                {
                    if (DateTime.Now < rv.miVuelo.fecha)
                    {
                        rv.miUsuario.credito += rv.pagado;
                    }

                    contexto.usuarioVuelo.ForEachAsync(uv =>
                    {
                        if (uv.usuario == rv.miUsuario && uv.vuelo == rv.miVuelo)
                        {
                            contexto.usuarioVuelo.Remove(uv);
                        }
                    });

                    rv.miUsuario.misReservasVuelos.Remove(rv);
                    contexto.usuarios.Update(rv.miUsuario);

                    rv.miVuelo.misReservas.Remove(rv);
                    contexto.vuelos.Update(rv.miVuelo);

                    contexto.reservasVuelo.Remove(rv);
                    contexto.SaveChanges();

                    return true;
                }
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
