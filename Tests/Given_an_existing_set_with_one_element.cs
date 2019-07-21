using System.Collections.Immutable;
using System.Linq;
using Xunit;
using WallyMathieu.Collections;
using With;

namespace Tests
{
    public class Given_an_existing_set_with_one_element
    {
        ImmutableHashSet<int> existing = new[] { 1 }.ToImmutableHashSet();

        [Fact]
        public void When_there_is_an_incoming_element() =>
            Assert.Equal(existing.SymmetricDiff(Plus(2, existing).ToHashSet()),
                new DifferenceBuilder<int>
                    {
                        OnlyInRight = {2}
                    }
                    .Build());

        [Fact]
        public void When_there_an_empty_incoming_list() =>
            Assert.Equal(existing.SymmetricDiff(new int[0].ToHashSet()),
                new DifferenceBuilder<int>
                    {
                        OnlyInLeft = {1}
                    }
                    .Build());

        [Fact]
        public void When_incoming_is_the_same_as_existing() =>
            Assert.Equal(existing.SymmetricDiff(existing),
                new DifferenceBuilder<int>().Build());

        private ImmutableHashSet<T> Plus<T>(T v, ImmutableHashSet<T> arr) => 
            arr.ToHashSet().Tap(l=>l.Add(v)).ToImmutableHashSet();
    }
}
