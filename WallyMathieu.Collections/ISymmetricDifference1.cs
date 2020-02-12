using System.Collections.Generic;

namespace WallyMathieu.Collections
{
    /// <summary>
    /// Represents a symmetric difference between two collections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISymmetricDifference<out T>
    {
        /// <summary>
        /// Items with keys only in the right collection.
        /// </summary>
        IReadOnlyCollection<T> OnlyInRight { get; }
        /// <summary>
        /// Items with keys only in the left collection.
        /// </summary>
        IReadOnlyCollection<T> OnlyInLeft { get; }        
    }
}
