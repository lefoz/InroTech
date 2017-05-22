﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Inrotech.Domain.Register;
namespace Inrotech.Domain.Register
{
    public class Sim_Register : IRegister
    {
        private DataTable SimReg;
        private DataTable SimRegSelected;

        public Sim_Register()
        {
            GetReg();
        }
        
        public DataTable GetReg()
        {
            SimReg = new DataTable();
            SimReg.Clear();
            SimReg.Columns.Add("id", typeof(int));
            SimReg.Columns.Add("Registry", typeof(int));
            SimReg.Columns.Add("Value", typeof(double));
            SimReg.Columns.Add("Selected", typeof(bool));
            SimReg.Rows.Add(new object[] { 1, 025, 500, false});
            SimReg.Rows.Add(new object[] { 2, 055, 25, false });
            SimReg.Rows.Add(new object[] { 3, 075, 50, false });
            SimReg.Rows.Add(new object[] { 4, 125, 960, false });
            SimReg.Rows.Add(new object[] { 5, 138, 58, false });
            SimReg.Rows.Add(new object[] { 6, 285, 74, false });
            SimReg.Rows.Add(new object[] { 7, 789, 35, false });
            SimReg.Rows.Add(new object[] { 8, 358, 40, false });
            return SimReg;
        }

        public DataTable GetSelectedReg(string[] selItems)
        {
                //Console.WriteLine(selItems.Tostring);
                DataTable old = GetReg();
                SimRegSelected = old.Clone();
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
                        SimRegSelected.ImportRow(row);
                    }
                }

            }
            return SimRegSelected;
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
            String[] SimInfo = new string[]{"Simulate Robot", "localhost:53982","SimJob", "SimProgress" };
            return SimInfo;
        }

    }
}