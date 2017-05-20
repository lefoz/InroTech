using Inrotech.Domain.Components.Robot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inrotech.Domain.Taskmanager
{
  public class Sim_Taskmanager : ITaskmanager
    {
        private DataTable tasks_DT;
        public Sim_Taskmanager()
        {
            if(tasks_DT == null)
            {
                this.SetTaskmanager();
            }
        }

        public DataTable getTaskmanager()
        {
            return tasks_DT;
        }

        // Simulates Data from IRobot
        public void SetTaskmanager()
        { 
            DataTable newTaskDT = new DataTable();
            newTaskDT.Clear();
            newTaskDT.Columns.Add("id", typeof(int));
            newTaskDT.Columns.Add("Registry", typeof(int));
            newTaskDT.Columns.Add("Value", typeof(double));
            newTaskDT.Columns.Add("Selected", typeof(bool));
            newTaskDT.Rows.Add(new object[] { 1, 025, 500, false });
            newTaskDT.Rows.Add(new object[] { 2, 055, 25, false });
            newTaskDT.Rows.Add(new object[] { 3, 075, 50, false });
            newTaskDT.Rows.Add(new object[] { 4, 125, 960, false });
            newTaskDT.Rows.Add(new object[] { 5, 138, 58, false });
            newTaskDT.Rows.Add(new object[] { 6, 285, 74, false });
            newTaskDT.Rows.Add(new object[] { 7, 789, 35, false });
            newTaskDT.Rows.Add(new object[] { 8, 358, 40, false });
            this.tasks_DT = newTaskDT;
        }
    }
}
