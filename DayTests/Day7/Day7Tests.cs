using System.Text;
using DayTests.Shared;
using Shouldly;

namespace DayTests.Day7;

public class Day7Tests
{
    [Theory]
    [InlineData("data.txt", 253603890)]
    [InlineData("example.txt", 6440)]
    public void TestStep1(string testFile, int expectedResult)
    {
        var lines = ResourceHelper
            .ForAssembly<Day7Tests>()
            .ReadLines(testFile).ToArray();

        var score = Implementations.Part1(lines);
        score.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("data.txt", 253630098)]
    [InlineData("example.txt", 5905)]
    public void TestStep2(string testFile, int expectedResult)
    {
        var lines = ResourceHelper
            .ForAssembly<Day7Tests>()
            .ReadLines(testFile).ToArray();

        var score = Implementations.Part2(lines);
        score.ShouldBe(expectedResult);
    }

    private static class Implementations
    {
        public static long Part1(string[] input)
        {
            var rankedHands = input.Select(line =>
                {
                    var cardData = line.Substring(0, 5);
                    var cards = SplitCards(cardData);
                    var bet = int.Parse(line.Substring(6));

                    var rank = GetRank(cards);
                    return new { Rank = rank, Bet = bet, Hand = cards, Line = line, Cards = cards };
                }).OrderBy(x => x.Rank)
                .ThenBy(x => x.Cards[0])
                .ThenBy(x => x.Cards[1])
                .ThenBy(x => x.Cards[2])
                .ThenBy(x => x.Cards[3])
                .ThenBy(x => x.Cards[4])
                .ToArray();

            var sr = new StringBuilder();
            var result = 0;
            for (var i = 0; i < rankedHands.Length; i++)
            {
                result += (i + 1) * rankedHands[i].Bet;
                sr.AppendLine($"{rankedHands[i].Line} {rankedHands[i].Rank}");
            }

            return result;
        }

        private static int[] SplitCards(string cards)
        {
            const string values = "  23456789TJQKA";
            return cards.Select(x => values.IndexOf(x)).ToArray();
        }

        private static int GetRank(int[] cards)
        {
            var grouped = cards.GroupBy(x => x).Select(x => new { CardValue = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count).ThenByDescending(x => x.CardValue)
                .ToList();

            if (grouped.Count == 1)
            {
                //Five of a kind 
                return 7;
            }

            if (grouped[0].Count == 4)
            {
                //Four of a kind
                return 6;
            }

            if (grouped[0].Count == 3 && grouped[1].Count == 2)
            {
                //Full house
                return 5;
            }

            if (grouped[0].Count == 3)
            {
                //Three of a kind
                return 4;
            }

            if (grouped[0].Count == 2 && grouped[1].Count == 2)
            {
                //Two pair
                return 3;
            }

            if (grouped[0].Count == 2 && grouped[1].Count == 1)
            {
                return 2;
            }

            return 1;
        }

        //Part 2

        private static int[] SplitCards2(string cards)
        {
            const string values = "J 23456789TQKA";
            return cards.Select(x => values.IndexOf(x)).ToArray();
        }

        private static int GetBestRank(int[] cards)
        {
            var combinations = GenerateCombinations(cards).Select(x => new { Rank = GetRank(x), Cards = cards })
                .OrderBy(x => x.Rank)
                .ThenBy(x => x.Cards[0])
                .ThenBy(x => x.Cards[1])
                .ThenBy(x => x.Cards[2])
                .ThenBy(x => x.Cards[3])
                .ThenBy(x => x.Cards[4])
                .ToArray();

            var sb = new StringBuilder();
            foreach (var combination in combinations)
            {
                sb.AppendLine($"{combination.Rank} ({string.Join(',', cards)}");
            }

            return combinations.Last().Rank;
        }

        private static IEnumerable<int[]> GenerateCombinations(int[] array)
        {
            var result = new List<int[]>();
            GenerateCombinationsHelper(array, 0, 2, 13, result);
            return result;
        }

        private static void GenerateCombinationsHelper(int[] array, int index, int minValue, int maxValue,
            List<int[]> result)
        {
            while (true)
            {
                if (index == array.Length)
                {
                    result.Add(array);
                    return;
                }

                if (array[index] == 0)
                {
                    for (var i = minValue; i <= maxValue; i++)
                    {
                        var clone = array.ToArray(); //Clone
                        clone[index] = i;
                        GenerateCombinationsHelper(clone, index + 1, minValue, maxValue, result);
                    }
                }
                else
                {
                    index += 1;
                    continue;
                }

                break;
            }
        }

        public static long Part2(string[] input)
        {
            var rankedHands = input.Select(line =>
                {
                    var cardData = line.Substring(0, 5);
                    var cards = SplitCards2(cardData);
                    var bet = int.Parse(line.Substring(6));

                    var rank = GetBestRank(cards);
                    return new { Rank = rank, Bet = bet, Hand = cards, Line = line, Cards = cards };
                }).OrderBy(x => x.Rank)
                .ThenBy(x => x.Cards[0])
                .ThenBy(x => x.Cards[1])
                .ThenBy(x => x.Cards[2])
                .ThenBy(x => x.Cards[3])
                .ThenBy(x => x.Cards[4])
                .ToArray();

            var sr = new StringBuilder();
            var result = 0;
            for (var i = 0; i < rankedHands.Length; i++)
            {
                result += (i + 1) * rankedHands[i].Bet;
                sr.AppendLine($"{rankedHands[i].Line} {rankedHands[i].Rank}");
            }

            return result;
        }
    }
}