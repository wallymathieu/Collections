using System.Collections.Generic;

namespace WallyMathieu.Collections
{
    /// <summary>
    /// Represents a symmetric difference between two collections where one side repressents deletions and the other additions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIncomingDifference<out T>
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
}
