using System;
using System.Collections.Generic;
using System.Linq;

namespace Carable
{
    public static class CollectionChangesExtensions
    {
        public static CollectionChanges<T> CollectionChanges<T>(this IEnumerable<T> existing, IEnumerable<T> updated)
        {
            return existing.CollectionChanges(updated, Id);
        }

        public static CollectionChanges<T> CollectionChanges<T, TKey>(this IEnumerable<T> existing, IEnumerable<T> updated, Func<T, TKey> getKey)
        {
            var e = existing.ToArray();
            var u = updated.ToArray();
            var dicE = existing.ToDictionary(getKey, Id);
            var dicU = updated.ToDictionary(getKey, Id);

            var setE = new HashSet<TKey>(dicE.Keys);
            var setU = new HashSet<TKey>(dicU.Keys);

            var toAdd = setU.Except(setE).Select(key => dicU[key]);
            var toRemove = setE.Except(setU).Select(key => dicE[key]);
            var toChange = setE.Intersect(setU)
                .Select(key => new CollectionItemsThatMayHaveChanged<T>(
                                    updated: dicU[key],
                                    existing: dicE[key]));

            return new CollectionChanges<T>(toChange: toChange, toBeAdded: toAdd, toBeRemoved: toRemove);
        }

        private static T Id<T>(T val) => val;
    }
}
