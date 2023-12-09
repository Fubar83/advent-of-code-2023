using System.Numerics;

namespace DayTests.Shared;

public static class Extrapolate
{
    public static T CalculateNextValue<T>(IReadOnlyCollection<T> values) where T : INumber<T>
    {
        var rows = InitializeRows(values);

        while (HasNonZeroValues(rows.Last()))
        {
            var lastRow = rows.Last();
            var nextRow = CalculateNextRow(lastRow);
            rows.Add(nextRow);
        }

        CalculateFinalValues(rows);

        return rows[0].Last();
    }

    public static T CalculatePreviousValue<T>(IReadOnlyCollection<T> values) where T : INumber<T>
    {
        var rows = InitializeRows(values);

        while (HasNonZeroValues(rows.Last()))
        {
            var lastRow = rows.Last();
            var nextRow = CalculateNextRow(lastRow);
            rows.Add(nextRow);
        }

        rows.Last().Add(T.Zero);
        CalculatePreviousValues(rows);

        return rows[0].First();
    }

    private static List<List<T>> InitializeRows<T>(IReadOnlyCollection<T> values) where T : INumber<T>
    {
        return new List<List<T>> { values.ToList() };
    }

    private static bool HasNonZeroValues<T>(List<T> row) where T : INumber<T>
    {
        return row.Any(x => x != T.Zero);
    }

    private static List<T> CalculateNextRow<T>(List<T> lastRow) where T : INumber<T>
    {
        var next = new List<T>();
        for (var i = 0; i < lastRow.Count - 1; i++)
        {
            next.Add(lastRow[i + 1] - lastRow[i]);
        }

        return next;
    }

    private static void CalculateFinalValues<T>(List<List<T>> rows) where T : INumber<T>
    {
        for (var i = rows.Count - 2; i >= 0; i--)
        {
            var row = rows[i];
            var nextRow = rows[i + 1];
            row.Add(row.Last() + nextRow.Last());
        }
    }

    private static void CalculatePreviousValues<T>(List<List<T>> rows) where T : INumber<T>
    {
        for (var i = rows.Count - 2; i >= 0; i--)
        {
            var row = rows[i];
            var nextRow = rows[i + 1];

            var prev = row[0] - nextRow[0];
            row.Insert(0, prev);
        }
    }
}