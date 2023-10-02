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
            cb_hotel.DataSource = hoteles.ToList();

            List<string> ciudades = new List<string>();
            ciudades.Add("");
            foreach (Ciudad c in miAgencia.obtenerCiudad())
            {
                ciudades.Add(c.nombre);
            }
            cb_ciudadH.DataSource = ciudades.ToList();
            cb_origenV.DataSource = ciudades.ToList();
            cb_destinoV.DataSource = ciudades.ToList();

            foreach (Vuelo v in miAgencia.obtenerVuelos())
            {
                dataGridView_vuelos_UC.Rows.Add(v.id, v.origen.nombre, v.destino.nombre, v.costo, v.capacidad, v.fecha, v.aerolinea, v.avion);
            }

            foreach (Ciudad c in miAgencia.obtenerCiudad())
            {
                dataGridView_ciudades_UC.Rows.Add(c.nombre);
            }

            Usuario usuario = miAgencia.obtenerUsuarioActual();

            int rowIndexHotel = 0;
            int rowIndexVuelo = 0;

            if (usuario.misReservasHoteles != null)
            {
                foreach (ReservaHotel reserva in usuario.misReservasHoteles)
                {
                    if (reserva.miHotel != null)
                    {
                        dataGridView_perfil_UC.Rows.Add();
                        dataGridView_perfil_UC.Rows[rowIndexHotel].Cells[0].Value = reserva.miHotel.nombre;

                        rowIndexHotel++;
                    }
                }
            }

            if (usuario.misReservasVuelos != null)
            {
                foreach (ReservaVuelo reserva in usuario.misReservasVuelos)
                {
                    if (reserva.miVuelo != null)
                    {
                        if (rowIndexVuelo >= dataGridView_perfil_UC.Rows.Count)
                        {
                            dataGridView_perfil_UC.Rows.Add();
                        }
                        dataGridView_perfil_UC.Rows[rowIndexVuelo].Cells[1].Value = reserva.miVuelo.destino.nombre;

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
            cb_hotel.Text = hotel;
            cb_ciudadH.Text = ciudad;
            hotelSeleccionado = int.Parse(id);
        }

        private void btn_comprarHotel_Click(object sender, EventArgs e)
        {
            reservarHotel();
        }

        private void reservarHotel()
        {
            Hotel hotel = miAgencia.obtenerHoteles().FirstOrDefault(h => h.id == hotelSeleccionado);
            Usuario usuario = miAgencia.obtenerUsuarioActual();
            DateTime fechaDesde = dateTimePicker1.Value;
            DateTime fechaHasta = dateTimePicker2.Value;
            string cantPersonas = cb_cantPersonasH.Text;
            int cantPersonasParseadas;

            if (int.TryParse(cantPersonas, out cantPersonasParseadas) && cantPersonasParseadas <= 10 && cantPersonasParseadas > 0)
            {
                if (usuario.credito >= hotel.costo)
                {
                    hotel.capacidad -= cantPersonasParseadas;
                    usuario.credito -= hotel.costo;
                    miAgencia.agregarReservaHotel(hotel, usuario, fechaDesde, fechaHasta, hotel.costo, cantPersonasParseadas);

                    cb_hotel.Text = "";
                    cb_ciudadH.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    dateTimePicker2.Value = DateTime.Now;
                    cb_cantPersonasH.Text = "";
                    MessageBox.Show("Se ha cargado una nueva reserva con exito");
                }
                else MessageBox.Show("Crédito insuficiente");
            }
            else MessageBox.Show("Cantidad de personas inválida");

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
            cb_origenV.Text = origen;
            cb_destinoV.Text = destino;
            vueloSeleccionado = int.Parse(id);
        }

        private void btn_comprarVuelo_Click(object sender, EventArgs e)
        {
            reservarVuelo();
        }

        private void reservarVuelo()
        {
            string origenSeleccionado = cb_origenV.Text.ToLower();
            string destinoSeleccionado = cb_destinoV.Text.ToLower();
            Ciudad origen = miAgencia.obtenerCiudad().FirstOrDefault(c => c.nombre.ToLower().Contains(origenSeleccionado));
            Ciudad destino = miAgencia.obtenerCiudad().FirstOrDefault(c => c.nombre.ToLower().Contains(destinoSeleccionado));
            Vuelo vuelo = miAgencia.obtenerVuelos().FirstOrDefault(v => v.id == vueloSeleccionado);
            Usuario usuario = miAgencia.obtenerUsuarioActual();
            DateTime fecha = dateTimePicker3.Value;
            string cantPersonas = cb_cantPersonasV.Text;
            int cantPersonasParseadas;

            if (int.TryParse(cantPersonas, out cantPersonasParseadas) && cantPersonasParseadas <= 10 && cantPersonasParseadas > 0)
            {
                if (usuario.credito >= vuelo.costo)
                {
                    vuelo.capacidad -= cantPersonasParseadas;
                    usuario.credito -= vuelo.costo;
                    miAgencia.agregarReservaVuelo(vuelo, usuario, vuelo.costo, cantPersonasParseadas);

                    cb_origenV.Text = "";
                    cb_destinoV.Text = "";
                    dateTimePicker3.Value = DateTime.Now;
                    cb_cantPersonasV.Text = "";
                    MessageBox.Show("Se ha cargado una nueva reserva con exito");
                }
                else MessageBox.Show("Crédito insuficiente");
            }
            else MessageBox.Show("Cantidad de personas inválida");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label_credito.Text = miAgencia.mostrarCredito().ToString();
            Usuario usuario = miAgencia.obtenerUsuarioActual();

            int rowIndexHotel = 0;
            int rowIndexVuelo = 0;

            if (usuario.misReservasHoteles != null)
            {
                foreach (ReservaHotel reserva in usuario.misReservasHoteles)
                {
                    if (reserva.miHotel != null)
                    {
                        dataGridView_perfil_UC.Rows.Add();
                        dataGridView_perfil_UC.Rows[rowIndexHotel].Cells[0].Value = reserva.miHotel.nombre;

                        rowIndexHotel++;
                    }
                }
            }

            if (usuario.misReservasVuelos != null)
            {
                foreach (ReservaVuelo reserva in usuario.misReservasVuelos)
                {
                    if (reserva.miVuelo != null)
                    {
                        if (rowIndexVuelo >= dataGridView_perfil_UC.Rows.Count)
                        {
                            dataGridView_perfil_UC.Rows.Add();
                        }
                        dataGridView_perfil_UC.Rows[rowIndexVuelo].Cells[1].Value = reserva.miVuelo.destino.nombre;

                        rowIndexVuelo++;
                    }
                }
            }
        }

        public delegate void cerrar();
    }
}
