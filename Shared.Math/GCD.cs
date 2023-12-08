using System.Numerics;

namespace DayTests.Shared;

/// <summary>
///     Greatest Common Divisor helpers
/// </summary>
public class GCD
{
    public static T Calculate<T>(T a, T b) where T : INumber<T>
    {
        while (b != T.Zero)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public static T Calculate<T>(T[] numbers) where T : INumber<T>
    {
        if (numbers.Length < 2)
        {
            throw new Exception("Cant calculate LCM");
        }

        var gcd = Calculate(numbers[0], numbers[1]);

        for (var i = 2; i < numbers.Length; i++)
        {
            gcd = Calculate(gcd, numbers[i]);
        }

        return gcd;
    }
}