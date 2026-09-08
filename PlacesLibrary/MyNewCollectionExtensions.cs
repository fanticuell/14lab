using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesLibrary
{
    public static class MyNewCollectionExtensions
    {
        public static IEnumerable<Place> Filter(this MyNewCollection collection, Func<Place, bool> predicate)
        {
            if (collection == null || predicate == null) throw new ArgumentNullException();
            var result = new List<Place>();
            foreach (var item in collection)
            {
                if (item is Place p && predicate(p)) result.Add(p);
            }
            return result;
        }

        public static int SumOf(this MyNewCollection collection, Func<Place, int> selector)
        {
            if (collection == null || selector == null) throw new ArgumentNullException();
            int sum = 0;
            foreach (var item in collection)
            {
                if (item is Place p) sum += selector(p);
            }
            return sum;
        }

        public static IEnumerable<Place> SortBy(this MyNewCollection collection, Func<Place, string> keySelector, bool ascending = true)
        {
            if (collection == null || keySelector == null) throw new ArgumentNullException();
            var list = new List<Place>();
            foreach (var item in collection)
            {
                if (item is Place p) list.Add(p);
            }
            return ascending ? list.OrderBy(keySelector) : list.OrderByDescending(keySelector);
        }

        public static IEnumerable<IGrouping<string, Place>> GroupByCriterion(this MyNewCollection collection, Func<Place, string> keySelector)
        {
            if (collection == null || keySelector == null) throw new ArgumentNullException();
            var list = new List<Place>();
            foreach (var item in collection)
            {
                if (item is Place p) list.Add(p);
            }
            return list.GroupBy(keySelector);
        }
    }
}
