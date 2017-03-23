using System;
using System.Collections.Generic;
using System.Linq;

namespace WhatIsUpdated
{
    public class Diff
    {
        public static Updated<T> Changed<T, TKey>(Func<T, TKey> getKey, IEnumerable<T> existing, IEnumerable<T> updated)
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
                .Select(key => new MaybeChanged<T>(
                                    updated: dicU[key],
                                    existing: dicE[key]));

            return new Updated<T>(toChange: toChange, toBeAdded: toAdd, toBeRemoved: toRemove);
        }

        private static T Id<T>(T val) => val;
    }
}
