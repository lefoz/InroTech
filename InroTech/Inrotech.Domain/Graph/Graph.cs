using Inrotech.Domain.Components.Robot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inrotech.Domain.Graph
{
    public class Graph : IGraph
    {
        Dictionary<string, int> GraphData;

        int v;
        int a;

        public Graph(int volt, int amps)
        {
            GraphData = new Dictionary<string, int>();
            v = volt;
            a = amps;
          
        }

        public Dictionary<string,int> GetGraph()
        {
            GraphData.Add("volt", v);
            GraphData.Add("amp", a);

            return GraphData;
        }
    }
}
