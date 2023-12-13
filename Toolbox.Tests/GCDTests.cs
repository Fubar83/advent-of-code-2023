using Shouldly;

namespace Toolbox.Tests;

public class GCDTests
{
    [Fact]
    public void Calculate_Int_ExpectedResult()
    {
        var result = GCD.Calculate(164, 88);
        result.ShouldBe(4);
    }

    [Fact]
    public void Calculate_Long_ExpectedResult()
    {
        var result = GCD.Calculate((long)164, 88);
        result.ShouldBe(4);
    }

    [Fact]
    public void Calculate_IntArray_ExpectedResult()
    {
        var result = GCD.Calculate(new[] { 24, 36, 48, 60 });
        result.ShouldBe(12);
    }

    [Fact]
    public void Calculate_LongArray_ExpectedResult()
    {
        var result = GCD.Calculate(new long[] { 24, 36, 48, 60 });
        result.ShouldBe(12);
    }
}