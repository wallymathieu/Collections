using System;
using System.Collections.Generic;
using System.Linq;
using WallyMathieu.Collections.Abstractions;

namespace WallyMathieu.Collections.Internals
{
    /// <summary>
    /// Symmetric difference between two dictionary or map collections
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TExisting"></typeparam>
    /// <typeparam name="TIncoming"></typeparam>
    public struct DataDifference<TKey, TIncoming, TExisting> :
        IEquatable<DataDifference<TKey, TIncoming, TExisting>>,
        IDataDifference<TKey, TIncoming, TExisting>
    {
        ///
        public DataDifference(
            IEnumerable<(TKey, TExisting)> toBeDeleted,
            IEnumerable<(TKey, TIncoming)> toBeAdded,
            IEnumerable<IDataIntersection<TKey, TIncoming, TExisting>> intersection)
        {
            this.ToBeDeleted = toBeDeleted.ToArray();
            this.ToBeAdded = toBeAdded.ToArray();
            this.Intersection = intersection.ToArray();
        }
        /// <summary>
        /// Items with keys only in the right collection.
        /// </summary>
        public IReadOnlyCollection<(TKey, TIncoming)> ToBeAdded { get; }
        /// <summary>
        /// Items with keys only in the left collection.
        /// </summary>
        public IReadOnlyCollection<(TKey, TExisting)> ToBeDeleted { get; }
        /// <summary>
        /// Items with same keys only both collections.
        /// </summary>
        public IReadOnlyCollection<IDataIntersection<TKey, TIncoming, TExisting>> Intersection { get; }
        /// <summary>
        /// String representation of difference
        /// </summary>
        public override string ToString() => $"+: {Format(ToBeAdded)}, -: {Format(ToBeDeleted)}, =: {Format(Intersection)}";

        private string Format<T>(IEnumerable<T> collection) => string.Join(",", collection);
        /// <summary>
        /// Determines if the specified object is equal to the other object
        /// </summary>
        public override bool Equals(object obj) => obj is DataDifference<TKey, TIncoming, TExisting> diff && Equals(diff);
        /// <summary>
        /// Get hash value of difference
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ ToBeAdded.ToArray().GetHashCode();
                result = (result * 397) ^ ToBeDeleted.ToArray().GetHashCode();
                result = (result * 397) ^ Intersection.ToArray().GetHashCode();
                return result;
            }
        }
        /// <summary>
        /// Determines if the specified object is equal to the other object
        /// </summary>
        public bool Equals(DataDifference<TKey, TIncoming,TExisting> other) =>
                ToBeAdded.SequenceEqual(other.ToBeAdded)
                && ToBeDeleted.SequenceEqual(other.ToBeDeleted)
                && Intersection.SequenceEqual(other.Intersection);

        /// <summary>
        /// Intersection values with the common key.
        /// </summary>
        public struct KeyIntersection :
            IEquatable<KeyIntersection>,
            IDataIntersection<TKey, TIncoming, TExisting>
        {
            /// <summary>
            /// The common key
            /// </summary>
            public TKey Key { get; }
            /// <summary>
            /// Right element with matching key
            /// </summary>
            public TIncoming Incoming { get; }
            /// <summary>
            /// Left element with matching key
            /// </summary>
            public TExisting Existing { get; }
            /// <summary>
            /// 
            /// </summary>
            public KeyIntersection(TKey key, TExisting existing, TIncoming incoming)
            {
                Key = key;
                Incoming = incoming;
                Existing = existing;
            }
            /// <summary>
            /// String representation of intersection value
            /// </summary>
            public override string ToString() => $"({Key}): {Existing} ~ {Incoming}";
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
                    result = (result * 397) ^ Incoming.GetHashCode();
                    result = (result * 397) ^ Existing.GetHashCode();
                    return result;
                }
            }
            /// <summary>
            /// Determines if the specified object is equal to the other object
            /// </summary>
            public bool Equals(KeyIntersection other) =>
                    Key.Equals(other.Key)
                    && Incoming.Equals(other.Incoming)
                    && Existing.Equals(other.Existing);

            public static bool operator ==(KeyIntersection left, KeyIntersection right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(KeyIntersection left, KeyIntersection right)
            {
                return !(left == right);
            }
        }

        public static bool operator ==(DataDifference<TKey, TIncoming,TExisting> left, DataDifference<TKey, TIncoming, TExisting> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DataDifference<TKey, TIncoming, TExisting> left, DataDifference<TKey, TIncoming, TExisting> right)
        {
            return !(left == right);
        }
    }

    /// <summary>
    /// Symmetric difference between two sets or set like collections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct DataDifference<T> :
        IEquatable<DataDifference<T>>,
        IDataDifference<T>
    {
        ///
        public DataDifference(
            IEnumerable<T> toBeDeleted,
            IEnumerable<T> toBeAdded)
        {
            this.ToBeAdded = toBeAdded.ToArray();
            this.ToBeDeleted = toBeDeleted.ToArray();
        }
        /// <summary>
        /// Items only in the existing collection.
        /// </summary>
        public IReadOnlyCollection<T> ToBeAdded { get; }
        /// <summary>
        /// Items only in the incoming collection.
        /// </summary>
        public IReadOnlyCollection<T> ToBeDeleted { get; }
        /// <summary>
        /// String representation of difference
        /// </summary>
        public override string ToString() => $"+: {Format(ToBeAdded)}, -: {Format(ToBeDeleted)}";

        private string Format(IEnumerable<T> collection) => string.Join(",", collection);
        /// <summary>
        /// Determines if the specified object is equal to the other object
        /// </summary>
        public override bool Equals(object obj) => obj is DataDifference<T> diff && Equals(diff);
        /// <summary>
        /// Get hash value for element
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ ToBeAdded.ToArray().GetHashCode();
                result = (result * 397) ^ ToBeDeleted.ToArray().GetHashCode();
                return result;
            }
        }
        /// <summary>
        /// Determines if the specified object is equal to the other object
        /// </summary>
        public bool Equals(DataDifference<T> other) =>
                ToBeAdded.SequenceEqual(other.ToBeAdded)
                && ToBeDeleted.SequenceEqual(other.ToBeDeleted);
        ///
        public static bool operator ==(DataDifference<T> left, DataDifference<T> right)
        {
            return left.Equals(right);
        }
        /// 
        public static bool operator !=(DataDifference<T> left, DataDifference<T> right)
        {
            return !(left == right);
        }
    }
}
