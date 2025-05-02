namespace Task3
{
    public class Program
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

            if(pattern == null || pattern == "")
            {
                Console.WriteLine("Образец не найден");
                return;
            }

            int state = 0;
            for(int i = 0; i < text.Length; i++)
            {
                if (state < pattern.Length && text[i] == pattern[state])
                    state++;
                else
                {
                    state = 0;
                    for(int o = 0; o < pattern.Length; o++)
                    {
                        if (text[i - state] == pattern[pattern.Length - 1 - o])
                        {
                            state++;
                        }
                        else
                        {
                            state = 0;
                        }
                    }
                }

                if (state == pattern.Length)
                {
                    Console.WriteLine("Подслово было найдено! Его позиция: " + (i - state + 1));
                }
            }
        }
    }

    
}
