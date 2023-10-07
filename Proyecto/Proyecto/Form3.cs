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
        private List<string> ciudades = new List<string>();
        private List<string> hoteles = new List<string>();
        private List<string> usuarios = new List<string>();
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
            string nombre = miAgencia.nombreLogueado();
            int indice = nombre.IndexOf(" ") + 1;
            string nombreU = nombre.Substring(indice);
            nombreUsuario.Text = nombreU;
            nombreUsuarioH.Text = nombreU;
            nombreUsuarioVuelos.Text = nombreU;
            nombreUsuarioRH.Text = nombreU;
            nombreUsuarioRV.Text = nombreU;
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

            ciudades.Add("");
            foreach (Ciudad c in miAgencia.obtenerCiudades())
            {
                ciudades.Add(c.nombre);
            }
            cb_ciudadH.DataSource = ciudades;
            cb_origenV.DataSource = ciudades;
            cb_destinoV.DataSource = ciudades.ToList();
            cb_origenRV.DataSource = ciudades;
            cb_destinoRV.DataSource = ciudades.ToList();

            hoteles.Add("");
            foreach (Hotel h in miAgencia.obtenerHoteles())
            {
                hoteles.Add(h.nombre);
            }
            cbHotelReservaH.DataSource = hoteles.ToList();


            usuarios.Add("");
            foreach (Usuario u in miAgencia.obtenerUsuarios())
            {
                usuarios.Add(u.id.ToString() + ". " + u.nombre);
            }
            cbUsuarioReservaH.DataSource = usuarios;
            cbUsuarioReservaV.DataSource = usuarios;
        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
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

        private void dataGridViewUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void btnModificarUsuario_Click(object sender, EventArgs e)
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

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
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

        private void btnBuscarHotel_Click(object sender, EventArgs e)
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

        private void btnCargarHotel_Click(object sender, EventArgs e)
        {
            cargarHotel();
        }

        private void cargarHotel()
        {
            string nombre = textBoxNombre.Text;
            string capacidadText = textBoxCapacidad.Text;
            string costoText = textBoxCosto.Text;
            string ciudad = cb_ciudadH.Text;
            int capacidad;
            double costo;

            if (int.TryParse(capacidadText, out capacidad))
            {
                if (double.TryParse(costoText, out costo))
                {
                    if (miAgencia.agregarHotel(ciudad, capacidad, costo, nombre))
                    {
                        textBoxNombre.Text = "";
                        textBoxCapacidad.Text = "";
                        textBoxCosto.Text = "";
                        cb_ciudadH.SelectedIndex = 0;
                        MessageBox.Show("Se ha cargado un nuevo hotel con exito");
                    }
                    else
                        MessageBox.Show("Hubo un problema");
                }
                else
                    MessageBox.Show("Costo inválido");
            }
            else
                MessageBox.Show("Capacidad inválida");
        }

        private void dataGridViewHoteles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void btnModificarHotel_Click(object sender, EventArgs e)
        {

            string nombre = textBoxNombre.Text;
            string capacidad = textBoxCapacidad.Text;
            string ciudad = cb_ciudadH.Text;
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
            if (string.IsNullOrWhiteSpace(ciudad))
            {
                MessageBox.Show("Ingrese ciudad");
                return;
            }

            if (string.IsNullOrWhiteSpace(costo))
            {
                MessageBox.Show("Ingrese costo");
                return;
            }

            if (int.TryParse(capacidad, out capacidadParseada))
            {
                if (double.TryParse(costo, out costoParseado))
                {
                    if (hotelSeleccionado != -1)
                    {
                        if (miAgencia.modificarHotel(hotelSeleccionado, ciudad, capacidadParseada, costoParseado, nombre))
                        {
                            textBoxNombre.Text = "";
                            textBoxCapacidad.Text = "";
                            textBoxCosto.Text = "";
                            cb_ciudadH.SelectedIndex = 0;
                        }
                        else
                            MessageBox.Show("Problemas al modificar");
                    }
                    else
                        MessageBox.Show("Debe seleccionar un Hotel");
                }
                else
                    MessageBox.Show("Costo inválido");
            }
            else
                MessageBox.Show("Capacidad inválida");
        }

        private void btnEliminarHotel_Click(object sender, EventArgs e)
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

        private void btnBuscarVuelos_Click(object sender, EventArgs e)
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

        private void btnCargarVuelo_Click(object sender, EventArgs e)
        {
            cargarVuelo();
        }

        private void cargarVuelo()
        {
            string origen = cb_origenV.Text;
            string destino = cb_destinoV.Text;
            string capacidad = tbCapacidad.Text;
            string costo = textBox_costo_vuelos.Text;
            DateTime fecha = dateTimePicker_vuelos.Value;
            DateTime fecha_horario = new DateTime(fecha.Year, fecha.Month, fecha.Day, 9, 0, 0);
            string aerolinea = textBox_aerolineas_vuelos.Text;
            string avion = textBox_avion_vuelos.Text;

            double costoParse;
            int capacidadParseada;


            if (double.TryParse(costo, out costoParse))
            {

                if (int.TryParse(capacidad, out capacidadParseada))
                {
                    if (miAgencia.agregarVuelo(origen, destino, capacidadParseada, costoParse, fecha_horario, aerolinea, avion))
                    {
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
                        MessageBox.Show("Hubo un problema");
                }
                else
                    MessageBox.Show("Capacidad inválida");
            }
            else
                MessageBox.Show("Costo inválido");

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

        private void btnModificarVuelo_Click(object sender, EventArgs e)
        {

            string origen = cb_origenV.Text;
            string destino = cb_destinoV.Text;
            string costo = textBox_costo_vuelos.Text;
            DateTime fecha = dateTimePicker_vuelos.Value;
            DateTime fecha_horario = new DateTime(fecha.Year, fecha.Month, fecha.Day, 9, 0, 0);
            string aerolinea = textBox_aerolineas_vuelos.Text;
            string avion = textBox_avion_vuelos.Text;
            string capacidad = tbCapacidad.Text;
            double costoParseado;
            int capacidadParseada;

            if (string.IsNullOrWhiteSpace(origen))
            {
                MessageBox.Show("Ingrese origen");
                return;
            }

            if (string.IsNullOrWhiteSpace(destino))
            {
                MessageBox.Show("Ingrese destino");
                return;
            }
            if (string.IsNullOrWhiteSpace(costo))
            {
                MessageBox.Show("Ingrese costo");
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

            if (double.TryParse(costo, out costoParseado))
            {

                if (int.TryParse(capacidad, out capacidadParseada))
                {
                    if (vueloSeleccionado != -1)
                    {
                        if (miAgencia.modificarVuelo(vueloSeleccionado, origen, destino, capacidadParseada, costoParseado, fecha_horario, aerolinea, avion))
                        {
                            cb_origenV.SelectedIndex = 0;
                            cb_destinoV.SelectedIndex = 0;
                            tbCapacidad.Text = "";
                            textBox_costo_vuelos.Text = "";
                            dateTimePicker_vuelos.Value = DateTime.Now;
                            textBox_aerolineas_vuelos.Text = "";
                            textBox_avion_vuelos.Text = "";
                        }
                    }
                    else
                        MessageBox.Show("Debe seleccionar un Vuelo");
                }
                else
                    MessageBox.Show("Capacidad inválida");
            }
            else
                MessageBox.Show("Costo inválido");

        }

        private void btnEliminarVuelo_Click(object sender, EventArgs e)
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

        private void btnBuscarReservaH_Click(object sender, EventArgs e)
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
            string usuario_id = dataGridViewReservasH[2, e.RowIndex].Value.ToString();
            string usuario = dataGridViewReservasH[3, e.RowIndex].Value.ToString();
            string fechaDesde = dataGridViewReservasH[4, e.RowIndex].Value.ToString();
            string fechaHasta = dataGridViewReservasH[5, e.RowIndex].Value.ToString();
            string pagado = dataGridViewReservasH[6, e.RowIndex].Value.ToString();
            string cantPersonas = dataGridViewReservasH[7, e.RowIndex].Value.ToString();

            reservaHotelSeleccionada = int.Parse(id);
            cbHotelReservaH.Text = hotel;
            cbUsuarioReservaH.Text = usuario_id + ". " + usuario;

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

        private void btnCargarReservaH_Click(object sender, EventArgs e)
        {
            reservasH();
        }

        private void reservasH()
        {

            string hotel = cbHotelReservaH.Text;
            string usuario = cbUsuarioReservaH.Text;
            DateTime fechaDesde = fecha_desde_reservarH.Value;
            DateTime fechaHasta = fecha_hasta_reservarH.Value;
            string pagado = tbPagadoReservaH.Text;
            string cantPersonas = cb_cantPersonasH.Text;

            double pagoParse;
            int cantPersonasParseadas;

            if (double.TryParse(pagado, out pagoParse))
            {

                if (int.TryParse(cantPersonas, out cantPersonasParseadas))
                {
                    if (miAgencia.agregarReservaHotel(hotel, usuario, fechaDesde, fechaHasta, pagoParse, cantPersonasParseadas))
                    {
                        fecha_desde_reservarH.Value = DateTime.Now;
                        fecha_hasta_reservarH.Value = DateTime.Now;
                        cbHotelReservaH.SelectedIndex = 0;
                        cbUsuarioReservaH.SelectedIndex = 0;
                        cb_cantPersonasH.SelectedIndex = 0;
                        tbPagadoReservaH.Text = "";
                        MessageBox.Show("Se ha cargado una nueva reserva con exito");
                    }
                }
                else MessageBox.Show("Cantidad de personas inválida");
            }
            else MessageBox.Show("Pago inválido");

        }

        private void btnModificarReservaH_Click(object sender, EventArgs e)
        {
            string hotel = cbHotelReservaH.Text;
            string usuario = cbUsuarioReservaH.Text;
            DateTime fechaDesde = fecha_desde_reservarH.Value;
            DateTime fechaHasta = fecha_hasta_reservarH.Value;
            string pagado = tbPagadoReservaH.Text;
            string cantPersonas = cb_cantPersonasH.Text;
            double pagadoParseado;
            int cantPersonasParseadas;

            if (string.IsNullOrWhiteSpace(hotel))
            {
                MessageBox.Show("Ingrese hotel");
                return;
            }

            if (string.IsNullOrWhiteSpace(usuario))
            {
                MessageBox.Show("Ingrese usuario");
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

            if (double.TryParse(pagado, out pagadoParseado))
            {

                if (int.TryParse(cantPersonas, out cantPersonasParseadas))
                {
                    if (reservaHotelSeleccionada != -1)
                    {
                        if (miAgencia.modificarReservaHotel(reservaHotelSeleccionada, hotel, usuario, fechaDesde, fechaHasta, pagadoParseado, cantPersonasParseadas))
                        {
                            fecha_desde_reservarH.Value = DateTime.Now;
                            fecha_hasta_reservarH.Value = DateTime.Now;
                            cbHotelReservaH.SelectedIndex = 0;
                            cbUsuarioReservaH.SelectedIndex = 0;
                            cb_cantPersonasH.SelectedIndex = 0;
                            tbPagadoReservaH.Text = "";
                        }
                    }
                    else
                        MessageBox.Show("Debe seleccionar una Reserva");
                }
                else
                    MessageBox.Show("Ingrese una cantidad de personas válida");
            }
            else
                MessageBox.Show("Ingrese un pago válido");
        }

        private void btnEliminarReservaH_Click(object sender, EventArgs e)
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

        private void btnBuscarReservaV_Click(object sender, EventArgs e)
        {
            mostrarReservasV();
            reservaVueloSeleccionada = -1;
        }

        private void mostrarReservasV()
        {

            dataGridViewReservasV.Rows.Clear();

            string origen = cb_origenRV.Text.ToLower();
            string destino = cb_destinoRV.Text.ToLower();
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
            string usuario_id = dataGridViewReservasV[3, e.RowIndex].Value.ToString();
            string usuario = dataGridViewReservasV[4, e.RowIndex].Value.ToString();
            string fecha = dataGridViewReservasV[5, e.RowIndex].Value.ToString();
            string cantPersonas = dataGridViewReservasV[6, e.RowIndex].Value.ToString();
            string pagado = dataGridViewReservasV[7, e.RowIndex].Value.ToString();

            reservaVueloSeleccionada = int.Parse(id);
            cb_origenRV.Text = origen;
            cb_destinoRV.Text = destino;
            cbUsuarioReservaV.Text = usuario_id + ". " + usuario;
            fecha_RV.Value = DateTime.Parse(fecha);
            cb_cantPersonasRV.Text = cantPersonas;
            tbPagadoReservaV.Text = pagado;
        }

        private void btnCargarReservaV_Click(object sender, EventArgs e)
        {
            reservasV();
        }

        private void reservasV()
        {
            string origen = cb_origenRV.Text;
            string destino = cb_destinoRV.Text;
            string usuario = cbUsuarioReservaV.Text;
            string cantPersonas = cb_cantPersonasRV.Text;
            string pagado = tbPagadoReservaV.Text;
            DateTime fecha = fecha_RV.Value;
            DateTime fecha_horario = new DateTime(fecha.Year, fecha.Month, fecha.Day, 9, 0, 0);

            double pagoParse;
            int cantPersonasParseadas;

            if (string.IsNullOrWhiteSpace(origen))
            {
                MessageBox.Show("Ingrese origen");
                return;
            }

            if (string.IsNullOrWhiteSpace(destino))
            {
                MessageBox.Show("Ingrese destino");
                return;
            }
            if (string.IsNullOrWhiteSpace(usuario))
            {
                MessageBox.Show("Ingrese usuario");
                return;
            }

            if (double.TryParse(pagado, out pagoParse))
            {

                if (int.TryParse(cantPersonas, out cantPersonasParseadas))
                {
                    if (miAgencia.agregarReservaVuelo(origen, destino, usuario, pagoParse, cantPersonasParseadas, fecha_horario))
                    {
                        cb_origenRV.Text = "";
                        cb_destinoRV.Text = "";
                        cbUsuarioReservaV.SelectedIndex = 0;
                        cb_cantPersonasRV.SelectedIndex = 0;
                        tbPagadoReservaV.Text = "";
                        fecha_RV.Value = DateTime.Now;
                        MessageBox.Show("Se ha cargado una nueva reserva con exito");
                    }
                }
                else
                    MessageBox.Show("Cantidad de personas inválida");
            }
            else
                MessageBox.Show("Pago inválido");
        }

        private void btnModificarReservaV_Click(object sender, EventArgs e)
        {

            string origen = cb_origenRV.Text;
            string destino = cb_destinoRV.Text;
            string usuario = cbUsuarioReservaV.Text;
            string cantPersonas = cb_cantPersonasRV.Text;
            string pagado = tbPagadoReservaV.Text;
            DateTime fecha = fecha_RV.Value;
            DateTime fecha_horario = new DateTime(fecha.Year, fecha.Month, fecha.Day, 9, 0, 0);

            double pagoParseado;
            int cantPersonasParseadas;

            if (string.IsNullOrWhiteSpace(origen))
            {
                MessageBox.Show("Ingrese origen");
                return;
            }
            if (string.IsNullOrWhiteSpace(destino))
            {
                MessageBox.Show("Ingrese destino");
                return;
            }
            if (string.IsNullOrWhiteSpace(usuario))
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
                if (double.TryParse(pagado, out pagoParseado))
                {

                    if (int.TryParse(cantPersonas, out cantPersonasParseadas))
                    {
                        if (miAgencia.modificarReservaVuelo(reservaVueloSeleccionada, origen, destino, usuario, pagoParseado, cantPersonasParseadas, fecha_horario))
                        {
                            cb_origenRV.Text = "";
                            cb_destinoRV.Text = "";
                            cbUsuarioReservaV.SelectedIndex = 0;
                            cb_cantPersonasRV.SelectedIndex = 0;
                            tbPagadoReservaV.Text = "";
                            fecha_RV.Value = DateTime.Now;
                        }
                    }
                    else
                        MessageBox.Show("Cantidad de personas inválida");
                }
                else
                    MessageBox.Show("Pago inválido");
            }
            else
                MessageBox.Show("Debe seleccionar una Reserva");

        }

        private void btnEliminarReservaV_Click(object sender, EventArgs e)
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
            cb_origenRV.SelectedIndex = 0;
            cb_destinoRV.SelectedIndex = 0;
            cbUsuarioReservaV.SelectedIndex = 0;
            cb_cantPersonasRV.SelectedIndex = 0;
            tbPagadoReservaV.Text = "";
            fecha_RV.Value = DateTime.Now;
        }

        //private void cb_origenRV_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string origenSeleccionado = cb_origenRV.SelectedItem as string;

        //    List<string> destinosFiltrados = miAgencia.obtenerDestinosAsociadosAlOrigen(origenSeleccionado);

        //    cb_destinoRV.DataSource = destinosFiltrados;
        //}

        //private void cb_destinoRV_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string destinoSeleccionado = cb_destinoRV.SelectedItem as string;

        //    List<string> origenesFiltrados = miAgencia.obtenerOrigenesAsociadosAlDestino(destinoSeleccionado);

        //    cb_origenRV.DataSource = origenesFiltrados;
        //}
    }
}
