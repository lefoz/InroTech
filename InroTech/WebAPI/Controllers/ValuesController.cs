using System.Collections.Generic;
using System.Data;
using Inrotech.Domain.Graph;
using Inrotech.Domain.Register;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private Sim_Graph SG = new Sim_Graph();
        private Sim_Register SR = new Sim_Register();
        

        // GET api/values
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("x", SG.GetSim_Graph());
            return dict;
        }

        // GET api/values/#
        [HttpGet("{id}")]
        public DataTable Get(int id)
        {
            DataTable simDt;
            switch (id)
            {
                case 1: simDt = SR.GetSimReg();
                      break;
                case 2:
                     simDt = SR.GetSelectedReg();
                    break;
                default:
                     simDt = SR.GetSimReg();
                   break;
            }
            return simDt;
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
