using ExtraLinq;
using NUnit.Framework;
using System.Linq;

namespace ExtraLinqTests
{
    [TestFixture]
    public class Extensions
    {
        [Test]
        public static void CycleRepeatsNumbers()
        {
            var array = new[] { 1, 2, 3, 4 };
            var take = 10;
            var expected = new[] { 1, 2, 3, 4, 1, 2, 3, 4, 1, 2 };
            var result = array.ToCycle().Take(take);

            Assert.True(Enumerable.SequenceEqual(result, result));
        }
    }
}
