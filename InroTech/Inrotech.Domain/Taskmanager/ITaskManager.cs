using Inrotech.Domain.Components.Robot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inrotech.Domain.TaskManager
{
    public interface ItaskManager
    {
        DataTable getTaskManager();
        void SetTaskmanager(IRobot robot);
    }
}
