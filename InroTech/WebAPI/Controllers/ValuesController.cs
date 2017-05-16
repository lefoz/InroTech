using System.Collections.Generic;
using Inrotech.Domain.Graph;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private Sim_Graph SG = new Sim_Graph();

        // GET api/values
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("x", SG.GetSim_Graph());
            return dict;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value "+id;
        }
        //http://nodogmablog.bryanhogan.net/2016/01/asp-net-5-web-api-controller-with-multiple-get-methods/
        //http://stackoverflow.com/questions/38109927/optional-int-parameter-in-web-api-attribute-routing
        // GET api/values/5
        [HttpGet("getmyvalues/{id}")]
        public string GetValues(int id)
        {
            return "my value: "+id;
        }


        // POST api/values/name=ole
        [HttpPost]
        public void Post([FromBody]string value)
        {
            System.Console.WriteLine("my value; value");
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
