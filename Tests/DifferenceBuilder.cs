using System.Collections.Generic;
using WallyMathieu.Collections;

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
}