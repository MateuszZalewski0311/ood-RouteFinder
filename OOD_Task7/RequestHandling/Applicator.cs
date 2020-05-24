// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Data.FilteredDatabase;
using BigTask2.Interfaces;
using BigTask2.Problems;

namespace BigTask2.RequestHandling
{
    interface IApplicator
    {
        IApplicator SetNext(IApplicator applicator);
        IRouteProblem Apply(Request request);
    }

    class CostProblemApplicator : IApplicator
    {
        private IApplicator Next { get; set; }

        public IApplicator SetNext(IApplicator applicator)
        {
            Next = applicator;
            return applicator;
        }

        public IRouteProblem Apply(Request request)
        {
            IRouteProblem problem = null;
            if (Next != null)
                problem = Next.Apply(request);
            if (problem == null && request.Problem == "Cost")
                problem = new CostProblem(request.From, request.To, request.Solver);

            return problem;
        }
    }

    class TimeProblemApplicator : IApplicator
    {
        private IApplicator Next { get; set; }

        public IApplicator SetNext(IApplicator applicator)
        {
            Next = applicator;
            return applicator;
        }

        public IRouteProblem Apply(Request request)
        {
            IRouteProblem problem = null;
            if (Next != null)
                problem = Next.Apply(request);
            if (problem == null && request.Problem == "Time")
                problem = new TimeProblem(request.From, request.To, request.Solver);

            return problem;
        }
    }

    class MinPopulationFilterApplicator : IApplicator
    {
        private IApplicator Next { get; set; }

        public IApplicator SetNext(IApplicator applicator)
        {
            Next = applicator;
            return applicator;
        }

        public IRouteProblem Apply(Request request)
        {
            IRouteProblem problem = null;
            if (Next != null)
                problem = Next.Apply(request);
            if (problem != null)
                problem.Graph = new MinPopulationFilteredDatabase(problem.Graph, request.Filter.MinPopulation);

            return problem;
        }
    }

    class RestaurantRequiredFilterApplicator : IApplicator
    {
        private IApplicator Next { get; set; }

        public IApplicator SetNext(IApplicator applicator)
        {
            Next = applicator;
            return applicator;
        }

        public IRouteProblem Apply(Request request)
        {
            IRouteProblem problem = null;
            if (Next != null)
                problem = Next.Apply(request);
            if (problem != null)
                problem.Graph = new RestaurantRequiredFilteredDatabase(problem.Graph, request.Filter.RestaurantRequired);

            return problem;
        }
    }

    class AllowedVehiclesFilterApplicator : IApplicator
    {
        private IApplicator Next { get; set; }

        public IApplicator SetNext(IApplicator applicator)
        {
            Next = applicator;
            return applicator;
        }

        public IRouteProblem Apply(Request request)
        {
            IRouteProblem problem = null;
            if (Next != null)
                problem = Next.Apply(request);
            if (problem != null)
                problem.Graph = new AllowedVehiclesFilteredDatabase(problem.Graph, request.Filter.AllowedVehicles);

            return problem;
        }
    }

    class MergeApplicator : IApplicator
    {
        private IApplicator Next { get; set; }
        private IGraphDatabase[] Databases { get; set; }

        public MergeApplicator(params IGraphDatabase[] databases) => Databases = databases;

        public IApplicator SetNext(IApplicator applicator)
        {
            Next = applicator;
            return applicator;
        }

        public IRouteProblem Apply(Request request)
        {
            IRouteProblem problem = null;
            if (Next != null)
                problem = Next.Apply(request);
            if (problem != null)
                problem.Graph = new MergeDatabase(Databases);
            return problem;
        }
    }
}
