using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoVenREST.DBUTil;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLib;

namespace AutoVenREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {

        private ManageStatus Status = new ManageStatus();
        // GET: api/Status
        [HttpGet]
        public IEnumerable<Status> Get()
        {
            return Status.Get();
        }

   

        // PUT: api/Status/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Status value)
        {
            ManageStatus mngItem = new ManageStatus();
            mngItem.Update(id, value);
        }


    }
}
