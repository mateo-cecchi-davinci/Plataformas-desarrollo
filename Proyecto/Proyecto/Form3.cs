using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Proyecto.Form2;

namespace Proyecto
{
    public partial class Form3 : Form
    {

        private Agencia miAgencia;
        private int usuarioSeleccionado;
        private int hotelSeleccionado;
        private int vueloSeleccionado;
        private int reservaHotelSeleccionada;
        private int reservaVueloSeleccionada;
        public cerrarSesion salir;

        public Form3(Agencia miAgencia)
        {
            InitializeComponent();
            this.miAgencia = miAgencia;
            nombreUsuario.Text = miAgencia.nombreLogueado();
            nombreUsuarioH.Text = miAgencia.nombreLogueado();
            nombreUsuarioVuelos.Text = miAgencia.nombreLogueado();
            nombreUsuarioRH.Text = miAgencia.nombreLogueado();
            nombreUsuarioRV.Text = miAgencia.nombreLogueado();
            usuarioSeleccionado = -1;
            hotelSeleccionado = -1;
            vueloSeleccionado = -1;
            reservaHotelSeleccionada = -1;
            reservaVueloSeleccionada = -1;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string[] opciones = { "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            cb_cantPersonasH.Items.AddRange(opciones);
            cb_cantPersonasH.SelectedIndex = 0;
            cb_cantPersonasRV.Items.AddRange(opciones);
            cb_cantPersonasRV.SelectedIndex = 0;

            List<string> ciudades = new List<string>();
            ciudades.Add("");
            foreach (Ciudad c in miAgencia.obtenerCiudad())
            {
                ciudades.Add(c.nombre);
            }
            cb_ciudadH.DataSource = ciudades.ToList();
            cb_origenV.DataSource = ciudades.ToList();
            cb_destinoV.DataSource = ciudades.ToList();

            List<string> hoteles = new List<string>();
            hoteles.Add("");
            foreach (Hotel h in miAgencia.obtenerHoteles())
            {
                hoteles.Add(h.nombre);
            }
            cbHotelReservaH.DataSource = hoteles.ToList();

            List<string> usuarios = new List<string>();
            usuarios.Add("");
            foreach (Usuario u in miAgencia.obtenerUsuarios())
            {
                usuarios.Add(u.nombre);
            }
            cbUsuarioReservaH.DataSource = usuarios.ToList();
            cbUsuarioReservaV.DataSource = usuarios.ToList();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            usuarioSeleccionado = -1;
        }

        private void limpiarCampos()
        {

            dataGridView1.Rows.Clear();

            string nombre = tbNombre.Text.ToLower();
            string apellido = tbApellido.Text.ToLower();
            string dni = tbDni.Text.ToLower();
            string mail = tbMail.Text.ToLower();
            string credito = tbCargarCredito.Text.ToLower();

            foreach (Usuario u in miAgencia.obtenerUsuarios())
            {
                string nombreUsuario = u.nombre.ToLower();
                string apellidoUsuario = u.apellido.ToLower();
                string dniUsuario = u.dni.ToString().ToLower();
                string mailUsuario = u.mail.ToLower();
                string creditoUsuario = u.credito.ToString().ToLower();

                if (nombreUsuario.Contains(nombre) &&
                    apellidoUsuario.Contains(apellido) &&
                    dniUsuario.Contains(dni) &&
                    mailUsuario.Contains(mail) &&
                    creditoUsuario.Contains(credito))
                {
                    dataGridView1.Rows.Add(u.ToString());
                }
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridView1[0, e.RowIndex].Value.ToString();
            string dni = dataGridView1[1, e.RowIndex].Value.ToString();
            string nombre = dataGridView1[2, e.RowIndex].Value.ToString();
            string apellido = dataGridView1[3, e.RowIndex].Value.ToString();
            string credito = dataGridView1[4, e.RowIndex].Value.ToString();
            string mail = dataGridView1[5, e.RowIndex].Value.ToString();

            tbDni.Text = dni;
            tbNombre.Text = nombre;
            tbApellido.Text = apellido;
            tbMail.Text = mail;
            tbCargarCredito.Text = credito;
            usuarioSeleccionado = int.Parse(id);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string nombre = tbNombre.Text;
            string apellido = tbApellido.Text;
            string dni = tbDni.Text;
            string mail = tbMail.Text;
            string credito = tbCargarCredito.Text;
            int numDni;
            double creditoParseado;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese nombre");
                return;
            }

            if (string.IsNullOrWhiteSpace(apellido))
            {
                MessageBox.Show("Ingrese apellido");
                return;
            }
            if (string.IsNullOrWhiteSpace(dni))
            {
                MessageBox.Show("Ingrese dni");
                return;
            }

            if (string.IsNullOrWhiteSpace(mail))
            {
                MessageBox.Show("Ingrese mail");
                return;
            }

            if (string.IsNullOrWhiteSpace(credito))
            {
                MessageBox.Show("Ingrese crédito");
                return;
            }

            if (int.TryParse(dni, out numDni) && double.TryParse(credito, out creditoParseado))
            {
                if (usuarioSeleccionado != -1)
                {
                    if (miAgencia.modificarUsuario(usuarioSeleccionado, numDni, nombre, apellido, mail, creditoParseado))
                        MessageBox.Show("Modificado con exito");
                    else
                        MessageBox.Show("Problemas al modificar");
                }
                else
                    MessageBox.Show("Debe seleccionar un usuario");
            }
            else MessageBox.Show("Los campos Dni y crédito solo admiten números");

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionado != -1)
            {
                if (miAgencia.eliminarUsuario(usuarioSeleccionado))
                    MessageBox.Show("Eliminado con exito");
                else
                    MessageBox.Show("Problemas al eliminar");
            }
            else
                MessageBox.Show("Debe seleccionar un usuario");
        }

