using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        private string[] simFullRegArray;
        private static string[] simSelRegArray; //= {"025", "055"};

        // GET api/values
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("volt", SG.GetSim_Graph());
            dict.Add("amp", SG.GetSim_Graph());
            dict.Add("date", DateTime.Now.Second);

            return dict;
        }

        // GET api/values/#
        [HttpGet("{id}")]
        public DataTable Get(int id)
        {
            DataTable simDt;
            switch (id)
            {
                case 1: simDt = SR.GetSimReg();//DataTable
                    break;
                case 2:
                    simDt = SR.GetSelectedReg(simSelRegArray);//DataTable
                    break;
                default:
                    simDt = new DataTable();
                    break;
            }
            return simDt;
        }

        [HttpGet("getarray/{id}")]
        public string[] GetValues(int id)
        {
            switch (id)
            {
                case 1: simFullRegArray = SR.GetAllReg();
                        break;
                default: simFullRegArray = new [] {"Fault"};
                    break;
            }
            return simFullRegArray;
        }
        
        // POST api/values/#
        [HttpPost]
        public void Post([FromBody]string[] values)
        {
            
            if (values != null)
            {
                //SR.RegArrayTester(values);
                simSelRegArray = values;


            }
           
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string[] value)
        {
            //switch (id)
            //{
            //    case 1:
            //        simSelRegArray = value;
            //        foreach (var item in simSelRegArray)
            //        {
            //            Console.WriteLine(item.ToString());
            //        }
            //        break;
            //}
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
