using System.Numerics;

namespace DayTests.Shared;

public static class LeastCommonMultiple
{
    public static T CalculateLCM<T>(T a, T b) where T : INumber<T>
    {
        return T.Abs(a * b) / EuclideanAlgorithm(a, b);
    }

    private static T EuclideanAlgorithm<T>(T a, T b) where T : INumber<T>
    {
        while (b != T.Zero)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public static T CalculateLCM<T>(T[] numbers) where T : INumber<T>
    {
        if (numbers.Length < 2)
        {
            throw new Exception("Cant calculate ");
        }

        var lcm = CalculateLCM(numbers[0], numbers[1]);

        for (var i = 2; i < numbers.Length; i++)
        {
            lcm = CalculateLCM(lcm, numbers[i]);
        }

        return lcm;
    }
}