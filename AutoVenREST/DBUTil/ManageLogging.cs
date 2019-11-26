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
        private const string ConnectionString = null;

        public void Post(Logging logging)
        {
            string queryString = "INSERT INTO Logging (Id, Dato, Luftfugtighed) VALUES (@id, @dato, @luft)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@id", logging.Id);
                command.Parameters.AddWithValue("@dato", logging.Dato);
                command.Parameters.AddWithValue("@luft", logging.Luftfugtighed);

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
    }
}
