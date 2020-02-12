using System;
using System.Collections.Generic;
using System.Linq;

namespace WallyMathieu.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIncomingDifferenceBuilder<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IIncomingDifference<T> Incoming(ISet<T> incoming);
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IIncomingDifferenceBuilder<TKey, TExisting>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IIncomingDifference<TKey, TIncoming, TExisting> Incoming<TIncoming>(IDictionary<TKey, TIncoming> incoming);
    }
    /// <inheritdoc/>
    public class IncomingDifferenceBuilder<TKey, TExisting>: IIncomingDifferenceBuilder<TKey, TExisting>
    {
        private IDictionary<TKey, TExisting> existing;
        /// 
        public IncomingDifferenceBuilder(IDictionary<TKey, TExisting> existing)
        {
            this.existing = existing;
        }
        /// <inheritdoc/>
        public IIncomingDifference<TKey, TIncoming, TExisting> Incoming<TIncoming>(IDictionary<TKey, TIncoming> incoming)
        {
            var setExisting = new HashSet<TKey>(existing.Keys);
            var setIncoming = new HashSet<TKey>(incoming.Keys);

            var toBeAdded = setIncoming.Except(setExisting).Select(key => (key, incoming[key]));
            var toBeDeleted = setExisting.Except(setIncoming).Select(key => (key, existing[key]));
            var intersection =
                setExisting.Intersect(setIncoming)
                .Select(key => new IncomingDifference<TKey, TIncoming, TExisting>.KeyIntersection(key: key, incoming: incoming[key], existing: existing[key]))
                .Cast<IKeyIncomingIntersection<TKey, TIncoming, TExisting>>();

            return new IncomingDifference<TKey, TIncoming, TExisting>(intersection: intersection, toBeAdded: toBeAdded, toBeDeleted: toBeDeleted);
        }
    }
    /// <inheritdoc/>
    public class IncomingDifferenceBuilder<T> : IIncomingDifferenceBuilder<T>
    {
        private ISet<T> existing;
        /// 
        public IncomingDifferenceBuilder(ISet<T> existing)
        {
            this.existing = existing;
        }
        /// <inheritdoc/>
        public IIncomingDifference<T> Incoming(ISet<T> incoming)
        {
            var toBeDeleted = existing.Except(incoming);
            var toBeAdded = incoming.Except(existing);

            return new IncomingDifference<T>(toBeDeleted: toBeDeleted, toBeAdded: toBeAdded);
        }
    }
}
