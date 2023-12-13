namespace Toolbox.Extensions;

public static class TwoDimensionArrayExtensions
{
    public static T[] GetRow<T>(this T[,] array, int rowIndex)
    {
        var cols = array.GetLength(1);
        return Enumerable.Range(0, cols)
            .Select(colIndex => array[rowIndex, colIndex])
            .ToArray();
    }

    public static T[] GetColumn<T>(this T[,] array, int colIndex)
    {
        var rows = array.GetLength(0);
        return Enumerable.Range(0, rows)
            .Select(rowIndex => array[rowIndex, colIndex])
            .ToArray();
    }
}