using System.Data;
using Inrotech.Domain.Register;
namespace Inrotech.Domain.Register
{
    internal class Sim_Register
    {
        public DataTable SimReg;

        public Sim_Register()
        {
            
        }
        

        public DataTable GetSimReg()
        {
            SimReg = new DataTable();
            SimReg.Clear();
            SimReg.Columns.Add("Regitry");
            SimReg.Columns.Add("Name");
            SimReg.Columns.Add("Value");

            return SimReg;
        }
    }
}