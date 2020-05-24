// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Interfaces;
using BigTask2.RequestHandling;
using BigTask2.Ui;
using System;
using System.Collections.Generic;
using System.IO;

namespace BigTask2
{
	class Program
	{
        static IEnumerable<Route> ServeRequest(Request request)
        {
            (IGraphDatabase cars, IGraphDatabase trains) = MockData.InitDatabases();
            RequestHandler requestHandler = RequestHandler.GetRequestHandler(cars, trains);
            if (!requestHandler.Validate(request)) 
                return null;
            IRouteProblem problem = requestHandler.Apply(request);
            return requestHandler.Solve(problem);
		}
		static void Main(string[] args)
		{
            Console.WriteLine("---- Xml Interface ----");
            ISystem xmlSystem = CreateSystem(new XmlFactory());
            Execute(xmlSystem, "xml_input.txt");
            Console.WriteLine();

            Console.WriteLine("---- KeyValue Interface ----");
            ISystem keyValueSystem = CreateSystem(new KeyValueFactory());
            Execute(keyValueSystem, "key_value_input.txt");
            Console.WriteLine();
        }

        /* Prepare method Create System here (add return, arguments and body)*/
        static ISystem CreateSystem(IAbstractUIFactory factory)
        {
            return factory.GetSystem(factory.GetForm(), factory.GetDisplay());
        }

        static void Execute(ISystem system, string path)
        {
            IEnumerable<IEnumerable<string>> allInputs = ReadInputs(path);
            foreach (var inputs in allInputs)
            {
                foreach (string input in inputs)
                {
                    system.Form.Insert(input);
                }
                var request = RequestMapper.Map(system.Form);
                var result = ServeRequest(request);
                system.Display.Print(result);
                Console.WriteLine("==============================================================");
            }
        }

        private static IEnumerable<IEnumerable<string>> ReadInputs(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                List<List<string>> allInputs = new List<List<string>>();
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    List<string> inputs = new List<string>();
                    while (!string.IsNullOrEmpty(line))
                    {
                        inputs.Add(line);
                        line = file.ReadLine();
                    }
                    if (inputs.Count > 0)
                    {
                        allInputs.Add(inputs);
                    }
                }
                return allInputs;
            }
        }
    }
}
