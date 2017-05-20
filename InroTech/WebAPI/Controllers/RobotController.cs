using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inrotech.Domain.Components.Robot;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class RobotController : Controller
    {
      //private Graph GI = new ;//GraphInterface
      //private Register RI = new ;//RegisterInterface
      //private Taskmanager TMI = new ;//TaskmanagerInterface
        private string[] FullRegArray;
        private static string[] SelRegArray;

        // GET: api/Robot
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            var dict = new Dictionary<string, int>();
            //dict.Add("x", GI.GetGraph());
            return dict;
        }

        // GET: api/Robot/5
        [HttpGet("{id}")]
        public DataTable Get(int id)
        {
            DataTable simDt;
            switch (id)
            {
                //    case 1:
                //        simDt = RI.GetReg();//DataTable
                //        break;
                //    case 2:
                //        simDt = RI.GetSelectedReg(SelRegArray);//DataTable
                //        break;
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