        public delegate void cerrarSesion();

        private void btnSalir_Click(object sender, EventArgs e)
        {
            miAgencia.cerrarSesion();
            salir();
        }


        //Seccion CRUD Hoteles

        private void Mostrar_Click(object sender, EventArgs e)
        {
            mostrarHoteles();
            hotelSeleccionado = -1;
        }

        private void mostrarHoteles()
        {

            dataGridView2Hoteles.Rows.Clear();

            string nombre = textBoxNombre.Text.ToLower();
            string capacidad = textBoxCapacidad.Text.ToLower();
            string costo = textBoxCosto.Text.ToLower();
            string ciudad = cb_ciudadH.Text.ToLower();

            foreach (Hotel h in miAgencia.obtenerHoteles())
            {
                string nombreHotel = h.nombre.ToLower();
                string capacidadHotel = h.capacidad.ToString().ToLower();
                string costoHotel = h.costo.ToString().ToLower();
                string ciudadHotel = h.ubicacion.nombre.ToLower();

                if (nombreHotel.Contains(nombre) &&
                    capacidadHotel.Contains(capacidad) &&
                    costoHotel.Contains(costo) &&
                    ciudadHotel.Contains(ciudad))
                {
                    dataGridView2Hoteles.Rows.Add(h.ToString());
                }
            }

        }

        private void Cargar_Click(object sender, EventArgs e)
        {
            cargarHotel();
        }

        private void cargarHotel()
        {
            string nombre = textBoxNombre.Text;
            string capacidadText = textBoxCapacidad.Text;
            string costoText = textBoxCosto.Text;
            string ciudadBuscada = cb_ciudadH.Text.ToLower();
            Ciudad ciudad = miAgencia.obtenerCiudad().FirstOrDefault(c => c.nombre.ToLower().Contains(ciudadBuscada));
            int capacidad;
            double costo;

            if (int.TryParse(capacidadText, out capacidad))
            {
                if (double.TryParse(costoText, out costo))
                {

                    miAgencia.agregarHotel(ciudad, capacidad, costo, nombre);
                    textBoxNombre.Text = "";
                    textBoxCapacidad.Text = "";
                    textBoxCosto.Text = "";
                    cb_ciudadH.SelectedIndex = 0;
                    MessageBox.Show("Se ha cargado un nuevo hotel con exito");
                }
                else MessageBox.Show("ingrese solo numero en Costo");


            }
            else MessageBox.Show("ingrese solo numero en Capacidad");

        }

        private void dataGridView2Hoteles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridView2Hoteles[0, e.RowIndex].Value.ToString();
            string nombre = dataGridView2Hoteles[1, e.RowIndex].Value.ToString();
            string ubicacion = dataGridView2Hoteles[2, e.RowIndex].Value.ToString();
            string costo = dataGridView2Hoteles[3, e.RowIndex].Value.ToString();
            string capacidad = dataGridView2Hoteles[4, e.RowIndex].Value.ToString();

