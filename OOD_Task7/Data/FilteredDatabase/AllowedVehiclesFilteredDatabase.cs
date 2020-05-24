// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
using BigTask2.Api;
using System.Collections.Generic;

namespace BigTask2.Data.FilteredDatabase
{
    class AllowedVehiclesFilteredDatabase : IGraphDatabase
    {
        private IGraphDatabase Database { get; set; }
        public ISet<VehicleType> AllowedVehicles { get; set; }

        public AllowedVehiclesFilteredDatabase(IGraphDatabase database, ISet<VehicleType> allowedVehicles)
        {
            Database = database;
            AllowedVehicles = allowedVehicles;
        }

        public IIterator<Route> GetRoutesFrom(City from) => new AllowedVehiclesFilteredDatabaseIterator(Database.GetRoutesFrom(from), AllowedVehicles);

        public City GetByName(string cityName) => Database.GetByName(cityName);

        private class AllowedVehiclesFilteredDatabaseIterator : IIterator<Route>
        {
            private IIterator<Route> Iterator { get; set; }
            public ISet<VehicleType> AllowedVehicles { get; set; }

            public AllowedVehiclesFilteredDatabaseIterator(IIterator<Route> iterator, ISet<VehicleType> allowedVehicles)
            {
                Iterator = iterator;
                AllowedVehicles = allowedVehicles;
            }

            public Route GetNext()
            {
                if (Iterator == null) return null;
                if (IsDone())
                    return null;
                Route route = Iterator.GetNext();
                if (route == null) return null;
                return AllowedVehicles.Contains(route.VehicleType) ? route : null;
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
