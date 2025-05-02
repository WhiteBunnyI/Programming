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
            //var goodSufics = GoodSufics(pattern);
            //var goodSufics = GoodSufics("aaccbccbcc");
            var goodSufics = GoodSufics("колокол");
            foreach (var i in goodSufics)
            {
                Console.WriteLine(i);
            }

        }

        static Dictionary<char, int> BadChar(string pattern)
        {
            Dictionary<char, int> result = [];

            for (int i = 0; i < pattern.Length - 1; i++)
                result[pattern[i]] = i;

            return result;
        }

        static int[] GoodSufics(string pattern)
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
