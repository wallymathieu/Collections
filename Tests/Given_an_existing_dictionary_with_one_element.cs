using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using WallyMathieu.Collections;
using Xunit;

namespace Tests
{
    public class Given_an_existing_dictionary_with_one_element
    {
        private static T Id<T>(T val) => val;
        private static string ToString<T>(T val) => val.ToString();
        ImmutableDictionary<int, string> existing = new[] { 1 }.ToImmutableDictionary(Id, ToString);

        [Fact]
        public void When_there_is_an_incoming_element() =>
            Assert.Equal(existing.SymmetricDiff(
                    new[] { 1 ,2 }.ToImmutableDictionary(Id)),
                new DifferenceBuilder<int, string, int>
                    {
                        OnlyInRight = {(2, 2)}
                    }.AddKeyIntersection(1, "1", 1)
                    .Build());

        [Fact]
        public void When_there_an_empty_incoming_list() =>
            Assert.Equal(existing.SymmetricDiff(new int[0].ToDictionary(Id)),
                new DifferenceBuilder<int, string, int>
                    {
                        OnlyInLeft = {(1, "1")}
                    }
                    .Build());

        [Fact]
        public void When_incoming_is_the_same_as_existing() =>
            Assert.Equal(existing.SymmetricDiff(existing),
                new DifferenceBuilder<int, string, string>().AddKeyIntersection(1, "1", "1").Build());

        private ImmutableDictionary<TKey,TValue> Cons<TKey,TValue>(TKey key,TValue value, ImmutableDictionary<TKey,TValue> arr)
        {//TODO: Make it more c#-y
            return new Dictionary<TKey, TValue>(arr) {{key, value}}.ToImmutableDictionary();
        }
    }
}