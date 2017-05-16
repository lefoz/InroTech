using System;
using System.Collections.Generic;
using System.Data;
using Inrotech.Domain.Register;
namespace Inrotech.Domain.Register
{
    public class Sim_Register
    {
        private DataTable SimReg;
        private DataTable SimRegSelected;

        public Sim_Register()
        {
            GetSimReg();
        }
        
        public DataTable GetSimReg()
        {
            SimReg = new DataTable();
            SimReg.Clear();
            SimReg.Columns.Add("id", typeof(int));
            SimReg.Columns.Add("Registry", typeof(int));
            SimReg.Columns.Add("Name", typeof(string));
            SimReg.Columns.Add("Value", typeof(double));
            SimReg.Columns.Add("Selected", typeof(bool));
            SimReg.Rows.Add(new object[] { 1, 025, "index 025", 500, false});
            SimReg.Rows.Add(new object[] { 2, 055, "index 055", 25, true });
            SimReg.Rows.Add(new object[] { 3, 075, "index 075", 50, false });
            SimReg.Rows.Add(new object[] { 4, 125, "index 125", 960, false});
            SimReg.Rows.Add(new object[] { 5, 138, "index 138", 58, true });
            SimReg.Rows.Add(new object[] { 6, 285, "index 285", 74, true });
            SimReg.Rows.Add(new object[] { 7, 789, "index 789", 35, true });
            SimReg.Rows.Add(new object[] { 8, 358, "index 358", 40, false });
            return SimReg;
        }

        public DataTable GetSelectedReg()
        {
           DataTable old = GetSimReg();
           SimRegSelected = old.Clone();
           //SimRegSelected.Clear();

            foreach (DataRow row in old.Rows)
            {
                //object val = row["Selected"];
                if (row["Selected"].Equals(true))
                {
                   SimRegSelected.ImportRow(row);
                } 

            }
            return SimRegSelected;
        }

        public string[] GetAllReg()
        {
            
            DataTable old = GetSimReg();
            var regList = new List<string>();
            foreach (DataRow row in old.Rows)
            {
              regList.Add(row["Registry"].ToString());   
            }
            var regArray = regList.ToArray();

            return regArray;
        } 
    }
}