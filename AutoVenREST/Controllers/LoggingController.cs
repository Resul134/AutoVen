using System;
using System.Collections;
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
            IEnumerable<Logging> list = logging.Get();

            DateTime now = DateTime.Now;

            foreach (var entry in list)
            {
                
                if (now - entry.Dato > TimeSpan.FromDays(1825))
                {
                    logging.Delete(entry.Id);
                }
                Console.WriteLine(new ArgumentException("Couldn't remove"));
                

            }

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
