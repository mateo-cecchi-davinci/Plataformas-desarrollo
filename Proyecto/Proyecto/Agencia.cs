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
        private int cantidadPersonasRH;
        private int cantidadPersonasRV;

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
            cantidadPersonasRH = 0;
            cantidadPersonasRV = 0;
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
                        if (u.intentosFallidos >= 3)
                        {
                            u.bloqueado = true;
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

                    //Aca tendria que eliminar las reservas???
                    //Cuando usemos bdd aca habria que hacer un borrado en cascada???

                    usuarios.Remove(u);
                    return true;
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

            Hotel hotelH = new Hotel(cantHoteles, ubicacion, capacidad, costo, nombre);
            hoteles.Add(hotelH);
            ubicacion.hoteles.Add(hotelH);
            cantHoteles++;

            return true;
        }

        public bool modificarHotel(int id, string ciudad, int capacidad, double costo, string nombre)
        {

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

                    Ciudad nuevaUbicacion = ciudades.FirstOrDefault(c => c.nombre == ciudad);

                    if (h.ubicacion != nuevaUbicacion)
                    {
                        h.ubicacion.hoteles.Remove(h);
                    }

                    h.ubicacion = nuevaUbicacion;
                    h.capacidad = capacidad;
                    h.costo = costo;
                    h.nombre = nombre;

                    if (!h.ubicacion.hoteles.Contains(h))
                    {
                        h.ubicacion.hoteles.Add(h);
                    }
                    MessageBox.Show("Modificado con exito");
                    return true;
                }
            }
            MessageBox.Show("Hotel no encontrado");
            return false;
        }

        public bool elminarHotel(int id)
        {
            foreach (Hotel h in hoteles)
            {
                if (h.id == id)
                {
                    h.misReservas.ForEach(rh =>
                    {

                        if (DateTime.Now < rh.fechaDesde)
                        {
                            rh.miUsuario.credito += rh.pagado;
                            rh.miUsuario.misReservasHoteles.Remove(rh);
                        }
                        //rh.miUsuario.hotelesVisitados.Remove(h);  --> ESTO VA???
                        h.ubicacion.hoteles.Remove(h);

                    });

                    hoteles.Remove(h);

                    return true;
                }
            }
            return false;
        }

        //CRUD Vuelos
        public bool agregarVuelo(string origen, string destino, int capacidad, double costo, DateTime fecha, string aerolinea, string avion)
        {

            Ciudad ciudadO = ciudades.FirstOrDefault(c => c.nombre == origen);
            Ciudad ciudadD = ciudades.FirstOrDefault(c => c.nombre == destino);
            Vuelo vuelo = new Vuelo(cantVuelos, ciudadO, ciudadD, capacidad, costo, fecha, aerolinea, avion);

            vuelos.Add(vuelo);
            ciudadO.vuelos.Add(vuelo);
            ciudadD.vuelos.Add(vuelo);

            cantVuelos++;

            return true;
        }

        public List<Vuelo> obtenerVuelos()
        {
            return vuelos.ToList();
        }

        public bool modificarVuelo(int id, string origen, string destino, int capacidad, double costo, DateTime fecha, string aerolinea, string avion)
        {
            foreach (Vuelo v in vuelos)
            {
                if (v.id == id)
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
                        Ciudad ciudadO = ciudades.FirstOrDefault(c => c.nombre == origen);
                        Ciudad ciudadD = ciudades.FirstOrDefault(c => c.nombre == destino);

                        if (v.origen != ciudadO)
                        {
                            v.origen.vuelos.Remove(v);
                        }
                        if (v.destino != ciudadD)
                        {
                            v.destino.vuelos.Remove(v);
                        }

                        v.origen = ciudadO;
                        v.destino = ciudadD;
                        v.capacidad = capacidad;
                        v.costo = costo;
                        v.fecha = fecha;
                        v.aerolinea = aerolinea;
                        v.avion = avion;

                        if (!ciudadO.vuelos.Contains(v))
                        {
                            ciudadO.vuelos.Add(v);
                        }
                        if (!ciudadD.vuelos.Contains(v))
                        {
                            ciudadD.vuelos.Add(v);
                        }
                        MessageBox.Show("Modificado con exito");

                        return true;
                    }
                }
            }
            MessageBox.Show("Vuelo no registrado");
            return false;
        }

        public bool elminarVuelo(int id)
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

                        //rv.miUsuario.vuelosTomados.Remove(v); --> ESTO VA???
                        v.origen.vuelos.Remove(v);

                    });

                    vuelos.Remove(v);
                    return true;
                }
            }
            return false;
        }

        //Crud ReservaHotel

        public bool agregarReservaHotel(string hotel, string usuario, DateTime fechaDesde, DateTime fechaHasta, double pagado, int cantPersonas)
        {

            string u = usuario.Substring(0, 1);
            int usuario_id = int.Parse(u);

            Usuario usuarioSeleccionado = usuarios.FirstOrDefault(u => u.id == usuario_id);
            Hotel hotelSeleccionado = hoteles.FirstOrDefault(h => h.nombre == hotel);

            if (hotelSeleccionado == null)
            {
                MessageBox.Show("El hotel no se encuentra disponible");
                return false;
            }

            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("El usuario no se encuentra disponible");
                return false;
            }

            hotelSeleccionado.misReservas.ForEach(r =>
            {
                if (r.fechaDesde < fechaHasta && r.fechaHasta > fechaDesde)
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
            else if (cantPersonas <= 0 || cantPersonas > 10)
            {
                MessageBox.Show("Cantidad de personas inválida");
                return false;
            }
            else if (pagado > usuarioSeleccionado.credito)
            {
                MessageBox.Show("Crédito insuficiente");
                return false;
            }
            else if (pagado >= hotelSeleccionado.costo * cantPersonas)
            {
                usuarioSeleccionado.credito -= hotelSeleccionado.costo * cantPersonas;
                pagado = hotelSeleccionado.costo * cantPersonas;

                ReservaHotel nuevaReserva = new ReservaHotel(cantReservasHoteles, hotelSeleccionado, usuarioSeleccionado, fechaDesde, fechaHasta, pagado, cantPersonas);

                reservasHotel.Add(nuevaReserva);
                usuarioSeleccionado.misReservasHoteles.Add(nuevaReserva);
                hotelSeleccionado.misReservas.Add(nuevaReserva);

                cantReservasHoteles++;

                return true;
            }
            else
            {
                MessageBox.Show("Pago insuficiente");
                return false;
            }
        }

        public List<ReservaHotel> obtenerReservaHotel()
        {
            return reservasHotel.ToList();
        }

        public bool modificarReservaHotel(int id, string hotel, string usuario, DateTime fechaDesde, DateTime fechaHasta, double pagado, int cantidad)
        {

            string u = usuario.Substring(0, 1);
            int usuario_id = int.Parse(u);

            Usuario usuarioSeleccionado = usuarios.FirstOrDefault(u => u.id == usuario_id);
            Hotel hotelSeleccionado = hoteles.FirstOrDefault(h => h.nombre == hotel);

            if (hotelSeleccionado == null)
            {
                MessageBox.Show("El hotel no se encuentra disponible");
                return false;
            }
            else if (usuarioSeleccionado == null)
            {
                MessageBox.Show("El usuario no se encuentra disponible");
                return false;
            }
            else if (cantidad <= 0 || cantidad > 10)
            {
                MessageBox.Show("Cantidad de personas inválida");
                return false;
            }

            hotelSeleccionado.misReservas.ForEach(r =>
            {
                if (r.fechaDesde < fechaHasta && r.fechaHasta > fechaDesde)
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

                    if (hotelSeleccionado.capacidad > cantidadPersonasRH)
                    {

                        if (usuarioSeleccionado.credito >= pagado)
                        {

                            if (usuarioSeleccionado.credito + rh.pagado - pagado >= hotelSeleccionado.costo * cantidad)
                            {

                                if (hotelSeleccionado != rh.miHotel)
                                {
                                    rh.miHotel.misReservas.Remove(rh);
                                }

                                if (usuarioSeleccionado != rh.miUsuario)
                                {
                                    rh.miUsuario.credito += rh.pagado;
                                    rh.miUsuario.misReservasHoteles.Remove(rh);
                                }

                                pagado = hotelSeleccionado.costo * cantidad;

                                usuarioSeleccionado.credito += rh.pagado;
                                usuarioSeleccionado.credito -= pagado;

                                hotelSeleccionado.misReservas.Remove(rh);
                                usuarioSeleccionado.misReservasHoteles.Remove(rh);

                                rh.miHotel = hotelSeleccionado;
                                rh.miUsuario = usuarioSeleccionado;
                                rh.fechaDesde = fechaDesde;
                                rh.fechaHasta = fechaHasta;
                                rh.pagado = pagado;
                                rh.cantPersonas = cantidad;

                                hotelSeleccionado.misReservas.Add(rh);
                                usuarioSeleccionado.misReservasHoteles.Add(rh);
                                MessageBox.Show("Modificada con exito");

                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Pago insuficiente");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Crédito insuficiente");
                            return false;
                        }
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
            foreach (ReservaHotel rh in reservasHotel)
            {
                if (rh.id == id)
                {

                    if (DateTime.Now < rh.fechaDesde)
                    {
                        rh.miUsuario.credito += rh.miHotel.costo;
                    }

                    //rh.miUsuario.hotelesVisitados.Remove(rh.miHotel); <--- ESTO VA???
                    rh.miUsuario.misReservasHoteles.Remove(rh);
                    rh.miHotel.misReservas.Remove(rh);
                    reservasHotel.Remove(rh);
                    return true;
                }
            }
            return false;
        }

        //Crud ReservaVuelo

        public bool agregarReservaVuelo(string origen, string destino, string usuario, double pagado, int cantPersonas, DateTime fecha)
        {


            string u = usuario.Substring(0, 1);
            int usuario_id = int.Parse(u);

            Usuario usuarioSeleccionado = usuarios.FirstOrDefault(u => u.id == usuario_id);
            Vuelo vueloSeleccionado = vuelos.FirstOrDefault(v => v.origen.nombre == origen && v.destino.nombre == destino && v.fecha == fecha);

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

            if ((vueloSeleccionado.capacidad - cantPersonas) < 0)
            {
                MessageBox.Show("La cantidad de personas ingresada excede la capacidad del vuelo");
                return false;
            }
            else if (cantPersonas <= 0 || cantPersonas > 10)
            {
                MessageBox.Show("Cantidad de personas inválida");
                return false;
            }
            else if (pagado > usuarioSeleccionado.credito)
            {
                MessageBox.Show("Crédito insuficiente");
                return false;
            }
            else if (pagado >= vueloSeleccionado.costo * cantPersonas)
            {
                vueloSeleccionado.capacidad -= cantPersonas;
                usuarioSeleccionado.credito -= vueloSeleccionado.costo * cantPersonas;
                pagado = vueloSeleccionado.costo * cantPersonas;

                ReservaVuelo rv = new ReservaVuelo(cantReservasVuelos, vueloSeleccionado, usuarioSeleccionado, pagado, cantPersonas);

                reservasVuelo.Add(rv);
                usuarioSeleccionado.misReservasVuelos.Add(rv);
                vueloSeleccionado.misReservas.Add(rv);

                cantReservasVuelos++;

                return true;
            }
            else
            {
                MessageBox.Show("Pago insuficiente");
                return false;
            }

        }

        public List<ReservaVuelo> obtenerReservaVuelo()
        {
            return reservasVuelo.ToList();
        }

        public bool modificarReservaVuelo(int id, string origen, string destino, string usuario, double pagado, int cantPersonas, DateTime fecha)
        {
            foreach (ReservaVuelo rv in reservasVuelo)
            {
                if (rv.id == id)
                {

                    string u = usuario.Substring(0, 1);
                    int usuario_id = int.Parse(u);

                    try
                    {
                        Usuario usuarioSeleccionado = usuarios.FirstOrDefault(u => u.id == usuario_id);
                        Vuelo vueloSeleccionado = vuelos.FirstOrDefault(v => v.origen.nombre == origen && v.destino.nombre == destino && v.fecha == fecha);

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

                        if (usuarioSeleccionado.credito >= pagado)
                        {

                            if (usuarioSeleccionado.credito + rv.pagado - pagado >= vueloSeleccionado.costo * cantPersonas)
                            {

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

                                vueloSeleccionado.capacidad += rv.cantPersonas;
                                vueloSeleccionado.capacidad -= cantPersonas;

                                usuarioSeleccionado.credito += rv.pagado;
                                usuarioSeleccionado.credito -= vueloSeleccionado.costo * cantPersonas;

                                pagado = vueloSeleccionado.costo * cantPersonas;

                                usuarioSeleccionado.misReservasVuelos.Remove(rv);
                                vueloSeleccionado.misReservas.Remove(rv);

                                rv.miVuelo = vueloSeleccionado;
                                rv.miUsuario = usuarioSeleccionado;
                                rv.pagado = pagado;
                                rv.cantPersonas = cantPersonas;

                                usuarioSeleccionado.misReservasVuelos.Add(rv);
                                vueloSeleccionado.misReservas.Add(rv);

                                MessageBox.Show("Modificada con exito");

                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Pago insuficiente");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Crédito insuficiente");
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

                    if (DateTime.Now < rv.miVuelo.fecha)
                    {
                        rv.miUsuario.credito += rv.miVuelo.costo;
                    }

                    //rv.miUsuario.vuelosTomados.Remove(rv.miVuelo); <--- ESTO VA???
                    rv.miUsuario.misReservasVuelos.Remove(rv);
                    rv.miVuelo.misReservas.Remove(rv);
                    reservasVuelo.Remove(rv);
                    return true;
                }
            }
            return false;
        }

        //Ciudades

        public List<Ciudad> obtenerCiudades()
        {
            return ciudades;
        }

        //  ***Esto creo que va a servir despues :)***
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
