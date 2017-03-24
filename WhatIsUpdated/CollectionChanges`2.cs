using System;
using System.Collections.Generic;
using System.Linq;

namespace Carable
{
    public class CollectionChanges<TExisting, TUpdated> : IEquatable<CollectionChanges<TExisting, TUpdated>>
    {
        public CollectionChanges(IEnumerable<TUpdated> toBeAdded, IEnumerable<TExisting> toBeRemoved, IEnumerable<CollectionItemsThatMayHaveChanged<TExisting, TUpdated>> toChange)
        {
            ToBeAdded = toBeAdded.ToArray();
            ToBeRemoved = toBeRemoved.ToArray();
            ToChange = toChange.ToArray();
        }

        public IReadOnlyCollection<TUpdated> ToBeAdded { get; }
        public IReadOnlyCollection<TExisting> ToBeRemoved { get; }
        public IReadOnlyCollection<CollectionItemsThatMayHaveChanged<TExisting, TUpdated>> ToChange { get; }
        public override string ToString()
        {
            return $"ToBeAdded: {Format(ToBeAdded)}, ToBeRemoved: {Format(ToBeRemoved)}, ToChange: {Format(ToChange)}";
        }

        private string Format<T>(IEnumerable<T> collection)
        {
            return String.Join(",", collection.Select(ToString));
        }

        private static string ToString<T>(T t) => t.ToString();
        public override bool Equals(object obj)
        {
            return Equals(obj as CollectionChanges<TExisting, TUpdated>);
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

        public bool Equals(CollectionChanges<TExisting, TUpdated> other)
        {
            if (Object.ReferenceEquals(null, other)) return false;
            return
                ToBeAdded.SequenceEqual(other.ToBeAdded)
                && ToBeRemoved.SequenceEqual(other.ToBeRemoved)
                && ToChange.SequenceEqual(other.ToChange);
        }
    }
}
