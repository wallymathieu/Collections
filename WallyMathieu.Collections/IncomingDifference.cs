using System;
using System.Collections.Generic;
using System.Linq;

namespace WallyMathieu.Collections
{
    /// <summary>
    /// Symmetric difference between two dictionary or map collections
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TExisting"></typeparam>
    /// <typeparam name="TIncoming"></typeparam>
    public struct IncomingDifference<TKey, TIncoming, TExisting> :
        IEquatable<IncomingDifference<TKey, TIncoming, TExisting>>,
        IIncomingDifference<TKey, TIncoming, TExisting>
    {
        ///
        public IncomingDifference(
            IEnumerable<(TKey, TExisting)> toBeDeleted,
            IEnumerable<(TKey, TIncoming)> toBeAdded,
            IEnumerable<IKeyIncomingIntersection<TKey, TIncoming, TExisting>> intersection)
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
        public IReadOnlyCollection<IKeyIncomingIntersection<TKey, TIncoming, TExisting>> Intersection { get; }
        /// <summary>
        /// String representation of difference
        /// </summary>
        public override string ToString() => $"+: {Format(ToBeAdded)}, -: {Format(ToBeDeleted)}, =: {Format(Intersection)}";

        private string Format<T>(IEnumerable<T> collection) => string.Join(",", collection);
        /// <summary>
        /// Determines if the specified object is equal to the other object
        /// </summary>
        public override bool Equals(object obj) => obj is IncomingDifference<TKey, TIncoming, TExisting> diff && Equals(diff);
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
        public bool Equals(IncomingDifference<TKey, TIncoming,TExisting> other) =>
                ToBeAdded.SequenceEqual(other.ToBeAdded)
                && ToBeDeleted.SequenceEqual(other.ToBeDeleted)
                && Intersection.SequenceEqual(other.Intersection);

        /// <summary>
        /// Intersection values with the common key.
        /// </summary>
        public struct KeyIntersection :
            IEquatable<KeyIntersection>,
            IKeyIncomingIntersection<TKey, TIncoming, TExisting>
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

        public static bool operator ==(IncomingDifference<TKey, TIncoming,TExisting> left, IncomingDifference<TKey, TIncoming, TExisting> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(IncomingDifference<TKey, TIncoming, TExisting> left, IncomingDifference<TKey, TIncoming, TExisting> right)
        {
            return !(left == right);
        }
    }

    /// <summary>
    /// Symmetric difference between two sets or set like collections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct IncomingDifference<T> :
        IEquatable<IncomingDifference<T>>,
        IIncomingDifference<T>
    {
        ///
        public IncomingDifference(
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
        public override bool Equals(object obj) => obj is IncomingDifference<T> diff && Equals(diff);
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
        public bool Equals(IncomingDifference<T> other) =>
                ToBeAdded.SequenceEqual(other.ToBeAdded)
                && ToBeDeleted.SequenceEqual(other.ToBeDeleted);
        ///
        public static bool operator ==(IncomingDifference<T> left, IncomingDifference<T> right)
        {
            return left.Equals(right);
        }
        /// 
        public static bool operator !=(IncomingDifference<T> left, IncomingDifference<T> right)
        {
            return !(left == right);
        }
    }
}
