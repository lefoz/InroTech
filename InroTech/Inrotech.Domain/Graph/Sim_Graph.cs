using System;
using System.Collections.Generic;
using System.Text;

namespace Inrotech.Domain.Graph
{
    public class Sim_Graph : IGraph
    {
        int data;
        Random rand = new Random();

        public Sim_Graph()
        {
            
        }
        public int GetGraph()
        {
            data = rand.Next(100);
            return data;
        }
    }
}
