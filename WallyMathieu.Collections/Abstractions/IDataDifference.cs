using System.Collections.Generic;

namespace WallyMathieu.Collections.Abstractions
{
    /// <summary>
    /// Represents a symmetric difference between two collections where one side repressents deletions and the other additions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataDifference<out T>
    {
        /// <summary>
        /// Items with keys only in the right collection.
        /// </summary>
        IReadOnlyCollection<T> ToBeDeleted { get; }
        /// <summary>
        /// Items with keys only in the left collection.
        /// </summary>
        IReadOnlyCollection<T> ToBeAdded { get; }
    }
    /// <summary>
    /// Represents a symmetric difference between two dictionaries or maps where the values have potentially different type 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TIncoming"></typeparam>
    /// <typeparam name="TExisting"></typeparam>
    public interface IDataDifference<TKey, TIncoming, TExisting>
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
        IReadOnlyCollection<IDataIntersection<TKey, TIncoming, TExisting>> Intersection { get; }
    }
}
