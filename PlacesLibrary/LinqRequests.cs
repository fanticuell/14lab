using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PlacesLibrary
{
    public class LinqRequests
    {
        private static IEnumerable<City> GetAllCities(List<SortedDictionary<string, List<Place>>> world)
        {
            return world.SelectMany(continent => continent.Values)
                        .SelectMany(places => places)
                        .OfType<City>();
        }

        public static void RequestWhere(List<SortedDictionary<string, List<Place>>> world)
        {
            Console.WriteLine("\n--- [Where] Крупные города (> 1 млн) ---");

            var query = from c in GetAllCities(world)
                        where c.Population > 1000000
                        select c;

            var method = GetAllCities(world).Where(c => c.Population > 1000000);

            foreach (var c in method) c.Show();
        }

        public static void RequestSets(List<SortedDictionary<string, List<Place>>> world)
        {
            Console.WriteLine("\n--- [Множества] Разность списков (Except) ---");
            var allCities = GetAllCities(world);
            var megapolises = world.SelectMany(c => c.Values).SelectMany(p => p).OfType<Megapolis>();

            var queryExcept = (from c in allCities select c).Except(megapolises);

            var methodExcept = allCities.Except(megapolises);

            Console.WriteLine("Обычные города (Исключили мегаполисы):");
            foreach (var c in methodExcept) Console.WriteLine(c.Name);
        }

        public static void RequestAggregation(List<SortedDictionary<string, List<Place>>> world)
        {
            Console.WriteLine("\n--- [Агрегирование] Статистика населения ---");

            int totalSumQuery = (from c in GetAllCities(world) select c.Population).Sum();
            double avgAreaQuery = (from c in GetAllCities(world) select c.Area).Average();

            int totalSumMethod = GetAllCities(world).Sum(c => c.Population);
            double avgAreaMethod = GetAllCities(world).Average(c => c.Area);
            int maxPop = GetAllCities(world).Max(c => c.Population);

            Console.WriteLine($"Всего населения: {totalSumMethod} чел.");
            Console.WriteLine($"Средняя площадь города: {avgAreaMethod:F2} кв.км");
            Console.WriteLine($"Макс. население: {maxPop} чел.");
        }

        public static void RequestGroupBy(List<SortedDictionary<string, List<Place>>> world)
        {
            Console.WriteLine("\n--- [Group By] Группировка по ID региона ---");

            var query = from c in GetAllCities(world)
                        group c by c.RegionId into g
                        select g;

            var method = GetAllCities(world).GroupBy(c => c.RegionId);

            foreach (var group in method)
            {
                Console.WriteLine($"Регион ID: {group.Key}");
                foreach (var city in group) Console.WriteLine($"  - {city.Name}");
            }
        }

        public static void RequestLet(List<SortedDictionary<string, List<Place>>> world)
        {
            Console.WriteLine("\n--- [Let / Свой тип] Плотность населения ---");

            var query = from c in GetAllCities(world)
                        let density = c.Population / c.Area
                        select new { CityName = c.Name, Density = density };

            var method = GetAllCities(world).Select(c => new {
                City = c,
                Density = c.Population / c.Area
            }).Select(x => new { CityName = x.City.Name, Density = x.Density });

            foreach (var item in method)
            {
                Console.WriteLine($"Город: {item.CityName}, Плотность: {item.Density:F1} чел/кв.км");
            }
        }

        public static void RequestJoin(List<SortedDictionary<string, List<Place>>> world)
        {
            Console.WriteLine("\n--- [Join] Соединение со справочником регионов ---");
            var directory = DataInitializer.GetRegionDirectory();

            var query = from c in GetAllCities(world)
                        join r in directory on c.RegionId equals r.Id
                        select new { CityName = c.Name, RegionName = r.RegionName };

            var method = GetAllCities(world).Join(
                directory,
                city => city.RegionId,
                reg => reg.Id,
                (city, reg) => new { CityName = city.Name, RegionName = reg.RegionName }
            );

            foreach (var item in method)
            {
                Console.WriteLine($"Город: {item.CityName} -> {item.RegionName}");
            }
        }
    }
}
