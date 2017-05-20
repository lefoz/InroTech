using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inrotech.Domain.Components.Robot;
using Inrotech.Domain.Graph;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class RobotController : Controller
    {
        private IGraph GI = new Graph();//GraphInterface
      //private Register RI = new ;//RegisterInterface
      //private Taskmanager TMI = new ;//TaskmanagerInterface
        
        private string[] FullRegArray;
        private static string[] SelRegArray;

        // GET: api/Robot
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            return GI.GetGraph();
           
        }

        // GET: api/robot/value 
        [HttpGet("{robotip}:{camip}")]
        public string Get(string robotip, string camip)
        {
            // for testing purpose
            return robotip +"/"+camip;
        }

        [HttpGet("getarray/{id}")]
        public string[] GetValues(int id)
        {
            switch (id)
            {
                //case 1:
                //    FullRegArray = SR.GetAllReg();
                //    break;
                default:
                    FullRegArray = new[] { "Fault" };
                    break;
            }
            return FullRegArray;
        }

        // POST: api/Robot
        [HttpPost]
        public void Post([FromBody]string[] values)
        {

            if (values != null)
            {
                
                SelRegArray = values;


            }

        }
    }
}
