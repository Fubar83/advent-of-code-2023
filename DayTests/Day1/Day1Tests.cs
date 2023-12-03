using DayTests.Shared;
using Shouldly;

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
            var numbers = input.Where(x => char.IsDigit(x)).ToArray();
            var result = int.Parse(numbers.First() + "" + numbers.Last());
            return result;
        }

        public static int Part2(string input)
        {
            var findWords = new[]
            {
                "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
            };

            bool TryFindNumberAtIndex(int index, out int number)
            {
                if (char.IsDigit(input[index]))
                {
                    number = int.Parse(input[index].ToString());
                    return true;
                }

                var remaining = input.Substring(index);
                for (var i = 0; i < findWords.Length; i++)
                {
                    var word = findWords[i];
                    if (remaining.StartsWith(word))
                    {
                        number = i + 1;
                        return true;
                    }
                }

                number = 0;
                return false;
            }

            int FindFirstNumber()
            {
                for (var i = 0; i < input.Length; i++)
                {
                    if (TryFindNumberAtIndex(i, out var number))
                    {
                        return number;
                    }
                }

                return 0;
            }

            int FindLastNumber()
            {
                for (var i = 0; i < input.Length; i++)
                {
                    if (TryFindNumberAtIndex(input.Length - i - 1, out var number))
                    {
                        return number;
                    }
                }

                return 0;
            }

            var result = FindFirstNumber() * 10 + FindLastNumber();
            return result;
        }
    }
}