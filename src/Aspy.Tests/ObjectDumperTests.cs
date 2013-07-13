
using System;
using System.Collections.Generic;
using ByteCarrot.Aspy.Tree;
using NUnit.Framework;

namespace ByteCarrot.Aspy.Tests
{
    public class ObjectDumperTests : TestFixture
    {
        [Test]
        public void A()
        {
            var dic = new Dictionary<string, string> { { "Key 1", "Value 1" }, { "Key 2", "Value 2" } };
            var x = new ObjectDumper().Dump(typeof(Object));
        }
    }
}
