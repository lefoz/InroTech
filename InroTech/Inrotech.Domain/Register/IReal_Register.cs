using System.Data;

namespace Inrotech.Real_Register
{
    public interface IReal_Register
    {
        string[] GetAllReg();
        DataTable GetReg();
        DataTable GetSelectedReg(string[] selItems);
        void RegArrayTester(string[] test);
        string[] RobotInfo();
    }
}