// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
namespace BigTask2.Data
{
    public interface IIterator<T>
    {
        void Reset();  // at the moment unused
        bool IsDone(); // at the moment only used internally
        T GetNext();
    }
}
