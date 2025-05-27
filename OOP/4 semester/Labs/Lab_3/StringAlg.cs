namespace Lab_3;

public static class StringAlg
{
    public static bool PrefixAlg(this string text, string pattern)
    {
        int[] prefics = PreficsFunc(pattern);
        int j = 0;
        for (int i = 0; i < text.Length; i++)
        {
            while (j > 0 && text[i] != pattern[j])
            {
                j = prefics[j - 1];
            }
            if (text[i] == pattern[j])
            {
                j++;
            }
            if (j == pattern.Length)
            {
                return true;
            }
        }

        return false;
    }

    static int[] PreficsFunc(string word)
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
