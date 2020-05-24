// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
using BigTask2.Api;

namespace BigTask2.Data.FilteredDatabase
{
    class MinPopulationFilteredDatabase : IGraphDatabase
    {
        private IGraphDatabase Database { get; set; }
        public int MinPopulation { get; set; }

        public MinPopulationFilteredDatabase(IGraphDatabase database, int minPopulation)
        {
            Database = database;
            MinPopulation = minPopulation;
        }

        public IIterator<Route> GetRoutesFrom(City from) => new MinPopulationFilteredDatabaseIterator(Database.GetRoutesFrom(from), MinPopulation);

        public City GetByName(string cityName)
        {
            City city = Database.GetByName(cityName);
            if (city == null)
                return null;
            return MinPopulation > city.Population ? null : city;
        }

        private class MinPopulationFilteredDatabaseIterator : IIterator<Route>
        {
            private IIterator<Route> Iterator { get; set; }
            public int MinPopulation { get; set; }

            public MinPopulationFilteredDatabaseIterator(IIterator<Route> iterator, int minPopulation)
            {
                Iterator = iterator;
                MinPopulation = minPopulation;
            }
            public Route GetNext()
            {
                if (Iterator == null) return null;
                if (IsDone())
                    return null;
                Route route = Iterator.GetNext();
                if (route == null) return null;
                return (route.To.Population < MinPopulation || route.From.Population < MinPopulation) ? null : route;
            }

            public void Reset()
            {
                if (Iterator == null) return;
                Iterator.Reset();
            }

            public bool IsDone()
            {
                if (Iterator == null) return true;
                return Iterator.IsDone();
            }
        }
    }
}
