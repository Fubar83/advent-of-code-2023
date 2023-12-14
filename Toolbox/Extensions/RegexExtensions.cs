using System.Text.RegularExpressions;

namespace Toolbox.Extensions;

public static class RegexExtensions
{
    public static IEnumerable<Match> Matches(this Regex regex, string input, bool overlap)
    {
        if (!overlap)
        {
            foreach (Match match in regex.Matches(input))
            {
                yield return match;
            }
        }
        else
        {
            var match = regex.Match(input);
            while (match.Success)
            {
                yield return match;
                match = regex.Match(input, match.Index + 1);
            }
        }
    }
}