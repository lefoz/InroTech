using System;
using System.Collections.Generic;
using System.Text;

namespace Inrotech.Domain.Graph
{
    public class Sim_database
    {
        int data;
        Random rand = new Random();

        public Sim_database()
        {

        }
        public int GetSim_database()
        {
            data = rand.Next(100);
            return data;
        }
    }
}
