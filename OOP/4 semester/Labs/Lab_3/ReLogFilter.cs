using System.Text.RegularExpressions;
namespace Lab_3;

internal class ReLogFilter : ILogFilter
{
    Regex m_regex;

    public ReLogFilter(string pattern)
    {
        m_regex = new Regex(pattern);
    }

    public bool Match(string message)
    {
        return m_regex.IsMatch(message);
    }
}
