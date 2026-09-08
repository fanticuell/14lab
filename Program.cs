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
                MyNewCollection customCollection = new MyNewCollection("Студенческая Коллекция Городов");

                customCollection.CollectionCountChanged += (s, e) =>
                    Console.WriteLine($"[Событие Count]: {e.ChangeType} -> {((Place)e.ChangedItem).Name}");

                object[] initialData = new object[]
                {
                    new Place("Центральный Парк"),
                    new City("Кунгур", "Center1", 59, 64000, 68.7),
                    new Megapolis("Москва", "Center2", 77, 13000000, 2561.5, true),
                    new Megapolis("Пермь", "Center3", 59, 1000000, 799.6, false),
                    new City("Потсдам", "Center4", 11, 180000, 187.3)
                };

                Console.WriteLine("--- Наполнение коллекции ---");
                customCollection.Add(initialData);
                Console.WriteLine($"\nКоллекция успешно создана. Всего элементов: {customCollection.Count}\n");

                Console.WriteLine("--- Тест А) Выборка (Только города-миллионники) ---");
                var millionCities = customCollection.Filter(p => p is City c && c.Population >= 1000000);
                foreach (var place in millionCities)
                {
                    place.Show();
                }

                Console.WriteLine("\n--- Тест Б) Агрегирование (Подсчет суммарного населения) ---");
                int totalPopulation = customCollection.SumOf(p => p is City c ? c.Population : 0);
                Console.WriteLine($"Общее население в коллекции: {totalPopulation} человек.");

                Console.WriteLine("\n--- Тест В) Сортировка по имени (По убыванию Я-А) ---");
                var sortedData = customCollection.SortBy(p => p.Name, ascending: false);
                foreach (var place in sortedData)
                {
                    Console.WriteLine($"- {place.Name}");
                }

                Console.WriteLine("\n--- Тест Г) Группировка по типу объектов ---");
                var groups = customCollection.GroupByCriterion(p => p.GetType().Name);
                foreach (var group in groups)
                {
                    Console.WriteLine($"Группа класса: [{group.Key}]");
                    foreach (var item in group)
                    {
                        Console.WriteLine($"  * {item.Name}");
                    }
                }

                Console.WriteLine("\nВсе тесты завершены успешно.");
                Console.ReadLine();
            }
        }
    }
}
