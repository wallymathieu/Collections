using System.Collections.Immutable;
using System.Linq;
using WallyMathieu.Collections;
using Xunit;

namespace Tests
{
    public class Given_an_existing_dictionary_with_one_element_data
    {
        private static T Id<T>(T val) => val;
        private static string ToString<T>(T val) => val.ToString();
        ImmutableDictionary<int, string> existing = new[] { 1 }.ToImmutableDictionary(Id, ToString);

        [Fact]
        public void When_there_is_an_incoming_element() =>
            Assert.Equal(Differences.Of().Existing(existing).Incoming(
                    new[] { 1, 2 }.ToImmutableDictionary(Id)),
                new DataDifferenceBuilder<int, int, string>
                {
                    ToBeAdded = { (2, 2) }
                }.AddKeyIntersection(1, "1", 1)
                    .Build());

        [Fact]
        public void When_there_an_empty_incoming_list() =>
            Assert.Equal(Differences.Of().Existing(existing).Incoming(new int[0].ToDictionary(Id)),
                new DataDifferenceBuilder<int, int, string>
                {
                    ToBeDeleted = { (1, "1") }
                }
                    .Build());

        [Fact]
        public void When_incoming_is_the_same_as_existing() =>
            Assert.Equal(Differences.Of().Existing(existing).Incoming(existing),
                new DataDifferenceBuilder<int, string, string>().AddKeyIntersection(1, "1", "1").Build());
    }
}