using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FRRJIf;
using Inrotech.Domain.Graph;
using Inrotech.Domain.Register;

namespace Inrotech.Domain.Components.Robot
{
    public class Robot
    {
        //vars
        private Core objCore;
        private FRRJIf.DataTable objDataTable1;
        private DataNumReg[] objSelectedReg;
        private DataNumReg numRegJob;
        private DataNumReg numRegofJob;
        private DataNumReg objRobotName;
        private DataTask[] objTaskList;

        private string hostName = null;
        private int[] selectedRegArr; //hvilke reg der er selected fra website
        private string[] selectedStringArr;

        private System.Data.DataTable dt_Selected;
        private string[] taskArr;
        private int voltage;
        private int amp;
        private string job = "";
        private string ofJob = "";
        private string robotName = "";

        private Real_Register reg;

        

        private bool isConnected = false;//has class been connected?
        private bool isInit = false; //has class been initialized?

        //constructor private for singleton
        private Robot()
        {
            reg = new Real_Register();
        }
        private static Robot instance = null;
        //get instance of class
        public Robot getInstance()
        {
            if (instance == null)
            {
                instance = new Robot();
            }      
        return instance;
        }
        
        //call this from outside with target IP
        public void startConnect(string targetHost)
        {
            hostName = targetHost;

            //tjek om der er hul igennem til DLL
            if (objCore == null)
            {
                //connect
                connectRobot(targetHost);
            }
            else
            {
                //disconnect
                Console.WriteLine("Disconnected");
            }
        }
                 
        //refresh datatable1
        private void refresh_dt1()
        {
            //init
            short intLine = 0, intState = 0;
            object vntValue = null, jobValue = null, nameValue = null;
            string strProg = "", strParentProg = "";
            Array lngAI = new int[2];
            bool validRefresh = false;

            var dtTemp = dt_Selected.Clone();

            //datatable gets refreshed
            validRefresh = objDataTable1.Refresh();

            //if refresh failed, disconnect
            if (validRefresh == false)
            {
                objCore.Disconnect();
                Console.WriteLine("Disconnecting - refresh failed");
                return;
            }

            //DATA FORMATTING
            //NUMERIC REGISTERS
            for (int i = 0; i <= objSelectedReg.Length-1; i++)
            {
                if (objSelectedReg[i].GetValue(selectedRegArr[i], ref vntValue) == true)
                {
                    //add data to return-datatable  {id,regnr,value,bool}
                    dtTemp.Rows.Add(new object[] {selectedRegArr[i], selectedRegArr[i], vntValue, false});
                }
                else
                {
                    dtTemp.Rows.Add(new object[] {selectedRegArr[i], selectedRegArr[i], null, false});
                }
            }
            dt_Selected = dtTemp;
            

            //TASKS
            for (int i = objTaskList.GetLowerBound(0); i <= objTaskList.GetUpperBound(0); i++)
            {
                if (objTaskList[i].GetValue(ref strProg, ref intLine, ref intState, ref strParentProg))
                {
                    //if empty, exit to outer loop
                    if (strProg == "")
                    {
                        break;
                    }
                    taskArr[i] = "TASK " + objTaskList[i].Index + "  -  Program: " + strProg + "  -  Line: " + intLine + "  -  State: "+ intState + "  -  Parent: " + strParentProg;
                }
                else
                {
                    taskArr[i] = "TASK " + objTaskList[i].Index + "  -  ERROR";
                }
            }

            //ANALOG IO
            //read AnalogOut - offset 1000 to get AO as in FANUC documentation
            {
                //read AnalogIn - offset 1000 to get AI as in FANUC documentation
                if (objCore.ReadGI(1000 + 1, ref lngAI, 2) == false)
                {
                    Console.WriteLine("Disconnected - read AI fail");
                    objCore.Disconnect();
                    return;
                }
                voltage = (int) lngAI.GetValue(1);
                amp = (int) lngAI.GetValue(0);
            }

            //TASKNR of total tasks & NAME
            if (numRegJob.GetValue(0, ref jobValue) == true)
            {
                job = "" + jobValue;
            }
            if (numRegofJob.GetValue(0, ref jobValue) == true)
            {
                ofJob = "" + jobValue;
            }
            if(objRobotName.GetValue(0, ref nameValue) == true)
            {
                robotName = "" + nameValue;
            }
        }

