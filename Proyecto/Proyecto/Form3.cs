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
        public cerrarSesion salir;

        public Form3(Agencia miAgencia)
        {
            InitializeComponent();
            this.miAgencia = miAgencia;
            nombreUsuario.Text = miAgencia.nombreLogueado();
            usuarioSeleccionado = -1;
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

    }
}
