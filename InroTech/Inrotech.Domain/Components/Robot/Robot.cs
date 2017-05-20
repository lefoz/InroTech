using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FRRJIf;
    
namespace Inrotech.Domain.Components.Robot
{
    public class Robot
    {
        private FRRJIf.Core objCore;
        private FRRJIf.DataTable objDataTable1;
        private FRRJIf.DataTable objDataTable2;
        private FRRJIf.DataNumReg objAllReg;
        private FRRJIf.DataNumReg[] objSelectedReg;
        private FRRJIf.DataTask[] objTaskList;
        private FRRJIf.DataTable selectedRegTable;

        private System.Data.DataTable dT;

        private string hostName;
        private int[] selectedRegArr;//hvilke reg der er selected fra website       

        //construct        
        public Robot(string[] selectedReg)
        {
            objSelectedReg = new FRRJIf.DataNumReg[selectedReg.Length];
            //parse string[] to int[]
            selectedRegArr = Array.ConvertAll(selectedReg, int.Parse);
        }
        

        private void startConnect(String targetHost)
        {
            hostName = targetHost;

            if (objCore == null)
            {
                //connect
                subInit();
            }
            else
            {
                //disconnect
                Console.WriteLine("Disconnected");
            }
        }

        
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
        

        private void refresh()
        {
            short intLine = 0, intState = 0;
            string strResult = null;
            string[] taskListArr = new string[10];
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

            //DATA FORMATTING
            //Numregs
            for (int i = 0; i <= objSelectedReg.Length-1; i++)
            {
                if (objSelectedReg[i].GetValue(selectedRegArr[i], ref vntValue) == true)
                {
                    dT.Rows.Add(new object[] {selectedRegArr[i], selectedRegArr[i], vntValue, false});

                    //strResult = strResult + "R[" + selectedRegArr[i] + "] = " + vntValue + "\r\n"; debugging
                }
                else
                {
                    dT.Rows.Add(new object[] {selectedRegArr[i], selectedRegArr[i], null, false});
                }
            }
            
            //Tasks
            for (int i = objTaskList.GetLowerBound(0); i <= objTaskList.GetUpperBound(0); i++)
            {
                if (objTaskList[i].GetValue(ref strProg, ref intLine, ref intState, ref strParentProg))
                {
                    if (strProg == "")
                    {
                        break;
                    }

                    taskListArr[i] = strResult + StrTask(objTaskList[i].Index, strProg, intLine, intState, strParentProg);
                }
                else
                {
                    strResult = strResult + "Task error!!!\r\n";
                }
            }
            //Analog IO
            //read AO - offset 1000 for AO
            {
                //read AI - offset 1000 for AI
                if (objCore.ReadGI(1000 + 1, ref lngAI, 2) == false)
                {
                    Console.WriteLine("Disconnected - read AI fail");
                    objCore.Disconnect();
                    return;
                }
                strResult = strResult + StrIO("AI", 1, 2, ref lngAI) + "\r\n";
            }
            Console.WriteLine(strResult);
        }

        //INITIALIZE, CONNECT, CONSTRUCT DATATABLES
        private void subInit()
        {
            //vars
            bool connectSuccess = false;
            string strHost = null;
            int timeOut = 5; //connection timeout value

            //init datatable return object
            dT = new System.Data.DataTable();
            dT.Clear();
            dT.Columns.Add("id", typeof(int));
            dT.Columns.Add("Registry", typeof(int));
            dT.Columns.Add("Value", typeof(double));
            dT.Columns.Add("Selected", typeof(bool));

            try
            {
                //Set core
                objCore = new FRRJIf.Core();

                //Set FANUC datatable1
                objDataTable1 = objCore.get_DataTable();


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

                //Set data table 2
                //Must not set/change the 1st data table after initializing the 2nd
                objDataTable2 = objCore.get_DataTable2();
                objAllReg = objDataTable2.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, 1, 500);





                //HOSTNAME ON LOCAL NETWORK
                if (string.IsNullOrEmpty(hostName))
                {
                    Console.WriteLine("No host specified");
                    hostName = strHost;
                }
                else
                {
                    strHost = hostName;
                }

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
                }
                return;
            }
            catch (Exception ex)
            {
                //handle ex
                Console.WriteLine(ex.ToString());
            }
        }


        //DATA TYPES
        private string StrTask(int Index, string strProg, short intLine, short intState, string strParentProg)
        {
            string tmp = null;

            tmp = "TASK " + Index + ": ";
            tmp = tmp + " Program = \"" + strProg + "\"";
            tmp = tmp + " Line = " + intLine;
            tmp = tmp + " State = " + intState;
            tmp = tmp + " Parent Program = \"" + strParentProg + "\"";

            return tmp + "\r\n";
        }
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

        //TEST EXECUTION METHOD
        public void StartTest(string IP)
        {
            Console.WriteLine("Trying to connect");
            startConnect(IP);
            Console.WriteLine("Sleeping for 500ms");
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("Refresh prompted");
            refresh();
        }

        public void refreshPrompt()
        {
            refresh();
        }
    }
}
