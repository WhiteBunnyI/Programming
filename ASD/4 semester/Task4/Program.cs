namespace Task4
{
    internal class Program
    {   //abab
        static int[] PreficsFunc(string word)
        {
            int[] result = new int[word.Length];
            int j = 0;
            for(int i = 1; i < word.Length; i++)
            {
                while (j > 0 && word[i] != word[j])
                    j = result[j - 1];

                if (word[i] == word[j])
                    j++;

                result[i] = j;
            }
            return result;
        }
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

            if(pattern == null || pattern.Length == 0)
            {
                Console.WriteLine("Некорректный образец");
                return;
            }

            int[] prefics = PreficsFunc(pattern);
            int j = 0;
            for(int i = 0; i < text.Length; i++)
            {
                while(j > 0 && text[i] != pattern[j])
                {
                    j = prefics[j - 1];
                }
                if (text[i] == pattern[j])
                {
                    j++;
                }
                if(j == pattern.Length)
                {
                    Console.WriteLine("Подслово было найдено! Его позиция: " + (i - j + 1));
                    j = prefics[j - 1];
                }
                //Console.WriteLine("i: " + i + " j: " + j);
            }
        }
    }
}
