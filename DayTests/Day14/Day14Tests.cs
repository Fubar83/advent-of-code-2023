using System.Text;
using DayTests.Shared;
using Shouldly;
using Toolbox.Extensions;

namespace DayTests.Day14;

public class Day14Tests
{
    [Theory]
    [InlineData("data.txt", 112773)]
    [InlineData("example1.txt", 136)]
    public void TestStep1(string testFile, long expectedResult)
    {
        var result = Implementations.Part1(ResourceHelper
            .ForAssembly<Day14Tests>()
            .ReadLines(testFile).ToArray());
        result.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("data.txt", 98894)]
    [InlineData("example1.txt", 64)]
    public void TestStep2(string testFile, long expectedResult)
    {
        var result = Implementations.Part2(ResourceHelper
            .ForAssembly<Day14Tests>()
            .ReadLines(testFile).ToArray());
        result.ShouldBe(expectedResult);
    }

    private static class Implementations
    {
        private static Func<char, bool> canMove = c => c != '#'; 
        public static long Part1(string[] lines)
        {
            var field = lines.ToCharArray();

            var width = field.GetLength(1);
            TiltNorth(width, field);
            return CountNorthBeamLoad(field);
        }

        public static long Part2(string[] lines)
        {
            var field = lines.ToCharArray();

            var width = field.GetLength(1);
            var height = field.GetLength(0);

            var cache = new Dictionary<string, int>();
            var iterations = 1_000_000_000;

            var final = false;
            
            for (var i = iterations; i> 0; i--)
            {
                //North
                TiltNorth(width, field);

                //West
                TiltWest(height, field);

                //South
                TiltSouth(width, field);

                //East
                TiltEast(height, field);

                if (!final)
                {
                    var dump = DumpField(field);
                    if (cache.TryGetValue(dump, out var cachedIndex))
                    {
                        //We're seen this before so taking a short cut
                        var repeat = cachedIndex - i;
                        i = cachedIndex % repeat;
                        final = true;
                    }
                    else
                    {
                        cache.Add(dump, i);
                    } 
                }
                
                
                
                
            }

            
            
            
            
            return CountNorthBeamLoad(field);
        }

        private static string DumpField(char[,] field)
        {
            var width = field.GetLength(1);
            var height = field.GetLength(0);
            
            var sb = new StringBuilder(width * height);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    sb.Append(field[y, x]);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
        
        private static void TiltEast(int height, char[,] field)
        {
            for (var y = 0; y < height; y++)
            {
                var row = field.GetRow(y);
                row.BubbleSortWithUnmovable(canMove);
                field.SetRow(y, row);
            }
        }

        private static void TiltSouth(int width, char[,] field)
        {
            for (var x = 0; x < width; x++)
            {
                var column = field.GetColumn(x);
                column.BubbleSortWithUnmovable(canMove);
                field.SetColumn(x, column);
            }
        }

        private static void TiltWest(int height, char[,] field)
        {
            for (var y = 0; y < height; y++)
            {
                var row = field.GetRow(y);
                row.BubbleSortWithUnmovable(canMove, false);
                field.SetRow(y, row);
            }
        }

        private static void TiltNorth(int width, char[,] field)
        {
            for (var x = 0; x < width; x++)
            {
                var column = field.GetColumn(x);
                column.BubbleSortWithUnmovable(canMove, false);
                field.SetColumn(x, column);
            }
        }

        private static int CountNorthBeamLoad(char[,] field)
        {
            var width = field.GetLength(1);
            var height = field.GetLength(0);

            var load = 0;
                
            for(var x = 0; x<width; x++)
            for (int y = 0; y < height; y++)
            {
                if (field[y, x] == 'O')
                {
                    var l = height - y;
                    load += l;
                }
            }

            return load;
        }
    }
}