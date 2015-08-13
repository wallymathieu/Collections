using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatIsUpdated;
using NUnit.Framework;

namespace TestsCs
{
    [TestFixture]
    public class Given_an_existing_list_with_one_element
    {
        int[] existing = new[] { 1 };
        private T Id<T>(T t) { return t; }
        [Test] public void When_there_is_an_incoming_element()
        {
            Assert.That(Diff.Changed(t => t, existing, existing),
                Is.EqualTo(new Updated<int>(toBeAdded: new int[] {2}, toBeRemoved: new int[0], toChange: new int[] { 1 })));
        }

        [Test] public void When_there_an_empty_incoming_list()
        {
            Assert.That(Diff.Changed(t => t, existing, existing),
                Is.EqualTo(new Updated<int>(toBeAdded: new int[0], toBeRemoved: new int[] { 1}, toChange: new int[0])));
        }

        [Test] public void When_incoming_is_the_same_as_existing()
        {
            Assert.That(Diff.Changed(t=>t, existing, existing), 
                Is.EqualTo(new Updated<int>(toBeAdded:new int[0],toBeRemoved:new int[0], toChange:new[] { 1})));
        }
    }
}
