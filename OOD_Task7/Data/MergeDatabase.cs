// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
using BigTask2.Api;
using System.Collections.Generic;

namespace BigTask2.Data
{
    class MergeDatabase : IGraphDatabase
    {
        private IGraphDatabase[] Databases { get; set; }
        public MergeDatabase(params IGraphDatabase[] databases) => Databases = databases;
        public IIterator<Route> GetRoutesFrom(City from)
        {
            IIterator<Route> it;
            List<IIterator<Route>> iterators = new List<IIterator<Route>>();

            foreach (IGraphDatabase database in Databases)
                if ((it = database.GetRoutesFrom(from)) != null)
                    iterators.Add(it);

            return new MergeDatabaseIterator(iterators);
        }
        public City GetByName(string cityName)
        {
            City city;

            foreach(IGraphDatabase database in Databases)
            {
                city = database.GetByName(cityName);
                if (city != null)
                    return city;
            }
            return null;
        }

        private class MergeDatabaseIterator : IIterator<Route>
        {
            private int index;
            private List<IIterator<Route>> Iterators { get; set; }

            public MergeDatabaseIterator(List<IIterator<Route>> iterators)
            {
                Iterators = iterators;
                Reset();
            }
            public Route GetNext()
            {
                if (Iterators.Count < 1)
                    return null;
                Route route = null;
                while (!IsDone() && route == null)
                {
                    if ((route = Iterators[index].GetNext()) == null)
                        index++;
                }
                return route;
            }

            public void Reset() => index = Iterators.Count < 1 ? -1 : 0;

            public bool IsDone() => index >= Iterators.Count;
        }
    }
}
