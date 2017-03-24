using System;
using System.Collections.Generic;
using System.Linq;

namespace WallyMathieu
{
    public static class CollectionChangesExtensions
    {
        public static CollectionChanges<T,T> CollectionChanges<T>(this IEnumerable<T> existing, IEnumerable<T> updated)
        {
            return existing.CollectionChanges(updated, Id);
        }

        public static CollectionChanges<T,T> CollectionChanges<T, TKey>(
			this IEnumerable<T> existing, IEnumerable<T> updated, Func<T, TKey> keySelector)
        {
			return CollectionChanges(existing, keySelector, updated, keySelector);
        }

        public static CollectionChanges<TExisting, TUpdated> CollectionChanges<TExisting, TUpdated, TKey>(
            this IEnumerable<TExisting> existing,
            Func<TExisting, TKey> keySelectorExisting,
            IEnumerable<TUpdated> updated,
            Func<TUpdated, TKey> keySelectorUpdated)
        {
            var e = existing.ToArray();
            var u = updated.ToArray();
            var dicE = existing.ToDictionary(keySelectorExisting, Id);
            var dicU = updated.ToDictionary(keySelectorUpdated, Id);

            var setE = new HashSet<TKey>(dicE.Keys);
            var setU = new HashSet<TKey>(dicU.Keys);

            var toAdd = setU.Except(setE).Select(key => dicU[key]);
            var toRemove = setE.Except(setU).Select(key => dicE[key]);
            var toChange = setE.Intersect(setU)
                .Select(key => new CollectionItemsThatMayHaveChanged<TExisting, TUpdated>(
                                    updated: dicU[key],
                                    existing: dicE[key]));

            return new CollectionChanges<TExisting, TUpdated>(toChange: toChange, toBeAdded: toAdd, toBeRemoved: toRemove);
        }

        private static T Id<T>(T val) => val;
    }
}
