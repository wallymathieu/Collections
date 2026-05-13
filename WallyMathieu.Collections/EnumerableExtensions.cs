using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WallyMathieu.Collections
{
    /// <summary>
    /// Collection extensions
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns sub lists of at most 'count' elements from the IEnumerable
        /// </summary>
        /// <returns>An IEnumerable of IEnumerable with Count less than 'count'</returns>
        /// <param name="enumerable"></param>
        /// <param name="count">The number of elements that should be at most found in each "batch".</param>
        public static IEnumerable<IEnumerable<T>> BatchesOf<T>(this IEnumerable<T> enumerable, int count)
        {
            using (var enumerator = enumerable.GetEnumerator())
            {
                while (true)
                {
                    var list = new List<T>(count);
                    for (int i = 0; i < count && enumerator.MoveNext(); i++)
                    {
                        list.Add(enumerator.Current);
                    }

                    if (!list.Any())
                    {
                        break;
                    }

                    yield return list;
                }
            }
        }

        private class Chunks<TKey, T> : IGrouping<TKey, T>
        {
            public Chunks(TKey key, T firstItem)
            {
                Key = key;
                Enumerable = new List<T>() { { firstItem } };
            }

            public readonly IList<T> Enumerable;
            public IEnumerator<T> GetEnumerator() => Enumerable.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => Enumerable.GetEnumerator();
            public TKey Key{ get; }
        }
        /// <summary>
        /// Enumerates over the items, chunking them together based on the return value of the block.
        ///
        /// Consecutive elements which return the same block value are chunked together.
        ///
        /// Compare this to GroupBy <see cref="Enumerable.GroupBy{TSource,TKey}(System.Collections.Generic.IEnumerable{TSource},System.Func{TSource,TKey})"/> 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="keySelector"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<IGrouping<TKey, T>> Chunk<TKey, T>(this IEnumerable<T> collection, Func<T, TKey> keySelector)
        {
            Chunks<TKey, T> currentChunk = null;
            foreach (var item in collection)
            {
                var currentKey = keySelector(item);
                if (null == currentKey)
                {
                    continue;
                }

                if (currentChunk == null)// first element
                {
                    currentChunk = new Chunks<TKey, T>(currentKey, item);
                }
                else
                {
                    if (currentChunk.Key.Equals(currentKey))
                    {
                        currentChunk.Enumerable.Add(item);
                    }
                    else
                    {
                        yield return currentChunk;
                        currentChunk = new Chunks<TKey, T>(currentKey, item);
                    }
                }
            }
            yield return currentChunk;
        }

        /// <summary>
        /// Enumerates over consecutive items, grouping adjacent elements that produce the same key.
        /// </summary>
        /// <param name="collection">The sequence to chunk.</param>
        /// <param name="keySelector">A function that computes the comparison key for each element.</param>
        /// <typeparam name="TKey">The type of the key used to split consecutive elements.</typeparam>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <returns>A sequence of adjacent groups that share the same projected key.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> or <paramref name="keySelector"/> is <see langword="null"/>.</exception>
        public static IEnumerable<IGrouping<TKey, T>> ChunkBy<TKey, T>(this IEnumerable<T> collection, Func<T, TKey> keySelector)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return ChunkByIterator(collection, keySelector);
        }

        private static IEnumerable<IGrouping<TKey, T>> ChunkByIterator<TKey, T>(IEnumerable<T> collection, Func<T, TKey> keySelector)
        {
            using (var enumerator = collection.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    yield break;
                }

                var comparer = EqualityComparer<TKey>.Default;
                var currentKey = keySelector(enumerator.Current);
                var currentChunk = new Chunks<TKey, T>(currentKey, enumerator.Current);

                while (enumerator.MoveNext())
                {
                    var key = keySelector(enumerator.Current);
                    if (comparer.Equals(currentKey, key))
                    {
                        currentChunk.Enumerable.Add(enumerator.Current);
                    }
                    else
                    {
                        yield return currentChunk;
                        currentKey = key;
                        currentChunk = new Chunks<TKey, T>(key, enumerator.Current);
                    }
                }

                yield return currentChunk;
            }
        }

        /// <summary>
        /// Used to iterate over collection and get the collection elements pairwise.
        /// </summary>
        /// <remarks>
        /// Note that the same element will at most 2 times. For example for
        /// 0.To(3).Pairwise().ToArray() you will get new[] { (0, 1), (1, 2), (2, 3) }
        /// </remarks>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<(T,T)> Pairwise<T>(
            this IEnumerable<T> collection)
        {
            using (var enumerator = collection.GetEnumerator())
            {

                if (!enumerator.MoveNext())
                {
                    yield break;
                }

                var last = enumerator.Current;
                for (; enumerator.MoveNext();)
                {
                    yield return (last, enumerator.Current);
                    last = enumerator.Current;
                }
            }
        }
    }
}
