using DayTests.Shared;
using Shouldly;
using Toolbox.Extensions;

namespace DayTests.Day13;

public class Day13Tests
{
    [Theory]
    [InlineData("data.txt", 28651)]
    [InlineData("example1.txt", 405)]
    public void TestStep1(string testFile, long expectedResult)
    {
        var result = Implementations.Part1(ResourceHelper
            .ForAssembly<Day13Tests>()
            .ReadLines(testFile).ToArray());
        result.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("data.txt", 25450)]
    [InlineData("example1.txt", 400)]
    public void TestStep2(string testFile, long expectedResult)
    {
        var result = Implementations.Part2(ResourceHelper
            .ForAssembly<Day13Tests>()
            .ReadLines(testFile).ToArray());
        result.ShouldBe(expectedResult);
        result.ShouldBe(expectedResult);
    }

    private static class Implementations
    {
        public static long Part1(string[] lines)
        {
            var result = 0L;
            foreach (var pattern in lines.Split(string.IsNullOrEmpty).Select(x => x.ToCharArray()))
            {
                if (TryFindVerticalMirror(pattern, 0, out var verticalIndex))
                {
                    result += verticalIndex + 1;
                }

                if (TryFindHorizontalMirror(pattern, 0, out var horizontalIndex))
                {
                    result += (horizontalIndex + 1) * 100;
                }
            }

            return result;
        }

        public static long Part2(string[] lines)
        {
            var result = 0L;
            foreach (var pattern in lines.Split(string.IsNullOrEmpty).Select(x => x.ToCharArray()))
            {
                if (TryFindVerticalMirror(pattern, 1, out var verticalIndex))
                {
                    result += verticalIndex + 1;
                }

                if (TryFindHorizontalMirror(pattern, 1, out var horizontalIndex))
                {
                    result += (horizontalIndex + 1) * 100;
                }
            }

            return result;
        }

        private static bool TryFindHorizontalMirror(char[,] pattern, int allowedSmudges, out int index)
        {
            var height = pattern.GetLength(0);

            for (var y = 0; y < height - 1; y++)
            {
                var remainingSmudges = allowedSmudges;
                if (ArrayEqual(pattern.GetRow(y), pattern.GetRow(y + 1), ref remainingSmudges))
                {
                    var allMatches = true;
                    for (var i = 1; i <= y && y + i < height - 1; i++)
                    {
                        allMatches &= ArrayEqual(pattern.GetRow(y - i), pattern.GetRow(y + i + 1),
                            ref remainingSmudges);
                    }

                    if (allMatches && remainingSmudges == 0)
                    {
                        index = y;
                        return true;
                    }
                }
            }

            index = default;
            return false;
        }

        private static bool TryFindVerticalMirror(char[,] pattern, int allowedSmudges, out int index)
        {
            var width = pattern.GetLength(1);

            for (var x = 0; x < width - 1; x++)
            {
                var remainingSmudges = allowedSmudges;
                if (ArrayEqual(pattern.GetColumn(x), pattern.GetColumn(x + 1), ref remainingSmudges))
                {
                    var allMatches = true;
                    for (var i = 1; i <= x && x + i < width - 1; i++)
                    {
                        allMatches &= ArrayEqual(pattern.GetColumn(x - i), pattern.GetColumn(x + i + 1),
                            ref remainingSmudges);
                    }

                    if (allMatches && remainingSmudges == 0)
                    {
                        index = x;
                        return true;
                    }
                }
            }

            index = default;
            return false;
        }

        private static bool ArrayEqual(char[] first, char[] second, ref int remainingSmudges)
        {
            if (first.SequenceEqual(second))
            {
                return true;
            }

            if (remainingSmudges > 0)
            {
                var differences = first.CountDifferences(second);
                if (differences > remainingSmudges)
                {
                    return false;
                }

                if (differences > 0)
                {
                    remainingSmudges -= differences;
                }

                return true;
            }

            return false;
        }
    }
}