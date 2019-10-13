using System.Runtime.InteropServices;
using NUnit.Framework;

namespace TargetLogger.Tests
{
    [TestFixture]
    internal sealed class StringExtensionsTests
    {
        [TestCase("/path/to/directory", "directory")]
        [TestCase(@"\path\to\directory", "directory")]
        [TestCase(@"C:\path\to\directory", "directory")]
        [TestCase("/path/to/file.extension", "file.extension")]
        public void TestGetPathFileName(string path, string shortName)
        {
            StringAssert.AreEqualIgnoringCase(shortName, path.GetPathFileName());
        }
    }
}
