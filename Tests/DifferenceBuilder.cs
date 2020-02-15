using System.Collections.Generic;
using System.Linq;
using WallyMathieu.Collections;
using WallyMathieu.Collections.Abstractions;
using WallyMathieu.Collections.Internals;

namespace Tests
{
    public class DifferenceBuilder<TKey,TLeft,TRight>
    {
        public ISymmetricDifference<TKey, TLeft, TRight> Build() =>
            new SymmetricDifference<TKey, TLeft, TRight>(
                onlyInRight: OnlyInRight,
                intersection: Intersection,
                onlyInLeft: OnlyInLeft);

        public IList<(TKey, TLeft)> OnlyInLeft { get; }=new List<(TKey, TLeft)>();

        public IList<IKeyIntersection<TKey, TLeft, TRight>> Intersection { get; }=new List<IKeyIntersection<TKey, TLeft, TRight>>();

        public IList<(TKey, TRight)> OnlyInRight { get; }=new List<(TKey, TRight)>();

        public DifferenceBuilder<TKey, TLeft, TRight> AddKeyIntersection(TKey key, TLeft left, TRight right)
        {
            Intersection.Add(new SymmetricDifference<TKey,TLeft,TRight>.KeyIntersection(key,left,right));
            return this;
        }
    }
    public class DifferenceBuilder<TKey>
    {
        public ISymmetricDifference<TKey> Build() =>
            new SymmetricDifference<TKey>(
                onlyInRight: OnlyInRight,
                onlyInLeft: OnlyInLeft);

        public IList<TKey> OnlyInLeft { get; }=new List<TKey>();
        
        public IList<TKey> OnlyInRight { get; }=new List<TKey>();

    }
    public class DataDifferenceBuilder<TKey, TIncoming, TExisting>
    {
        public IDataDifference<TKey, TIncoming, TExisting> Build() =>
            new DataDifference<TKey, TIncoming, TExisting>(
                toBeAdded: ToBeAdded,
                intersection: Intersection,
                toBeDeleted: ToBeDeleted);

        public IList<(TKey, TIncoming)> ToBeAdded { get; } = new List<(TKey, TIncoming)>();

        public IList<IDataIntersection<TKey, TIncoming, TExisting>> Intersection { get; } = new List<IDataIntersection<TKey, TIncoming, TExisting>>();

        public IList<(TKey, TExisting)> ToBeDeleted { get; } = new List<(TKey, TExisting)>();

        public DataDifferenceBuilder<TKey, TIncoming, TExisting> AddKeyIntersection(TKey key, TExisting existing,TIncoming incoming)
        {
            Intersection.Add(new DataDifference<TKey, TIncoming, TExisting>.KeyIntersection(key, existing, incoming));
            return this;
        }
    }
    public class DataDifferenceBuilder<TKey>
    {
        public IDataDifference<TKey> Build() =>
         new DataDifference<TKey>(
             toBeAdded: ToBeAdded,
             toBeDeleted: ToBeDeleted);

        public IList<TKey> ToBeDeleted { get; } = new List<TKey>();

        public IList<TKey> ToBeAdded { get; } = new List<TKey>();
    }
}