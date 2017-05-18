using System;
using System.Collections.Generic;
using System.Text;
using FRRJIf;
    
namespace Inrotech.Domain.Components.Robot
{
    public class Robot
    {
        private FRRJIf.Core objCore;
        private FRRJIf.DataTable objDataTable1;
        private FRRJIf.DataTable objDataTable2;
        private FRRJIf.DataNumReg objNumReg;
        private FRRJIf.DataNumReg objNumReg2;
        private FRRJIf.DataNumReg objNumReg3;
        private FRRJIf.DataTask[] objTask;

        public string hostName;

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
                objCore.Disconnect();
            }
        }

        private void refresh()
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
            if(blnValidDT == false)
            {
                objCore.Disconnect();
                Console.WriteLine("Disconnecting - refresh failed");
                return;
            }

            //TESTDATA FORMAT
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
            for (int i = objTask.GetLowerBound(0); i <= objTask.GetUpperBound(0); i++)
            {
                if (objTask[i].GetValue(ref strProg, ref intLine, ref intState, ref strParentProg))
                {
                    if (strProg == "")
                    {
                        break;
                    }                        
                    strResult = strResult + StrTask(objTask[i].Index, strProg, intLine, intState, strParentProg);
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
        }

        //INITIALIZE, CONNECT, CONSTRUCT DATATABLES
        private void subInit()
        {
            bool connectSuccess = false;
            string strHost = null;
            int timeOut = 5; //connection timeout value

            try
            {
                //Set core
                objCore = new FRRJIf.Core();
                //Set datatable1
                objDataTable1 = objCore.get_DataTable();
                {                    
                    objNumReg = objDataTable1.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, 1, 5);
                    objNumReg2 = objDataTable1.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_REAL, 6, 10);

                    objTask = new FRRJIf.DataTask[10];
                    objTask[0] = objDataTable1.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, 1);
                    objTask[1] = objDataTable1.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, 2);
                    objTask[2] = objDataTable1.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, 3);
                    objTask[3] = objDataTable1.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, 4);
                    objTask[4] = objDataTable1.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, 5);
                    objTask[5] = objDataTable1.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, 6);
                    objTask[6] = objDataTable1.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, 7);
                    objTask[7] = objDataTable1.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, 8);
                    objTask[8] = objDataTable1.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, 9);
                    objTask[9] = objDataTable1.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, 10);
                }

                // 2nd data table.
                // You must not set the first data table.
                objDataTable2 = objCore.get_DataTable2();
                objNumReg3 = objDataTable2.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, 1, 5);
                
                //HOSTNAME ON LOCAL NETWORK
                if (string.IsNullOrEmpty(hostName))
                {
                    Console.WriteLine("Write hostname and press enter:");
                    strHost = Console.ReadLine();//default
                    hostName = strHost;
                }
                else
                {
                    strHost = hostName;
                }

                //CONNECTING
                timeOut = 5; //time out value                
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

            tmp = "TASK" + Index + ": ";
            tmp = tmp + " Prog=\"" + strProg + "\"";
            tmp = tmp + " Line=" + intLine;
            tmp = tmp + " State=" + intState;
            tmp = tmp + " ParentProg=\"" + strParentProg + "\"";

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
    }
}
