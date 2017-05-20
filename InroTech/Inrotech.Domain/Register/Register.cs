using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Inrotech.Domain.Register;
namespace Inrotech.Real_Register
{
    public class Real_Register
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
            Reg.Columns.Add("Name", typeof(string));
            Reg.Columns.Add("Value", typeof(double));
            Reg.Columns.Add("Selected", typeof(bool));
            Reg.Rows.Add(new object[] { 1, 025, "index 025", 500, false});
            Reg.Rows.Add(new object[] { 2, 055, "index 055", 25, false });
            Reg.Rows.Add(new object[] { 3, 075, "index 075", 50, false });
            Reg.Rows.Add(new object[] { 4, 125, "index 125", 960, false });
            Reg.Rows.Add(new object[] { 5, 138, "index 138", 58, false });
            Reg.Rows.Add(new object[] { 6, 285, "index 285", 74, false });
            Reg.Rows.Add(new object[] { 7, 789, "index 789", 35, false });
            Reg.Rows.Add(new object[] { 8, 358, "index 358", 40, false });
            return Reg;
        }

        public DataTable GetSelectedReg(string[] selItems)
        {
                //Console.WriteLine(selItems.Tostring);
                DataTable old = GetReg();
                RegSelected = old.Clone();
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
                        RegSelected.ImportRow(row);
                    }
                }

            }
            return RegSelected;
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
    }
}