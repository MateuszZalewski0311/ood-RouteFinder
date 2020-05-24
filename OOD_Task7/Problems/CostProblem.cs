// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
//This file Can be modified

using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Interfaces;
using BigTask2.RequestHandling;
using System.Collections.Generic;

namespace BigTask2.Problems
{
	class CostProblem : IRouteProblem
	{
		public IGraphDatabase Graph { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public string Algorithm { get; set; }

		public CostProblem(string from, string to, string algorithm)
		{
			From = from;
			To = to;
			Algorithm = algorithm;
		}

		public IEnumerable<Route> Solve(IHandler handler)
		{
			return handler.Handle(this);
		}
	}
}
