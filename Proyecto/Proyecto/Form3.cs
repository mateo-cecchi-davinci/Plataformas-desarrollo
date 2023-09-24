using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public cerrarSesion salir;

        public Form3(Agencia miAgencia)
        {
            InitializeComponent();
            this.miAgencia = miAgencia;
            nombreUsuario.Text = miAgencia.nombreLogueado();
            nombreUsuarioH.Text = miAgencia.nombreLogueado();
            nombreUsuarioVuelos.Text = miAgencia.nombreLogueado();
            usuarioSeleccionado = -1;
            hotelSeleccionado = -1;
            vueloSeleccionado = -1;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            usuarioSeleccionado = -1;
        }

        private void limpiarCampos()
        {
            dataGridView1.Rows.Clear();

            foreach (Usuario u in miAgencia.obtenerUsuarios())
            {
                dataGridView1.Rows.Add(u.ToString());
            }

            tbNombre.Text = "";
            tbApellido.Text = "";
            tbDni.Text = "";
            tbMail.Text = "";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridView1[0, e.RowIndex].Value.ToString();
            string dni = dataGridView1[1, e.RowIndex].Value.ToString();
            string nombre = dataGridView1[2, e.RowIndex].Value.ToString();
            string apellido = dataGridView1[3, e.RowIndex].Value.ToString();
            string mail = dataGridView1[4, e.RowIndex].Value.ToString();
            tbDni.Text = dni;
            tbNombre.Text = nombre;
            tbApellido.Text = apellido;
            tbMail.Text = mail;
            usuarioSeleccionado = int.Parse(id);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionado != -1)
            {
                if (miAgencia.modificarUsuario(usuarioSeleccionado, tbDni.Text, tbNombre.Text, tbApellido.Text, tbMail.Text))
                    MessageBox.Show("Modificado con exito");
                else
                    MessageBox.Show("Problemas al modificar");
            }
            else
                MessageBox.Show("Debe seleccionar un usuario");
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

            foreach (Hotel h in miAgencia.obtenerHoteles())
            {
                dataGridView2Hoteles.Rows.Add(h.ToString());
            }
            textBoxNombre.Text = "";
            textBoxCapacidad.Text = "";
            textBoxCosto.Text = "";
            textBoxCiudad.Text = "";
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
            Ciudad ciudad = new Ciudad(textBoxCiudad.Text);
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
                    textBoxCiudad.Text = "";
                    MessageBox.Show("Se ha cargado un nuevo hotel con exito");
                }
                else MessageBox.Show("ingrese solo numero en Costo");


            }
            else MessageBox.Show("ingrese solo numero en Capacidad");

        }

        private void dataGridView2Hoteles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridView2Hoteles[0, e.RowIndex].Value.ToString();
            string ubicacion = dataGridView2Hoteles[4, e.RowIndex].Value.ToString();
            string capacidad = dataGridView2Hoteles[2, e.RowIndex].Value.ToString();
            string costo = dataGridView2Hoteles[3, e.RowIndex].Value.ToString();
            string nombre = dataGridView2Hoteles[1, e.RowIndex].Value.ToString();
            textBoxCiudad.Text = ubicacion;
            textBoxNombre.Text = nombre;
            textBoxCapacidad.Text = capacidad;
            textBoxCosto.Text = costo;
            hotelSeleccionado = int.Parse(id);
        }

        private void Modificar_Click(object sender, EventArgs e)
        {
            if (hotelSeleccionado != -1)
            {
                if (miAgencia.modificarHoteles(hotelSeleccionado, new Ciudad(textBoxCiudad.Text), textBoxCapacidad.Text, textBoxCosto.Text, textBoxNombre.Text))
                    MessageBox.Show("Modificado con exito");
                else
                    MessageBox.Show("Problemas al modificar");
            }
            else
                MessageBox.Show("Debe seleccionar un Hotel");
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

        private void Salir_Click(object sender, EventArgs e)
        {
            salir();
        }


        //Seccion CRUD Vuelos


        private void Cargar_vuelos_Click(object sender, EventArgs e)
        {
            cargarVuelo();
        }


        private void cargarVuelo()
        {
            Ciudad origen = new Ciudad(textBox_origen_vuelos.Text);
            Ciudad destino = new Ciudad(textBox_destino_vuelos.Text);
            string costo = textBox_costo_vuelos.Text;
            DateTime fecha = dateTimePicker_vuelos.Value;
            string aerolinea = textBox_aerolineas_vuelos.Text;
            string avion = textBox_avion_vuelos.Text;

            double costoParse;


            if (double.TryParse(costo, out costoParse))
            {
                miAgencia.agregarVuelos(origen, destino, costoParse, fecha, aerolinea, avion);
                textBox_origen_vuelos.Text = "";
                textBox_destino_vuelos.Text = "";
                textBox_costo_vuelos.Text = "";
                textBox_aerolineas_vuelos.Text = "";
                textBox_avion_vuelos.Text = "";
                MessageBox.Show("Se ha cargado un nuevo Vuelo con exito");
            }
            else MessageBox.Show("ingrese solo numero en Costo");


        }

        private void Mostrar_vuelos_Click(object sender, EventArgs e)
        {
            mostrarVuelos();
            vueloSeleccionado = -1;
        }

        private void mostrarVuelos()
        {
            dataGridViewVuelos.Rows.Clear();
            foreach (Vuelo v in miAgencia.obtenerVuelos())
            {
                dataGridViewVuelos.Rows.Add(v.ToString());
            }
            textBox_origen_vuelos.Text = "";
            textBox_destino_vuelos.Text = "";
            textBox_costo_vuelos.Text = "";
            textBox_aerolineas_vuelos.Text = "";
            textBox_avion_vuelos.Text = "";
            dateTimePicker_vuelos.Value = DateTime.Now;
        }

        private void dataGridViewVuelos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewVuelos[0, e.RowIndex].Value.ToString();
            string origen = dataGridViewVuelos[1, e.RowIndex].Value.ToString();
            string destino = dataGridViewVuelos[2, e.RowIndex].Value.ToString();
            string fechaString = dataGridViewVuelos[4, e.RowIndex].Value.ToString();
            string costo = dataGridViewVuelos[3, e.RowIndex].Value.ToString();
            string aerolinea = dataGridViewVuelos[5, e.RowIndex].Value.ToString();
            string avion = dataGridViewVuelos[6, e.RowIndex].Value.ToString();

            vueloSeleccionado = int.Parse(id);
            textBox_origen_vuelos.Text = origen;
            textBox_destino_vuelos.Text = destino;
            if (DateTime.TryParse(fechaString, out DateTime fecha))
            {
                dateTimePicker_vuelos.Value = fecha;
            }
            else
            {
                MessageBox.Show("Formato de fecha incorrecto");
            }
            textBox_costo_vuelos.Text = costo;
            textBox_aerolineas_vuelos.Text = aerolinea;
            textBox_avion_vuelos.Text = avion;

        }

        private void Modificar_vuelos_Click(object sender, EventArgs e)
        {
            if (vueloSeleccionado != -1)
            {
                if (miAgencia.modificarVuelos(vueloSeleccionado, new Ciudad(textBox_origen_vuelos.Text), new Ciudad(textBox_destino_vuelos.Text), textBox_costo_vuelos.Text, dateTimePicker_vuelos.Text, textBox_aerolineas_vuelos.Text, textBox_avion_vuelos.Text))
                    MessageBox.Show("Modificado con exito");
                else
                    MessageBox.Show("Problemas al modificar");
            }
            else
                MessageBox.Show("Debe seleccionar un Vuelo");
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
    }
}
