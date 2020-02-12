using System.Collections.Generic;

namespace WallyMathieu.Collections
{
    /// <summary>
    /// Represents a symmetric difference between two dictionaries or maps where the values have potentially different type 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TIncoming"></typeparam>
    /// <typeparam name="TExisting"></typeparam>
    public interface IIncomingDifference<TKey, TIncoming, TExisting>
    {
        /// <summary>
        /// Items with keys only in the right collection.
        /// </summary>
        IReadOnlyCollection<(TKey, TExisting)> ToBeDeleted { get; }
        /// <summary>
        /// Items with keys only in the left collection.
        /// </summary>
        IReadOnlyCollection<(TKey, TIncoming)> ToBeAdded { get; }
        /// <summary>
        /// Items with same keys only both collections.
        /// </summary>
        IReadOnlyCollection<IKeyIncomingIntersection<TKey, TIncoming, TExisting>> Intersection { get; }
    }
}
