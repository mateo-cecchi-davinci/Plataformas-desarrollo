namespace Proyecto
{
    public partial class Form1 : Form
    {

        private Agencia agencia;
        private Form2 login;
        private Form3 main;
        private Form4 register;
        private Form5 mainComun;

        public Form1()
        {
            InitializeComponent();
            agencia = new Agencia();

            login = new Form2(agencia);
            login.MdiParent = this;
            login.TransfEvento += TransfDelegado;
            login.TransfEvento2 += TransfDelegado2;
            login.TransfEvento3 += TransfDelegado3;
            login.Show();
        }

        private void TransfDelegado()
        {
            main = new Form3(agencia);
            main.salir += cerrarSesion;
            main.MdiParent = this;
            login.Close();
            main.Show();
        }
        private void TransfDelegado2()
        {
            register = new Form4(agencia);
            register.submit += registrado;
            register.back += volver;
            register.MdiParent = this;
            login.Close();
            register.Show();
        }

        private void TransfDelegado3()
        {
            mainComun = new Form5(agencia);
            mainComun.salir += cerrar;
            mainComun.MdiParent = this;
            login.Close();
            mainComun.Show();
        }

        private void registrado()
        {
            MessageBox.Show("Usuario Registrado con éxito");
            register.Close();
            login = new Form2(agencia);
            login.MdiParent = this;
            login.TransfEvento += TransfDelegado;
            login.TransfEvento2 += TransfDelegado2;
            login.TransfEvento3 += TransfDelegado3;
            login.Show();
        }
        private void volver()
        {
            register.Close();
            login = new Form2(agencia);
            login.MdiParent = this;
            login.TransfEvento += TransfDelegado;
            login.TransfEvento2 += TransfDelegado2;
            login.TransfEvento3 += TransfDelegado3;
            login.Show();
        }

        private void cerrarSesion()
        {
            main.Close();
            login = new Form2(agencia);
            login.MdiParent = this;
            login.TransfEvento += TransfDelegado;
            login.TransfEvento2 += TransfDelegado2;
            login.TransfEvento3 += TransfDelegado3;
            login.Show();
        }

        private void cerrar()
        {
            mainComun.Close();
            login = new Form2(agencia);
            login.MdiParent = this;
            login.TransfEvento += TransfDelegado;
            login.TransfEvento2 += TransfDelegado2;
            login.TransfEvento3 += TransfDelegado3;
            login.Show();
        }

    }
}