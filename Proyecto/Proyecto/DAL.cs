using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto
{
    public class DAL
    {

        private string connectionString;

        public DAL()
        {
            connectionString = Properties.Resources.ConnectionStr;
        }

        public List<Usuario> inicializarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            string queryString = "SELECT * FROM dbo.usuario;";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    Usuario aux;

                    while (reader.Read())
                    {
                        aux = new Usuario(
                            reader.GetInt32(0), reader.GetInt32(1),
                            reader.GetString(2), reader.GetString(3),
                            reader.GetString(4), reader.GetString(5),
                            reader.GetInt32(6), reader.GetBoolean(7),
                            reader.GetDouble(8), reader.GetBoolean(9));

                        usuarios.Add(aux);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return usuarios;
        }

        public int agregarUsuario(int dni, string nombre, string apellido, string mail, string clave, bool isAdmin, double credito)
        {
            int resultadoQuery;
            int idNuevoUsuario = -1;
            string queryString = 
                "INSERT INTO [dbo].[usuario] ([dni], [nombre], [apellido], [mail], [contraseña], [esAdmin], [credito]) " +
                "VALUES (@dni, @nombre, @apellido, @mail, @clave, @isAdmin, @credito);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@mail", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@clave", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@isAdmin", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@credito", SqlDbType.Decimal));
                command.Parameters["@dni"].Value = dni;
                command.Parameters["@nombre"].Value = nombre;
                command.Parameters["@apellido"].Value = apellido;
                command.Parameters["@mail"].Value = mail;
                command.Parameters["@clave"].Value = clave;
                command.Parameters["@isAdmin"].Value = isAdmin;
                command.Parameters["@credito"].Value = credito;

                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();

                    string consultaID = "SELECT MAX([usuario_id]) FROM [dbo].[usuario];";
                    command = new SqlCommand(consultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoUsuario = reader.GetInt32(0);
                    reader.Close();
                }
                
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevoUsuario;
            }
        }

        public int modificarUsuario(int id, int dni, string nombre, string apellido, string mail, double credito)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string querySrting = 
                "UPDATE [dbo].[usuario] " +
                "SET dni = @dni, nombre = @nombre, apellido = @apellido, mail = @mail, credito = @credito " +
                "WHERE usuario_id = @id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(querySrting, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@mail", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@credito", SqlDbType.Decimal));
                command.Parameters["@id"].Value = id;
                command.Parameters["@dni"].Value = dni;
                command.Parameters["@nombre"].Value = nombre;
                command.Parameters["@apellido"].Value = apellido;
                command.Parameters["@mail"].Value = mail;
                command.Parameters["@credito"].Value = credito;
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int eliminarUsuario(int id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[usuario] WHERE usuario_id = @id;";
            using (SqlConnection connection = 
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public List<Hotel> inicializarHoteles()
        {
            List<Hotel> hoteles = new List<Hotel>();

            string queryString = "SELECT * FROM dbo.hotel;";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    Hotel aux;

                    while (reader.Read())
                    {
                        aux = new Hotel(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetDouble(3), reader.GetInt32(4));

                        hoteles.Add(aux);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return hoteles;
        }

        public int agregarHotel(int ciudad_fk, int capacidad, double costo, string nombre)
        {
            int resultadoQuery;
            int idNuevoHotel = -1;
            string queryString =
                "INSERT INTO [dbo].[hotel] ([nombre], [capacidad], [costo], [ciudad_fk]) " +
                "VALUES (@nombre, @capacidad, @costo, @ciudad_fk);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@capacidad", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@costo", SqlDbType.Decimal));
                command.Parameters.Add(new SqlParameter("@ciudad_fk", SqlDbType.Int));
                command.Parameters["@nombre"].Value = nombre;
                command.Parameters["@capacidad"].Value = capacidad;
                command.Parameters["@costo"].Value = costo;
                command.Parameters["@ciudad_fk"].Value = ciudad_fk;

                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();

                    string consultaID = "SELECT MAX([hotel_id]) FROM [dbo].[hotel];";
                    command = new SqlCommand(consultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoHotel = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevoHotel;
            }
        }

        public int modificarHotel(int id, int ciudad_fk, int capacidad, double costo, string nombre)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string querySrting =
                "UPDATE [dbo].[hotel] " +
                "SET nombre = @nombre, capacidad = @capacidad, costo = @costo, ciudad_fk = @ciudad_fk " +
                "WHERE hotel_id = @id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(querySrting, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@capacidad", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@costo", SqlDbType.Decimal));
                command.Parameters.Add(new SqlParameter("@ciudad_fk", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                command.Parameters["@nombre"].Value = nombre;
                command.Parameters["@capacidad"].Value = capacidad;
                command.Parameters["@costo"].Value = costo;
                command.Parameters["@ciudad_fk"].Value = ciudad_fk;
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int eliminarHotel(int id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[hotel] WHERE hotel_id = @id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public List<UsuarioHotel> inicializarUsuarioHotel()
        {
            List<UsuarioHotel> usuarioHotel = new List<UsuarioHotel>();
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM dbo.usuario_hotel;";
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        usuarioHotel.Add(new UsuarioHotel(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                    }
                    reader.Close();
                }
                catch (Exception ex) { 
                    Console.WriteLine(ex.Message); 
                }
            }
            return usuarioHotel;
        }

        public List<Vuelo> inicializarVuelos()
        {
            List<Vuelo> vuelos = new List<Vuelo>();

            string queryString = "SELECT * FROM dbo.vuelo;";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    Vuelo aux;

                    while (reader.Read())
                    {
                        aux = new Vuelo(
                            reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), 
                            reader.GetDouble(3), reader.GetDateTime(4), reader.GetString(5), 
                            reader.GetString(6), reader.GetInt32(7), reader.GetInt32(8));

                        vuelos.Add(aux);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return vuelos;
        }

        public int agregarVuelo(int origen_fk, int destino_fk, int capacidad, double costo, DateTime fecha, string aerolinea, string avion)
        {
            int resultadoQuery;
            int idNuevoVuelo = -1;
            string queryString =
                "INSERT INTO [dbo].[vuelo] ([capacidad], [costo], [fecha], [aerolinea], [avion], [origen_fk], [destino_fk]) " +
                "VALUES (@capacidad, @costo, @fecha, @aerolinea, @avion, @origen_fk, @destino_fk);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@capacidad", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@costo", SqlDbType.Decimal));
                command.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@aerolinea", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@avion", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@origen_fk", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@destino_fk", SqlDbType.Int));
                command.Parameters["@capacidad"].Value = capacidad;
                command.Parameters["@costo"].Value = costo;
                command.Parameters["@fecha"].Value = fecha;
                command.Parameters["@aerolinea"].Value = aerolinea;
                command.Parameters["@avion"].Value = avion;
                command.Parameters["@origen_fk"].Value = origen_fk;
                command.Parameters["@destino_fk"].Value = destino_fk;

                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();

                    string consultaID = "SELECT MAX([vuelo_id]) FROM [dbo].[vuelo];";
                    command = new SqlCommand(consultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoVuelo = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevoVuelo;
            }
        }

        public int modificarVuelo(int id, int capacidad, double costo, DateTime fecha, string aerolinea, string avion, int origen_fk, int destino_fk)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string querySrting =
                "UPDATE [dbo].[vuelo] " +
                "SET  capacidad = @capacidad, costo = @costo, fecha = @fecha, aerolinea = @aerolinea, avion = @avion, origen_fk = @origen_fk, destino_fk = @destino_fk " +
                "WHERE vuelo_id = @id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(querySrting, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@capacidad", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@costo", SqlDbType.Decimal));
                command.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@aerolinea", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@avion", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@origen_fk", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@destino_fk", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                command.Parameters["@capacidad"].Value = capacidad;
                command.Parameters["@costo"].Value = costo;
                command.Parameters["@fecha"].Value = fecha;
                command.Parameters["@aerolinea"].Value = aerolinea;
                command.Parameters["@avion"].Value = avion;
                command.Parameters["@origen_fk"].Value = origen_fk;
                command.Parameters["@destino_fk"].Value = destino_fk;
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int eliminarVuelo(int id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[vuelo] WHERE vuelo_id = @id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public List<UsuarioVuelo> inicializarUsuarioVuelo()
        {
            List<UsuarioVuelo> usuarioVuelo = new List<UsuarioVuelo>();
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM dbo.usuario_vuelo;";
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        usuarioVuelo.Add(new UsuarioVuelo(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return usuarioVuelo;
        }

        public List<ReservaHotel> inicializarReservasH()
        {
            List<ReservaHotel> reservasHoteles = new List<ReservaHotel>();

            string queryString = "SELECT * FROM dbo.reservaHotel;";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    ReservaHotel aux;

                    while (reader.Read())
                    {
                        aux = new ReservaHotel(
                            reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2),
                            reader.GetDouble(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6));

                        reservasHoteles.Add(aux);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return reservasHoteles;
        }

        public int agregarReservaHotel(DateTime fechaDesde, DateTime fechaHasta, double pagado, int cantPersonas, int hotel_fk, int usuario_fk)
        {
            int resultadoQuery;
            int idNuevaReservaHotel = -1;
            string queryString =
                "INSERT INTO [dbo].[reservaHotel] ([fechaDesde], [fechaHasta], [pagado], [cantPersonas], [hotel_fk], [usuario_fk]) " +
                "VALUES (@fechaDesde, @fechaHasta, @pagado, @cantPersonas, @hotel_fk, @usuario_fk);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@fechaDesde", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@fechaHasta", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@pagado", SqlDbType.Decimal));
                command.Parameters.Add(new SqlParameter("@cantPersonas", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@hotel_fk", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@usuario_fk", SqlDbType.Int));
                command.Parameters["@fechaDesde"].Value = fechaDesde;
                command.Parameters["@fechaHasta"].Value = fechaHasta;
                command.Parameters["@pagado"].Value = pagado;
                command.Parameters["@cantPersonas"].Value = cantPersonas;
                command.Parameters["@hotel_fk"].Value = hotel_fk;
                command.Parameters["@usuario_fk"].Value = usuario_fk;

                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();

                    string consultaID = "SELECT MAX([reservaHotel_id]) FROM [dbo].[reservaHotel];";
                    command = new SqlCommand(consultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevaReservaHotel = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevaReservaHotel;
            }
        }

        public int modificarReservaHotel(int id, DateTime fechaDesde, DateTime fechaHasta, double pagado, int cantPersonas, int hotel_fk, int usuario_fk)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string querySrting =
                "UPDATE [dbo].[reservaHotel] " +
                "SET  fechaDesde = @fechaDesde, fechaHasta = @fechaHasta, pagado = @pagado, cantPersonas = @cantPersonas, hotel_fk = @hotel_fk, usuario_fk = @usuario_fk " +
                "WHERE reservaHotel_id = @id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(querySrting, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@fechaDesde", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@fechaHasta", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@pagado", SqlDbType.Decimal));
                command.Parameters.Add(new SqlParameter("@cantPersonas", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@hotel_fk", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@usuario_fk", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                command.Parameters["@fechaDesde"].Value = fechaDesde;
                command.Parameters["@fechaHasta"].Value = fechaHasta;
                command.Parameters["@pagado"].Value = pagado;
                command.Parameters["@cantPersonas"].Value = cantPersonas;
                command.Parameters["@hotel_fk"].Value = hotel_fk;
                command.Parameters["@usuario_fk"].Value = usuario_fk;
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int eliminarReservaHotel(int id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[reservaHotel] WHERE reservaHotel_id = @id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public List<ReservaVuelo> inicializarReservasV()
        {
            List<ReservaVuelo> reservasVuelos = new List<ReservaVuelo>();

            string queryString = "SELECT * FROM dbo.reservaVuelo;";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    ReservaVuelo aux;

                    while (reader.Read())
                    {
                        aux = new ReservaVuelo(reader.GetInt32(0),  reader.GetDouble(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4));

                        reservasVuelos.Add(aux);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return reservasVuelos;
        }

        public int agregarReservaVuelo(double pagado, int cantPersonas, int vuelo_fk, int usuario_fk)
        {
            int resultadoQuery;
            int idNuevaReservaVuelo = -1;
            string queryString =
                "INSERT INTO [dbo].[reservaVuelo] ([pagado], [cantPersonas], [vuelo_fk], [usuario_fk]) " +
                "VALUES (@pagado, @cantPersonas, @vuelo_fk, @usuario_fk);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@pagado", SqlDbType.Decimal));
                command.Parameters.Add(new SqlParameter("@cantPersonas", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@vuelo_fk", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@usuario_fk", SqlDbType.Int));
                command.Parameters["@pagado"].Value = pagado;
                command.Parameters["@cantPersonas"].Value = cantPersonas;
                command.Parameters["@vuelo_fk"].Value = vuelo_fk;
                command.Parameters["@usuario_fk"].Value = usuario_fk;

                try
                {
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();

                    string consultaID = "SELECT MAX([reservaVuelo_id]) FROM [dbo].[reservaVuelo];";
                    command = new SqlCommand(consultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevaReservaVuelo = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevaReservaVuelo;
            }
        }

        public int modificarReservaVuelo(int id, double pagado, int cantPersonas, int vuelo_fk, int usuario_fk)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string querySrting =
                "UPDATE [dbo].[reservaVuelo] " +
                "SET  pagado = @pagado, cantPersonas = @cantPersonas, vuelo_fk = @vuelo_fk, usuario_fk = @usuario_fk " +
                "WHERE reservaVuelo_id = @id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(querySrting, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@pagado", SqlDbType.Decimal));
                command.Parameters.Add(new SqlParameter("@cantPersonas", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@vuelo_fk", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@usuario_fk", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                command.Parameters["@pagado"].Value = pagado;
                command.Parameters["@cantPersonas"].Value = cantPersonas;
                command.Parameters["@hotel_fk"].Value = vuelo_fk;
                command.Parameters["@usuario_fk"].Value = usuario_fk;
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int eliminarReservaVuelo(int id)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[reservaVuelo] WHERE reservaVuelo_id = @id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public List<Ciudad> inicializarCiudades()
        {
            List<Ciudad> ciudades = new List<Ciudad>();

            string queryString = "SELECT * FROM dbo.ciudad;";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    Ciudad aux;

                    while (reader.Read())
                    {
                        aux = new Ciudad(reader.GetInt32(0), reader.GetString(1));

                        ciudades.Add(aux);
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return ciudades;
        }

    }
}
