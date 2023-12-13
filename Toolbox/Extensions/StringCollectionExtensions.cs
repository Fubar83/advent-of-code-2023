namespace Toolbox.Extensions;

public static class StringCollectionExtensions
{
    /// <summary>
    ///     Creates a 2D array with Y, X as dimensions
    /// </summary>
    /// <param name="lines"></param>
    /// <returns></returns>
    public static char[,] ToCharArray(this IReadOnlyCollection<string> lines)
    {
        var rows = lines.Count;
        var maxLength = lines.Max(s => s.Length);
        var charArray = new char[rows, maxLength];

        var y = 0;
        foreach (var line in lines)
        {
            for (var x = 0; x < line.Length; x++)
            {
                charArray[y, x] = line[x];
            }

            y++;
        }

        return charArray;
    }
}