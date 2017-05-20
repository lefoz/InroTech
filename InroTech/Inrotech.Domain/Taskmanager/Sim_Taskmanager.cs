using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inrotech.Domain.Taskmanager
{
  public class Sim_Taskmanager
    {
        private DataTable SimReg;
        public Sim_Taskmanager()
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
        }

        public DataTable getTaskmanager()
        {
            return SimReg;
        }
    }
}
