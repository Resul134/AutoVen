using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ModelLib;

namespace AutoVenREST.DBUTil
{
    public class ManageStatus : IStatus
    {
        private const string ConnectionString =
            "Data Source=simonshndbserver.database.windows.net;Initial Catalog = SimonSHN; User ID = simo35c9; Password=Grethe7538!;Connect Timeout = 30; Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

        public Status Get()
        {
            Status status = new Status();
            string queryString = "SELECT * FROM Status WHERE Id = 1";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        status.Id = reader.GetInt32(0);
                        status.Dato = reader.GetDateTime(1);
                        status.AllowChange = reader.GetBoolean(2);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return status;
        }

        public void Update(int id, Status status)
        {
            string queryString = "UPDATE Status SET Id=@id,Dato=@dato, Active=@allow WHERE Id=@id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@id", status.Id);
                    command.Parameters.AddWithValue("@dato", status.Dato);
                    command.Parameters.AddWithValue("@allow", status.AllowChange);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (SqlException)
                {
                    throw new Exception("Fejl");
                }
            }
        }
    }
}
