namespace Proyecto
{
    public partial class Form1 : Form
    {

        private Agencia agencia;
        private Form2 login;
        private Form3 main;
        private Form4 register;

        public Form1()
        {
            InitializeComponent();
            agencia = new Agencia();

            agencia.agregarUsuario(1234, "cris", "morena", "cris@gmail.com", "123");
            agencia.agregarUsuario(3456, "leo", "messi", "leo@gmail.com", "123");

            login = new Form2(agencia);
            login.MdiParent = this;
            login.TransfEvento += TransfDelegado;
            login.TransfEvento2 += TransfDelegado2;           
            login.Show();
        }

        private void TransfDelegado()
        {
            MessageBox.Show("Log-in correcto, Usuario: " + agencia.nombreLogueado());
            login.Close();
            main = new Form3(agencia);
            main.salir += cerrarSesion;
            main.MdiParent = this;
            main.Show();
        }
        private void TransfDelegado2()
        {
            register = new Form4(agencia);
            register.submit += registrado;
            register.back += volver;
            register.MdiParent = this;
            register.Show();
        }

        private void registrado()
        {
            MessageBox.Show("Usuario Registrado con éxito");
            register.Close();
            login = new Form2(agencia);
            login.MdiParent = this;
            login.TransfEvento += TransfDelegado;
            login.TransfEvento2 += TransfDelegado2;
            login.Show();
        }
        private void volver()
        {
            register.Close();
            login = new Form2(agencia);
            login.MdiParent = this;
            login.TransfEvento += TransfDelegado;
            login.TransfEvento2 += TransfDelegado2;
            login.Show();
        }
        


        private void cerrarSesion()
        {
            MessageBox.Show("Hasta luego " + agencia.nombreLogueado());
            main.Close();
            login = new Form2(agencia);
            login.MdiParent = this;
            login.TransfEvento += TransfDelegado;
            login.Show();
        }

    }
}