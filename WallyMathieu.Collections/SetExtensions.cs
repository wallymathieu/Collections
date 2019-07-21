using System;
using System.Collections.Generic;
using System.Linq;

namespace WallyMathieu.Collections
{
    /// <summary>
    /// Extensions on sets.
    /// </summary>
    public static class SetExtensions
    {
        /// <summary>
        /// Compare two sets.
        /// </summary>
        public static ISymmetricDifference<T> SymmetricDiff<T>(this ISet<T> right, ISet<T> left) 
            where T: struct, IEquatable<T>
        {
            var onlyInRight = right.Except(left);
            var onlyInLeft = left.Except(right);

            return new SymmetricDifference<T>(onlyInRight, onlyInLeft);
        }
    }
   
}
