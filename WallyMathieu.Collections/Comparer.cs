using System;
using System.Collections.Generic;

namespace WallyMathieu.Collections
{
    /// <summary>
    /// Class that holds methods related to comparers
    /// </summary>
    public static class Comparer
    {
        /// <summary>
        /// Create a <see cref="System.Collections.Generic.IComparer{T}"/> from a comparer function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IComparer<T> Create<T>(Func<T, T, int> comparer) => new FunctionComparerAdapter<T>(comparer);
        /// <summary>
        /// Create a <see cref="System.Collections.Generic.IComparer{T}"/> from a selector function, using the default comparer for the selected values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TComparable"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IComparer<T> Create<T, TComparable>(Func<T, TComparable> selector)
            where TComparable : IComparable => new SelectComparer<T, TComparable>(selector);
        private class SelectComparer<T, TComparable> : IComparer<T> where TComparable : IComparable
        {
            private readonly Func<T, TComparable> _selector;
            private readonly IComparer<TComparable> _comparer = Comparer<TComparable>.Default;

            public SelectComparer(Func<T, TComparable> selector) => _selector = selector;

            public int Compare(T x, T y) => _comparer.Compare(_selector(x), _selector(y));
        }
        /// <summary>
        /// An adapter class to let a compare func act as a <see cref="System.Collections.Generic.IComparer{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class FunctionComparerAdapter<T> : IComparer<T>
        {
            private readonly Func<T, T, int> _compare;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="compare"></param>
            public FunctionComparerAdapter(Func<T, T, int> compare) => _compare = compare;
            /// <summary>
            /// Compares two objects and returns a value indicating the ordering between the two objects.
            /// </summary>
            public int Compare(T x, T y) => _compare(x, y);
        }
    }
}
