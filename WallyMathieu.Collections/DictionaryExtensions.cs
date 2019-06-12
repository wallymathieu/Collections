using System;
using System.Collections.Generic;
using System.Linq;

namespace WallyMathieu.Collections
{
    public static class DictionaryExtensions
    {
        public static DictionaryComparison<TLeft, TRight> Compare<TLeft, TRight, TKey>(
            this IDictionary<TKey,TLeft> left,
            IDictionary<TKey, TRight> right)
        {
            var setLeft = new HashSet<TKey>(left.Keys);
            var setRight = new HashSet<TKey>(right.Keys);

            var plus = setRight.Except(setLeft).Select(key => right[key]);
            var minus = setLeft.Except(setRight).Select(key => left[key]);
            var intersection = 
                setLeft.Intersect(setRight)
                .Select(key => new DictionaryComparison<TLeft, TRight>.ValueIntersection(right: right[key], left: left[key]));

            return new DictionaryComparison<TLeft, TRight>(Intersection: intersection, Plus: plus, Minus: minus);
        }
    }
}
