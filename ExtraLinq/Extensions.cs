using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static class Extensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> values) =>
            new HashSet<T>(values);

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> values, IEqualityComparer<T> comparer) =>
            new HashSet<T>(values, comparer);

        public static Queue<T> ToQueue<T>(this IEnumerable<T> values) =>
            new Queue<T>(values);

        public static Stack<T> ToStack<T>(this IEnumerable<T> values) =>
            new Stack<T>(values);

        public static IEnumerable<T> ToCycle<T>(this IEnumerable<T> items)
        {
            ArgumentNullCheck(items, "items");

            var enumerator = items.GetEnumerator();

            while (true)
                if (enumerator.MoveNext())
                    yield return enumerator.Current;
                else
                    enumerator.Reset();
        }
        private static void ArgumentNullCheck(object arg, string argName)
        { if (arg == null) throw new ArgumentNullException(argName); }
    }
}
