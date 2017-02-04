using ExtraLinq;
using NUnit.Framework;
using System;
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

        [Test]
        public static void AllUnique_WhenSequenceAllUnique_ReturnTrue()
        {
            var sequence = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Assert.True(sequence.AllUnique());
        }

        [Test]
        public static void AllUnique_WhenSequenceNotUnique_ReturnFalse()
        {
            var sequence = new[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 };
            Assert.False(sequence.AllUnique());
        }

        [Test]
        public static void AllUnique_WhenStringSequenceAllUniqueExceptCasingAndUsingIgnoreCaseComparer_ReturnTrue()
        {
            var sequence = new[] { "a", "b", "c", "A", "B", "C" };
            Assert.True(sequence.AllUnique(StringComparer.CurrentCulture));
        }

        [Test]
        public static void AllUnique_WhenSequenceAllUniqueExceptCasingAndUsingNonIgnoreCaseComparer_ReturnFalse()
        {
            var sequence = new[] { "a", "b", "c", "A", "B", "C"};
            Assert.False(sequence.AllUnique(StringComparer.CurrentCultureIgnoreCase));
        }

        [Test]
        public static void AllDuplicate_WhenSequenceAllDuplicate_ReturnTrue()
        {
            var sequence = new[] { 1, 1, 1, 1, 1, 1 };
            Assert.True(sequence.AllDuplicate());
        }

        [Test]
        public static void AllDuplicate_WhenSequenceNotDuplicate_ReturnFalse()
        {
            var sequence = new[] { 1, 2, 1, 1, 1 };
            Assert.False(sequence.AllDuplicate());
        }

        [Test]
        public static void AllDuplicate_WhenStringSequenceAllDuplicateExceptCasingAndUsingIgnoreCaseComparer_ReturnTrue()
        {
            var sequence = new[] { "a", "A", "a", "a" };
            Assert.True(sequence.AllDuplicate(StringComparer.CurrentCultureIgnoreCase));
        }
    }
}
