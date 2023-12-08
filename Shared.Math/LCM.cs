using System.Numerics;

namespace DayTests.Shared;

/// <summary>
///     L Common Multiple helpers
/// </summary>
public static class LCM
{
    public static T Calculate<T>(T a, T b) where T : INumber<T>
    {
        return T.Abs(a * b) / GCD.Calculate(a, b);
    }

    public static T Calculate<T>(T[] numbers) where T : INumber<T>
    {
        if (numbers.Length < 2)
        {
            throw new Exception("Cant calculate LCM");
        }

        var lcm = Calculate(numbers[0], numbers[1]);

        for (var i = 2; i < numbers.Length; i++)
        {
            lcm = Calculate(lcm, numbers[i]);
        }

        return lcm;
    }
}