﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Inrotech.Domain.Graph;

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
            return "value";
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
