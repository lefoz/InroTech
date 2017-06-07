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
        private string[] FullRegArray;
        private static string[] SelRegArray;
        private Dictionary<string,int> dict;

        private Robot n;
        private bool isConnect = false;


        // GET: api/Robot
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            if (isConnect)
            {
                n.refreshPrompt();
                dict = n.GetGraph();           
            }
            return dict;
        }

        // GET: api/robot/value 
        [HttpGet("setrobot/{robotip}")]
        public bool Get(string robotip)
        {
            try
            {
                n = new Robot();
                n.startConnect(robotip);
                while(n.getIsConnected == false)
                {
                    int i = 0;
                    //wait
                    Console.WriteLine("waiting for connect.. " + i);
                    i++;
                    System.Threading.Thread.Sleep(100);
                }
                if (n.getIsConnected)
                {
                    isConnect = true;
                }
            }
            catch
            {
                Console.WriteLine("catch error");
            }
            //kald til IRobot med robot ip
            //skal ikke sende kamera ip
            //skal returnere bool fra robot hvis conn.
            // for testing purpose
            return isConnect;
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

        // GET api/robot/#
        [HttpGet("{id}")]
        public DataTable Get(int id)
        {
            DataTable dt=null;
            if (isConnect)
            {
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

                try
                {

                if (n.subClear())
                {
                    isConnect = false;
                }           
                //init robot with string[] to define FRRJIf.DataTable size
                //init datatables specifically for selected values
                while(isConnect == false)
                {
                    int i = 0;
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
                catch (Exception)
                {
                    Console.WriteLine("catch error");
                }
            }
        }
    }
}
