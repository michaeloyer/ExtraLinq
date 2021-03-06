﻿using System;
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
            ArgumentNullCheck(items, nameof(items));

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
            ArgumentNullCheck(objects, nameof(objects));

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
            ArgumentNullCheck(objects, nameof(objects));

            var set = new HashSet<T>(comparer);

            foreach (var obj in objects)
                if (set.Add(obj) && set.Count > 1)
                    return false;

            return true;
        }

        public static IEnumerable<TResult> Permutations<TSource1, TSource2, TResult>(this IEnumerable<TSource1> collection1,
            IEnumerable<TSource2> collection2, Func<TSource1, TSource2, TResult> selector)
        {
            ArgumentNullCheck(collection1, nameof(collection1));
            ArgumentNullCheck(collection2, nameof(collection2));
            ArgumentNullCheck(selector, nameof(selector));

            foreach (var item1 in collection1)
                foreach (var item2 in collection2)
                    yield return selector(item1, item2);
        }

        public static string StringJoin<T>(this IEnumerable<T> collection, string delimiter)
        {
            ArgumentNullCheck(collection, nameof(collection));
            return string.Join(delimiter, collection);
        }

        public static IEnumerable<object> NotOfType<T>(this IEnumerable source)
        {
            ArgumentNullCheck(source, nameof(source));

            foreach (object obj in source)
                if (!(obj is T))
                    yield return obj;
        }

        public static IEnumerable<object> NotOfType<T1, T2>(this IEnumerable source)
        {
            ArgumentNullCheck(source, nameof(source));

            foreach (object obj in source)
                if (!(obj is T1 || obj is T2))
                    yield return obj;
        }

        public static IEnumerable<object> NotOfType<T1, T2, T3>(this IEnumerable source)
        {
            ArgumentNullCheck(source, nameof(source));

            foreach (object obj in source)
                if (!(obj is T1 || obj is T2 || obj is T3))
                    yield return obj;
        }

        public static IEnumerable<object> NotOfType<T1, T2, T3, T4>(this IEnumerable source)
        {
            ArgumentNullCheck(source, nameof(source));

            foreach (object obj in source)
                if (!(obj is T1 || obj is T2 || obj is T3 || obj is T4))
                    yield return obj;
        }

        public static IEnumerable<object> NotOfType<T1, T2, T3, T4, T5>(this IEnumerable source)
        {
            ArgumentNullCheck(source, nameof(source));

            foreach (object obj in source)
                if (!(obj is T1 || obj is T2 || obj is T3 || obj is T4 || obj is T5))
                    yield return obj;
        }

        public static IEnumerable<T> IntersectBy<T, TKey>(
            this IEnumerable<T> source,
            IEnumerable<T> compareWith,
            Func<T, TKey> keySelector) =>
                IntersectBy(source, compareWith, keySelector, keySelector, (source, compareWith) => source, null);

        public static IEnumerable<T> IntersectBy<T, TKey>(
            this IEnumerable<T> source,
            IEnumerable<T> compareWith,
            Func<T, TKey> keySelector,
            IEqualityComparer<TKey> comparer) =>
                IntersectBy(source, compareWith, keySelector, keySelector, (source, compareWith) => source, comparer);

        public static IEnumerable<TSource> IntersectBy<TSource, TCompareWith, TKey>(
            this IEnumerable<TSource> source,
            IEnumerable<TCompareWith> compareWith,
            Func<TSource, TKey> sourceKeySelector,
            Func<TCompareWith, TKey> compareWithKeySelector) =>
                IntersectBy(source, compareWith, sourceKeySelector, compareWithKeySelector, (source, compareWith) => source, null);

        public static IEnumerable<TSource> IntersectBy<TSource, TCompareWith, TKey>(
            this IEnumerable<TSource> source,
            IEnumerable<TCompareWith> compareWith,
            Func<TSource, TKey> sourceKeySelector,
            Func<TCompareWith, TKey> compareWithKeySelector,
            IEqualityComparer<TKey> comparer) =>
                IntersectBy(source, compareWith, sourceKeySelector, compareWithKeySelector, (source, compareWith) => source, comparer);

        public static IEnumerable<TResult> IntersectBy<TSource, TCompareWith, TKey, TResult>(
            this IEnumerable<TSource> source,
            IEnumerable<TCompareWith> compareWith,
            Func<TSource, TKey> sourceKeySelector,
            Func<TCompareWith, TKey> compareWithKeySelector,
            Func<TSource, TCompareWith, TResult> resultSelector) =>
                IntersectBy(source, compareWith, sourceKeySelector, compareWithKeySelector, resultSelector, null);

        public static IEnumerable<TResult> IntersectBy<TSource, TCompareWith, TKey, TResult>(
            this IEnumerable<TSource> source,
            IEnumerable<TCompareWith> compareWith,
            Func<TSource, TKey> sourceKeySelector,
            Func<TCompareWith, TKey> compareWithKeySelector,
            Func<TSource, TCompareWith, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            ArgumentNullCheck(source, nameof(source));
            ArgumentNullCheck(compareWith, nameof(compareWith));
            ArgumentNullCheck(sourceKeySelector, nameof(sourceKeySelector));
            ArgumentNullCheck(compareWithKeySelector, nameof(compareWithKeySelector));
            ArgumentNullCheck(resultSelector, nameof(resultSelector));

            var sourceDict = new Dictionary<TKey, TSource>(comparer);

            foreach (var sourceItem in source)
                sourceDict.TryAdd(sourceKeySelector(sourceItem), sourceItem);

            foreach (var compareWithItem in compareWith)
            {
                TKey key = compareWithKeySelector(compareWithItem);
                if (sourceDict.TryGetValue(key, out var sourceItem))
                {
                    yield return resultSelector(sourceItem, compareWithItem);
                    sourceDict.Remove(key);
                }
            }
        }

        private static void ArgumentNullCheck(object arg, string argName)
        { if (arg == null) throw new ArgumentNullException(argName); }
    }
}
