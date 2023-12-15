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
    
    public static void SetColumn<T>(this T[,] array, int colIndex, T[] newColumn)
    {
        var rows = Math.Min(array.GetLength(0), newColumn.Length);

        for (var rowIndex = 0; rowIndex < rows; rowIndex++)
        {
            array[rowIndex, colIndex] = newColumn[rowIndex];
        }
    }
    
    public static void SetRow<T>(this T[,] array, int rowIndex, T[] newRow)
    {
        var cols = Math.Min(array.GetLength(1), newRow.Length);

        for (var colIndex = 0; colIndex < cols; colIndex++)
        {
            array[rowIndex, colIndex] = newRow[colIndex];
        }
    }
    
}