namespace Toolbox.Extensions;

public static class CollectionExtensions
{
    public static void BubbleSortWithUnmovable<T>(this T[] array, Func<T, bool> canMove, bool ascending = true)
    {
        for (var i = 0; i < array.Length - 1; i++)
        {
            var swapped = false;

            for (var j = 0; j < array.Length - 1 - i; j++)
            {
                if (!canMove(array[j])  || !canMove(array[j+1]))
                {
                    continue;
                }
                
                var compare = Comparer<T>.Default.Compare(array[j], array[j + 1]);
                var shouldSwap = ascending ? compare > 0 : compare < 0;

                if (shouldSwap)
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    swapped = true;
                }
            }

            if (!swapped)
            {
                break;
            }
        }
    }
}