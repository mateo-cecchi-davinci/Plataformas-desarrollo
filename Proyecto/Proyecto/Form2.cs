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
        public TransfDelegado TransfEvento3;

        public Form2(Agencia agencia)
        {
            miAgencia = agencia;
            InitializeComponent();
        }

        private void btnIngresar(object sender, EventArgs e)
        {
            string mail = tbMail.Text;
            string clave = tbClave.Text;
            if (!string.IsNullOrEmpty(mail) && !string.IsNullOrEmpty(clave))
            {
                List<Usuario> usuarios = miAgencia.obtenerUsuarios();

                Usuario usuario = usuarios.FirstOrDefault(u => u.mail == mail);

                if (usuario != null)
                {
                    if (usuario.bloqueado)
                    {
                        MessageBox.Show("El usuario está bloqueado. Contáctese con el administrador.");
                    }
                    else
                    {
                        if (miAgencia.iniciarSesion(mail, clave))
                        {
                            usuario.intentosFallidos = 0;
                            if (usuario.isAdmin)
                            {
                                TransfEvento();
                            }
                            else
                            TransfEvento3();
                        }
                        else
                        {
                            MessageBox.Show("Error, usuario o contraseña incorrectos");
                            usuario.intentosFallidos++; 
                            if (usuario.intentosFallidos >= 3)
                            {
                                usuario.bloqueado = true; 
                                MessageBox.Show("El usuario ha sido bloqueado debido a múltiples intentos fallidos.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Usuario no encontrado.");
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
