using ExtraLinq;
using FluentAssertions;
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
            new[] { 1, 2, 3, 4 }.ToCycle().Take(10)
                .Should().Equal(new[] { 1, 2, 3, 4, 1, 2, 3, 4, 1, 2 });
        }

        [Fact]
        public static void AllUnique_WhenSequenceAllUnique_ReturnTrue()
        {
            new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.AllUnique()
                .Should().BeTrue();
        }

        [Fact]
        public static void AllUnique_WhenSequenceNotUnique_ReturnFalse()
        {
            new[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 }.AllUnique()
                .Should().BeFalse();
        }

        [Fact]
        public static void AllUnique_WhenStringSequenceAllUniqueExceptCasingAndUsingIgnoreCaseComparer_ReturnTrue()
        {
            new[] { "a", "b", "c", "A", "B", "C" }.AllUnique(StringComparer.CurrentCulture)
                .Should().BeTrue();
        }

        [Fact]
        public static void AllUnique_WhenSequenceAllUniqueExceptCasingAndUsingNonIgnoreCaseComparer_ReturnFalse()
        {
            new[] { "a", "b", "c", "A", "B", "C" }.AllUnique(StringComparer.CurrentCultureIgnoreCase)
                .Should().BeFalse();
        }

        [Fact]
        public static void AllDuplicate_WhenSequenceAllDuplicate_ReturnTrue()
        {
            new[] { 1, 1, 1, 1, 1, 1 }.AllDuplicate()
                .Should().BeTrue();
        }

        [Fact]
        public static void AllDuplicate_WhenSequenceNotDuplicate_ReturnFalse()
        {
            new[] { 1, 2, 1, 1, 1 }.AllDuplicate()
                .Should().BeFalse();
        }

        [Fact]
        public static void AllDuplicate_WhenStringSequenceAllDuplicateExceptCasingAndUsingIgnoreCaseComparer_ReturnTrue()
        {
            new[] { "a", "A", "a", "a" }.AllDuplicate(StringComparer.CurrentCultureIgnoreCase)
                .Should().BeTrue();
        }

        [Fact]
        public static void Permutations()
        {
            new[] { 'a', 'b', 'c' }.Permutations(new[] { 1, 2, 3 }, (i1, i2) => $"{i1}{i2}")
                .Should().Equal(new[] { "a1", "a2", "a3", "b1", "b2", "b3", "c1", "c2", "c3" });
        }

        [Theory]
        [InlineData(",", new[] { 1, 2, 3, 4 }, "1,2,3,4")]
        [InlineData("~", new[] { 1, 2, 3, 4, }, "1~2~3~4")]
        [InlineData("", new[] { 1, 2, 3, 4, }, "1234")]
        [InlineData(null, new[] { 1, 2, 3, 4, }, "1234")]
        public static void StringJoin(string delimiter, int[] array, string expected)
        {
            array.StringJoin(delimiter).Should().Be(expected);
        }

        [Fact]
        public static void IntersectBy_RemovesDuplicatesFromCompareWithAndSelfBasedOnKey_SameTypes()
        {
            new[]
            {
                new { ID = 1, Name = "Test" },
                new { ID = 2, Name = "Tester2" },
                new { ID = 1, Name = "Tester10" }
            }.IntersectBy(new[]
            {
                new { ID = 1, Name = "Tester12" },
                new { ID = 3, Name = "Tester17" }
            }, a => a.ID)
            .Should().Equal(new[] { new { ID = 1, Name = "Test" } });
        }

        [Fact]
        public static void IntersectBy_RemovesDuplicatesFromCompareWithAndSelfBasedOnKey_DiverseTypes()
        {
            new[]
            {
                new { ID = 1, Name = "Test" },
                new { ID = 2, Name = "Tester2" },
                new { ID = 1, Name = "Tester10" }
            }.IntersectBy(new[]
            {
                new { theID = 1, Number = 10 },
                new { theID = 3, Number = 17 }
            },
            a => a.ID,
            a => a.theID)
            .Should().Equal(new[] { new { ID = 1, Name = "Test" } });
        }
    }
}
