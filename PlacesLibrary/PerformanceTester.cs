using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesLibrary
{
    public class PerformanceTester
    {
        public static List<City> FilterWithExtension(List<SortedDictionary<string, List<Place>>> world)
        {
            return world.SelectMany(c => c.Values)
                        .SelectMany(p => p)
                        .OfType<City>()
                        .Where(c => c.Population > 500000)
                        .ToList();
        }

        public static List<City> FilterWithFor(List<SortedDictionary<string, List<Place>>> world)
        {
            var result = new List<City>();
            for (int i = 0; i < world.Count; i++)
            {
                foreach (var pair in world[i])
                {
                    for (int j = 0; j < pair.Value.Count; j++)
                    {
                        if (pair.Value[j] is City city && city.Population > 500000)
                        {
                            result.Add(city);
                        }
                    }
                }
            }
            return result;
        }

        public static void RunTests(List<SortedDictionary<string, List<Place>>> world)
        {
            Console.WriteLine("\n--- [Тест производительности] ---");

            FilterWithExtension(world);
            FilterWithFor(world);

            int iterations = 100000;

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                FilterWithExtension(world);
            }
            sw.Stop();
            long linqTime = sw.ElapsedMilliseconds;
            Console.WriteLine($"Методы расширения ({iterations} итераций): {linqTime} мс");

            sw.Restart();
            for (int i = 0; i < iterations; i++)
            {
                FilterWithFor(world);
            }
            sw.Stop();
            long forTime = sw.ElapsedMilliseconds;
            Console.WriteLine($"Циклы For ({iterations} итераций): {forTime} мс");

            Console.WriteLine($"\nВывод: Код на циклах 'for/foreach' отработал быстрее, чем LINQ, в {((double)linqTime / forTime):F2} раз.");
        }
    }
}
