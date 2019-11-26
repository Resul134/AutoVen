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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Logging> Get()
        {
            throw new NotImplementedException();
        }

        public Logging GetOne(int id)
        {
            throw new NotImplementedException();
        }
    }
}
