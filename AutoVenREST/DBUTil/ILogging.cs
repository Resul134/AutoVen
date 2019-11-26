using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLib;

namespace AutoVenREST.DBUTil
{
    interface ILogging
    {
        void Post(Logging logging);

        void Delete(int id);

        List<Logging> Get();

        Logging GetOne(int id);
    }
}
