using Carable;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class Given_an_existing_list_with_one_element
    {
        int[] existing = new[] { 1 };
        private T Id<T>(T t) { return t; }
        [Fact]
        public void When_there_is_an_incoming_element()
        {
            Assert.Equal(existing.CollectionChanges(Cons(2, existing), t => t),
                new CollectionChanges<int>(toBeAdded: new int[] { 2 }, toBeRemoved: new int[0], toChange: new[] { new CollectionItemsThatMayHaveChanged<int>(1, 1) }));
        }

        [Fact]
        public void When_there_an_empty_incoming_list()
        {
            Assert.Equal(existing.CollectionChanges(new int[0], t => t),
                new CollectionChanges<int>(toBeAdded: new int[0], toBeRemoved: new int[] { 1 }, toChange: new CollectionItemsThatMayHaveChanged<int>[0]));
        }

        [Fact]
        public void When_incoming_is_the_same_as_existing()
        {
            Assert.Equal(existing.CollectionChanges(existing, t => t),
                new CollectionChanges<int>(toBeAdded: new int[0], toBeRemoved: new int[0], toChange: new[] { new CollectionItemsThatMayHaveChanged<int>(1, 1) }));
        }

        private IEnumerable<T> Cons<T>(T v, T[] existing)
        {
            return existing
                .ToList()
                .Tap(l => l.Add(v))
                .ToArray();
        }
    }
}
