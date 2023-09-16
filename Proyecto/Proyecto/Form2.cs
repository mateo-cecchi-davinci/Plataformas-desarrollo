using Microsoft.VisualBasic.Logging;
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

namespace Proyecto
{
    public partial class Form2 : Form
    {
        private Agencia miAgencia;

        public TransfDelegado TransfEvento;

        public Form2(Agencia agencia)
        {
            miAgencia = agencia;
            InitializeComponent();
        }

        private void tbMail_Enter(object sender, EventArgs e)
        {
            if (tbMail.Text == "Ingrese su mail")
            {
                tbMail.Text = "";

                tbMail.ForeColor = Color.Black;
            }
        }

        private void tbMail_Leave(object sender, EventArgs e)
        {
            if (tbMail.Text == "")
            {
                tbMail.Text = "Ingrese su mail";

                tbMail.ForeColor = Color.DimGray;
            }
        }

        private void tbClave_Enter(object sender, EventArgs e)
        {
            if (tbClave.Text == "Ingrese su contraseña")
            {
                tbClave.Text = "";

                tbClave.ForeColor = Color.Black;
            }
        }

        private void tbClave_Leave(object sender, EventArgs e)
        {
            if (tbClave.Text == "")
            {
                tbClave.Text = "Ingrese su contraseña";

                tbClave.ForeColor = Color.DimGray;
            }
        }

        private void btnIngresar(object sender, EventArgs e)
        {
            string mail = tbMail.Text;
            string clave = tbClave.Text;
            if (mail != null && mail != "" && clave != null && clave != "")
            {
                if (miAgencia.iniciarSesion(mail, clave))
                    TransfEvento();
                else
                    MessageBox.Show("Error, usuario o contraseña incorrectos");
            }
            else
                MessageBox.Show("Debe ingresar un usuario y contraseña");
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public delegate void TransfDelegado();

    }
}
