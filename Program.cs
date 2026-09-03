using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PlacesLibrary;

namespace _10
{
    public class Program
    {
        static void Main(string[] args)
        {
            string zadanie = "";
            Console.WriteLine("Введите номер задания");
            zadanie = Console.ReadLine();
            if(zadanie == "1")
            {
                var worldData = DataInitializer.CreateWorldData();

                LinqRequests.RequestWhere(worldData);
                LinqRequests.RequestSets(worldData);
                LinqRequests.RequestAggregation(worldData);
                LinqRequests.RequestGroupBy(worldData);
                LinqRequests.RequestLet(worldData);
                LinqRequests.RequestJoin(worldData);

                PerformanceTester.RunTests(worldData);

                Main(null);
            }
            else
            {

            }
        }
    }
}
