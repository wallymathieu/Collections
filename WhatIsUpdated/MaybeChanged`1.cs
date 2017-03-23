using System;
using System.Collections.Generic;
using System.Linq;

namespace Carable.WhatIsUpdated
{
    public class MaybeChanged<T> : IEquatable<MaybeChanged<T>>
    {
        public T Existing { get;}
        public T Updated { get;}
        public MaybeChanged(T existing, T updated)
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
            return Equals(obj as MaybeChanged<T>);
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

        public bool Equals(MaybeChanged<T> other)
        {
            if (Object.ReferenceEquals(null, other)) return false;
            return
                Existing.Equals(other.Existing)
                && Updated.Equals(other.Updated);
        }
    }
}
