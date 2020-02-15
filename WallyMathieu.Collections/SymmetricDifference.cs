using System;
using System.Collections.Generic;
using System.Linq;

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
    /// <summary>
    /// Intersection values where the keys match 
    /// </summary>
    public interface IKeyIntersection<out TKey, out TLeft, out TRight>
    {
        /// <summary>
        /// The matching key
        /// </summary>
        TKey Key { get; }
        /// <summary>
        /// Right element with matching key
        /// </summary>
        TRight Right { get; }
        /// <summary>
        /// Left element with matching key
        /// </summary>
        TLeft Left { get; }
    }
    /// <summary>
    /// Symmetric difference between two dictionary or map collections
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    public struct SymmetricDifference<TKey, TLeft, TRight> : 
        IEquatable<SymmetricDifference<TKey, TLeft, TRight>>,
        ISymmetricDifference<TKey,TLeft, TRight>
    {
        ///
        public SymmetricDifference(
            IEnumerable<(TKey,TLeft)> onlyInLeft, 
            IEnumerable<(TKey,TRight)> onlyInRight, 
            IEnumerable<IKeyIntersection<TKey,TLeft,TRight>> intersection)
        {
            this.OnlyInRight = onlyInRight.ToArray();
            this.OnlyInLeft = onlyInLeft.ToArray();
            this.Intersection = intersection.ToArray();
        }
        /// <summary>
        /// Items with keys only in the right collection.
        /// </summary>
        public IReadOnlyCollection<(TKey,TRight)> OnlyInRight { get; }
        /// <summary>
        /// Items with keys only in the left collection.
        /// </summary>
        public IReadOnlyCollection<(TKey,TLeft)> OnlyInLeft { get; }
        /// <summary>
        /// Items with same keys only both collections.
        /// </summary>
        public IReadOnlyCollection<IKeyIntersection<TKey, TLeft, TRight>> Intersection { get; }
        /// <summary>
        /// String representation of difference
        /// </summary>
        public override string ToString() => $"+: {Format(OnlyInRight)}, -: {Format(OnlyInLeft)}, =: {Format(Intersection)}";

        private string Format<T>(IEnumerable<T> collection) => string.Join(",", collection);
        /// <summary>
        /// Determines if the specified object is equal to the other object
        /// </summary>
        public override bool Equals(object obj) => obj is SymmetricDifference<TKey, TLeft, TRight> diff && Equals(diff);
        /// <summary>
        /// Get hash value of difference
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ OnlyInRight.ToArray().GetHashCode();
                result = (result * 397) ^ OnlyInLeft.ToArray().GetHashCode();
                result = (result * 397) ^ Intersection.ToArray().GetHashCode();
                return result;
            }
        }
        /// <summary>
        /// Determines if the specified object is equal to the other object
        /// </summary>
        public bool Equals(SymmetricDifference<TKey, TLeft, TRight> other) =>
                OnlyInRight.SequenceEqual(other.OnlyInRight)
                && OnlyInLeft.SequenceEqual(other.OnlyInLeft)
                && Intersection.SequenceEqual(other.Intersection);

        /// <summary>
        /// Intersection values with the common key.
        /// </summary>
        public struct KeyIntersection : 
            IEquatable<KeyIntersection>,
            IKeyIntersection<TKey, TLeft, TRight>
        {
            /// <summary>
            /// The common key
            /// </summary>
            public TKey Key { get; }
            /// <summary>
            /// Right element with matching key
            /// </summary>
            public TRight Right { get; }
            /// <summary>
            /// Left element with matching key
            /// </summary>
            public TLeft Left { get; }
            /// <summary>
            /// 
            /// </summary>
            public KeyIntersection(TKey key, TLeft left, TRight right)
            {
                Key = key;
                Right = right;
                Left = left;
            }
            /// <summary>
            /// String representation of intersection value
            /// </summary>
            public override string ToString() => $"({Key}): {Left} ~ {Right}";
            /// <summary>
            /// Determines if the specified object is equal to the other object
            /// </summary>
            public override bool Equals(object obj) => obj is KeyIntersection i && Equals(i);

            /// <summary>
            /// Get hash value for element
            /// </summary>
            public override int GetHashCode()
            {
                unchecked
                {
                    var result = 0;
                    result = (result * 397) ^ Key.GetHashCode();
                    result = (result * 397) ^ Right.GetHashCode();
                    result = (result * 397) ^ Left.GetHashCode();
                    return result;
                }
            }
            /// <summary>
            /// Determines if the specified object is equal to the other object
            /// </summary>
            public bool Equals(KeyIntersection other) =>
                    Key.Equals(other.Key)
                    && Right.Equals(other.Right)
                    && Left.Equals(other.Left);
            /// <summary>
            /// Determines if the specified objects are equal
            /// </summary>
            public static bool operator ==(KeyIntersection left, KeyIntersection right)
            {
                return left.Equals(right);
            }
            /// <summary>
            /// Determines if the specified objects are not equal
            /// </summary>
            public static bool operator !=(KeyIntersection left, KeyIntersection right)
            {
                return !(left == right);
            }
        }
        /// <summary>
        /// Determines if the specified objects are equal
        /// </summary>
        public static bool operator ==(SymmetricDifference<TKey, TLeft, TRight> left, SymmetricDifference<TKey, TLeft, TRight> right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// Determines if the specified objects are not equal
        /// </summary>
        public static bool operator !=(SymmetricDifference<TKey, TLeft, TRight> left, SymmetricDifference<TKey, TLeft, TRight> right)
        {
            return !(left == right);
        }
    }
    /// <summary>
    /// Symmetric difference between two sets or set like collections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct SymmetricDifference<T> : 
        IEquatable<SymmetricDifference<T>>,
        ISymmetricDifference<T>
    {
        ///
        public SymmetricDifference(
            IEnumerable<T> onlyInLeft, 
            IEnumerable<T> onlyInRight)
        {
            this.OnlyInRight = onlyInRight.ToArray();
            this.OnlyInLeft = onlyInLeft.ToArray();
        }
        /// <summary>
        /// Items only in the right collection.
        /// </summary>
        public IReadOnlyCollection<T> OnlyInRight { get; }
        /// <summary>
        /// Items only in the left collection.
        /// </summary>
        public IReadOnlyCollection<T> OnlyInLeft { get; }
        /// <summary>
        /// String representation of difference
        /// </summary>
        public override string ToString() => $"+: {Format(OnlyInRight)}, -: {Format(OnlyInLeft)}";

        private string Format(IEnumerable<T> collection) => string.Join(",", collection);
        /// <summary>
        /// Determines if the specified object is equal to the other object
        /// </summary>
        public override bool Equals(object obj) => obj is SymmetricDifference<T> diff && Equals(diff);
        /// <summary>
        /// Get hash value for element
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ OnlyInRight.ToArray().GetHashCode();
                result = (result * 397) ^ OnlyInLeft.ToArray().GetHashCode();
                return result;
            }
        }
        /// <summary>
        /// Determines if the specified object is equal to the other object
        /// </summary>
        public bool Equals(SymmetricDifference<T> other) =>
                OnlyInRight.SequenceEqual(other.OnlyInRight)
                && OnlyInLeft.SequenceEqual(other.OnlyInLeft);

        /// <summary>
        /// Determines if the specified objects are equal
        /// </summary>
        public static bool operator ==(SymmetricDifference<T> left, SymmetricDifference<T> right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// Determines if the specified objects are not equal
        /// </summary>
        public static bool operator !=(SymmetricDifference<T> left, SymmetricDifference<T> right)
        {
            return !(left == right);
        }
    }
}
