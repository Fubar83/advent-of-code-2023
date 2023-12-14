using System.Text.RegularExpressions;
using DayTests.Shared;
using Shouldly;
using Toolbox.Extensions;
using Toolbox.Lookups;

namespace DayTests.Day1;

public class Day1Tests
{
    [Theory]
    [InlineData("data.txt", 55477)]
    [InlineData("example.txt", 142)]
    public void TestStep1(string testFile, int expectedResult)
    {
        var sum = 0;
        foreach (var line in ResourceHelper
                     .ForAssembly<Day1Tests>()
                     .ReadLines(testFile))
        {
            sum += Implementations.Part1(line);
        }

        sum.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("data.txt", 54431)]
    [InlineData("example2.txt", 281)]
    public void TestStep2(string testFile, int expectedResult)
    {
        var sum = 0;
        foreach (var line in ResourceHelper
                     .ForAssembly<Day1Tests>()
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
            var numbers = input.Where(char.IsDigit).Select(x => x - '0').ToArray();
            var result = numbers.First() * 10 + numbers.Last();
            return result;
        }

        public static int Part2(string input)
        {
            var regex = new Regex(@"([0-9]|one|two|three|four|five|six|seven|eight|nine)");

            var matches = regex.Matches(input, true).ToArray();

            var first = matches.First().Value;
            var last = matches.Last().Value;


            return ToNumber(first) * 10 + ToNumber(last);

            int ToNumber(string str)
            {
                if (int.TryParse(str, out var value))
                {
                    return value;
                }

                return NumberLookup.Convert(str);
            }
        }
    }
}