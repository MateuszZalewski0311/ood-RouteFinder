// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
using BigTask2.Algorithms;
using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Interfaces;
using System.Collections.Generic;

namespace BigTask2.RequestHandling
{
    class RequestHandler
    {
        private static RequestHandler instance = null;

        public static RequestHandler GetRequestHandler(params IGraphDatabase[] databases)
        {
            if (instance == null)
                instance = new RequestHandler(databases);
            return instance;
        }
        //-------------------------------------------------------------------
        private IValidator Validator { get; set; }
        private IHandler Solver { get; set; }
        private IApplicator Applicator { get; set; }

        private RequestHandler(params IGraphDatabase[] databases)
        {
            Validator = new CityFromValidator();
            Validator.SetNext(new CityToValidator()).SetNext(new MinimalPopulationValidator()).SetNext(new VechicleTypeValidator());
            Solver = new DFSHandler(new DFS());
            Applicator = new MinPopulationFilterApplicator();
            Applicator.SetNext(new RestaurantRequiredFilterApplicator()).SetNext(new AllowedVehiclesFilterApplicator()).SetNext(new MergeApplicator(databases)).SetNext(new TimeProblemApplicator()).SetNext(new CostProblemApplicator());
            Solver.SetNext(new BFSHandler(new BFS())).SetNext(new DijkstraHandler(new DijkstraCost(), new DijkstraTime()));
        }

        public bool Validate(Request request)
        {
            return Validator.Validate(request);
        }

        public IRouteProblem Apply(Request request)
        {
            return Applicator.Apply(request);
        }

        public IEnumerable<Route> Solve(IRouteProblem problem)
        {
            return problem.Solve(Solver);
        }
    }
}
