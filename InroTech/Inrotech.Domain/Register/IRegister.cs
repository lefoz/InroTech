using System.Data;

namespace Inrotech.Domain.Register
{
    public interface IRegister
    {
        string[] GetAllReg();
        DataTable GetSelectedReg(string[] selItems);
        DataTable GetReg();
        void RegArrayTester(string[] test);
        string[] RobotInfo();
    }
}