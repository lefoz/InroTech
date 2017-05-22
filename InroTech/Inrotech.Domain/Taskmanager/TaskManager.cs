using Inrotech.Domain.Components.Robot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inrotech.Domain.Taskmanager
{
  public class TaskManager : ITaskManager
    {
        private DataTable tasks_DT;
        public TaskManager()
        {
        }

        public DataTable getTaskManager()
        {
            return tasks_DT;
        }
    }
}
