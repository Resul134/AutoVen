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
        private const string ConnectionString = null;

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

        public bool Update(Status status,int id)
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
                    return false;
                }
            }
            return true;
        }
    }
}
