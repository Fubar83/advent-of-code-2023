using DayTests.Shared;
using Shouldly;
using Toolbox;

namespace DayTests.Day9;

public class Day9Tests
{
    [Theory]
    [InlineData("data.txt", 2043677056)]
    [InlineData("example.txt", 114)]
    public void TestStep1(string testFile, long expectedResult)
    {
        long result = 0;
        foreach (var line in ResourceHelper
                     .ForAssembly<Day9Tests>()
                     .ReadLines(testFile))
        {
            result += Implementations.Part1(line);
        }

        result.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("data.txt", 1062)]
    [InlineData("example.txt", 2)]
    public void TestStep2(string testFile, long expectedResult)
    {
        long result = 0;
        foreach (var line in ResourceHelper
                     .ForAssembly<Day9Tests>()
                     .ReadLines(testFile))
        {
            result += Implementations.Part2(line);
        }

        result.ShouldBe(expectedResult);
    }

    private static class Implementations
    {
        public static long Part1(string input)
        {
            var numbers = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            return Extrapolate.CalculateNextValue(numbers);
        }

        public static long Part2(string input)
        {
            var numbers = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            return Extrapolate.CalculatePreviousValue(numbers);
        }
    }
}