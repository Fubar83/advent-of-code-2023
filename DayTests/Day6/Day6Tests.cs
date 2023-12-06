using DayTests.Shared;
using Shouldly;

namespace DayTests.Day6;

public class Day6Tests
{
    [Theory]
    [InlineData("data.txt", 1155175)]
    [InlineData("example.txt", 288)]
    public void TestStep1(string testFile, int expectedResult)
    {
        var lines = ResourceHelper
            .ForAssembly<Day6Tests>()
            .ReadLines(testFile).ToArray();

        var score = Implementations.Part1(lines);
        score.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("data2.txt", 35961505)]
    [InlineData("example2.txt", 71503)]
    public void TestStep2(string testFile, int expectedResult)
    {
        var lines = ResourceHelper
            .ForAssembly<Day6Tests>()
            .ReadLines(testFile).ToArray();

        var score = Implementations.Part2(lines);
        score.ShouldBe(expectedResult);
    }

    private static class Implementations
    {
        public static long Part1(string[] input)
        {
            var timeList = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse)
                .ToArray();
            var distances = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse)
                .ToArray();

            long result = 1;

            for (var i = 0; i < timeList.Length; i++)
            {
                long wins = 0;
                for (var time = 0; time < timeList[i]; time++)
                {
                    var remaining = timeList[i] - time;

                    if (time * remaining > distances[i])
                    {
                        wins++;
                    }
                }

                result *= wins;
            }

            return result;
        }

        public static long Part2(string[] input)
        {
            var timeList = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse)
                .ToArray();
            var distances = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse)
                .ToArray();

            long result = 1;

            for (var i = 0; i < timeList.Length; i++)
            {
                long wins = 0;
                for (var time = 0; time < timeList[i]; time++)
                {
                    var remaining = timeList[i] - time;

                    if (time * remaining > distances[i])
                    {
                        wins++;
                    }
                }

                result *= wins;
            }

            return result;
        }
    }
}