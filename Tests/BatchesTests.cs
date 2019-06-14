using System.Linq;
using Xunit;
using WallyMathieu.Collections;
using AutoFixture.Xunit2;

namespace Tests
{
    public class BatchesTests
    {
        [Fact]
        public void Test() => Assert.Equal(new[]{
            new []{1, 2, 3},
            new []{4, 5, 6},
            new []{7, 8, 9},
            new []{10}
        }, Enumerable.Range(1, 10).BatchesOf(3));

        [Theory, AutoData]
        public void Int_range(int size) =>
            Assert.Equal(size, Enumerable.Range(0, size * 2 - 1).BatchesOf(2).Count());

        [Theory, AutoData]
        public void Array(int size) =>
            Assert.Equal(size, new object[size * 2].BatchesOf(2).Count());
    }
}
