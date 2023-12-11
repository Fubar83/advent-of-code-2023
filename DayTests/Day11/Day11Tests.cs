using System.Drawing;
using DayTests.Shared;
using Shouldly;

namespace DayTests.Day11;

public class Day11Tests
{
    [Theory]
    [InlineData("data.txt", 9521776)]
    [InlineData("example.txt", 374)]
    public void TestStep1(string testFile, long expectedResult)
    {
        var result = Implementations.Part1(ResourceHelper
            .ForAssembly<Day11Tests>()
            .ReadLines(testFile).ToArray());
       
        result.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("data.txt", 1000000,553224415344)]
    [InlineData("example.txt", 10,1030)]
    [InlineData("example.txt", 100,8410)]
    public void TestStep2(string testFile, int factor, long expectedResult)
    {
        var result = Implementations.Part2(ResourceHelper
            .ForAssembly<Day11Tests>()
            .ReadLines(testFile).ToArray(), factor);
       
        result.ShouldBe(expectedResult);
    }

    private static class Implementations
    {

        public static long Part1(string[] input)
        {
            return Part2(input, 2);
        }
        public static long Part2(string[] input, int emptyFactor)
        {
            var points = GetPoints(input).ToArray();
            var emptyRows = GetEmptyRows(input).ToArray();
            var emptyCols = GetEmptyColumns(input).ToArray();

            long result = 0;
            for (var i = 0; i < points.Length-1; i++)
            {
                for (var j = i + 1; j < points.Length; j++)
                {
                    var p1 = points[i];
                    var p2 = points[j];

                    var colsBetween = Between(p1.X, p2.X);
                    var rowsBetween = Between(p1.Y, p2.Y);

                    var moves = Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);

                    moves += colsBetween.Count(c => emptyCols.Contains(c))*(emptyFactor-1);
                    moves += rowsBetween.Count(c => emptyRows.Contains(c))*(emptyFactor-1);

                    result += moves;
                }
            }

            return result;
        }

        private static int[] Between(int i1, int i2)
        {
            var min = Math.Min(i1, i2);
            var max = Math.Max(i1, i2);

            var length = max - min;

            if (length < 2)
                return Array.Empty<int>();

            return Enumerable.Range(min + 1, length - 1).ToArray();
        }

        private static IEnumerable<Point> GetPoints(string[] input)
        {
            var width = input[0].Length;
            var height = input.Length;
            
            for (var x = 0; x<width; x++)
            for (var y = 0; y < height; y++)
            {
                if (input[y][x] == '#')
                    yield return new Point(x, y);
            }
        }
        
        private static IEnumerable<int> GetEmptyRows(string[] input)
        {
            var height = input.Length;
            
            for (var y = 0; y < height; y++)
            {
                if (!input[y].Contains('#'))
                    yield return y;
            }
        }
        
        private static IEnumerable<int> GetEmptyColumns(string[] input)
        {
            var width = input[0].Length;
            var height = input.Length;

            for (var x = 0; x < width; x++)
            {
                var empty = true;
                for (var y = 0; y < height; y++)
                {
                    if (input[y][x] == '#')
                        empty = false;
                }

                if (empty)
                    yield return x;
            }
        }
        
    }
}