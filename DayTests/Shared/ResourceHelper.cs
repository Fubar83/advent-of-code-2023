using System.Reflection;

namespace DayTests.Shared;

public class ResourceHelper
{
    private readonly Assembly _assembly;
    private readonly string _ns;

    private ResourceHelper(Assembly assembly, string ns)
    {
        _assembly = assembly;
        _ns = ns;
    }

    public static ResourceHelper ForAssembly<T>()
    {
        var type = typeof(T);

        return new ResourceHelper(type.Assembly, type.Namespace!);
    }

    public IEnumerable<List<string?>> ReadGroupOfLines(string name)
    {
        var group = new List<string?>();
        foreach (var line in ReadLines(name))
        {
            if (string.IsNullOrEmpty(line))
            {
                yield return group;
                group.Clear();
            }
            else
            {
                group.Add(line);
            }
        }
    }

    public IEnumerable<string> ReadLines(string name)
    {
        var resourceName = _assembly.GetManifestResourceNames()
            .FirstOrDefault(x => x == $"{_ns}.{name}");
        if (string.IsNullOrEmpty(resourceName))
        {
            yield break;
        }

        using var stream = _assembly.GetManifestResourceStream(resourceName)!;
        using var reader = new StreamReader(stream);
        while (reader.ReadLine() is { } row)
        {
            yield return row;
        }
    }
}