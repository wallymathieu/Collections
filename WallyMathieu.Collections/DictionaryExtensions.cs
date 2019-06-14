using System;
using System.Collections.Generic;
using System.Linq;

namespace WallyMathieu.Collections
{
    /// <summary>
    /// 
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
        public static DictionaryComparison<TLeft, TRight> Compare<TLeft, TRight, TKey>(
            this IDictionary<TKey,TLeft> left,
            IDictionary<TKey, TRight> right)
        {
            var setLeft = new HashSet<TKey>(left.Keys);
            var setRight = new HashSet<TKey>(right.Keys);

            var onlyInRight = setRight.Except(setLeft).Select(key => right[key]);
            var onlyInLeft = setLeft.Except(setRight).Select(key => left[key]);
            var intersection = 
                setLeft.Intersect(setRight)
                .Select(key => new DictionaryComparison<TLeft, TRight>.ValueIntersection(right: right[key], left: left[key]));

            return new DictionaryComparison<TLeft, TRight>(Intersection: intersection, onlyInRight: onlyInRight, onlyInLeft: onlyInLeft);
        }
    }
}
