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

        public List<Status> Get()
        {
            List<Status> StatusList = new List<Status>();
            string queryString = "SELECT * FROM Status";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Status stat = new Status();
                        stat.Id = reader.GetInt32(0);
                        stat.Dato = reader.GetDateTime(1);
                        stat.Active = reader.GetBoolean(2);
                        StatusList.Add(stat);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return StatusList;
        }

        public void Update(int id, Status status)
        {
            string queryString = "UPDATE Status SET Id=@id,Dato=@dato, Active=@active WHERE Id=@id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@id", status.Id);
                    command.Parameters.AddWithValue("@dato", status.Dato);
                    command.Parameters.AddWithValue("@active", status.Active);
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
