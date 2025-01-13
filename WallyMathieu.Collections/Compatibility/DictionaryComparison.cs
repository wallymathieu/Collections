using System;
using System.Collections.Generic;
using System.Linq;

namespace WallyMathieu.Collections
{
    public class DictionaryComparison<TLeft, TRight> : IEquatable<DictionaryComparison<TLeft, TRight>>
    {
        public DictionaryComparison(IEnumerable<TRight> onlyInRight, IEnumerable<TLeft> onlyInLeft, IEnumerable<ValueIntersection> Intersection)
        {
            this.OnlyInRight = onlyInRight.ToArray();
            this.OnlyInLeft = onlyInLeft.ToArray();
            this.Intersection = Intersection.ToArray();
        }
        /// <summary>
        /// Items with keys only in the right collection.
        /// </summary>
        public IReadOnlyCollection<TRight> OnlyInRight { get; }
        /// <summary>
        /// Items with keys only in the left collection.
        /// </summary>
        public IReadOnlyCollection<TLeft> OnlyInLeft { get; }
        /// <summary>
        /// Items with same keys only both collections.
        /// </summary>
        public IReadOnlyCollection<ValueIntersection> Intersection { get; }
        public override string ToString() => $"+: {Format(OnlyInRight)}, -: {Format(OnlyInLeft)}, =: {Format(Intersection)}";

        private string Format<T>(IEnumerable<T> collection) => string.Join(",", collection);
        public override bool Equals(object obj) => Equals(obj as DictionaryComparison<TLeft, TRight>);

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

        public bool Equals(DictionaryComparison<TLeft, TRight> other) =>
                !(other is null)
                && OnlyInRight.SequenceEqual(other.OnlyInRight)
                && OnlyInLeft.SequenceEqual(other.OnlyInLeft)
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
