// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
using BigTask2.Api;

namespace BigTask2.RequestHandling
{
    interface IValidator
    {
        IValidator SetNext(IValidator nextValidator);

        bool Validate(Request request);
    }

    class CityFromValidator : IValidator
    {
        private IValidator Next { get; set; }
        public IValidator SetNext(IValidator nextValidator)
        {
            Next = nextValidator;
            return nextValidator;
        }

        public bool Validate(Request request)
        {
            if (string.IsNullOrEmpty(request.From))
                return false;
            else if (Next != null)
                return Next.Validate(request);
            else 
                return false;
        }
    }

    class CityToValidator : IValidator
    {
        private IValidator Next { get; set; }
        public IValidator SetNext(IValidator nextValidator)
        {
            Next = nextValidator;
            return nextValidator;
        }

        public bool Validate(Request request)
        {
            if (string.IsNullOrEmpty(request.To))
                return false;
            else if (Next != null)
                return Next.Validate(request);
            else
                return true;
        }
    }

    class MinimalPopulationValidator : IValidator
    {
        private IValidator Next { get; set; }
        public IValidator SetNext(IValidator nextValidator)
        {
            Next = nextValidator;
            return nextValidator;
        }

        public bool Validate(Request request)
        {
            if (request.Filter.MinPopulation < 0)
                return false;
            else if (Next != null)
                return Next.Validate(request);
            else
                return true;
        }
    }

    class VechicleTypeValidator : IValidator
    {
        private IValidator Next { get; set; }
        public IValidator SetNext(IValidator nextValidator)
        {
            Next = nextValidator;
            return nextValidator;
        }

        public bool Validate(Request request)
        {
            if (request.Filter.AllowedVehicles.Count < 1)
                return false;
            else if (Next != null)
                return Next.Validate(request);
            else
                return true;
        }
    }
}
