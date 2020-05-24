// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
using BigTask2.Api;
using System;
using System.Collections.Generic;

namespace BigTask2.Ui
{
    class KeyValueForm : IForm
    {
        private Dictionary<string, string> KeyValuePairs { get; }
        public KeyValueForm() => KeyValuePairs = new Dictionary<string, string>();
        public void Insert(string command)
        {
            string name, value;
            int i = 1;
            while (command[i] != '=') i++;
            name = command.Substring(0, i);
            value = command.Substring(++i);

            KeyValuePairs[name] = value;
        }
        public bool GetBoolValue(string name)
        {
            if (!KeyValuePairs.ContainsKey(name))
            {
                throw new KeyNotFoundException($"Error - KeyValueForm - doesn't contain key: {name}");
            }
            if (KeyValuePairs[name] == "True") return true;
            else if (KeyValuePairs[name] == "False") return false;
            else
            {
                throw new InvalidOperationException($"Error - KeyValueForm - value of key: {name} is not a boolean value");
            }
        }
        public string GetTextValue(string name)
        {
            if (!KeyValuePairs.ContainsKey(name))
            {
                throw new KeyNotFoundException($"Error - KeyValueForm - doesn't contain key: {name}");
            }
            return KeyValuePairs[name];
        }
        public int GetNumericValue(string name)
        {
            if (!KeyValuePairs.ContainsKey(name))
            {
                throw new KeyNotFoundException($"Error - KeyValueForm - doesn't contain key: {name}");
            }
            try { return int.Parse(KeyValuePairs[name]); }
            catch
            {
                throw new FormatException($"Error - KeyValueForm - Parse failed in GetNumericValue for key: {name}");
            }
        }
    }

    class KeyValueDisplay : IDisplay
    {
        public void Print(IEnumerable<Route> routes)
        {
            if (routes == null)
            {
                Console.WriteLine("=");
                return;
            }
            City to = new City();
            double totalTime = 0;
            double totalCost = 0;
            foreach (Route r in routes)
            {
                Console.WriteLine("=City=");
                Console.WriteLine($"Name={r.From.Name}");
                Console.WriteLine($"Population={r.From.Population}");
                Console.WriteLine($"HasRestaurant={r.From.HasRestaurant}\n");
                Console.WriteLine("=Route=");
                Console.WriteLine($"Vehicle={r.VehicleType}");
                Console.WriteLine($"Cost={r.Cost:0.##}");
                Console.WriteLine($"TravelTime={r.TravelTime:0.##}\n");
                totalTime += r.TravelTime;
                totalCost += r.Cost;
                to.Name = r.To.Name;
                to.Population = r.To.Population;
                to.HasRestaurant = r.To.HasRestaurant;
            }
            Console.WriteLine("=City=");
            Console.WriteLine($"Name={to.Name}");
            Console.WriteLine($"Population={to.Population}");
            Console.WriteLine($"HasRestaurant={to.HasRestaurant}\n");
            Console.WriteLine($"totalTime={totalTime:0.##}");
            Console.WriteLine($"totalCost={totalCost:0.##}\n");
        }
    }

    class KeyValueSystem : ISystem
    {
        public IForm Form { get; }
        public IDisplay Display { get; }
        public KeyValueSystem(IForm form, IDisplay display)
        {
            Form = form;
            Display = display;
        }
    }
}
