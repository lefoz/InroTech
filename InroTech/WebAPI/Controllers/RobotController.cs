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
      //private Register RI = new ;//RegisterInterface
      //private Taskmanager TMI = new ;//TaskmanagerInterface
        
        private string[] FullRegArray;
        private static string[] SelRegArray;

        Robot n = null;
        bool isConnect = false;


        // GET: api/Robot
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            if (isConnect)
            {
                n.refreshPrompt();
            }
            return n.GetGraph();
           
        }

        // GET: api/robot/value 
        [HttpGet("setrobot/{robotip}:{camip}")]
        public string Get(string robotip, string camip)
        {
            try
            {
                n.getInstance();

                n.startConnect(robotip);
                isConnect = n.getIsConnected;
            }
            catch
            {
                Console.WriteLine("catch error");
            }
            


            //kald til IRobot med robot ip
            //skal ikke sende kamera ip
            //skal returnere bool fra robot hvis conn.
            // for testing purpose
            return robotip +"/"+camip;
        }

        [HttpGet("getarray/{id}")]
        public string[] GetValues(int id)
        {
            switch (id)
            {
                case 1:
                    FullRegArray = n.getAllReg;
                    break;
                case 2:
                    FullRegArray = n.getRobotInfo;
                    break;
                default:
                    FullRegArray = new[] { "Fault" };
                    break;
            }
            return FullRegArray;
        }

        // GET api/values/#
        [HttpGet("{id}")]
        public DataTable Get(int id)
        {
            DataTable simDt;
            switch (id)
            {
                case 1: simDt = SR.GetReg();//DataTable
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
