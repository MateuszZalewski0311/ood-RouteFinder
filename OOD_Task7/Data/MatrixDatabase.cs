// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
//This file contains fragments that You have to fulfill

using BigTask2.Api;
using System.Collections.Generic;
using System.Linq;

namespace BigTask2.Data
{
	class MatrixDatabase : IGraphDatabase
	{
		private Dictionary<City, int> cityIds = new Dictionary<City, int>();
		private Dictionary<string, City> cityDictionary = new Dictionary<string, City>();
		private List<List<Route>> routes = new List<List<Route>>();

		private void AddCity(City city)
		{
			if (!cityDictionary.ContainsKey(city.Name))
			{
				cityDictionary[city.Name] = city;
				cityIds[city] = cityIds.Count;
				foreach (var routes in routes)
				{
					routes.Add(null);
				}
				routes.Add(new List<Route>(Enumerable.Repeat<Route>(null, cityDictionary.Count)));
			}
		}
		public MatrixDatabase(IEnumerable<Route> routes)
		{
            foreach (var route in routes)
			{
				AddCity(route.From);
				AddCity(route.To);
			}
			foreach (var route in routes)
			{
				this.routes[cityIds[route.From]][cityIds[route.To]] = route;
			}
		}

		public void AddRoute(City from, City to, double cost, double travelTime, VehicleType vehicle)
		{
			AddCity(from);
			AddCity(to);
			routes[cityIds[from]][cityIds[to]] = new Route { From = from, To = to, Cost = cost, TravelTime = travelTime, VehicleType = vehicle };
		}

		public IIterator<Route> GetRoutesFrom(City from)
		{
			if (!cityIds.ContainsKey(from))
				return null;
			return new MatrixIterator(this, from);
		}

		public City GetByName(string cityName)
		{
			return cityDictionary[cityName];
		}

		private class MatrixIterator : IIterator<Route>
		{
			private int index = 0;
			private MatrixDatabase Matrix { get; set; }
			private int CityId { get; set; }

			public MatrixIterator(MatrixDatabase matrix, City fromCity)
			{
				Matrix = matrix;
				CityId = Matrix.cityIds.TryGetValue(fromCity, out int index) ? index : -1;
			}

			public Route GetNext()
			{
				Route route = null;
				while (!IsDone() && route == null)
					route = Matrix.routes[CityId][index++];
				return route;
			}

			public void Reset() => index = 0;

			public bool IsDone() => CityId == -1 || index >= Matrix.routes[CityId].Count;
		}
	}
}