        //INITIALIZE, CONNECT, CONSTRUCT DATATABLES
        public void subInit(string[] selectedReg)
        {

            objSelectedReg = new FRRJIf.DataNumReg[selectedReg.Length];

            //parse string[] to int[]
            selectedRegArr = Array.ConvertAll(selectedReg, int.Parse);

            //tasklist return object
            taskArr = new string[10];

            //init datatable return objects
            dt_Selected = new System.Data.DataTable();
            dt_Selected.Clear();
            dt_Selected.Columns.Add("id", typeof(int));
            dt_Selected.Columns.Add("Registry", typeof(int));
            dt_Selected.Columns.Add("Value", typeof(double));
            dt_Selected.Columns.Add("Selected", typeof(bool));

            //welding params
            voltage = 0;
            amp = 0;

            try
            {
                //Set FANUC datatable1
                objDataTable1 = objCore.get_DataTable();

                //robotinfo
                numRegJob = objDataTable1.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, 2, 2);
                numRegofJob = objDataTable1.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, 3, 3);
                objRobotName = objDataTable1.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, 5, 5);

                //selected numregs
                for (int i = 0; i < objSelectedReg.Length; i++)
                {
                    objSelectedReg[i] = objDataTable1.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, selectedRegArr[i], selectedRegArr[i]);
                }

                //10 data tasks
                objTaskList = new FRRJIf.DataTask[10];
                for (int i = 0; i < objTaskList.Length; i++)
                {
                    objTaskList[i] = objDataTable1.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, i+1);
                }
                isInit = true;
                return;
            }
            catch (Exception ex)
            {
                //handle ex
                Console.WriteLine(ex.ToString());
            }
        }

        private void connectRobot(string target)
        {
            //method vars
            bool connectSuccess = false;
            string strHost = null;
            int timeOut = 5; //connection timeout value

            try
            {
                //Set core
                objCore = new FRRJIf.Core();

                //HOSTNAME ON LOCAL NETWORK
                if (string.IsNullOrEmpty(hostName))
                {
                    Console.WriteLine("No host specified");
                    hostName = strHost;
                }
                else
                {
                    strHost = hostName;

                    //CONNECTING
                    timeOut = 5000; //time out value         
                    if (timeOut > 0)
                        objCore.set_TimeOutValue(timeOut);

                    connectSuccess = objCore.Connect(strHost);

                    if (connectSuccess == false)
                    {
                        //disconnected
                        Console.WriteLine("Disconnected");
                        objCore.Disconnect();
                    }
                    else
                    {
                        //connected
                        Console.WriteLine("Connected");
                        isConnected = true;
                    }
                }                
            }
            catch (Exception ex)
            {
                //handle ex
                Console.WriteLine(ex.ToString());
            }
        }

        // ----------- test
        private void autoInit(string target, string[] selectedArr)
        {
            if(isConnected == false)
            {
                startConnect(target);
                isInit = false;
            }           
            if(isInit == false)
            {
                subInit(selectedArr);
            }
        }

        //GETTERS
        public string[] getTaskArr { get => taskArr; }
        public int getVoltage { get => voltage; }
        public int getAmp { get => amp; }
        public System.Data.DataTable getSelectedData { get => dt_Selected; }//return datatable with selected regs
        public bool getIsConnected { get => isConnected; }//true if bool isconnected == true

        //get fullreg method on real_reg instance
        public string[] getAllReg { get => reg.GetAllReg(); }
        public System.Data.DataTable getReg { get => reg.GetReg(); }
        public System.Data.DataTable getSelectedReg(string[] arr)
        {
            return reg.GetSelectedReg(arr);
        }
        //get robot info from numregs
        public string[] getRobotInfo { get => reg.RobotInfo(robotName, hostName, job, ofJob); }

        public void refreshPrompt()
        {
            refresh_dt1();
        }

        public bool subClear()
        {
            bool succes = objCore.Disconnect();
            if (succes)
            {
                isConnected = false;

                //clear local objects
                if (isConnected)
                {
                    objCore = null;
                }
                objDataTable1.Clear();
                dt_Selected.Clear();
                startConnect(hostName);
                return true;
            }
            else
            {
                isConnected = true;
                return false;
            }   
        }


        public Dictionary<string, int> GetGraph()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();

            d.Add("volt", voltage);
            d.Add("amp", amp);

            return d;
        }


        //TEST EXECUTION METHODS
        /*public void StartTest(string IP)
        {
            Console.WriteLine("Trying to connect");
            startConnect(IP);
            Console.WriteLine("Sleeping for 500ms");
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("Refresh prompted");
            refresh_dt1();
        }         
        */
        //DATA TYPES for debugging
        /* debugging
        private string StrTask(int Index, string strProg, short intLine, short intState, string strParentProg)
        {
            string tmp = null;

            tmp = "TASK " + Index + ": ";
            tmp = tmp + " Program = \"" + strProg + "\"";
            tmp = tmp + " Line = " + intLine;
            tmp = tmp + " State = " + intState;
            tmp = tmp + " Parent Program = \"" + strParentProg + "\"";

            return tmp + "\r\n";
        
        
        private string StrIO(string strIOType, short StartIndex, short EndIndex, ref Array values)
        {
            string tmp = null;
            int i = 0;

            tmp = strIOType + "[" + Convert.ToString(StartIndex) + "-" + Convert.ToString(EndIndex) + "]=";
            for (i = 0; i <= EndIndex - StartIndex; i++)
            {
                if (i != 0)
                {
                    tmp = tmp + ",";
                }
                tmp = tmp + values.GetValue(i);
            }
            return tmp;
        }
        */
        //test method
        /*private void refresh()
        {
            int ii = 0;
            short intLine = 0, intState = 0;
            string strResult = null;
            bool blnValidDT = false;
            object vntValue = null;
            string strProg = "", strParentProg = "";
            Array lngAI = new int[3];
            Array lngAO = new int[3];

            blnValidDT = objDataTable1.Refresh();
            //if refresh failed, disconnect
            if (blnValidDT == false)
            {
                objCore.Disconnect();
                Console.WriteLine("Disconnecting - refresh failed");
                return;
            }

            //TESTDATA FORMATTING
            //Numregs
            strResult = strResult + "--- NumRegs ---\r\n";
            for (ii = objNumReg.StartIndex; ii <= objNumReg.EndIndex; ii++)
            {
                if (objNumReg.GetValue(ii, ref vntValue) == true)
                {
                    strResult = strResult + "R[" + ii + "] = " + vntValue + "\r\n";
                }
                else
                {
                    strResult = strResult + "R[" + ii + "] : Error!!! \r\n";
                }
            }
            for (ii = objNumReg2.StartIndex; ii <= objNumReg2.EndIndex; ii++)
            {
                if (objNumReg2.GetValue(ii, ref vntValue) == true)
                {
                    strResult = strResult + "R[" + ii + "] = " + vntValue + "\r\n";
                }
                else
                {
                    strResult = strResult + "R[" + ii + "] : Error!!! \r\n";
                }
            }
            //Tasks
            strResult = strResult + "--- Tasks ---\r\n";
            for (int i = objTaskList.GetLowerBound(0); i <= objTaskList.GetUpperBound(0); i++)
            {
                if (objTaskList[i].GetValue(ref strProg, ref intLine, ref intState, ref strParentProg))
                {
                    if (strProg == "")
                    {
                        break;
                    }
                    strResult = strResult + StrTask(objTaskList[i].Index, strProg, intLine, intState, strParentProg);
                }
                else
                {
                    strResult = strResult + "Task error!!!\r\n";
                }
            }
            //Analog IO
            //read AO - offset 1000 for AO
            {
                if (objCore.ReadGO(1000 + 1, ref lngAO, 2) == false)
                {
                    Console.WriteLine("Disconnected - read AO fail");
                    objCore.Disconnect();
                    return;
                }
                //read AI - offset 1000 for AI
                if (objCore.ReadGI(1000 + 1, ref lngAI, 2) == false)
                {
                    Console.WriteLine("Disconnected - read AI fail");
                    objCore.Disconnect();
                    return;
                }
                strResult = strResult + "--- AO ---" + "\r\n";
                strResult = strResult + StrIO("AO", 1, 2, ref lngAO) + "\r\n";
                strResult = strResult + "--- AI ---" + "\r\n";
                strResult = strResult + StrIO("AI", 1, 2, ref lngAI) + "\r\n";
            }
            Console.WriteLine(strResult);
        }*/
    }
}
