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
    public class LoggingController : ControllerBase
    {
        private ManageLogging logging = new ManageLogging();

        // GET: api/Logging
        [HttpGet]
        public IEnumerable<Logging> Get()
        {
            return logging.Get();
        }

        // GET: api/Logging/5
        [HttpGet("{id}", Name = "Get")]
        public Logging Get(int id)
        {
            return logging.GetOne(id);
        }

        // POST: api/Logging
        [HttpPost]
        public void Post([FromBody] Logging value)
        { 
            logging.Post(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logging.Delete(id);
        }
    }
}
