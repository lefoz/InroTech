using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;



namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        MeassurementHelper _mess = new MeassurementHelper();

        //https://channel9.msdn.com/Blogs/ASP-NET-Site-Videos/aspnet-web-api
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
        public String Get(int id)
        {
            var name = _mess.name;

            return name;
        }

        // POST api/values
        [HttpPost]
        public bool Post([FromBody]string value)
        {
            return true;
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
