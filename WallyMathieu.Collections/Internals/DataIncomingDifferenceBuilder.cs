using System.Collections.Generic;
using System.Linq;
using WallyMathieu.Collections.Abstractions;

namespace WallyMathieu.Collections.Internals
{
    /// <inheritdoc/>
    class DataIncomingDifferenceBuilder<T> : IDataIncomingDifferenceBuilder<T>
    {
        private ISet<T> existing;
        /// 
        public DataIncomingDifferenceBuilder(ISet<T> existing)
        {
            this.existing = existing;
        }
        /// <inheritdoc/>
        public IDataDifference<T> Incoming(ISet<T> incoming)
        {
            var toBeDeleted = existing.Except(incoming);
            var toBeAdded = incoming.Except(existing);

            return new DataDifference<T>(toBeDeleted: toBeDeleted, toBeAdded: toBeAdded);
        }
    }

    /// <inheritdoc/>
    class DataIncomingDifferenceBuilder<TKey, TExisting> : IDataIncomingDifferenceBuilder<TKey, TExisting>
    {
        private IDictionary<TKey, TExisting> existing;
        /// 
        public DataIncomingDifferenceBuilder(IDictionary<TKey, TExisting> existing)
        {
            this.existing = existing;
        }
        /// <inheritdoc/>
        public IDataDifference<TKey, TIncoming, TExisting> Incoming<TIncoming>(IDictionary<TKey, TIncoming> incoming)
        {
            var setExisting = new HashSet<TKey>(existing.Keys);
            var setIncoming = new HashSet<TKey>(incoming.Keys);

            var toBeAdded = setIncoming.Except(setExisting).Select(key => (key, incoming[key]));
            var toBeDeleted = setExisting.Except(setIncoming).Select(key => (key, existing[key]));
            var intersection =
                setExisting.Intersect(setIncoming)
                .Select(key => new DataDifference<TKey, TIncoming, TExisting>.KeyIntersection(key: key, incoming: incoming[key], existing: existing[key]))
                .Cast<IDataIntersection<TKey, TIncoming, TExisting>>();

            return new DataDifference<TKey, TIncoming, TExisting>(intersection: intersection, toBeAdded: toBeAdded, toBeDeleted: toBeDeleted);
        }
    }
}
