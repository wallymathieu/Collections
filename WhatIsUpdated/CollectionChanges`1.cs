using System;
using System.Collections.Generic;
using System.Linq;

namespace Carable
{
    public class CollectionChanges<T>:IEquatable<CollectionChanges<T>>
    {
        public CollectionChanges(IEnumerable<T> toBeAdded, IEnumerable<T> toBeRemoved, IEnumerable<CollectionItemsThatMayHaveChanged<T>> toChange)
        {
            ToBeAdded = toBeAdded.ToArray();
            ToBeRemoved = toBeRemoved.ToArray();
            ToChange = toChange.ToArray();
        }

        public IReadOnlyCollection<T> ToBeAdded { get; }
        public IReadOnlyCollection<T> ToBeRemoved { get; }
        public IReadOnlyCollection<CollectionItemsThatMayHaveChanged<T>> ToChange { get; }
        public override string ToString()
        {
            return $"ToBeAdded: {Format(ToBeAdded)}, ToBeRemoved: {Format(ToBeRemoved)}, ToChange: {Format(ToChange)}";
        }

        private string Format<T1>(IEnumerable<T1> collection)
        {
            return String.Join(",", collection.Select(ToString));
        }

        private static string ToString<T1>(T1 t) => t.ToString();
        public override bool Equals(object obj)
        {
            return Equals(obj as CollectionChanges<T>);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ ToBeAdded.ToArray().GetHashCode();
                result = (result * 397) ^ ToBeRemoved.ToArray().GetHashCode();
                result = (result * 397) ^ ToChange.ToArray().GetHashCode();
                return result;
            }
        }

        public bool Equals(CollectionChanges<T> other)
        {
            if (Object.ReferenceEquals(null, other)) return false;
            return
                ToBeAdded.SequenceEqual(other.ToBeAdded)
                && ToBeRemoved.SequenceEqual(other.ToBeRemoved)
                && ToChange.SequenceEqual(other.ToChange);
        }
    }
}
