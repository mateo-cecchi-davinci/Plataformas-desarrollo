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
        public TransfDelegado TransfEvento2;
       



        public Form2(Agencia agencia)
        {
            miAgencia = agencia;
            InitializeComponent();
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

        private void button2_Click(object sender, EventArgs e)
        {
            TransfEvento2();
        }

        public delegate void TransfDelegado();

    }
}
