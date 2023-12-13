using Shouldly;

namespace Toolbox.Tests;

public class LCMTests
{
    [Fact]
    public void Calculate_Int_ExpectedResult()
    {
        var result = LCM.Calculate(9, 28);
        result.ShouldBe(252);
    }

    [Fact]
    public void Calculate_Long_ExpectedResult()
    {
        var result = LCM.Calculate((long)9, 28);
        result.ShouldBe(252);
    }

    [Fact]
    public void Calculate_IntArray_ExpectedResult()
    {
        var result = LCM.Calculate(new[] { 9, 28, 139 });
        result.ShouldBe(35028);
    }

    [Fact]
    public void Calculate_LongArray_ExpectedResult()
    {
        var result = LCM.Calculate(new long[] { 9, 28, 139 });
        result.ShouldBe(35028);
    }
}