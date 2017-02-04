using System;
using System.Collections;
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

        public static bool AllUnique<T>(this IEnumerable<T> objects) =>
            AllUnique(objects, null);

        public static bool AllUnique<T>(this IEnumerable<T> objects, IEqualityComparer<T> comparer)
        {
            ArgumentNullCheck(objects, "objects");

            var set = new HashSet<T>(comparer);

            foreach (var obj in objects)
                if (!set.Add(obj))
                    return false;

            return true;
        }

        public static bool AllDuplicate<T>(this IEnumerable<T> objects) =>
            AllDuplicate(objects, null);

        public static bool AllDuplicate<T>(this IEnumerable<T> objects, IEqualityComparer<T> comparer)
        {
            ArgumentNullCheck(objects, "objects");

            var set = new HashSet<T>(comparer);

            foreach (var obj in objects)
                if (set.Add(obj) && set.Count > 1)
                    return false;

            return true;
        }

        public static IEnumerable<object> NotOfType<T>(this IEnumerable source)
        {
            ArgumentNullCheck(source, "source");

            foreach (object obj in source)
                if (!(obj is T))
                    yield return obj;
        }

        public static IEnumerable<object> NotOfType<T1, T2>(this IEnumerable source)
        {
            ArgumentNullCheck(source, "source");

            foreach (object obj in source)
                if (!(obj is T1 || obj is T2))
                    yield return obj;
        }

        public static IEnumerable<object> NotOfType<T1, T2, T3>(this IEnumerable source)
        {
            ArgumentNullCheck(source, "source");

            foreach (object obj in source)
                if (!(obj is T1 || obj is T2 || obj is T3))
                    yield return obj;
        }

        public static IEnumerable<object> NotOfType<T1, T2, T3, T4>(this IEnumerable source)
        {
            ArgumentNullCheck(source, "source");

            foreach (object obj in source)
                if (!(obj is T1 || obj is T2 || obj is T3 || obj is T4))
                    yield return obj;
        }

        public static IEnumerable<object> NotOfType<T1, T2, T3, T4, T5>(this IEnumerable source)
        {
            ArgumentNullCheck(source, "source");

            foreach (object obj in source)
                if (!(obj is T1 || obj is T2 || obj is T3 || obj is T4 || obj is T5))
                    yield return obj;
        }

        private static void ArgumentNullCheck(object arg, string argName)
        { if (arg == null) throw new ArgumentNullException(argName); }
    }
}
