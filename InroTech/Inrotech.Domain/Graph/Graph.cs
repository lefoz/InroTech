using Inrotech.Domain.Components.Robot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inrotech.Domain.Graph
{
    public class Graph : IGraph
    {
        Dictionary<string, int> GraphData;

        public Graph()
        {
            GraphData = new Dictionary<string, int>();
          
        }

        public Dictionary<string,int> GetGraph()
        {
            throw new NotImplementedException();
        }
    }
}
