using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesLibrary
{
    public class DataInitializer
    {
        public static List<SortedDictionary<string, List<Place>>> CreateWorldData()
        {
            var continents = new List<SortedDictionary<string, List<Place>>>();

            var eurasia = new SortedDictionary<string, List<Place>>();
            eurasia.Add("Россия", new List<Place>
            {
                new Megapolis("Москва", "Center1", 77, 13000000, 2561.5, true),
                new Megapolis("Пермь", "Center2", 59, 1000000, 799.6, false),
                new City("Кунгур", "Center3", 59, 64000, 68.7)
            });
            eurasia.Add("Германия", new List<Place>
            {
                new Megapolis("Берлин", "Center4", 11, 3700000, 891.8, true),
                new City("Потсдам", "Center5", 11, 180000, 187.3)
            });

            var northAmerica = new SortedDictionary<string, List<Place>>();
            northAmerica.Add("США", new List<Place>
            {
                new Megapolis("Нью-Йорк", "Center6", 36, 8300000, 783.8, true),
                new City("Олбани", "Center7", 36, 99000, 55.4)
            });
            northAmerica.Add("Канада", new List<Place>
            {
                new Megapolis("Торонто", "Center8", 42, 2900000, 630.2, true)
            });

            continents.Add(eurasia);
            continents.Add(northAmerica);

            return continents;
        }




        public static List<RegionInfo> GetRegionDirectory()
        {
            return new List<RegionInfo>
            {
                new RegionInfo { Id = 77, RegionName = "Московский регион" },
                new RegionInfo { Id = 59, RegionName = "Пермский край" },
                new RegionInfo { Id = 36, RegionName = "Штат Нью-Йорк" },
                new RegionInfo { Id = 11, RegionName = "Земля Бранденбург" }
            };
        }
    }

    public class RegionInfo
    {
        public int Id { get; set; }
        public string RegionName { get; set; }
    }
}
