using System.Collections.Generic;
using System.Linq;
using Xunit;
using WallyMathieu.Collections;

namespace Tests
{
    public class Given_an_existing_list_with_one_element
    {
        private static T Id<T>(T val) => val;
        int[] existing = new[] { 1 };

        [Fact]
        public void When_there_is_an_incoming_element() => 
            Assert.Equal(existing.ToDictionary(Id).Compare(Cons(2, existing).ToDictionary(Id)),
                new DictionaryComparison<int, int>(onlyInLeft: new int[0],
                                                onlyInRight: new int[] { 2 },
                                                Intersection: new[] { new DictionaryComparison<int, int>.ValueIntersection(1, 1) }));

        [Fact]
        public void When_there_an_empty_incoming_list() => 
            Assert.Equal(existing.ToDictionary(Id).Compare(new int[0].ToDictionary(Id)),
                new DictionaryComparison<int, int>(onlyInLeft: new int[] { 1 },
                                                onlyInRight: new int[0],
                                                Intersection: new DictionaryComparison<int, int>.ValueIntersection[0]));

        [Fact]
        public void When_incoming_is_the_same_as_existing() => 
            Assert.Equal(existing.ToDictionary(Id).Compare(existing.ToDictionary(Id)),
                new DictionaryComparison<int, int>(onlyInLeft: new int[0],
                                                onlyInRight: new int[0],
                                                Intersection: new[] { new DictionaryComparison<int, int>.ValueIntersection(1, 1) }));

        private IEnumerable<T> Cons<T>(T v, T[] arr)
        {
            var l = arr.ToList();
            l.Add(v);
            return l.ToArray();
        }
    }
}
