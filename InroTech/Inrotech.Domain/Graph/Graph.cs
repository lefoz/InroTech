using Inrotech.Domain.Components.Robot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inrotech.Domain.Graph
{
    class Graph : IGraph
    {
        Dictionary<string, int> GraphData;

        public Graph()
        {
            
        }

        public Dictionary<string,int> GetGraph(IRobot Robot)
        {
            throw new NotImplementedException();
        }
    }
}
