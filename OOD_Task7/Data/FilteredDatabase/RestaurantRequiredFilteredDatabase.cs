// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
using BigTask2.Api;

namespace BigTask2.Data.FilteredDatabase
{
    class RestaurantRequiredFilteredDatabase : IGraphDatabase
    {
        private IGraphDatabase Database { get; set; }
        public bool RestaurantRequired { get; set; }

        public RestaurantRequiredFilteredDatabase(IGraphDatabase database, bool restaurantRequired)
        {
            Database = database;
            RestaurantRequired = restaurantRequired;
        }

        public IIterator<Route> GetRoutesFrom(City from) => new RestaurantRequiredFilteredDatabaseIterator(Database.GetRoutesFrom(from), RestaurantRequired);

        public City GetByName(string cityName)
        {
            City city = Database.GetByName(cityName);
            if (city == null)
                return null;
            return (RestaurantRequired && !city.HasRestaurant) ? null : city;
        }

        private class RestaurantRequiredFilteredDatabaseIterator : IIterator<Route>
        {
            private IIterator<Route> Iterator { get; set; }
            public bool RestaurantRequired { get; set; }
            public RestaurantRequiredFilteredDatabaseIterator(IIterator<Route> iterator, bool restaurantRequired)
            {
                Iterator = iterator;
                RestaurantRequired = restaurantRequired;
            }

            public Route GetNext()
            {
                if (Iterator == null) return null;
                if (IsDone()) 
                    return null;
                Route route = Iterator.GetNext();
                if (route == null) return null;
                return (RestaurantRequired && (!route.From.HasRestaurant || !route.To.HasRestaurant)) ? null : route;
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
