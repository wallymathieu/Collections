using System;
using System.Collections.Generic;
using System.Linq;

namespace WallyMathieu.Collections
{
    /// <summary>
    /// Extension methods on IDictionary
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Compare two dictionaries with potentially different values for key differences.
        /// </summary>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="TRight"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>A comparison result</returns>
        public static ISymmetricDifference<TKey, TLeft, TRight> SymmetricDiff<TKey, TLeft, TRight>(
            this IDictionary<TKey,TLeft> left,
            IDictionary<TKey, TRight> right)
        {
            var setLeft = new HashSet<TKey>(left.Keys);
            var setRight = new HashSet<TKey>(right.Keys);

            var onlyInRight = setRight.Except(setLeft).Select(key => (key,right[key]));
            var onlyInLeft = setLeft.Except(setRight).Select(key => (key,left[key]));
            var intersection = 
                setLeft.Intersect(setRight)
                .Select(key => new SymmetricDifference<TKey,TLeft,TRight>.KeyIntersection(key:key, right: right[key], left: left[key]))
                .Cast<IKeyIntersection<TKey,TLeft,TRight>>();

            return new SymmetricDifference<TKey, TLeft, TRight>(intersection: intersection, onlyInRight: onlyInRight, onlyInLeft: onlyInLeft);
        }
    }
}
