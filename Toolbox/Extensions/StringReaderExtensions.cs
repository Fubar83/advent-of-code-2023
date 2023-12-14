using System.Text;

namespace Toolbox.Extensions;

public static class StringReaderExtensions
{
    public static string ReadUntil(this StringReader reader, params char[] delimiters)
    {
        return ReadUntil(reader, out _, delimiters);
    }
    public static string ReadUntil(this StringReader reader, out char delimiter, params char[] delimiters)
    {
        delimiter = default;
        
        var result = new StringBuilder();

        int nextChar;
        while ((nextChar = reader.Read()) != -1)
        {
            var c = (char)nextChar;

            if (delimiters.Contains(c))
            {
                delimiter = c;
                break;
            }

            result.Append(c);
        }

        
        return result.ToString();
    }
    
}