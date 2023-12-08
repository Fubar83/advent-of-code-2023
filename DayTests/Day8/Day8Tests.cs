using DayTests.Shared;
using Shouldly;

namespace DayTests.Day8;

public class Day8Tests
{
    [Theory]
    [InlineData("data.txt", 18157)]
    [InlineData("example.txt", 6)]
    public void TestStep1(string testFile, long expectedResult)
    {
        var lines = ResourceHelper
            .ForAssembly<Day8Tests>()
            .ReadLines(testFile).ToArray();

        var score = Implementations.Part1(lines);
        score.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("data.txt", 14299763833181)]
    [InlineData("example2.txt", 6)]
    public void TestStep2(string testFile, long expectedResult)
    {
        var lines = ResourceHelper
            .ForAssembly<Day8Tests>()
            .ReadLines(testFile).ToArray();

        var score = Implementations.Part2(lines);
        score.ShouldBe(expectedResult);
    }

    private static class Implementations
    {
        public static long Part1(string[] input)
        {
            var sequence = GetSequence(input[0]);

            var lookup = new Dictionary<string, string[]>();

            foreach (var line in input.Skip(2))
            {
                var id = line.Substring(0, 3);
                var moveLeft = line.Substring(7, 3);
                var moveRight = line.Substring(12, 3);
                lookup.Add(id, new[] { moveLeft, moveRight });
            }

            var steps = 0;
            var current = "AAA";
            foreach (var next in sequence)
            {
                var nextId = lookup[current][next];


                if (current == "ZZZ")
                {
                    break;
                }

                steps++;
                current = nextId;
            }


            return steps;
        }


        //Part 2
        public static long Part2(string[] input)
        {
            var sequence = GetSequence(input[0]);

            var lookup = new Dictionary<string, string[]>();

            foreach (var line in input.Skip(2))
            {
                var id = line.Substring(0, 3);
                var moveLeft = line.Substring(7, 3);
                var moveRight = line.Substring(12, 3);
                lookup.Add(id, new[] { moveLeft, moveRight });
            }

            var steps = 0;
            var current = new List<string>(lookup.Keys.Where(x => x.EndsWith('A')));
            var reachEndIn = new Dictionary<long, long>();
            foreach (var next in sequence)
            {
                steps++;
                for (var i = 0; i < current.Count; i++)
                {
                    if (!reachEndIn.ContainsKey(i))
                    {
                        var c = current[i];
                        var nextId = lookup[c][next];
                        if (nextId.EndsWith('Z'))
                        {
                            reachEndIn.Add(i, steps);
                        }

                        current[i] = nextId;
                    }
                }

                if (reachEndIn.Count == current.Count)
                {
                    break;
                }
            }

            var result = new Dictionary<long, long>();
            foreach (var r in reachEndIn)
            {
                result.Add(r.Key, r.Value);
            }

            return LCM.Calculate(result.Values.ToArray());
        }

        private static IEnumerable<long> GetSequence(string sequence)
        {
            do
            {
                foreach (var t in sequence.Select(x => x == 'L' ? 0 : 1))
                {
                    yield return t;
                }
            } while (true);
            // ReSharper disable once IteratorNeverReturns
        }
    }
}