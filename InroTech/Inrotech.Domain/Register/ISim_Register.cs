using System.Data;

namespace Inrotech.Domain.Register
{
    public interface ISim_Register
    {
        string[] GetAllReg();
        DataTable GetSelectedReg(string[] selItems);
        DataTable GetSimReg();
        void RegArrayTester(string[] test);
        string[] Sim_RobotInfo();
    }
}