using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Expressions
{
    public static class Extensions
    {
        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach(T item in collection)
            {
                if (predicate(item))
                    yield return item;
            }
        }

        public static IEnumerable<T> Every<T>(this IEnumerable<T> collection, int step)
        {
            var sourceEnumerator = collection.GetEnumerator();
            int i = 0;
            do
            {
                i = step;
                while (i > 0 && sourceEnumerator.MoveNext())
                {
                    i--;
                }
                if (i == 0)
                {
                    yield return sourceEnumerator.Current;
                }
            }
            while (i == 0);
        }
    }
}
