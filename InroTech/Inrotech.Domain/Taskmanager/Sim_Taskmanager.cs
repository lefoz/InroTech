using Inrotech.Domain.Components.Robot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inrotech.Domain.Taskmanager
{
  public class Sim_TaskManager : ITaskManager
    {
        private DataTable tasks_DT;
        public Sim_TaskManager()
        {
            DataTable newTaskDT = new DataTable();
            newTaskDT.Clear();
            newTaskDT.Columns.Add("id", typeof(int));
            newTaskDT.Columns.Add("Name", typeof(String));
            newTaskDT.Columns.Add("Status", typeof(String));
            newTaskDT.Columns.Add("Cpu", typeof(String));
            newTaskDT.Rows.Add(new object[] { 1, "Startup", "Done", 0 });
            newTaskDT.Rows.Add(new object[] { 2, "Laser Adjustment", "Running", 25 });
            newTaskDT.Rows.Add(new object[] { 3, "Ctrl32", "Running", 50 });
            newTaskDT.Rows.Add(new object[] { 4, "Gyro Mesurment", "Error", 0 });
            this.tasks_DT = newTaskDT;
        }

        public DataTable getTaskManager()
        {
            return tasks_DT;
        }
    }
}
