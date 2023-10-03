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
using System.Runtime.InteropServices;

namespace Proyecto
{
    public partial class Form2 : Form
    {
        private Agencia miAgencia;

        public TransfDelegado TransfEvento;
        public TransfDelegado TransfEvento2;
        public TransfDelegado TransfEvento3;

        public Form2(Agencia agencia)
        {
            miAgencia = agencia;
            InitializeComponent();
        }



        private void txtMail_Enter(object sender, EventArgs e)
        {
            txtMail.ForeColor = Color.LightGray;
        }

        private void txtMail_Leave(object sender, EventArgs e)
        {
            if (txtMail.PlaceholderText == "")
            {
                txtMail.PlaceholderText = "Ingrese su mail";
                txtMail.ForeColor = Color.DimGray;
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            txtPass.ForeColor = Color.LightGray;
            txtPass.UseSystemPasswordChar = true;
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.PlaceholderText == "")
            {
                txtPass.PlaceholderText = "Ingrese su mail";
                txtPass.ForeColor = Color.DimGray;
                txtPass.UseSystemPasswordChar = false;
            }
        }

        private void btnIniciarSesion(object sender, EventArgs e)
        {
            string mail = txtMail.Text;
            string clave = txtPass.Text;

            if (!string.IsNullOrEmpty(mail) && !string.IsNullOrEmpty(clave))
            {
                if (miAgencia.iniciarSesion(mail, clave))
                {
                    Usuario usuario = miAgencia.obtenerUsuarioActual();
                    if (usuario.isAdmin)
                    {
                        TransfEvento();
                    }
                    else
                        TransfEvento3();
                }
            }
            else
                MessageBox.Show("Debe ingresar un usuario y contraseña");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TransfEvento2();
        }

        public delegate void TransfDelegado();

    }
}
