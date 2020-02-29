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

    }
}
