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

            string queryString = "SELECT * FROM usuario";

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

        public List<Hotel> inicializarHoteles()
        {
            List<Hotel> hoteles = new List<Hotel>();

            string queryString = "SELECT * FROM hotel";

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

        public List<Vuelo> inicializarVuelos()
        {
            List<Vuelo> vuelos = new List<Vuelo>();

            string queryString = "SELECT * FROM vuelo";

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

        public List<ReservaHotel> inicializarReservasH()
        {
            List<ReservaHotel> reservasHoteles = new List<ReservaHotel>();

            string queryString = "SELECT * FROM reservaHotel";

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

        public List<ReservaVuelo> inicializarReservasV()
        {
            List<ReservaVuelo> reservasVuelos = new List<ReservaVuelo>();

            string queryString = "SELECT * FROM reservaVuelo";

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

        public List<Ciudad> inicializarCiudades()
        {
            List<Ciudad> ciudades = new List<Ciudad>();

            string queryString = "SELECT * FROM ciudad";

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