            cb_ciudadH.Text = ubicacion;
            textBoxNombre.Text = nombre;
            textBoxCapacidad.Text = capacidad;
            textBoxCosto.Text = costo;
            hotelSeleccionado = int.Parse(id);
        }

        private void Modificar_Click(object sender, EventArgs e)
        {

            string nombre = textBoxNombre.Text;
            string capacidad = textBoxCapacidad.Text;
            string ciudadBuscada = cb_ciudadH.Text.ToLower();
            Ciudad ciudad = miAgencia.obtenerCiudad().FirstOrDefault(c => c.nombre.ToLower().Contains(ciudadBuscada));
            string costo = textBoxCosto.Text;
            int capacidadParseada;
            double costoParseado;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese nombre");
                return;
            }

            if (string.IsNullOrWhiteSpace(capacidad))
            {
                MessageBox.Show("Ingrese capacidad");
                return;
            }
            if (string.IsNullOrWhiteSpace(ciudadBuscada))
            {
                MessageBox.Show("Ingrese ciudad");
                return;
            }

            if (string.IsNullOrWhiteSpace(costo))
            {
                MessageBox.Show("Ingrese costo");
                return;
            }

            if (int.TryParse(capacidad, out capacidadParseada) && double.TryParse(costo, out costoParseado))
            {
                if (hotelSeleccionado != -1)
                {
                    if (miAgencia.modificarHoteles(hotelSeleccionado, ciudad, capacidadParseada, costoParseado, nombre))
                        MessageBox.Show("Modificado con exito");
                    else
                        MessageBox.Show("Problemas al modificar");
                }
                else
                    MessageBox.Show("Debe seleccionar un Hotel");
            }
            else MessageBox.Show("Capacidad y Dni solo admiten números");
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (hotelSeleccionado != -1)
            {
                if (miAgencia.elminarHotel(hotelSeleccionado))
                    MessageBox.Show("Eliminado con exito");
                else
                    MessageBox.Show("Problemas al eliminar");
            }

