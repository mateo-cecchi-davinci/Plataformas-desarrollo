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

            agencia.agregarCiudad("Buenos Aires");
            agencia.agregarCiudad("Mendoza");
            agencia.agregarCiudad("Jujuy");
            agencia.agregarCiudad("Bariloche");
            agencia.agregarCiudad("Córdoba");
            agencia.agregarCiudad("Tucuman");
            agencia.agregarCiudad("Tierra del Fuego");
            agencia.agregarCiudad("Chaco");
            agencia.agregarCiudad("San Luis");
            agencia.agregarCiudad("Santiago del Estero");
            agencia.agregarCiudad("Corrientes");
            agencia.agregarCiudad("La Pampa");

            agencia.agregarUsuario(1234, "cris", "morena", "cris@gmail.com", "123", true, 500000);
            agencia.agregarUsuario(3456, "leo", "messi", "leo@gmail.com", "123", true, 600000);
            agencia.agregarUsuario(6543, "pepe", "luis", "pepe@gmail.com", "123", false, 500000);
            agencia.agregarUsuario(4312, "juan", "perez", "juan@gmail.com", "123", false, 500000);

            agencia.agregarHotel("Buenos Aires", 100, 100000, "Sheratong");
            agencia.agregarHotel("Mendoza", 200, 150000, "Teloide");
            agencia.agregarHotel("Jujuy", 300, 180000, "BuenHotel");
            agencia.agregarHotel("Bariloche", 170, 200000, "Sarasa");

            DateTime fecha1 = new DateTime(2023, 10, 15, 9, 0, 0);
            DateTime fecha2 = new DateTime(2023, 10, 20, 9, 0, 0);
            DateTime fecha3 = new DateTime(2023, 11, 3, 9, 0, 0);
            DateTime fecha4 = new DateTime(2023, 11, 10, 9, 0, 0);
            DateTime fecha5 = new DateTime(2023, 10, 30);
            DateTime fecha6 = new DateTime(2023, 11, 15);
            DateTime fecha7 = new DateTime(2023, 11, 30);
            DateTime fecha8 = new DateTime(2023, 12, 10);

            agencia.agregarVuelo("Córdoba", "Tucuman", 50, 100000, fecha1, "Aerolineas Argentinas", "Boeing 747");
            agencia.agregarVuelo("Tierra del Fuego", "Chaco", 40, 150000, fecha2, "Aerolineas Brasileras", "Boeing 737");
            agencia.agregarVuelo("San Luis", "Santiago del Estero", 30, 175000, fecha3, "Aerolineas Peruanas", "Boeing 777");
            agencia.agregarVuelo("Corrientes", "La Pampa", 20, 200000, fecha4, "Aerolineas Chilenas", "MD-80");

            agencia.agregarReservaHotel("Sheratong", "0", fecha1, fecha5, 200000, 2);
            agencia.agregarReservaHotel("Teloide", "1", fecha2, fecha6, 300000, 2);
            agencia.agregarReservaHotel("BuenHotel", "2", fecha3, fecha7, 180000, 1);
            agencia.agregarReservaHotel("Sarasa", "3", fecha4, fecha8, 200000, 1);

            agencia.agregarReservaVuelo("Córdoba", "Tucuman", "0", 200000, 2, fecha1);
            agencia.agregarReservaVuelo("Tierra del Fuego", "Chaco", "1", 300000, 2, fecha2);
            agencia.agregarReservaVuelo("San Luis", "Santiago del Estero", "2", 175000, 1, fecha3);
            agencia.agregarReservaVuelo("Corrientes", "La Pampa", "3", 200000, 1, fecha4);

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