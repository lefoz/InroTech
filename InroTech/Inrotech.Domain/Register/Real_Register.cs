using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Inrotech.Domain.Register;
using Inrotech.Domain.Components.Robot;
namespace Inrotech.Real_Register
{
    public class Real_Register : IReal_Register
    {
        private DataTable Reg;
        private DataTable RegSelected;

        public Real_Register()
        {
            GetReg();
        }
        
        //full register overview
        public DataTable GetReg()
        {
            Reg = new DataTable();
            Reg.Clear();
            Reg.Columns.Add("id", typeof(int));
            Reg.Columns.Add("Registry", typeof(int));
            Reg.Columns.Add("Value", typeof(double));
            Reg.Columns.Add("Selected", typeof(bool));
            for (int i = 0; i < 500; i++)
            {
                Reg.Rows.Add(new object[] { i+1, 999, 999, false });
            }
            return Reg;
        }

        public DataTable GetSelectedReg(string[] selItems)
        {
            int[] forwardArr = new int[selItems.Length];

            //Console.WriteLine(selItems.Tostring);
            DataTable old = GetReg();
            RegSelected = old.Clone();
            //nulstil
            foreach (DataRow row in old.Rows)
            {
                row["Selected"] = false;
            }
            if (selItems != null)
            {
                foreach (var item in selItems)
                {
                    int reg = Convert.ToInt32(item);
                    DataRow[] regfound = old.Select("Registry = '" + reg + "'");
                    if (regfound.Length != 0)
                    {
                        DataRow row = old.Select("Registry = '" + reg + "'").FirstOrDefault();
                        row["Selected"] = true;
                    }
                 }
                foreach (DataRow row in old.Rows)
                {
                    if (row["Selected"].Equals(true))
                    {
                        
                        forwardArr[(int)row["id"]-1] = (int)row["Registry"];


                    }
                }
            }


            return RegSelected;
        }

        private DataTable getRobotData(string[] incomingArr)
        {

            Robot r = new Robot(incomingArr); //TESTTESTTEST BAD CONNECTION

            return r.getSelectedData; //LAV OM!!!
        }

        public string[] GetAllReg()
        {
            
            DataTable old = GetReg();
            var regList = new List<string>();
            foreach (DataRow row in old.Rows)
            {
              regList.Add(row["Registry"].ToString());   
            }
            var regArray = regList.ToArray();

            return regArray;
        }

        public void RegArrayTester(string[] test)
        {
            foreach (var item in test)
            {
                Console.WriteLine(item);
            }
        }

        public String[] RobotInfo()
        {
            String[] SimInfo = new string[] { "Simulate Robot", "localhost:53982", "SimJob", "SimProgress" };
            return SimInfo;
        }
    }
}