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

            agencia.agregarUsuario(1234, "cris", "morena", "cris@gmail.com", "123", true, 100000);
            agencia.agregarUsuario(3456, "leo", "messi", "leo@gmail.com", "123", true, 200000);
            agencia.agregarUsuario(6543, "pepe", "luis", "pepe@gmail.com", "123", false, 300000);
            agencia.agregarUsuario(4312, "juan", "perez", "juan@gmail.com", "123", false, 200000);

            agencia.agregarHotel(agencia.obtenerCiudad().ElementAt(0), 100, 100000, "Sheratong");
            agencia.agregarHotel(agencia.obtenerCiudad().ElementAt(1), 200, 150000, "Teloide");
            agencia.agregarHotel(agencia.obtenerCiudad().ElementAt(2), 300, 180000, "BuenHotel");
            agencia.agregarHotel(agencia.obtenerCiudad().ElementAt(3), 170, 200000, "Sarasa");

            DateTime fecha1 = new DateTime(2023, 10, 15);
            DateTime fecha2 = new DateTime(2023, 10, 20);
            DateTime fecha3 = new DateTime(2023, 11, 3);
            DateTime fecha4 = new DateTime(2023, 11, 10);
            DateTime fecha5 = new DateTime(2023, 10, 30);
            DateTime fecha6 = new DateTime(2023, 11, 15);
            DateTime fecha7 = new DateTime(2023, 11, 30);
            DateTime fecha8 = new DateTime(2023, 12, 10);

            agencia.agregarVuelos(agencia.obtenerCiudad().ElementAt(4), agencia.obtenerCiudad().ElementAt(5), 50, 100000, fecha1, "Aerolineas Argentinas", "Boeing 747");
            agencia.agregarVuelos(agencia.obtenerCiudad().ElementAt(6), agencia.obtenerCiudad().ElementAt(7), 40, 150000, fecha2, "Aerolineas Brasileras", "Boeing 737");
            agencia.agregarVuelos(agencia.obtenerCiudad().ElementAt(8), agencia.obtenerCiudad().ElementAt(9), 30, 175000, fecha3, "Aerolineas Peruanas", "Boeing 777");
            agencia.agregarVuelos(agencia.obtenerCiudad().ElementAt(10), agencia.obtenerCiudad().ElementAt(11), 20, 200000, fecha4, "Aerolineas Chilenas", "MD-80");

            List<Ciudad> ciudades = agencia.obtenerCiudad();
            List<Hotel> hoteles = agencia.obtenerHoteles();
            List<Vuelo> vuelos = agencia.obtenerVuelos();

            int ciudadesIndex = 0;
            int vuelosIndex = 0;
            Vuelo vueloActual = null;

            foreach (Ciudad c in ciudades)
            {
                c.hoteles ??= new List<Hotel>();
                c.vuelosOrigen ??= new List<Vuelo>();
                c.vuelosDestino ??= new List<Vuelo>();

                if (ciudadesIndex >= 4)
                {
                    if (vuelosIndex < vuelos.Count)
                    {
                        if (vueloActual == null)
                        {
                            vueloActual = vuelos[vuelosIndex];
                        }

                        if (ciudadesIndex % 2 == 0)
                        {
                            c.vuelosOrigen.Add(vueloActual);
                        }
                        else
                        {
                            c.vuelosDestino.Add(vueloActual);
                            vuelosIndex++;
                        }

                        if (ciudadesIndex % 2 == 1)
                        {
                            vueloActual = null;
                        }
                    }
                }

                if (ciudadesIndex < hoteles.Count)
                {
                    Hotel hotel = hoteles[ciudadesIndex];
                    c.hoteles.Add(hotel);
                }

                ciudadesIndex++;
            }


            agencia.agregarReservaHotel(agencia.obtenerHoteles().ElementAt(0), agencia.obtenerUsuarios().ElementAt(0), fecha1, fecha5, 70000, 2);
            agencia.agregarReservaHotel(agencia.obtenerHoteles().ElementAt(1), agencia.obtenerUsuarios().ElementAt(1), fecha2, fecha6, 90000, 4);
            agencia.agregarReservaHotel(agencia.obtenerHoteles().ElementAt(2), agencia.obtenerUsuarios().ElementAt(2), fecha3, fecha7, 100000, 6);
            agencia.agregarReservaHotel(agencia.obtenerHoteles().ElementAt(3), agencia.obtenerUsuarios().ElementAt(3), fecha4, fecha8, 120000, 8);

            agencia.agregarReservaVuelo(agencia.obtenerVuelos().ElementAt(0), agencia.obtenerUsuarios().ElementAt(0), 100000, 2);
            agencia.agregarReservaVuelo(agencia.obtenerVuelos().ElementAt(1), agencia.obtenerUsuarios().ElementAt(1), 120000, 4);
            agencia.agregarReservaVuelo(agencia.obtenerVuelos().ElementAt(2), agencia.obtenerUsuarios().ElementAt(2), 110000, 6);
            agencia.agregarReservaVuelo(agencia.obtenerVuelos().ElementAt(3), agencia.obtenerUsuarios().ElementAt(3), 115000, 8);

            List<Usuario> usuarios = agencia.obtenerUsuarios();
            List<ReservaHotel> reservasH = agencia.obtenerReservaHotel();

            int reservaHIndex = 0;

            foreach (Usuario u in usuarios)
            {
                if (u.misReservasHoteles == null)
                {
                    u.misReservasHoteles = new List<ReservaHotel>();

                    if (reservaHIndex < reservasH.Count)
                    {
                        ReservaHotel reserva = reservasH[reservaHIndex];
                        u.misReservasHoteles.Add(reserva);
                        reservaHIndex++;
                    }
                }
            }

            List<ReservaVuelo> reservasV = agencia.obtenerReservaVuelo();

            int reservaVIndex = 0;

            foreach (Usuario u in usuarios)
            {
                if (u.misReservasVuelos == null)
                {
                    u.misReservasVuelos = new List<ReservaVuelo>();

                    if (reservaVIndex < reservasV.Count)
                    {
                        ReservaVuelo reserva = reservasV[reservaVIndex];
                        u.misReservasVuelos.Add(reserva);
                        reservaVIndex++;
                    }
                }
            }

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