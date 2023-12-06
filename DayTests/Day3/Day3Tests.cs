using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using DayTests.Shared;
using Shouldly;

namespace DayTests.Day3;

public class Day3Tests
{
    [Theory]
    [InlineData("example.txt", 4361)]
    [InlineData("data.txt", 553825)]
    public void TestStep1(string testFile, int expectedResult)
    {
        var allLines = ResourceHelper
            .ForAssembly<Day3Tests>()
            .ReadLines(testFile).ToArray();

        var result = Implementations.Part1(allLines);
        result.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("example.txt", 467835)]
    [InlineData("data.txt", 93994191)]
    public void TestStep2(string testFile, int expectedResult)
    {
        var allLines = ResourceHelper
            .ForAssembly<Day3Tests>()
            .ReadLines(testFile).ToArray();

        var result = Implementations.Part2(allLines);
        result.ShouldBe(expectedResult);
    }
}

public static class Implementations
{
    public static int Part1(string[] lines)
    {
        var dataList = new List<Data>();
        var symbols = new List<SymbolData>();

        var numberRegex = new Regex("\\d+");
        var symbolRegex = new Regex("[^.\\d]");
        for (var lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            var line = lines[lineIndex];
            var matches = numberRegex.Matches(line);

            foreach (Match match in matches)
            {
                dataList.Add(new Data(int.Parse(match.Value), new Point(match.Index, lineIndex), match.Value.Length));
            }

            foreach (Match symbol in symbolRegex.Matches(line))
            {
                symbols.Add(new SymbolData(new Point(symbol.Index, lineIndex)));
            }
        }

        var result = 0;
        foreach (var data in dataList)
        {
            var intersects = symbols.FirstOrDefault(y => y.Intersects(data.Position));

            if (intersects != null)
            {
                result += data.Number;
            }
        }

        return result;
    }

    public static int Part2(string[] lines)
    {
        var dataList = new List<Data>();

        var numberRegex = new Regex("\\d+");
        var gearRegex = new Regex("\\*");
        for (var lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            var line = lines[lineIndex];
            var gearMatches = numberRegex.Matches(line);

            foreach (Match gearMatch in gearMatches)
            {
                dataList.Add(new Data(int.Parse(gearMatch.Value), new Point(gearMatch.Index, lineIndex),
                    gearMatch.Value.Length));
            }
        }

        var result = 0;
        for (var lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            var line = lines[lineIndex];
            foreach (Match symbol in gearRegex.Matches(line))
            {
                var gear = new SymbolData(new Point(symbol.Index, lineIndex));

                var adjacent = dataList.Where(x => gear.Intersects(x.Position)).ToArray();

                if (adjacent.Length == 2)
                {
                    result += adjacent[0].Number * adjacent[1].Number;
                }
            }
        }

        return result;
    }

    [DebuggerDisplay("{_point}")]
    private class SymbolData
    {
        private readonly Point _point;
        private readonly Rectangle _rectangle;

        public SymbolData(Point point)
        {
            _rectangle = new Rectangle(point.X - 1, point.Y - 1, 3, 3);
            _point = point;
        }

        public bool Intersects(Rectangle rect)
        {
            return _rectangle.IntersectsWith(rect);
        }
    }

    private class Data
    {
        public Data(int number, Point point, int length)
        {
            Number = number;
            Position = new Rectangle(point, new Size(length, 1));
        }

        public int Number { get; }
        public Rectangle Position { get; }
    }
}