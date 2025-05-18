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
        int[] prefics = PreficsFunc(m_pattern);
        int j = 0;
        for (int i = 0; i < message.Length; i++)
        {
            while (j > 0 && message[i] != m_pattern[j])
            {
                j = prefics[j - 1];
            }
            if (message[i] == m_pattern[j])
            {
                j++;
            }
            if (j == m_pattern.Length)
            {
                return true;
                j = prefics[j - 1];
            }
        }

        return false;
    }

    int[] PreficsFunc(string word)
    {
        int[] result = new int[word.Length];
        int j = 0;
        for (int i = 1; i < word.Length; i++)
        {
            while (j > 0 && word[i] != word[j])
                j = result[j - 1];

            if (word[i] == word[j])
                j++;

            result[i] = j;
        }
        return result;
    }
}
