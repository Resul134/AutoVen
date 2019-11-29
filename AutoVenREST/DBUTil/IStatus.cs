using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLib;

namespace AutoVenREST.DBUTil
{
    interface IStatus
    {
        Status Get();

        void Update(int id, Status status);
    }
}
