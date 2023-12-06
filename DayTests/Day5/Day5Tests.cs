using DayTests.Shared;
using Shouldly;

namespace DayTests.Day5;

public class Day5Tests
{
    [Theory]
    [InlineData("data.txt", 165788812L)]
    [InlineData("example.txt", 35)]
    public void TestStep1(string testFile, int expectedResult)
    {
        var lines = ResourceHelper
            .ForAssembly<Day5Tests>()
            .ReadLines(testFile).ToArray();

        var score = Implementations.Part1(lines);
        score.ShouldBe(expectedResult);
    }

    //Brute force until I've got more time :)
    [Theory]
    [InlineData("data.txt", 1928058)]
    [InlineData("example.txt", 46)]
    public void TestStep2(string testFile, int expectedResult)
    {
        var lines = ResourceHelper
            .ForAssembly<Day5Tests>()
            .ReadLines(testFile).ToArray();

        var score = Implementations.Part2(lines);
        score.ShouldBe(expectedResult);
    }

    private static class Implementations
    {
        public static long Part1(string[] input)
        {
            var map = new List<List<Map>>();
            var seeds = new List<long>();

            seeds.AddRange(input[0].Split(' ').Skip(1).Select(long.Parse));
            List<Map>? mapping = null;
            foreach (var line in input.Skip(2).Where(x => !string.IsNullOrEmpty(x)))
            {
                if (line.EndsWith("map:"))
                {
                    mapping = new List<Map>();
                    map.Add(mapping);
                }
                else
                {
                    var data = line.Split(' ').Select(long.Parse).ToArray();
                    mapping!.Add(new Map { Source = data[1], Target = data[0], Length = data[2] });
                }
            }

            long lowest = int.MaxValue;

            foreach (var seed in seeds)
            {
                var d = seed;
                foreach (var m in map)
                {
                    var match = m.FirstOrDefault(x => d >= x.Source && d < x.Source + x.Length);
                    if (match != null)
                    {
                        var r = d - match.Source + match.Target;
                        d = r;
                    }
                }

                lowest = Math.Min(d, lowest);
            }

            return lowest;
        }

        public static long Part2(string[] input)
        {
            var map = new List<List<Map>>();
            var seeds = new List<long>();

            var seedData = input[0].Split(' ').Skip(1).Select(long.Parse).ToArray();

            for (var i = 0; i < seedData.Length; i += 2)
            {
                for (var j = 0; j < seedData[i + 1]; j++)
                {
                    seeds.Add(seedData[i] + j);
                }
            }

            List<Map>? mapping = null;
            foreach (var line in input.Skip(2).Where(x => !string.IsNullOrEmpty(x)))
            {
                if (line.EndsWith("map:"))
                {
                    mapping = new List<Map>();
                    map.Add(mapping);
                }
                else
                {
                    var data = line.Split(' ').Select(long.Parse).ToArray();
                    mapping!.Add(new Map { Source = data[1], Target = data[0], Length = data[2] });
                }
            }

            long lowest = int.MaxValue;

            foreach (var seed in seeds)
            {
                var d = seed;
                foreach (var m in map)
                {
                    var match = m.FirstOrDefault(x => d >= x.Source && d < x.Source + x.Length);
                    if (match != null)
                    {
                        var r = d - match.Source + match.Target;
                        d = r;
                    }
                }

                lowest = Math.Min(d, lowest);
            }

            return lowest;
        }
    }

    private class Map
    {
        public long Source { get; init; }
        public long Target { get; init; }
        public long Length { get; init; }
    }
}