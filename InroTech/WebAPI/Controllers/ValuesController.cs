using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public Dictionary<string, DateTime> Get()
        {
            var dict = new Dictionary<string, DateTime>();
            dict.Add("now", DateTime.Now);
            dict.Add("won", DateTime.Now.AddYears(1));
            return dict;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Dictionary<string,int> Get(int id)
        {
            var dict = new Dictionary<string, int>();
            dict.Add("now", id);
            dict.Add("won", id+10);
            return dict;
            //return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
