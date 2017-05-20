using System;
using System.Collections.Generic;
using System.Text;
using Inrotech.Domain.Components.Robot;

namespace Inrotech.Domain.Graph
{
    public class Sim_Graph : IGraph
    {
        Dictionary<string, int> GraphData;
        Random rand = new Random();

        public Sim_Graph()
        {
           GraphData = new Dictionary<string, int>();
        }
        public Dictionary<string, int> GetGraph(IRobot Robot)
        {
            GraphData.Add("volt", rand.Next(10));
            GraphData.Add("amp", rand.Next(10));

            return GraphData;
        }
    }
}
