// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
//This file contains fragments that You have to fulfill

using BigTask2.Api;
using System.Collections.Generic;

namespace BigTask2.Data
{
	class AdjacencyListDatabase : IGraphDatabase
    {
		private Dictionary<string, City> cityDictionary = new Dictionary<string, City>();
		private Dictionary<City, List<Route>> routes = new Dictionary<City, List<Route>>();
		
		private void AddCity(City city)
		{
			if (!cityDictionary.ContainsKey(city.Name))
				cityDictionary[city.Name] = city;
		}
		public AdjacencyListDatabase(IEnumerable<Route> routes)
		{
			foreach(Route route in routes)
			{
				AddCity(route.From);
				AddCity(route.To);
				if (!this.routes.ContainsKey(route.From))
				{
					this.routes[route.From] = new List<Route>();
				}
				this.routes[route.From].Add(route);
			}
		}
		public AdjacencyListDatabase()
		{
		}
		public void AddRoute(City from, City to, double cost, double travelTime, VehicleType vehicle)
		{
			AddCity(from);
			AddCity(to);
			if (!routes.ContainsKey(from))
			{
				routes[from] = new List<Route>();
			}
			routes[from].Add(new Route { From = from, To = to, Cost = cost, TravelTime = travelTime, VehicleType = vehicle});
		}

		public IIterator<Route> GetRoutesFrom(City from)
		{
			if (!routes.ContainsKey(from))
				return null;
			return new AdjacencyListIterator(this, from);
		}

		public City GetByName(string cityName)
		{
			return cityDictionary.GetValueOrDefault(cityName);
		}

		private class AdjacencyListIterator : IIterator<Route>
		{
			private int index;
			private AdjacencyListDatabase Database { get; set; }
			private City FromCity { get; set; }

			public AdjacencyListIterator(AdjacencyListDatabase database, City fromCity)
			{
				index = 0;
				Database = database;
				FromCity = fromCity;
			}

			public Route GetNext()
			{
				if (IsDone())
					return null;
				return Database.routes[FromCity][index++];
			}

			public void Reset() => index = 0;

            public bool IsDone() => index >= Database.routes[FromCity].Count;
        }
	}
}
