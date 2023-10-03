using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Proyecto
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
