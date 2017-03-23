using System;
using System.Collections.Generic;
using System.Linq;

namespace Carable
{
    public class CollectionItemsThatMayHaveChanged<T> : IEquatable<CollectionItemsThatMayHaveChanged<T>>
    {
        public T Existing { get; }
        public T Updated { get; }
        public CollectionItemsThatMayHaveChanged(T existing, T updated)
        {
            Existing = existing;
            Updated = updated;
        }
        public override string ToString()
        {
            return $"Maybe changed {Existing} -> {Updated}";
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as CollectionItemsThatMayHaveChanged<T>);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ Existing.GetHashCode();
                result = (result * 397) ^ Updated.GetHashCode();
                return result;
            }
        }

        public bool Equals(CollectionItemsThatMayHaveChanged<T> other)
        {
            if (Object.ReferenceEquals(null, other)) return false;
            return
                Existing.Equals(other.Existing)
                && Updated.Equals(other.Updated);
        }
    }
}
