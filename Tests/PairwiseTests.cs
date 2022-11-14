using System.Linq;
using Xunit;
using WallyMathieu.Collections;
using System;

namespace Tests
{
    public class PairwiseTests
    {
        [Fact]
        public void Pairwise()
        {
            var r = Enumerable.Range(0,5);
            var result = r.Pairwise();
            Assert.Equal(new[] { (0, 1), (1, 2), (2, 3), (3, 4) },
                result.ToArray());
        }
        [Fact]
        public void Pairwise_tuple()
        {
            var result = Enumerable.Range(0, 5).Pairwise();
            Assert.Equal(new[] { (0, 1), (1, 2), (2, 3), (3, 4) },
                result.ToArray());
        }

        [Fact]
        public void Pairwise_for_list_of_odd_length()
        {
            var result = Enumerable.Range(0, 4).Pairwise();
            Assert.Equal(new[] { (0, 1), (1, 2), (2, 3) },
                result.ToArray());
        }
        [Fact]
        public void Pairwise_for_list_of_odd_length_tuple()
        {
            var result = Enumerable.Range(0, 4).Pairwise();
            Assert.Equal(new[] { (0, 1), (1, 2), (2, 3) },
                result.ToArray());
        }
        [Fact]
        public void ShouldHandleEmptyCollection()
        {
            var emptyList = Array.Empty<string>();
            var result = emptyList.Pairwise().ToList();
            Assert.Empty(result);
        }
        [Fact]
        public void ShouldHandleEmptyCollection_tuple()
        {
            var emptyList = new string[0];
            var result = emptyList.Pairwise().ToList();
            Assert.Empty(result);
        }
    }
}
