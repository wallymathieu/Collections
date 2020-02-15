using System.Collections.Generic;
//NOTE: to be changed to abstractions in 2.0 release
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
    /// <summary>
    /// Represents a symmetric difference between two dictionaries or maps where the values have potentially different type 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    public interface ISymmetricDifference<TKey, TLeft, TRight>
    {
        /// <summary>
        /// Items with keys only in the right collection.
        /// </summary>
        IReadOnlyCollection<(TKey,TRight)> OnlyInRight { get; }
        /// <summary>
        /// Items with keys only in the left collection.
        /// </summary>
        IReadOnlyCollection<(TKey,TLeft)> OnlyInLeft { get; }
        /// <summary>
        /// Items with same keys only both collections.
        /// </summary>
        IReadOnlyCollection<IKeyIntersection<TKey, TLeft, TRight>> Intersection { get; }
    }
}
