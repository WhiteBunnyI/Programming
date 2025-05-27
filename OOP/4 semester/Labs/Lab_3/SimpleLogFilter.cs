namespace Lab_3;

internal class SimpleLogFilter : ILogFilter
{
    string m_pattern;
    public SimpleLogFilter(string pattern)
    {
        m_pattern = pattern;
    }

    public bool Match(string message)
    {
        return message.PrefixAlg(m_pattern);
    }
}
