using DayTests.Shared;
using Shouldly;
using Toolbox.Extensions;

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
            var reader = new StringReader(input);

            reader.ReadUntil(' ');
            var gameId = int.Parse(reader.ReadUntil(':'));
            var config = BuildConfig();
            while (reader.Read() != -1)
            {
                var count = int.Parse(reader.ReadUntil(' '));
                var color = reader.ReadUntil(out var delimiter, ',', ';');

                config[color] -= count;

                if (config[color] < 0)
                {
                    return 0;
                }

                if (delimiter == ';')
                {
                    config = BuildConfig();
                }
            }

            return gameId;

            IDictionary<string, int> BuildConfig()
            {
                return new Dictionary<string, int>
                {
                    { "red", 12 }, { "green", 13 }, { "blue", 14 }
                };
            }
        }

        public static int Part2(string input)
        {
            var reader = new StringReader(input);

            reader.ReadUntil(' ');

            var result = new Dictionary<string, int>
            {
                { "red", 0 },
                { "green", 0 },
                { "blue", 0 }
            };

            while (reader.Read() != -1)
            {
                var count = int.Parse(reader.ReadUntil(' '));
                var color = reader.ReadUntil(',', ';');

                if (count > result[color])
                {
                    result[color] = count;
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