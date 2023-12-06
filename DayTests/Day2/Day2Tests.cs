using DayTests.Shared;
using Shouldly;

namespace DayTests.Day2;

public class Day2Tests
{
    [Theory]
    [InlineData("data.txt", 2406)]
    [InlineData("example.txt", 8)]
    public void TestStep1(string testFile, int expectedResult)
    {
        var sum = 0;
        foreach (var line in ResourceHelper
                     .ForAssembly<Day2Tests>()
                     .ReadLines(testFile))
        {
            sum += Implementations.Part1(line);
        }

        sum.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("data.txt", 78375)]
    [InlineData("example.txt", 2286)]
    public void TestStep2(string testFile, int expectedResult)
    {
        var sum = 0;
        foreach (var line in ResourceHelper
                     .ForAssembly<Day2Tests>()
                     .ReadLines(testFile))
        {
            sum += Implementations.Part2(line);
        }

        sum.ShouldBe(expectedResult);
    }

    private static class Implementations
    {
        public static int Part1(string input)
        {
            IDictionary<string, int> BuildConfig()
            {
                return new Dictionary<string, int>
                {
                    { "red", 12 }, { "green", 13 }, { "blue", 14 }
                };
            }

            var remaining = BuildConfig();

            var items = input.Split(new[] { ':', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            var gameId = int.Parse(items[1]);
            var valid = false;
            for (var i = 2; i < items.Length; i += 2)
            {
                var color = items[i + 1];
                var count = int.Parse(items[i]);
                var newGroup = color.EndsWith(';');
                if (newGroup)
                {
                    color = color.TrimEnd(';');
                }

                remaining[color] -= count;
                valid = !remaining.Any(x => x.Value < 0);
                if (!valid)
                {
                    break;
                }

                if (newGroup)
                {
                    remaining = BuildConfig();
                }
            }

            return valid ? gameId : 0;
        }

        public static int Part2(string input)
        {
            IDictionary<string, int> BuildConfig()
            {
                return new Dictionary<string, int>
                {
                    { "red", 12 }, { "green", 13 }, { "blue", 14 }
                };
            }

            var remaining = BuildConfig();

            var items = input.Split(new[] { ':', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            var result = new Dictionary<string, int>
            {
                { "red", 0 }, { "blue", 0 }, { "green", 0 }
            };
            for (var x = 2; x < items.Length; x += 2)
            {
                var color = items[x + 1];
                var count = int.Parse(items[x]);
                var newGroup = color.EndsWith(';');
                if (newGroup)
                {
                    color = color.TrimEnd(';');
                }

                remaining[color] -= count;

                if (result[color] < count)
                {
                    result[color] = count;
                }

                if (newGroup)
                {
                    remaining = BuildConfig();
                }
            }

            var power = 1;
            foreach (var v in result.Select(x => x.Value))
            {
                power *= v;
            }

            return power;
        }
    }
}