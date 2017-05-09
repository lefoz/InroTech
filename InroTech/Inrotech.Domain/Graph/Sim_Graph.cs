using System;
using System.Collections.Generic;
using System.Text;

namespace Inrotech.Domain.Graph
{
    public class Sim_Graph
    {
        int data;
        Random rand = new Random();

        public Sim_Graph()
        {
            
        }
        public int GetSim_Graph()
        {
            data = rand.Next(100);
            return data;
        }
    }
}
