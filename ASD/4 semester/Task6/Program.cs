namespace Task6
{
    internal class Program
    {
        static uint Base = 26;

        static void Main()
        {
            string filepath = "../../../Text.txt";
            string pattern = "bab";
            string text = GetText(filepath);

            ulong cachedBasePow = CalculatePow(pattern);
            ulong patternHash = GetHash(pattern);
            ulong cutHash = GetHash(text[..pattern.Length]);

            for(int i = 1; i < text.Length - pattern.Length + 2; i++)
            {
                if(cutHash == patternHash)
                {
                    int j = 0;
                    for(; j < pattern.Length; j++)
                    {
                        if (pattern[j] != text[i - 1 + j])
                            break;
                    }
                    if (j == pattern.Length)
                        Console.WriteLine($"Подслово найдено: {i - 1}");
                }
                if (i == text.Length - pattern.Length + 1) break;

                cutHash = GetHashOptimized(cutHash, cachedBasePow, text[i - 1], text[pattern.Length - 1 + i]);
                
            }

        }
        static string GetText(string filepath)
        {
            if (!File.Exists(filepath))
            {
                var file = File.Create(filepath);
                file.Close();
            }

            return File.ReadAllText(filepath).ToLower();
        }

        static ulong CalculatePow(string pattern)
        {
            ulong past = 1;

            for (int i = 1; i < pattern.Length; i++)
                past *= Base;

            return past;
        }
            
        static ulong GetHash(string text)
        {
            ulong hash = 0;

            for(int i = 0; i < text.Length; i++)
            {
                hash = hash * Base + RebaseChar(text[i]);
            }

            return hash;
        }

        static ulong GetHashOptimized(ulong prevHash, ulong cachedBasePow, char firstChar, char newChar)
        {
            prevHash -= RebaseChar(firstChar) * cachedBasePow;
            prevHash *= Base;
            prevHash += RebaseChar(newChar);

            return prevHash;
        }

        static ulong RebaseChar(char chr) => (ulong)(chr - 97);

    }
}
