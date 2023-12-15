using DayTests.Shared;
using Shouldly;

namespace DayTests.Day15;

public class Day15Tests
{
    [Theory]
    [InlineData("data.txt", 521341)]
    [InlineData("example1.txt", 1320)]
    public void TestStep1(string testFile, long expectedResult)
    {
        var result = Implementations.Part1(ResourceHelper
            .ForAssembly<Day15Tests>()
            .ReadLines(testFile).First());
        result.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("data.txt", 252782)]
    [InlineData("example1.txt", 145)]
    public void TestStep2(string testFile, long expectedResult)
    {
        var result = Implementations.Part2(ResourceHelper
            .ForAssembly<Day15Tests>()
            .ReadLines(testFile).First());
        result.ShouldBe(expectedResult);
    }

    private static class Implementations
    {
        public static long Part1(string line)
        {
            var result = 0L;

            foreach (var step in line.Split(','))
            {
                var hash = CalculateHash(step);
                result += hash;
            }

            return result;
        }

        private static byte CalculateHash(string str)
        {
            byte b = 0;
            foreach (var c in str)
            {
                b += (byte)c;
                b *= 17;
            }

            return b;
        }
        
         public static long Part2(string str)
         {
             var boxes = new List<List<Box>>();
             for (var i = 0; i < 256; i++) boxes.Add(new List<Box>());
    
             foreach (var input in str.Split(','))
             {
                 var opCode = input.Contains('=') ? '=' : '-';
                 var label = input.Substring(0, input.IndexOfAny(new[] { '=', '-' }));
                 int hashIndex = CalculateHash(label);
                 var index = boxes[hashIndex].FindIndex(x => x.Label == label);

                 switch (opCode)
                 {
                     case '=':
                         var lensPower = input.Last() - '0';
                         if (index < 0)
                         {
                             boxes[hashIndex].Add(new Box(label, lensPower));
                         }
                         else
                         {
                             boxes[hashIndex][index] = new Box(label, lensPower);
                         }
                         break;
                     case '-':
                         if (index >= 0) boxes[hashIndex].RemoveAt(index);
                         break;
                 }
             }

             var totalLensPower = boxes
                 .Select((box, boxIdx) =>
                     box.Select((lens, lensIdx) =>
                         (1 + boxIdx) * (1 + lensIdx) * lens.LensPower
                     ).Sum()
                 ).Sum();

             return totalLensPower;
         }

         private class Box
         {
             public Box(string label, int lensPower)
             {
                 Label = label;
                 LensPower = lensPower;
             }
             public string Label { get; }
             public int LensPower { get; }
         }
    }
    
}