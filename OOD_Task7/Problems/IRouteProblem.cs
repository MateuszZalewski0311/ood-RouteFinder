// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
//This file Can be modified

using BigTask2.Api;
using BigTask2.Data;
using BigTask2.RequestHandling;
using System.Collections.Generic;

namespace BigTask2.Interfaces
{
    interface IRouteProblem
	{
        IGraphDatabase Graph { get; set; }
        string From { get; set; }
        string To { get; set; }
        string Algorithm { get; set; }

        public IEnumerable<Route> Solve(IHandler handler);
    }
}
