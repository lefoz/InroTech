using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inrotech.Domain.Sim;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private Sim_database _val = new Sim_database();

        // GET: api/User
        [HttpGet]
        public string Get()
        {
            return "OK_get";
        }

        // GET: api/User/userid:pass
        [HttpGet("{userid}:{pass}")]
        public bool Get(string userid, string pass)
        {
            var _val = new Sim_database();
            return _val.Sim_GetUser(userid, pass);
        }


        // POST: api/User
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
