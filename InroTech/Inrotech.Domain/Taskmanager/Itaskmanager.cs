using System.Data;

namespace Inrotech.Domain.Taskmanager
{
    public interface ITaskmanager
    {
        DataTable getTaskmanager();
        void SetTaskmanager();
    }
}