using System;
using NUnit.Framework;

namespace TargetLogger.Tests
{
    [TestFixture]
    public sealed class TimeSpanExtensionsTests
    {
        [TestCase(1, "1 ms")]
        [TestCase(1000, "1 sec")]
        [TestCase(60000, "1 min")]
        [TestCase(204000, "3 min 24 sec")]
        [TestCase(3600000, "1 h")]
        [TestCase(27720000, "7 h 42 min")]
        [TestCase(86400000, "1 d")]
        [TestCase(183600000, "2 d 3 h")]
        public void TestToShortString(double milliseconds, string output)
        {
            var duration = TimeSpan.FromMilliseconds(milliseconds);
            StringAssert.AreEqualIgnoringCase(output, duration.ToShortString());
        }
    }
}
