using DayTests.Shared;
using Shouldly;

namespace DayTests.Day4;

public class Day4Tests
{
    [Theory]
    [InlineData("data.txt", 25231)]
    [InlineData("example.txt", 13)]
    public void TestStep1(string testFile, int expectedResult)
    {
        var sum = 0;
        foreach (var line in ResourceHelper
                     .ForAssembly<Day4Tests>()
                     .ReadLines(testFile))
        {
            var score = Implementations.Part1(line);
            sum += score;
        }

        sum.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("data.txt", 9721255)]
    [InlineData("example2.txt", 30)]
    public void TestStep2(string testFile, int expectedResult)
    {
        var result = Implementations.Part2(ResourceHelper
            .ForAssembly<Day4Tests>()
            .ReadLines(testFile).ToArray());
        result.ShouldBe(expectedResult);
    }

    private static class Implementations
    {
        public static int Part1(string input)
        {
            var split = input.Split(new[] { ':', '|' },
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var winning = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var hand = split[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();


            var count = hand.Count(x => winning.Contains(x));
            var score = (int)Math.Pow(2, count - 1);

            return score;
        }

        public static int Part2(string[] input)
        {
            var countList = new Dictionary<int, int>();
            var instances = new Dictionary<int, int>();
            for (var index = 0; index < input.Length; index++)
            {
                var line = input[index];
                var split = line.Split(new[] { ':', '|' },
                    StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var winning = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var hand = split[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                var count = hand.Count(x => winning.Contains(x));
                countList.Add(index, count);
                instances.Add(index, 1);
            }

            foreach (var (key, value) in countList)
            {
                if (value > 0)
                {
                    for (var i = 0; i < value; i++)
                    {
                        var index = key + i + 1;
                        if (instances.ContainsKey(index))
                        {
                            instances[index] += instances[key];
                        }
                    }
                }
            }

            return instances.Sum(x => x.Value);
        }
    }
}