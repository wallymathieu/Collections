using System;
using System.Collections.Generic;
using System.Linq;

namespace WallyMathieu.Collections
{
    public class DictionaryComparison<TLeft, TRight> : IEquatable<DictionaryComparison<TLeft, TRight>>
    {
        public DictionaryComparison(IEnumerable<TRight> Plus, IEnumerable<TLeft> Minus, IEnumerable<ValueIntersection> Intersection)
        {
            this.Plus = Plus.ToArray();
            this.Minus = Minus.ToArray();
            this.Intersection = Intersection.ToArray();
        }
        /// <summary>
        /// Items with keys only in the right collection.
        /// </summary>
        public IReadOnlyCollection<TRight> Plus { get; }
        /// <summary>
        /// Items with keys only in the left collection.
        /// </summary>
        public IReadOnlyCollection<TLeft> Minus { get; }
        /// <summary>
        /// Items with same keys only both collections.
        /// </summary>
        public IReadOnlyCollection<ValueIntersection> Intersection { get; }
        public override string ToString() => $"+: {Format(Plus)}, -: {Format(Minus)}, =: {Format(Intersection)}";

        private string Format<T>(IEnumerable<T> collection) => string.Join(",", collection);
        public override bool Equals(object obj) => Equals(obj as DictionaryComparison<TLeft, TRight>);

        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ Plus.ToArray().GetHashCode();
                result = (result * 397) ^ Minus.ToArray().GetHashCode();
                result = (result * 397) ^ Intersection.ToArray().GetHashCode();
                return result;
            }
        }

        public bool Equals(DictionaryComparison<TLeft, TRight> other) =>
                !(other is null)
                && Plus.SequenceEqual(other.Plus)
                && Minus.SequenceEqual(other.Minus)
                && Intersection.SequenceEqual(other.Intersection);

        /// <summary>
        /// Value intersection
        /// </summary>
        public class ValueIntersection : IEquatable<ValueIntersection>
        {
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
            public ValueIntersection(TRight right, TLeft left)
            {
                Right = right;
                Left = left;
            }
            public override string ToString() => $"Intersection {Left} ~ {Right}";
            public override bool Equals(object obj) => Equals(obj as ValueIntersection);

            public override int GetHashCode()
            {
                unchecked
                {
                    var result = 0;
                    result = (result * 397) ^ Right.GetHashCode();
                    result = (result * 397) ^ Left.GetHashCode();
                    return result;
                }
            }

            public bool Equals(ValueIntersection other) => !(other is null) &&
                    Right.Equals(other.Right)
                    && Left.Equals(other.Left);
        }
    }
}
