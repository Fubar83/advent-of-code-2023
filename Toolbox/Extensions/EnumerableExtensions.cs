namespace Toolbox.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<List<T>> Split<T>(this IEnumerable<T> source, Func<T, bool> condition)
    {
        var currentGroup = new List<T>();

        foreach (var item in source)
        {
            if (condition(item))
            {
                if (currentGroup.Count > 0)
                {
                    yield return currentGroup;
                    currentGroup = new List<T>();
                }
            }
            else
            {
                currentGroup.Add(item);
            }
        }

        if (currentGroup.Count > 0)
        {
            yield return currentGroup;
        }
    }

    public static int CountDifferences<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        var differences = 0;
        using var firstEnumerator = first.GetEnumerator();
        using var secondEnumerator = second.GetEnumerator();

        var hasFirst = firstEnumerator.MoveNext();
        var hasSecond = secondEnumerator.MoveNext();

        while (hasFirst && hasSecond)
        {
            if (!EqualityComparer<T>.Default.Equals(firstEnumerator.Current, secondEnumerator.Current))
            {
                differences++;
            }

            hasFirst = firstEnumerator.MoveNext();
            hasSecond = secondEnumerator.MoveNext();
        }

        if ((hasFirst && !hasSecond) || (!hasFirst && hasSecond))
        {
            differences++;
        }

        while (firstEnumerator.MoveNext())
        {
            differences++;
        }

        while (secondEnumerator.MoveNext())
        {
            differences++;
        }

        return differences;
    }
}