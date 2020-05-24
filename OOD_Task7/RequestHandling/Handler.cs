// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
using BigTask2.Algorithms;
using BigTask2.Api;
using BigTask2.Problems;
using System.Collections.Generic;

namespace BigTask2.RequestHandling
{
    interface IHandler
    {
        IHandler SetNext(IHandler nextSolver);

        IEnumerable<Route> Handle(CostProblem costProblem);

        IEnumerable<Route> Handle(TimeProblem timeProblem);
    }

    class BFSHandler : IHandler
    {
        public IAlgorithm BFSAlgorithm { get; private set; }

        private IHandler Next { get; set; }

        public BFSHandler(BFS algorithm) => BFSAlgorithm = algorithm;

        public IHandler SetNext(IHandler nextSolver) => Next = nextSolver;

        public IEnumerable<Route> Handle(CostProblem costProblem)
        {
            if (costProblem.Algorithm == "BFS")
                return BFSAlgorithm.Solve(costProblem.Graph, costProblem.Graph.GetByName(costProblem.From), costProblem.Graph.GetByName(costProblem.To));
            return Next != null ? Next.Handle(costProblem) : null;
        }

        public IEnumerable<Route> Handle(TimeProblem timeProblem)
        {
            if (timeProblem.Algorithm == "BFS")
                return BFSAlgorithm.Solve(timeProblem.Graph, timeProblem.Graph.GetByName(timeProblem.From), timeProblem.Graph.GetByName(timeProblem.To));
            return Next != null ? Next.Handle(timeProblem) : null;
        }
    }

    class DFSHandler : IHandler
    {
        public IAlgorithm DFSAlgorithm { get; private set; }

        private IHandler Next { get; set; }

        public DFSHandler(DFS algorithm) => DFSAlgorithm = algorithm;

        public IHandler SetNext(IHandler nextSolver) => Next = nextSolver;

        public IEnumerable<Route> Handle(CostProblem costProblem)
        {
            if (costProblem.Algorithm == "DFS")
                return DFSAlgorithm.Solve(costProblem.Graph, costProblem.Graph.GetByName(costProblem.From), costProblem.Graph.GetByName(costProblem.To));
            return Next != null ? Next.Handle(costProblem) : null;
        }

        public IEnumerable<Route> Handle(TimeProblem timeProblem)
        {
            if (timeProblem.Algorithm == "DFS")
                return DFSAlgorithm.Solve(timeProblem.Graph, timeProblem.Graph.GetByName(timeProblem.From), timeProblem.Graph.GetByName(timeProblem.To));
            return Next != null ? Next.Handle(timeProblem) : null;
        }
    }

    class DijkstraHandler : IHandler
    {
        public IAlgorithm DijkstraCostAlgorithm { get; private set; }
        public IAlgorithm DijkstraTimeAlgorithm { get; private set; }
        private IHandler Next { get; set; }

        public DijkstraHandler(DijkstraCost dijkstraCostAlgorithm, DijkstraTime dijkstraTimeAlgorithm)
        {
            DijkstraCostAlgorithm = dijkstraCostAlgorithm;
            DijkstraTimeAlgorithm = dijkstraTimeAlgorithm; 
        }

        public IHandler SetNext(IHandler nextSolver) => Next = nextSolver;

        public IEnumerable<Route> Handle(CostProblem costProblem)
        {
            if (costProblem.Algorithm == "Dijkstra")
                return DijkstraCostAlgorithm.Solve(costProblem.Graph, costProblem.Graph.GetByName(costProblem.From), costProblem.Graph.GetByName(costProblem.To));
            return Next != null ? Next.Handle(costProblem) : null;
        }

        public IEnumerable<Route> Handle(TimeProblem timeProblem)
        {
            if (timeProblem.Algorithm == "Dijkstra")
                return DijkstraTimeAlgorithm.Solve(timeProblem.Graph, timeProblem.Graph.GetByName(timeProblem.From), timeProblem.Graph.GetByName(timeProblem.To));
            return Next != null ? Next.Handle(timeProblem) : null;
        }
    }
}
