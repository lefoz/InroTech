using System.Data;
using Inrotech.Domain.Register;
namespace Inrotech.Domain.Register
{
    internal class Sim_Register
    {
        public DataTable SimReg;
        
        public DataTable GetSimReg()
        {
            SimReg = new DataTable();
            SimReg.Clear();
            SimReg.Columns.Add("Regitry");
            SimReg.Columns.Add("Name");
            SimReg.Columns.Add("Value");
            SimReg.Rows.Add(new object[] {025, "index 025", 500});
            SimReg.Rows.Add(new object[] {055, "index 055", 25 });
            SimReg.Rows.Add(new object[] {075, "index 075", 50 });
            SimReg.Rows.Add(new object[] {125, "index 125", 960});
            SimReg.Rows.Add(new object[] {138, "index 138", 58 });
            SimReg.Rows.Add(new object[] {285, "index 285", 74 });
            SimReg.Rows.Add(new object[] {789, "index 789", 35 });
            SimReg.Rows.Add(new object[] {358, "index 358", 40 });
            return SimReg;
        }
    }
}