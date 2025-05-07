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
            var goodSuffix = GoodSuffics(pattern);
            //var goodSuffics = GoodSuffics("aaccbccbcc");
            //var goodSuffics = GoodSuffics("колокол");

            Console.Write("Таблица плохого символа:   ");
            foreach (var i in badChar)
                Console.Write($"{i} ");
            Console.WriteLine();

            Console.Write("Таблица хорошего суффикса: ");
            foreach (var i in goodSuffix)
                Console.Write($"{i} ");
            Console.WriteLine();

            int state;
            for (int i = 0; i <= text.Length - pattern.Length;)
            {
                int j;
                for (j = pattern.Length - 1; j >= 0; j--)
                {
                    if (text[i + j] != pattern[j])
                        break;
                }

                state = pattern.Length - j - 1;

                int shift;
                if (state == pattern.Length)
                {
                    Console.WriteLine("Подслово было найдено! Его позиция: " + i);
                    shift = (pattern.Length > 1) ? goodSuffix[state] : 1;
                }
                else
                {
                    int shiftBadChar;
                    int shiftGoodSuffix;
                    if (badChar.TryGetValue(text[i + j], out int bcShift))
                        shiftBadChar = j - bcShift;
                    else
                        shiftBadChar = j + 1;

                    shiftGoodSuffix = goodSuffix[state];

                    shift = Math.Max(shiftBadChar, shiftGoodSuffix);
                }

                i += shift;
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

    }
}
