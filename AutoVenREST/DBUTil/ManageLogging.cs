using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ModelLib;

namespace AutoVenREST.DBUTil
{
    public class ManageLogging : ILogging
    {
        private const string ConnectionString =
            "Data Source=simonshn.database.windows.net;Initial Catalog=SimonSHN;User ID=simo35c9;Password=Simon1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void Post(Logging logging)
        {
            string queryString = "INSERT INTO Logging (Dato, Luftfugtighed, Aktiv, ULuftfugtighed) VALUES (@dato, @luft, @aktiv, @uluft)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@dato", logging.Dato);
                command.Parameters.AddWithValue("@luft", logging.Luftfugtighed);
                command.Parameters.AddWithValue("@aktiv", logging.Aktiv);
                command.Parameters.AddWithValue("@uluft", logging.ULuftfugtighed);

                connection.Open();

                try
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch
                {
                    new Exception("Fejl");
                }
            }
        }

        public void Delete(int id)
        {
            Logging logging = new Logging();
            logging = GetOne(id);

            string queryString = "DELETE FROM Logging WHERE Id=@IDG";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@IDG", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Logging> Get()
        {
            List<Logging> LoggingList = new List<Logging>();

            string queryString = "Select * FROM Logging";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Logging logging = new Logging();
                        logging.Id = reader.GetInt32(0);
                        logging.Dato = reader.GetDateTime(1);
                        logging.Luftfugtighed = reader.GetDouble(2);
                        logging.Aktiv = reader.GetBoolean(3);
                        logging.ULuftfugtighed = reader.GetDouble(4);
                        LoggingList.Add(logging);
                    }
                }
                finally
                {
                    reader.Close();
                }
                connection.Close();
            }

            return LoggingList;
        }

        public Logging GetOne(int id)
        {
            Logging logging = new Logging();

            string queryString = "Select * FROM Logging WHERE Id=@ID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        logging.Id = reader.GetInt32(0);
                        logging.Dato = reader.GetDateTime(1);
                        logging.Luftfugtighed = reader.GetDouble(2);
                        logging.Aktiv = reader.GetBoolean(3);
                        logging.ULuftfugtighed = reader.GetDouble(4);
                    }
                }
                finally
                {
                    reader.Close();
                }
                connection.Close();
            }

            return logging;
        }


        public Logging getLatestEntry()
        {
            Logging logs = new Logging();

            string queryLatest = "SELECT TOP 1 * FROM Logging ORDER BY Id DESC";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryLatest, connection);

                connection.Open();


                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {

                        logs.Id = reader.GetInt32(0);
                        logs.Dato = reader.GetDateTime(1);
                        logs.Luftfugtighed = reader.GetDouble(2);
                        logs.Aktiv = reader.GetBoolean(3);
                        logs.ULuftfugtighed = reader.GetDouble(4);
                    }
                }
                finally
                {
                    reader.Close();
                }
                connection.Close();
            }

            return logs;
        }
    }
}
