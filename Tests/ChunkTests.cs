using System;
using System.Linq;
using Xunit;
using WallyMathieu.Collections;
using System.Collections.Generic;

namespace Tests
{
    public class ChunkTests
    {
        [Fact]
        public void Example1()
        {
            var array = new[] { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            var chunked = new List<(bool, int[])>();

            foreach (var grouping in array.Chunk(n => n % 2 == 0))
            {
                chunked.Add((grouping.Key, grouping.ToArray()));
            }
            Assert.Equal(new[]
            {
                (false, new[] {3, 1}),
                (true, new[] {4}),
                (false, new[] {1, 5, 9}),
                (true, new[] {2, 6}),
                (false, new[] {5, 3, 5})
            }, chunked.ToArray());
        }

        [Fact]
        public void ChunkBy_uses_consecutive_keys()
        {
            var array = new[] { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            var chunked = new List<(bool, int[])>();

            foreach (var grouping in array.ChunkBy(n => n % 2 == 0))
            {
                chunked.Add((grouping.Key, grouping.ToArray()));
            }

            Assert.Equal(new[]
            {
                (false, new[] {3, 1}),
                (true, new[] {4}),
                (false, new[] {1, 5, 9}),
                (true, new[] {2, 6}),
                (false, new[] {5, 3, 5})
            }, chunked.ToArray());
        }

        [Fact]
        public void ChunkBy_keeps_null_keys()
        {
            var array = new[] { "a", null, null, "b", null };
            var chunked = new List<(string, string[])>();

            foreach (var grouping in array.ChunkBy(item => item))
            {
                chunked.Add((grouping.Key, grouping.ToArray()));
            }

            Assert.Equal(new[]
            {
                ("a", new[] {"a"}),
                ((string)null, new string[] {null, null}),
                ("b", new[] {"b"}),
                ((string)null, new string[] {null})
            }, chunked.ToArray());
        }

        [Fact]
        public void ChunkBy_handles_empty_collection()
        {
            var result = new int[0].ChunkBy(i => i % 2).ToArray();
            Assert.Empty(result);
        }

        [Fact]
        public void ChunkBy_throws_for_null_collection()
        {
            Assert.Throws<ArgumentNullException>(() => EnumerableExtensions.ChunkBy<int, int>(null, i => i));
        }

        [Fact]
        public void ChunkBy_throws_for_null_selector()
        {
            Assert.Throws<ArgumentNullException>(() => new[] { 1 }.ChunkBy<int, int>(null));
        }

        private bool? Drop9And6(int i)
        {
            return i == 9 || i == 6 ? (bool?)null : i%2==0;
        }

        [Fact]
        public void ShouldDropItemsWhenNullIsReturned()
        {
            var array = new[] { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            var chunked = new List<(bool?, int[])>();

            foreach (var grouping in array.Chunk(Drop9And6))
            {
                chunked.Add((grouping.Key, grouping.ToArray()));
            }
            Assert.Equal(new[]
            {
                ((bool?)false, new[] {3, 1}),
                (true, new[] {4}),
                (false, new[] {1, 5}),
                (true, new[] {2}),
                (false, new[] {5, 3, 5})
            }, chunked.ToArray());
        }
    }
}
