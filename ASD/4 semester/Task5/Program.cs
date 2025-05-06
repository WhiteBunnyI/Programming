namespace Task5
{
    internal class Program
    {
        static void Main()
        {
            if (!File.Exists("../../../Text.txt"))
            {
                var file = File.Create("../../../Text.txt");
                file.Close();
            }
            string text = File.ReadAllText("../../../Text.txt");

            Console.WriteLine("Введите образец для поиска: ");
            string? pattern = Console.ReadLine();

            if (pattern == null || pattern.Length == 0)
            {
                Console.WriteLine("Некорректный образец");
                return;
            }

            var badChar = BadChar(pattern);
            var goodSuffics = GoodSuffics(pattern);
            //var goodSuffics = GoodSuffics("aaccbccbcc");
            //var goodSuffics = GoodSuffics("колокол");

            foreach (var i in badChar)
                Console.Write($"{i} ");
            Console.WriteLine();

            foreach (var i in goodSuffics)
                Console.Write($"{i} ");
            Console.WriteLine();

            int state = 0;
            for(int i = 0; i < text.Length - pattern.Length + 1; i++)
            {
                for(int j = pattern.Length - 1; j >= 0; j--)
                {
                    char text_ch = text[i + j];
                    if (text_ch != pattern[j])
                    {
                        int shift;
                        if (state == 0)
                            shift = badChar.TryGetValue(text_ch, out shift) ? shift : -1;
                        else
                            shift = goodSuffics[state];

                        i += pattern.Length - shift;
                        state = 0;
                        break;
                    }

                    state++;
                    if(state == pattern.Length)
                    {
                        Console.WriteLine("Подслово было найдено! Его позиция: " + i);
                        i += pattern.Length - 1 - goodSuffics[state];
                        state = 0;
                    }
                }
            }
        }

        static Dictionary<char, int> BadChar(string pattern)
        {
            Dictionary<char, int> result = [];

            for (int i = 0; i < pattern.Length - 1; i++)
                result[pattern[i]] = i;

            return result;
        }

        static int[] GoodSuffics(string pattern)
        {
            int[] result = new int[pattern.Length + 1];

            for (int i = pattern.Length; i >= 0; i--)
            {
                int state = 0;
                result[pattern.Length - i] = pattern.Length;
                for (int j = i == pattern.Length ? pattern.Length - 1 : pattern.Length - 2; j >= 0; j--)
                {
                    if (i < pattern.Length && pattern[pattern.Length - 1 - state] == pattern[j]) state++;
                    else state = 0;

                    if ((j - 1 < 0 && state != 0) || state == pattern.Length - i)
                    {
                        if (j - 1 < 0 || pattern[i - 1] != pattern[j - 1])
                        {
                            result[pattern.Length - i] = pattern.Length - j - state;
                            break;
                        }

                        state = 0;
                    }
                }
            }

            return result;

        }

        static int[] GoodSufics(char[] pattern)
        {
            int[] result = new int[pattern.Length + 1];
            result[pattern.Length] = pattern.Length;
            for (int i = pattern.Length; i > 0; i--)
            {
                int shift = pattern.Length;
                int state = 0;
                int maxState = pattern.Length - i;
                for (int j = i - 1; j >= 0; j--)
                {
                    char a = pattern[i - 1];
                    char b = pattern[j];
                    char c;
                    if (i - state != pattern.Length)
                    {
                        c = pattern[i - state];
                    }
                    if (state == maxState)
                    {
                        if (j == 0 || pattern[i - 1] != pattern[j])
                        {
                            shift = i - j - 1;
                            break;
                        }
                        else
                        {
                            state = 0;
                        }
                    }
                    else if (pattern[pattern.Length - 1 - state] == pattern[j])
                    {
                        state++;
                    }
                    else
                    {
                        state = 0;
                    }
                }
                result[pattern.Length - i] = shift;
            }

            return result;
        }

    }
}
