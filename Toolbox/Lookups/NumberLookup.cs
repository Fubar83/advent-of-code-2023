namespace Toolbox.Lookups;

public class NumberLookup
{
    private static readonly Dictionary<string, int> _numberLookup = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };

    public static int Convert(string str)
    {
        if (_numberLookup.TryGetValue(str, out var result))
        {
            return result;
        }

        throw new ArgumentException($"{str} can not be converted to a integer");
    }
}