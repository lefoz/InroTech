using System;
using System.Collections.Generic;
using System.Text;
using FRRJIf;
    
namespace Inrotech.Domain.Components.Robot
{
    public class Robot
    {
        public string hostName;

        private FRRJIf.Core objCore;
        private FRRJIf.DataTable objDataTable;
        private FRRJIf.DataTable objDataTable2;
        private FRRJIf.DataNumReg objNumReg;
        private FRRJIf.DataNumReg objNumReg2;
        private FRRJIf.DataNumReg objNumReg3;


        private void connect(String targetHost)
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
                objCore.Disconnect();
            }
        }

        private void refresh()
        {
            int ii = 0;
            string strResult = null;
            bool blnValidDT = false;
            object vntValue = null;

            blnValidDT = objDataTable.Refresh();
            //if refresh failed, disconnect
            if(blnValidDT == false)
            {
                objCore.Disconnect();
                Console.WriteLine("Disconnecting - refresh failed");
                return;
            }

            //data printout
            strResult = strResult + "--- NumReg ---\r\n";
            {
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
            }
            {
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
            }
            Console.WriteLine(strResult);
        }

        //initialize
        private void subInit()
        {
            bool connectSuccess = false;
            string strHost = null;
            int timeOut = 0;

            try
            {
                objCore = new FRRJIf.Core();

                // You need to set data table before connecting.
                objDataTable = objCore.get_DataTable();
                {                    
                    objNumReg = objDataTable.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, 1, 5);
                    objNumReg2 = objDataTable.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_REAL, 6, 10);
                }

                // 2nd data table.
                // You must not set the first data table.
                objDataTable2 = objCore.get_DataTable2();
                objNumReg3 = objDataTable2.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, 1, 5);
                
                //get host name
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

                //time out value
                timeOut = 5;

                //connect
                if (timeOut > 0)
                    objCore.set_TimeOutValue(timeOut);
                connectSuccess = objCore.Connect(strHost);
                if (connectSuccess == false)
                {
                    //disconnected
                }
                else
                {
                    //connected
                }
                return;
            }
            catch (Exception ex)
            {
                //handle ex
                Console.WriteLine(ex.ToString());
            }
        }


        static void Main(string[] args)
        {
            Robot r = new Robot();
            Console.WriteLine("Trying to connect");
            r.connect("192.168.117.40");
            Console.WriteLine("Sleeping for 500ms");
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("Refresh prompted");
            r.refresh();
        }
    }
}
