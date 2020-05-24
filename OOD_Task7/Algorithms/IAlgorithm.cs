// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
using BigTask2.Api;
using BigTask2.Data;
using System.Collections.Generic;

namespace BigTask2.Algorithms
{
    interface IAlgorithm
    {
        IEnumerable<Route> Solve(IGraphDatabase graph, City from, City to);
    }
}
