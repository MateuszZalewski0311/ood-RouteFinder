// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
//This file contains fragments that You have to fulfill

using BigTask2.Api;
namespace BigTask2.Data
{
    public interface IGraphDatabase
    {
        IIterator<Route> GetRoutesFrom(City from);
        City GetByName(string cityName);
    }
}
