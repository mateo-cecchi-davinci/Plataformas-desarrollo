using Microsoft.VisualBasic.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Proyecto.Form2;

namespace Proyecto
{
    public partial class Form4 : Form
    {
        private Agencia miAgencia;
        public registrado submit;
        public volver back;



        public Form4(Agencia agencia)
        {
            miAgencia = agencia;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            validarUsuario();



        }


        private void validarUsuario()
        {
            string nombre = tbNombre.Text;
            string apellido = tbApellido.Text;
            string dni = tbDni.Text;
            string mail = tbMail.Text;
            string clave = tbClave.Text;
            string repClave = tbRepClave.Text;
            int numDni;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El campo nombre no puede estar vacío");
                return;
            }

            if (string.IsNullOrWhiteSpace(apellido))
            {
                MessageBox.Show("El campo apellido no puede estar vacío");
                return;
            }
            if (string.IsNullOrWhiteSpace(dni))
            {
                MessageBox.Show("El campo Dni no puede estar vacío");
                return;
            }

            if (string.IsNullOrWhiteSpace(mail))
            {
                MessageBox.Show("El campo mail no puede estar vacío");
                return;
            }

            if (string.IsNullOrWhiteSpace(clave))
            {
                MessageBox.Show("El campo contraseña no puede estar vacío");
                return;
            }

            if (string.IsNullOrWhiteSpace(repClave))
            {
                MessageBox.Show("El campo repetir contraseña no puede estar vacío");
                return;
            }

            if (int.TryParse(dni, out numDni))
            {
                miAgencia.agregarUsuario(numDni, nombre, apellido, mail, clave);
                submit();
            }

        }

        private void tbNombre_Enter_1(object sender, EventArgs e)
        {
            if (tbNombre.PlaceholderText == "Ingrese su nombre")
            {
                tbNombre.PlaceholderText = "";

                tbNombre.ForeColor = Color.Black;
            }
        }

        private void tbNombre_Leave_1(object sender, EventArgs e)
        {
            if (tbNombre.PlaceholderText == "")
            {
                tbNombre.PlaceholderText = "Ingrese su nombre";

                tbNombre.ForeColor = Color.DimGray;
            }


        }
        private void tbMail_Enter_1(object sender, EventArgs e)
        {
            if (tbMail.PlaceholderText == "Ingrese su mail")
            {
                tbMail.PlaceholderText = "";

                tbMail.ForeColor = Color.Black;
            }
        }

        private void tbMail_Leave_1(object sender, EventArgs e)
        {
            if (tbMail.PlaceholderText == "")
            {
                tbMail.PlaceholderText = "Ingrese su mail";

                tbMail.ForeColor = Color.DimGray;
            }
        }




        private void tbClave_Enter_1(object sender, EventArgs e)
        {
            if (tbRepClave.PlaceholderText == "Ingrese una contraseña")
            {
                tbRepClave.PlaceholderText = "";

                tbRepClave.ForeColor = Color.DimGray;
            }

        }

        private void tbClave_Leave_1(object sender, EventArgs e)
        {
            if (tbRepClave.PlaceholderText == "")
            {
                tbRepClave.PlaceholderText = "Ingrese una contraseña";

                tbRepClave.ForeColor = Color.DimGray;
            }
        }
        private void tbRepClave_Enter_1(object sender, EventArgs e)
        {
            if (tbRepClave.PlaceholderText == "Repita la contraseña")
            {
                tbRepClave.PlaceholderText = "";

                tbRepClave.ForeColor = Color.DimGray;
            }

        }

        private void tbRepClave_Leave_1(object sender, EventArgs e)
        {
            if (tbRepClave.PlaceholderText == "")
            {
                tbRepClave.PlaceholderText = "Repita la contraseña";

                tbRepClave.ForeColor = Color.DimGray;
            }
        }
        private void tbApellido_Enter_1(object sender, EventArgs e)
        {
            if (tbApellido.PlaceholderText == "")
            {
                tbApellido.PlaceholderText = "Repita la contraseña";

                tbApellido.ForeColor = Color.DimGray;
            }
        }

        private void tbApellido_Leave_1(object sender, EventArgs e)
        {
            if (tbApellido.PlaceholderText == "")
            {
                tbApellido.PlaceholderText = "Repita la contraseña";

                tbApellido.ForeColor = Color.DimGray;
            }
        }

        private void tbDni_Enter_1(object sender, EventArgs e)
        {
            if (tbDni.PlaceholderText == "")
            {
                tbDni.PlaceholderText = "Repita la contraseña";

                tbDni.ForeColor = Color.DimGray;
            }
        }

        private void tbDni_Leave_1(object sender, EventArgs e)
        {
            if (tbDni.PlaceholderText == "")
            {
                tbDni.PlaceholderText = "Repita la contraseña";

                tbDni.ForeColor = Color.DimGray;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            back();
        }


        public delegate void registrado();
        public delegate void volver();
    }
}


