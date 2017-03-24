using System;

namespace Carable
{
    public class CollectionItemsThatMayHaveChanged<TExisting, TUpdated> : IEquatable<CollectionItemsThatMayHaveChanged<TExisting, TUpdated>>
    {
        public TExisting Existing { get; }
        public TUpdated Updated { get; }
        public CollectionItemsThatMayHaveChanged(TExisting existing, TUpdated updated)
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
            return Equals(obj as CollectionItemsThatMayHaveChanged<TExisting,TUpdated>);
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

        public bool Equals(CollectionItemsThatMayHaveChanged<TExisting,TUpdated> other)
        {
            if (Object.ReferenceEquals(null, other)) return false;
            return
                Existing.Equals(other.Existing)
                && Updated.Equals(other.Updated);
        }
    }
}