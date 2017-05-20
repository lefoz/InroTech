using Inrotech.Domain.Components.Robot;
using System.Collections.Generic;

namespace Inrotech.Domain.Graph
{
    public interface IGraph
    {
        Dictionary<string, int> GetGraph(IRobot Robot);

    }
}