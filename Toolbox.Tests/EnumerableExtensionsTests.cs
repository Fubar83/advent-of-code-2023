using Shouldly;
using Toolbox.Extensions;

namespace Toolbox.Tests;

public class EnumerableExtensionsTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, 0)]
    [InlineData(new[] { 1, 2, 3 }, new[] { 1, 4, 3 }, 1)]
    [InlineData(new[] { 1, 2, 3, 4 }, new[] { 1, 2, 3 }, 1)]
    [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 3, 4 }, 1)]
    [InlineData(new int[0], new int[0], 0)]
    [InlineData(new[] { 1, 2 }, new[] { 1, 2, 3 }, 1)]
    [InlineData(new[] { 1, 2, 3 }, new int[0], 3)]
    public void CountDifferences_ShouldReturnCorrectCount(int[] first, int[] second, int expectedDifferences)
    {
        var differences = first.CountDifferences(second);
        Assert.Equal(expectedDifferences, differences);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 2, new[] { "1", "3,4,5,6" })]
    public void Split_Integers_ShouldReturnCorrectSequences(IEnumerable<int> source, int divider,
        string[] expectedGroups)
    {
        var result = source.Split(x => x == divider).Select(data => string.Join(",", data)).ToArray();

        result.ShouldBe(expectedGroups);
    }


    [Theory]
    [InlineData(new[] { "a", "b", "c", "d", "b", "e" }, "b", new[] { "a", "c,d", "e" })]
    public void Split_Strings_ShouldReturnCorrectSequences(IEnumerable<string> source, string divider,
        string[] expectedGroups)
    {
        var result = source.Split(x => x == divider).Select(data => string.Join(",", data)).ToArray();

        result.ShouldBe(expectedGroups);
    }
}