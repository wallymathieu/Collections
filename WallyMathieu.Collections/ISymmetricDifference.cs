using System.Collections.Generic;

namespace WallyMathieu.Collections
{
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