            else
                MessageBox.Show("Debe seleccionar un Hotel");
        }


        //Seccion CRUD Vuelos


        private void Cargar_vuelos_Click(object sender, EventArgs e)
        {
            cargarVuelo();
        }


        private void cargarVuelo()
        {
            string ciudadOrigen = cb_origenV.Text.ToLower();
            string ciudadDestino = cb_destinoV.Text.ToLower();
            Ciudad origen = miAgencia.obtenerCiudad().FirstOrDefault(c => c.nombre.ToLower().Contains(ciudadOrigen));
            Ciudad destino = miAgencia.obtenerCiudad().FirstOrDefault(c => c.nombre.ToLower().Contains(ciudadDestino));
            string capacidad = tbCapacidad.Text;
            string costo = textBox_costo_vuelos.Text;
            DateTime fecha = dateTimePicker_vuelos.Value;
            string aerolinea = textBox_aerolineas_vuelos.Text;
            string avion = textBox_avion_vuelos.Text;

            double costoParse;
            int capacidadParseada;


            if (double.TryParse(costo, out costoParse) && int.TryParse(capacidad, out capacidadParseada))
            {
                miAgencia.agregarVuelos(origen, destino, capacidadParseada, costoParse, fecha, aerolinea, avion);
                cb_origenV.SelectedIndex = 0;
                cb_destinoV.SelectedIndex = 0;
                tbCapacidad.Text = "";
                textBox_costo_vuelos.Text = "";
                dateTimePicker_vuelos.Value = DateTime.Now;
                textBox_aerolineas_vuelos.Text = "";
                textBox_avion_vuelos.Text = "";
                MessageBox.Show("Se ha cargado un nuevo Vuelo con exito");
            }
            else
                MessageBox.Show("Solo puede ingresar numeros en costo y capacidad");

        }

        private void Mostrar_vuelos_Click(object sender, EventArgs e)
        {
            mostrarVuelos();
            vueloSeleccionado = -1;
        }

        private void mostrarVuelos()
        {

            dataGridViewVuelos.Rows.Clear();

            string origen = cb_origenV.Text.ToLower();
            string destino = cb_destinoV.Text.ToLower();
            string costo = textBox_costo_vuelos.Text.ToLower();
            string capacidad = tbCapacidad.Text.ToLower();
            string aerolineas = textBox_aerolineas_vuelos.Text.ToLower();
            string avion = textBox_avion_vuelos.Text.ToLower();

            foreach (Vuelo v in miAgencia.obtenerVuelos())
            {
                string origenVuelo = v.origen.nombre.ToLower();
                string destinoVuelo = v.destino.nombre.ToLower();
                string costoVuelo = v.costo.ToString().ToLower();
                string capacidadVuelo = v.capacidad.ToString().ToLower();
                string aerolineasVuelo = v.aerolinea.ToLower();
                string avionVuelo = v.avion.ToLower();

                if (origenVuelo.Contains(origen) &&
                    destinoVuelo.Contains(destino) &&
                    costoVuelo.Contains(costo) &&
                    capacidadVuelo.Contains(capacidad) &&
                    aerolineasVuelo.Contains(aerolineas) &&
                    avionVuelo.Contains(avion))
                {
                    dataGridViewVuelos.Rows.Add(v.ToString());
                }
            }

        }

        private void dataGridViewVuelos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewVuelos[0, e.RowIndex].Value.ToString();
            string origen = dataGridViewVuelos[1, e.RowIndex].Value.ToString();
            string destino = dataGridViewVuelos[2, e.RowIndex].Value.ToString();
            string costo = dataGridViewVuelos[3, e.RowIndex].Value.ToString();
            string capacidad = dataGridViewVuelos[4, e.RowIndex].Value.ToString();
            string fechaString = dataGridViewVuelos[5, e.RowIndex].Value.ToString();
            string aerolinea = dataGridViewVuelos[6, e.RowIndex].Value.ToString();
            string avion = dataGridViewVuelos[7, e.RowIndex].Value.ToString();

            vueloSeleccionado = int.Parse(id);
            cb_origenV.Text = origen;
            cb_destinoV.Text = destino;
            textBox_costo_vuelos.Text = costo;
            tbCapacidad.Text = capacidad;
            textBox_aerolineas_vuelos.Text = aerolinea;
            textBox_avion_vuelos.Text = avion;

            if (DateTime.TryParse(fechaString, out DateTime fecha))
            {
                dateTimePicker_vuelos.Value = fecha;
            }
            else
            {
                MessageBox.Show("Formato de fecha incorrecto");
            }
        }

        private void Modificar_vuelos_Click(object sender, EventArgs e)
        {

            string ciudadOrigen = cb_origenV.Text.ToLower();
            string ciudadDestino = cb_destinoV.Text.ToLower();
            Ciudad origen = miAgencia.obtenerCiudad().FirstOrDefault(c => c.nombre.ToLower().Contains(ciudadOrigen));
            Ciudad destino = miAgencia.obtenerCiudad().FirstOrDefault(c => c.nombre.ToLower().Contains(ciudadDestino));
            string costo = textBox_costo_vuelos.Text;
            string fecha = dateTimePicker_vuelos.Text;
            string aerolinea = textBox_aerolineas_vuelos.Text;
            string avion = textBox_avion_vuelos.Text;
            string capacidad = tbCapacidad.Text;
            double costoParseado;
            int capacidadParseada;

            if (string.IsNullOrWhiteSpace(ciudadOrigen))
            {
                MessageBox.Show("Ingrese origen");
                return;
            }

            if (string.IsNullOrWhiteSpace(ciudadDestino))
            {
                MessageBox.Show("Ingrese destino");
                return;
            }
            if (string.IsNullOrWhiteSpace(costo))
            {
                MessageBox.Show("Ingrese costo");
                return;
            }

            if (string.IsNullOrWhiteSpace(fecha))
            {
                MessageBox.Show("Ingrese fecha");
                return;
            }

            if (string.IsNullOrWhiteSpace(aerolinea))
            {
                MessageBox.Show("Ingrese aerolinea");
                return;
            }

            if (string.IsNullOrWhiteSpace(avion))
            {
                MessageBox.Show("Ingrese avion");
                return;
            }

            if (string.IsNullOrWhiteSpace(avion))
            {
                MessageBox.Show("Ingrese capacidad");
                return;
            }

            if (double.TryParse(costo, out costoParseado) && int.TryParse(capacidad, out capacidadParseada))
            {
                if (vueloSeleccionado != -1)
                {
                    if (miAgencia.modificarVuelos(vueloSeleccionado, origen, destino, capacidadParseada, costoParseado, fecha, aerolinea, avion))
                        MessageBox.Show("Modificado con exito");
                    else
                        MessageBox.Show("Problemas al modificar");
                }
                else
                    MessageBox.Show("Debe seleccionar un Vuelo");
            }
            else
                MessageBox.Show("Ingrese un costo o una capacidad válida");

        }

        private void Eliminar_vuelos_Click(object sender, EventArgs e)
        {
            if (vueloSeleccionado != -1)
            {
                if (miAgencia.elminarVuelo(vueloSeleccionado))
                    MessageBox.Show("Eliminado con exito");
                else
                    MessageBox.Show("Problemas al eliminar");
            }

            else
                MessageBox.Show("Debe seleccionar un Vuelo");
        }

        private void mostrar_reservasH(object sender, EventArgs e)
        {
            mostrarReservasH();
            reservaHotelSeleccionada = -1;
        }

        private void mostrarReservasH()
        {

            dataGridViewReservasH.Rows.Clear();

            string hotel = cbHotelReservaH.Text.ToLower();
            string usuario = cbUsuarioReservaH.Text.ToLower();
            string cantidad = cb_cantPersonasH.Text.ToLower();
            string pagado = tbPagadoReservaH.Text.ToLower();

            foreach (ReservaHotel rh in miAgencia.obtenerReservaHotel())
            {
                string hotelRH = rh.miHotel.nombre.ToLower();
                string usuarioRH = rh.miUsuario.nombre.ToLower();
                string cantidadRH = rh.cantPersonas.ToString().ToLower();
                string pagadoRH = rh.pagado.ToString().ToLower();

                if (hotelRH.Contains(hotel) &&
                    usuarioRH.Contains(usuario) &&
                    cantidadRH.Contains(cantidad) &&
                    pagadoRH.Contains(pagado))
                {
                    dataGridViewReservasH.Rows.Add(rh.ToString());
                }
            }

        }

        private void dobleClickReservasH(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewReservasH[0, e.RowIndex].Value.ToString();
            string hotel = dataGridViewReservasH[1, e.RowIndex].Value.ToString();
            string usuario = dataGridViewReservasH[2, e.RowIndex].Value.ToString();
            string fechaDesde = dataGridViewReservasH[3, e.RowIndex].Value.ToString();
            string fechaHasta = dataGridViewReservasH[4, e.RowIndex].Value.ToString();
            string pagado = dataGridViewReservasH[5, e.RowIndex].Value.ToString();
            string cantPersonas = dataGridViewReservasH[6, e.RowIndex].Value.ToString();

            reservaHotelSeleccionada = int.Parse(id);
            cbHotelReservaH.Text = hotel;
            cbUsuarioReservaH.Text = usuario;

            if (DateTime.TryParse(fechaDesde, out DateTime fechaD) && DateTime.TryParse(fechaHasta, out DateTime fechaH))
            {
                fecha_desde_reservarH.Value = fechaD;
                fecha_hasta_reservarH.Value = fechaH;
            }
            else
            {
                MessageBox.Show("Formato de fecha incorrecto");
            }
            tbPagadoReservaH.Text = pagado;
            cb_cantPersonasH.Text = cantPersonas;
        }

        private void cargar_reservasH(object sender, EventArgs e)
        {
            reservasH();
        }

        private void reservasH()
        {

            string hotelSeleccionado = cbHotelReservaH.Text.ToLower();
            string usuarioSeleccionado = cbUsuarioReservaH.Text.ToLower();
            Hotel hotel = miAgencia.obtenerHoteles().FirstOrDefault(h => h.nombre.ToLower().Contains(hotelSeleccionado));
            Usuario usuario = miAgencia.obtenerUsuarios().FirstOrDefault(u => u.nombre.ToLower().Contains(usuarioSeleccionado));
            DateTime fechaDesde = fecha_desde_reservarH.Value;
            DateTime fechaHasta = fecha_hasta_reservarH.Value;
            string pagado = tbPagadoReservaH.Text;
            string cantPersonas = cb_cantPersonasH.Text;

            double pagoParse;
            int cantPersonasParseadas;

            if (double.TryParse(pagado, out pagoParse) && int.TryParse(cantPersonas, out cantPersonasParseadas))
            {
                if (usuario.credito >= hotel.costo)
                {
                    hotel.capacidad -= cantPersonasParseadas;
                    usuario.credito -= hotel.costo;

                    miAgencia.agregarReservaHotel(hotel, usuario, fechaDesde, fechaHasta, hotel.costo, cantPersonasParseadas);

                    fecha_desde_reservarH.Value = DateTime.Now;
                    fecha_hasta_reservarH.Value = DateTime.Now;
                    cbHotelReservaH.SelectedIndex = 0;
                    cbUsuarioReservaH.SelectedIndex = 0;
                    cb_cantPersonasH.SelectedIndex = 0;
                    tbPagadoReservaH.Text = "";
                    MessageBox.Show("Se ha cargado una nueva reserva con exito");
                }
                else MessageBox.Show("Crédito insuficiente");
            }
            else MessageBox.Show("Cantidad de personas inválida");

        }

        private void modificar_reservasH(object sender, EventArgs e)
        {
            string hotelSeleccionado = cbHotelReservaH.Text.ToLower();
            string usuarioSeleccionado = cbUsuarioReservaH.Text.ToLower();
            Hotel hotel = miAgencia.obtenerHoteles().FirstOrDefault(h => h.nombre.ToLower().Contains(hotelSeleccionado));
            Usuario usuario = miAgencia.obtenerUsuarios().FirstOrDefault(u => u.nombre.ToLower().Contains(usuarioSeleccionado));
            string fechaDesde = fecha_desde_reservarH.Text;
            string fechaHasta = fecha_hasta_reservarH.Text;
            string pagado = tbPagadoReservaH.Text;
            string cantPersonas = cb_cantPersonasH.Text;
            double pagadoParseado;
            int cantPersonasParseadas;

            if (string.IsNullOrWhiteSpace(hotelSeleccionado))
            {
                MessageBox.Show("Ingrese hotel");
                return;
            }

            if (string.IsNullOrWhiteSpace(usuarioSeleccionado))
            {
                MessageBox.Show("Ingrese usuario");
                return;
            }
            if (string.IsNullOrWhiteSpace(fechaDesde))
            {
                MessageBox.Show("Ingrese fechaDesde");
                return;
            }

            if (string.IsNullOrWhiteSpace(fechaHasta))
            {
                MessageBox.Show("Ingrese fechaHasta");
                return;
            }

            if (string.IsNullOrWhiteSpace(pagado))
            {
                MessageBox.Show("Ingrese pago");
                return;
            }

            if (string.IsNullOrWhiteSpace(cantPersonas))
            {
                MessageBox.Show("Ingrese cantidad de personas");
                return;
            }

            if (double.TryParse(pagado, out pagadoParseado) && int.TryParse(cantPersonas, out cantPersonasParseadas))
            {
                if (reservaHotelSeleccionada != -1)
                {
                    if (miAgencia.modificarReservaHotel(reservaHotelSeleccionada, hotel, usuario, fechaDesde, fechaHasta, pagadoParseado, cantPersonasParseadas))
                        MessageBox.Show("Modificada con exito");
                    else
                        MessageBox.Show("Problemas al modificar");
                }
                else
                    MessageBox.Show("Debe seleccionar una Reserva");
            }
            else
                MessageBox.Show("Ingrese un pago válido");
        }

        private void eliminar_reservasH(object sender, EventArgs e)
        {
            if (reservaHotelSeleccionada != -1)
            {
                if (miAgencia.elminarReservaHotel(reservaHotelSeleccionada))
                    MessageBox.Show("Eliminada con exito");
                else
                    MessageBox.Show("Problemas al eliminar");
            }

            else
                MessageBox.Show("Debe seleccionar un Reserva");
        }

        private void mostrar_reservasV(object sender, EventArgs e)
        {
            mostrarReservasV();
            reservaVueloSeleccionada = -1;
        }

        private void mostrarReservasV()
        {

            dataGridViewReservasV.Rows.Clear();

            string origen = tb_origenRV.Text.ToLower();
            string destino = tb_destinoRV.Text.ToLower();
            string usuario = cbUsuarioReservaV.Text.ToLower();
            string cantidad = cb_cantPersonasRV.Text.ToLower();
            string pagado = tbPagadoReservaV.Text.ToLower();

            foreach (ReservaVuelo rv in miAgencia.obtenerReservaVuelo())
            {
                string origenRV = rv.miVuelo.origen.nombre.ToLower();
                string destinoRV = rv.miVuelo.destino.nombre.ToLower();
                string usuarioRV = rv.miUsuario.nombre.ToLower();
                string cantidadRV = rv.cantPersonas.ToString().ToLower();
                string pagadoRV = rv.pagado.ToString().ToLower();

                if (origenRV.Contains(origen) &&
                    destinoRV.Contains(destino) &&
                    usuarioRV.Contains(usuario) &&
                    cantidadRV.Contains(cantidad) &&
                    pagadoRV.Contains(pagado))
                {
                    dataGridViewReservasV.Rows.Add(rv.ToString());
                }
            }

        }

        private void dobleClickReservasV(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewReservasV[0, e.RowIndex].Value.ToString();
            string origen = dataGridViewReservasV[1, e.RowIndex].Value.ToString();
            string destino = dataGridViewReservasV[2, e.RowIndex].Value.ToString();
            string usuario = dataGridViewReservasV[3, e.RowIndex].Value.ToString();
            string fecha = dataGridViewReservasV[4, e.RowIndex].Value.ToString();
            string cantPersonas = dataGridViewReservasV[5, e.RowIndex].Value.ToString();
            string pagado = dataGridViewReservasV[6, e.RowIndex].Value.ToString();

            reservaVueloSeleccionada = int.Parse(id);
            tb_origenRV.Text = origen;
            tb_destinoRV.Text = destino;
            cbUsuarioReservaV.Text = usuario;
            fecha_RV.Value = DateTime.Parse(fecha);
            cb_cantPersonasRV.Text = cantPersonas;
            tbPagadoReservaV.Text = pagado;
        }

        private void cargar_reservasV(object sender, EventArgs e)
        {
            reservasV();
        }

        private void reservasV()
        {
            string origenSeleccionado = tb_origenRV.Text.ToLower();
            string destinoSeleccionado = tb_destinoRV.Text.ToLower();
            string usuarioSeleccionado = cbUsuarioReservaV.Text.ToLower();
            Ciudad origen = miAgencia.obtenerCiudad().FirstOrDefault(c => c.nombre.ToLower().Contains(origenSeleccionado));
            Ciudad destino = miAgencia.obtenerCiudad().FirstOrDefault(c => c.nombre.ToLower().Contains(destinoSeleccionado));
            Vuelo vuelo = miAgencia.obtenerVuelos().FirstOrDefault(v => v.id == reservaVueloSeleccionada);
            Usuario usuario = miAgencia.obtenerUsuarios().FirstOrDefault(u => u.nombre.ToLower().Contains(usuarioSeleccionado));
            string cantPersonas = cb_cantPersonasRV.Text;
            string pagado = tbPagadoReservaV.Text;
            DateTime fecha = fecha_RV.Value;

            double pagoParse;
            int cantPersonasParseadas;

            if (double.TryParse(pagado, out pagoParse) && int.TryParse(cantPersonas, out cantPersonasParseadas) && cantPersonasParseadas > 0 && cantPersonasParseadas <= 10)
            {
                miAgencia.agregarReservaVuelo(vuelo, usuario, pagoParse, cantPersonasParseadas);
                tb_origenRV.Text = "";
                tb_destinoRV.Text = "";
                cbUsuarioReservaV.SelectedIndex = 0;
                cb_cantPersonasRV.Text = "";
                tbPagadoReservaV.Text = "";
                fecha_RV.Value = DateTime.Now;

                MessageBox.Show("Se ha cargado una nueva reserva con exito");
            }
            else MessageBox.Show("Número inválido");
        }

        private void modificar_reservasV(object sender, EventArgs e)
        {

            string origenSeleccionado = tb_origenRV.Text.ToLower();
            string destinoSeleccionado = tb_destinoRV.Text.ToLower();
            string usuarioSeleccionado = cbUsuarioReservaV.Text.ToLower();
            Ciudad origen = miAgencia.obtenerCiudad().FirstOrDefault(c => c.nombre.ToLower().Contains(origenSeleccionado));
            Ciudad destino = miAgencia.obtenerCiudad().FirstOrDefault(c => c.nombre.ToLower().Contains(destinoSeleccionado));
            Vuelo vuelo = miAgencia.obtenerVuelos().FirstOrDefault(v => v.id == reservaVueloSeleccionada);
            Usuario usuario = miAgencia.obtenerUsuarios().FirstOrDefault(u => u.nombre.ToLower().Contains(usuarioSeleccionado));
            string cantPersonas = cb_cantPersonasRV.Text;
            string pagado = tbPagadoReservaV.Text;
            DateTime fecha = fecha_RV.Value;

            double pagoParseado;
            int cantPersonasParseadas;

            if (string.IsNullOrWhiteSpace(origenSeleccionado))
            {
                MessageBox.Show("Ingrese origen");
                return;
            }
            if (string.IsNullOrWhiteSpace(destinoSeleccionado))
            {
                MessageBox.Show("Ingrese destino");
                return;
            }
            if (string.IsNullOrWhiteSpace(usuarioSeleccionado))
            {
                MessageBox.Show("Ingrese usuario");
                return;
            }
            if (string.IsNullOrWhiteSpace(cantPersonas))
            {
                MessageBox.Show("Ingrese cantidad de personas");
                return;
            }
            if (string.IsNullOrWhiteSpace(pagado))
            {
                MessageBox.Show("Ingrese pago");
                return;
            }

            if (reservaVueloSeleccionada != -1)
            {
                if (double.TryParse(pagado, out pagoParseado) && int.TryParse(cantPersonas, out cantPersonasParseadas) && cantPersonasParseadas > 0 && cantPersonasParseadas <= 10)
                {
                    vuelo.capacidad -= cantPersonasParseadas;

                    miAgencia.modificarReservaVuelo(reservaVueloSeleccionada, vuelo, usuario, pagoParseado, cantPersonasParseadas);
                    miAgencia.modificarFechaVuelo(vuelo.id, fecha);

                    tb_origenRV.Text = "";
                    tb_destinoRV.Text = "";
                    cbUsuarioReservaV.SelectedIndex = 0;
                    cb_cantPersonasRV.Text = "";
                    tbPagadoReservaV.Text = "";
                    fecha_RV.Value = DateTime.Now;

                    MessageBox.Show("Modificada con exito");
                }
                else
                    MessageBox.Show("Ingrese un pago válido");
            }
            else
                MessageBox.Show("Debe seleccionar una Reserva");

        }

        private void eliminar_reservasV(object sender, EventArgs e)
        {
            if (reservaVueloSeleccionada != -1)
            {
                if (miAgencia.elminarReservaVuelo(reservaVueloSeleccionada))
                    MessageBox.Show("Eliminada con exito");
                else
                    MessageBox.Show("Problemas al eliminar");
            }

            else
                MessageBox.Show("Debe seleccionar un Reserva");
        }

        private void limpiar(object sender, EventArgs e)
        {
            tbDni.Text = "";
            tbNombre.Text = "";
            tbApellido.Text = "";
            tbCargarCredito.Text = "";
            tbMail.Text = "";
            textBoxNombre.Text = "";
            cb_ciudadH.SelectedIndex = 0;
            textBoxCosto.Text = "";
            textBoxCapacidad.Text = "";
            cb_origenV.SelectedIndex = 0;
            cb_destinoV.SelectedIndex = 0;
            textBox_costo_vuelos.Text = "";
            dateTimePicker_vuelos.Value = DateTime.Now;
            textBox_avion_vuelos.Text = "";
            textBox_aerolineas_vuelos.Text = "";
            tbCapacidad.Text = "";
            cbHotelReservaH.SelectedIndex = 0;
            cbUsuarioReservaH.SelectedIndex = 0;
            cb_cantPersonasH.SelectedIndex = 0;
            tbPagadoReservaH.Text = "";
            fecha_desde_reservarH.Value = DateTime.Now;
            fecha_hasta_reservarH.Value = DateTime.Now;
            tb_origenRV.Text = "";
            tb_destinoRV.Text = "";
            cbUsuarioReservaV.SelectedIndex = 0;
            cb_cantPersonasRV.SelectedIndex = 0;
            tbPagadoReservaV.Text = "";
            fecha_RV.Value = DateTime.Now;
        }
    }
}
