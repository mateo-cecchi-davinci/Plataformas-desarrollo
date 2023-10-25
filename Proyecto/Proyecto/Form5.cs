using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Proyecto.Form3;
using static Proyecto.Form4;

namespace Proyecto
{
    public partial class Form5 : Form
    {
        private Agencia miAgencia;
        private int hotelSeleccionado;
        private int vueloSeleccionado;
        public cerrar salir;


        public Form5(Agencia agencia)
        {
            miAgencia = agencia;
            InitializeComponent();
            hotelSeleccionado = -1;
            vueloSeleccionado = -1;
        }

        private void Form5_Load(object sender, EventArgs e)
        {

            Usuario usuario = miAgencia.obtenerUsuarioActual();

            int rowIndexHotel = 0;
            int rowIndexVuelo = 0;
            int rowIndexReservaHotel = 0;
            int rowIndexReservaVuelo = 0;

            string nombre = miAgencia.nombreLogueado();
            int indice = nombre.IndexOf(" ") + 1;
            string nombreU = nombre.Substring(indice);
            lbl_nombre.Text = nombreU;

            label_credito.Text = miAgencia.mostrarCredito().ToString();
            string[] opciones = { "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            cb_cantPersonasH.Items.AddRange(opciones);
            cb_cantPersonasH.SelectedIndex = 0;
            cb_cantPersonasV.Items.AddRange(opciones);
            cb_cantPersonasV.SelectedIndex = 0;

            List<string> hoteles = new List<string>();
            hoteles.Add("");
            foreach (Hotel h in miAgencia.obtenerHoteles())
            {
                hoteles.Add(h.nombre);
            }
            cb_hotel.DataSource = hoteles;

            List<string> ciudades = new List<string>();
            ciudades.Add("");
            foreach (Ciudad c in miAgencia.obtenerCiudades())
            {
                ciudades.Add(c.nombre);
            }
            cb_ciudadH.DataSource = ciudades;
            cb_origenV.DataSource = ciudades;
            cb_destinoV.DataSource = ciudades.ToList();

            foreach (Hotel h in miAgencia.obtenerHoteles())
            {
                dataGridView_hoteles_UC.Rows.Add(h.ToString());

            }

            foreach (Vuelo v in miAgencia.obtenerVuelos())
            {
                dataGridView_vuelos_UC.Rows.Add(v.ToString());
            }

            foreach (Ciudad c in miAgencia.obtenerCiudades())
            {
                dataGridView_ciudades_UC.Rows.Add(c.nombre);
            }

            foreach (ReservaHotel reserva in usuario.misReservasHoteles)
            {
                if (reserva.miHotel != null)
                {
                    dataGridView_perfil_UC.Rows.Add();

                    dataGridView_perfil_UC.Rows[rowIndexReservaHotel].Cells[0].Value = reserva.miHotel.nombre;

                    rowIndexReservaHotel++;
                }
            }

            foreach (ReservaVuelo reserva in usuario.misReservasVuelos)
            {
                if (reserva.miVuelo != null)
                {
                    if (rowIndexReservaVuelo >= dataGridView_perfil_UC.Rows.Count)
                    {
                        dataGridView_perfil_UC.Rows.Add();
                    }
                    
                    dataGridView_perfil_UC.Rows[rowIndexReservaVuelo].Cells[1].Value = reserva.miVuelo.destino.nombre;

                    rowIndexReservaVuelo++;
                }
            }

            foreach (UsuarioHotel uh in miAgencia.obtenerUsuariosHoteles())
            {
                foreach (Hotel h in usuario.hotelesVisitados)
                {
                    if (uh.usuario_fk == usuario.id && uh.hotel_fk == h.id)
                    {
                        if (rowIndexHotel >= dataGridView_perfil_UC.Rows.Count)
                        {
                            dataGridView_perfil_UC.Rows.Add();
                        }

                        dataGridView_perfil_UC.Rows[rowIndexHotel].Cells[3].Value = h.nombre + " x" + uh.cantidad;

                        rowIndexHotel++;
                    }
                }
            }

            foreach (UsuarioVuelo uv in miAgencia.obtenerUsuariosVuelos())
            {
                foreach (Vuelo v in usuario.vuelosTomados)
                {
                    if (uv.usuario_fk == usuario.id && uv.vuelo_fk == v.id)
                    {
                        if (rowIndexVuelo >= dataGridView_perfil_UC.Rows.Count)
                        {
                            dataGridView_perfil_UC.Rows.Add();
                        }

                        dataGridView_perfil_UC.Rows[rowIndexVuelo].Cells[2].Value = v.destino.nombre + " x" + uv.cantidad;

                        rowIndexVuelo++;
                    }
                }
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            miAgencia.cerrarSesion();
            salir();
        }

        private void btn_buscarHotel_Click(object sender, EventArgs e)
        {

            dataGridView_hoteles_UC.Rows.Clear();

            string hotel = cb_hotel.Text.ToLower();
            string ciudad = cb_ciudadH.Text.ToLower();

            foreach (Hotel h in miAgencia.obtenerHoteles())
            {
                string nombreHotel = h.nombre.ToLower();
                string ciudadHotel = h.ubicacion.nombre.ToLower();

                if (nombreHotel.Contains(hotel) &&
                    ciudadHotel.Contains(ciudad))
                {
                    dataGridView_hoteles_UC.Rows.Add(h.ToString());
                }
            }

        }

        private void doble_click_hoteles(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridView_hoteles_UC[0, e.RowIndex].Value.ToString();
            string hotel = dataGridView_hoteles_UC[1, e.RowIndex].Value.ToString();
            string ciudad = dataGridView_hoteles_UC[2, e.RowIndex].Value.ToString();
            string costo = dataGridView_hoteles_UC[3, e.RowIndex].Value.ToString();
            cb_hotel.Text = hotel;
            cb_ciudadH.Text = ciudad;
            tb_pagoH.Text = costo;
            hotelSeleccionado = int.Parse(id);
        }

        private void btn_comprarHotel_Click(object sender, EventArgs e)
        {
            reservarHotel();
        }

        private void reservarHotel()
        {
            string hotel = cb_hotel.Text;
            string usuario = miAgencia.nombreLogueado();
            string pago = tb_pagoH.Text;
            DateTime fechaDesde = dateTimePicker1.Value;
            DateTime fechaHasta = dateTimePicker2.Value;
            DateTime fechaDesde_horario = new DateTime(fechaDesde.Year, fechaDesde.Month, fechaDesde.Day, 9, 0, 0);
            DateTime fechaHasta_horario = new DateTime(fechaHasta.Year, fechaHasta.Month, fechaHasta.Day, 9, 0, 0);
            string cantPersonas = cb_cantPersonasH.Text;

            double pagoParseado;
            int cantPersonasParseadas;

            if (int.TryParse(cantPersonas, out cantPersonasParseadas))
            {
                if (double.TryParse(pago, out pagoParseado))
                {
                    if (miAgencia.agregarReservaHotel(hotel, usuario, fechaDesde_horario, fechaHasta_horario, pagoParseado, cantPersonasParseadas))
                    {
                        cb_hotel.Text = "";
                        cb_ciudadH.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        dateTimePicker2.Value = DateTime.Now;
                        cb_cantPersonasH.Text = "";
                        MessageBox.Show("Se ha cargado una nueva reserva con exito");
                    }
                }
                else
                    MessageBox.Show("Cantidad de personas inválida");
            }
            else
                MessageBox.Show("Cantidad de personas inválida");

        }

        private void btn_buscarVuelo_Click(object sender, EventArgs e)
        {

            dataGridView_vuelos_UC.Rows.Clear();

            string origen = cb_origenV.Text.ToLower();
            string destino = cb_destinoV.Text.ToLower();

            foreach (Vuelo v in miAgencia.obtenerVuelos())
            {
                string origenVuelo = v.origen.nombre.ToLower();
                string destinoVuelo = v.destino.nombre.ToLower();

                if (origenVuelo.Contains(origen) &&
                    destinoVuelo.Contains(destino))
                {
                    dataGridView_vuelos_UC.Rows.Add(v.ToString());
                }
            }

        }

        private void doble_click_vuelos(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridView_vuelos_UC[0, e.RowIndex].Value.ToString();
            string origen = dataGridView_vuelos_UC[1, e.RowIndex].Value.ToString();
            string destino = dataGridView_vuelos_UC[2, e.RowIndex].Value.ToString();
            string costo = dataGridView_vuelos_UC[3, e.RowIndex].Value.ToString();
            string fecha = dataGridView_vuelos_UC[5, e.RowIndex].Value.ToString();
            cb_origenV.Text = origen;
            cb_destinoV.Text = destino;
            tb_pagoV.Text = costo;
            vueloSeleccionado = int.Parse(id);

            if (DateTime.TryParse(fecha, out DateTime fechaParseada))
            {
                dateTimePicker3.Value = fechaParseada;
            }
            else
            {
                MessageBox.Show("Formato de fecha incorrecto");
            }

        }

        private void btn_comprarVuelo_Click(object sender, EventArgs e)
        {
            reservarVuelo();
        }

        private void reservarVuelo()
        {
            string origen = cb_origenV.Text;
            string destino = cb_destinoV.Text;
            string pago = tb_pagoV.Text;
            string usuario = miAgencia.nombreLogueado();
            DateTime fecha = dateTimePicker3.Value;
            DateTime fecha_horario = new DateTime(fecha.Year, fecha.Month, fecha.Day, 9, 0, 0);
            string cantPersonas = cb_cantPersonasV.Text;

            double pagoParseado;
            int cantPersonasParseadas;

            if (int.TryParse(cantPersonas, out cantPersonasParseadas))
            {
                if (double.TryParse(pago, out pagoParseado))
                {
                    if (miAgencia.agregarReservaVuelo(origen, destino, usuario, pagoParseado, cantPersonasParseadas, fecha_horario))
                    {
                        cb_origenV.Text = "";
                        cb_destinoV.Text = "";
                        dateTimePicker3.Value = DateTime.Now;
                        cb_cantPersonasV.Text = "";
                        MessageBox.Show("Se ha cargado una nueva reserva con exito");
                    }
                }
                else
                    MessageBox.Show("Cantidad de personas inválida");
            }
            else
                MessageBox.Show("Cantidad de personas inválida");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label_credito.Text = miAgencia.mostrarCredito().ToString();
            Usuario usuario = miAgencia.obtenerUsuarioActual();

            dataGridView_perfil_UC.Rows.Clear();

            int rowIndexReservaHotel = 0;
            int rowIndexReservaVuelo = 0;
            int rowIndexHotel = 0;
            int rowIndexVuelo = 0;

            foreach (ReservaHotel reserva in usuario.misReservasHoteles)
            {
                if (reserva.miHotel != null)
                {
                    dataGridView_perfil_UC.Rows.Add();
                    dataGridView_perfil_UC.Rows[rowIndexReservaHotel].Cells[0].Value = reserva.miHotel.nombre;

                    rowIndexReservaHotel++;
                }
            }

            foreach (ReservaVuelo reserva in usuario.misReservasVuelos)
            {
                if (reserva.miVuelo != null)
                {
                    if (rowIndexVuelo >= dataGridView_perfil_UC.Rows.Count)
                    {
                        dataGridView_perfil_UC.Rows.Add();
                    }
                    dataGridView_perfil_UC.Rows[rowIndexReservaVuelo].Cells[1].Value = reserva.miVuelo.destino.nombre;

                    rowIndexReservaVuelo++;
                }
            }

            foreach (UsuarioHotel uh in miAgencia.obtenerUsuariosHoteles())
            {
                foreach (Hotel h in usuario.hotelesVisitados)
                {
                    if (uh.usuario_fk == usuario.id && uh.hotel_fk == h.id)
                    {
                        if (rowIndexHotel >= dataGridView_perfil_UC.Rows.Count)
                        {
                            dataGridView_perfil_UC.Rows.Add();
                        }

                        dataGridView_perfil_UC.Rows[rowIndexHotel].Cells[3].Value = h.nombre + " x" + uh.cantidad;

                        rowIndexHotel++;
                    }
                }
            }

            foreach (UsuarioVuelo uv in miAgencia.obtenerUsuariosVuelos())
            {
                foreach (Vuelo v in usuario.vuelosTomados)
                {
                    if (uv.usuario_fk == usuario.id && uv.vuelo_fk == v.id)
                    {
                        if (rowIndexVuelo >= dataGridView_perfil_UC.Rows.Count)
                        {
                            dataGridView_perfil_UC.Rows.Add();
                        }

                        dataGridView_perfil_UC.Rows[rowIndexVuelo].Cells[2].Value = v.destino.nombre + " x" + uv.cantidad;

                        rowIndexVuelo++;
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cb_hotel.SelectedIndex = 0;
            cb_ciudadH.SelectedIndex = 0;
            cb_cantPersonasH.SelectedIndex = 0;
            tb_pagoH.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            cb_origenV.SelectedIndex = 0;
            cb_destinoV.SelectedIndex = 0;
            cb_cantPersonasV.SelectedIndex = 0;
            tb_pagoV.Text = "";
        }

        public delegate void cerrar();
    }
}
