using DayTests.Shared;
using Shouldly;

namespace Shared.Math.Tests;

public class LeastCommonMultipleTests
{
    [Fact]
    public void CalculateLCM_Int_ExpectedResult()
    {
        var result = LeastCommonMultiple.CalculateLCM(9, 28);
        result.ShouldBe(252);
    }
    
    [Fact]
    public void CalculateLCM_Long_ExpectedResult()
    {
        var result = LeastCommonMultiple.CalculateLCM((long)9, 28);
        result.ShouldBe(252);
    }
    
    [Fact]
    public void CalculateLCM_IntArray_ExpectedResult()
    {
        var result = LeastCommonMultiple.CalculateLCM(new[] { 9, 28, 139 });
        result.ShouldBe(35028);
    }
    
    [Fact]
    public void CalculateLCM_LongArray_ExpectedResult()
    {
        var result = LeastCommonMultiple.CalculateLCM(new long[] { 9, 28, 139 });
        result.ShouldBe(35028);
    }
}