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
            var goodSufics = GoodSufics(pattern);

            foreach(var i in goodSufics)
            {
                Console.WriteLine(i);
            }

        }

        static Dictionary<char, int> BadChar(string pattern)
        {
            Dictionary<char, int> result = [];

            for(int i = 0; i < pattern.Length - 1; i++)
                result[pattern[i]] = i;

            return result;
        }

        static int[] GoodSufics(string pattern)
        {
            int[] result = new int[pattern.Length + 1];

            for(int i = pattern.Length; i >= 0; i--)
            {
                int shift = 0;
                int state = 0;
                for(int j = i-1; j >= 0; j--)
                {

                }
            }

            return result;
        }

    }
}
