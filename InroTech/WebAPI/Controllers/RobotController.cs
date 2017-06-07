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

        private Robot n;
        private bool isConnect = false;


        // GET: api/Robot
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            var dic = new Dictionary<string, int>();
            if (isConnect)
            {
                //refresh locally on robot
                dic = n.GetGraph();
                n.refreshPrompt();
            }
            return dic;        
        }

        // GET: api/robot/value 
        [HttpGet("setrobot/{robotip}")]
        public bool Get(string robotip)
        {
            try
            {
                string[] arr = new string[10];

                n = new Robot();
                n.subInit(arr);

                int i = 0;
                while (!n.startConnect(robotip))
                {                    
                    //wait
                    Console.WriteLine("waiting for connect.. " + i);
                    i++;
                    System.Threading.Thread.Sleep(100);
                }       
                isConnect = true;
                return true;
            }
            catch
            {
                Console.WriteLine("catch error");
            }

            //kald til IRobot med robot ip
            //skal ikke sende kamera ip
            //skal returnere bool fra robot hvis conn.
            // for testing purpose
            return false;
        }

        [HttpGet("getarray/{id}")]
        public string[] GetValues(int id)
        {
            switch (id)
            {
                case 1:
                    if(isConnect)
                        FullRegArray = n.getAllReg;
                    break;
                case 2:
                    if (isConnect)
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
            DataTable dt;
            switch (id)
            {
                case 1:
                    dt = n.getReg;//DataTable
                    break;
                case 2:
                    dt = n.getSelectedData;//DataTable
                    break;
                default:
                    dt = new DataTable();
                    break;
            }
            return dt;
        }

        // POST: api/Robot
        [HttpPost]
        public void Post([FromBody]string[] values)
        {
            if (values != null)
            {                
                SelRegArray = values;

                if (n.subClear())
                {
                    isConnect = false;
                }
                //init robot with string[] to define FRRJIf.DataTable size
                //init datatables specifically for selected values
                int i = 0;
                while (isConnect == false)
                {                    
                    //wait
                    Console.WriteLine("waiting for connection.. " + i);
                    i++;
                    System.Threading.Thread.Sleep(100);
                }
                if (isConnect)
                {
                    n.subInit(values);
                }
            }
        }
    }
}
