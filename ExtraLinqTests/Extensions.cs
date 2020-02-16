using ExtraLinq;
using System;
using System.Linq;
using Xunit;

namespace ExtraLinqTests
{
    public class Extensions
    {
        [Fact]
        public static void CycleRepeatsNumbers()
        {
            var array = new[] { 1, 2, 3, 4 };
            var take = 10;
            var expected = new[] { 1, 2, 3, 4, 1, 2, 3, 4, 1, 2 };
            var result = array.ToCycle().Take(take);

            Assert.True(Enumerable.SequenceEqual(expected, result));
        }

        [Fact]
        public static void AllUnique_WhenSequenceAllUnique_ReturnTrue()
        {
            var sequence = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Assert.True(sequence.AllUnique());
        }

        [Fact]
        public static void AllUnique_WhenSequenceNotUnique_ReturnFalse()
        {
            var sequence = new[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 };
            Assert.False(sequence.AllUnique());
        }

        [Fact]
        public static void AllUnique_WhenStringSequenceAllUniqueExceptCasingAndUsingIgnoreCaseComparer_ReturnTrue()
        {
            var sequence = new[] { "a", "b", "c", "A", "B", "C" };
            Assert.True(sequence.AllUnique(StringComparer.CurrentCulture));
        }

        [Fact]
        public static void AllUnique_WhenSequenceAllUniqueExceptCasingAndUsingNonIgnoreCaseComparer_ReturnFalse()
        {
            var sequence = new[] { "a", "b", "c", "A", "B", "C" };
            Assert.False(sequence.AllUnique(StringComparer.CurrentCultureIgnoreCase));
        }

        [Fact]
        public static void AllDuplicate_WhenSequenceAllDuplicate_ReturnTrue()
        {
            var sequence = new[] { 1, 1, 1, 1, 1, 1 };
            Assert.True(sequence.AllDuplicate());
        }

        [Fact]
        public static void AllDuplicate_WhenSequenceNotDuplicate_ReturnFalse()
        {
            var sequence = new[] { 1, 2, 1, 1, 1 };
            Assert.False(sequence.AllDuplicate());
        }

        [Fact]
        public static void AllDuplicate_WhenStringSequenceAllDuplicateExceptCasingAndUsingIgnoreCaseComparer_ReturnTrue()
        {
            var sequence = new[] { "a", "A", "a", "a" };
            Assert.True(sequence.AllDuplicate(StringComparer.CurrentCultureIgnoreCase));
        }

        [Fact]
        public static void Permutations()
        {
            var collection1 = new[] { 'a', 'b', 'c' };
            var collection2 = new[] { 1, 2, 3 };
            Func<char, int, string> selector = (i1, i2) => i1.ToString() + i2.ToString();

            var result = collection1.Permutations(collection2, selector);
            var expected = new[] { "a1", "a2", "a3", "b1", "b2", "b3", "c1", "c2", "c3" };

            Assert.True(Enumerable.SequenceEqual(result, expected));
        }

        [Theory]
        [InlineData(",", new[] { 1, 2, 3, 4 }, "1,2,3,4")]
        [InlineData("~", new[] { 1, 2, 3, 4, }, "1~2~3~4")]
        [InlineData("", new[] { 1, 2, 3, 4, }, "1234")]
        [InlineData(null, new[] { 1, 2, 3, 4, }, "1234")]
        public static void StringJoin(string delimiter, int[] array, string expected)
        {
            Assert.Equal(expected, array.StringJoin(delimiter));
        }

        [Fact]
        public static void IntersectBy_RemovesDuplicatesFromCompareWithAndSelfBasedOnKey_SameTypes()
        {
            var source = new[]
            {
                new
                {
                    ID = 1,
                    Name = "Test"
                },
                new
                {
                    ID = 2,
                    Name = "Tester2"
                },
                new
                {
                    ID = 1,
                    Name = "Tester10"
                }
            };

            var compareWith = new[]
            {
                new
                {
                    ID = 1,
                    Name = "Tester12"
                },
                new
                {
                    ID = 3,
                    Name = "Tester17"
                }
            };

            var intersected = source.IntersectBy(compareWith, a => a.ID).ToArray();

            Assert.Single(intersected);
            Assert.Equal(intersected[0], new { ID = 1, Name = "Test" });
        }

        [Fact]
        public static void IntersectBy_RemovesDuplicatesFromCompareWithAndSelfBasedOnKey_DiverseTypes()
        {
            var source = new[]
            {
                new
                {
                    ID = 1,
                    Name = "Test"
                },
                new
                {
                    ID = 2,
                    Name = "Tester2"
                },
                new
                {
                    ID = 1,
                    Name = "Tester10"
                }
            };

            var compareWith = new[]
            {
                new
                {
                    theID = 1,
                    Number=  10,
                },
                new
                {
                    theID = 3,
                    Number = 17
                }
            };

            var intersected = source.IntersectBy(compareWith, a => a.ID, a => a.theID).ToArray();

            Assert.Single(intersected);
            Assert.Equal(intersected[0], new { ID = 1, Name = "Test" });
        }
    }
}
