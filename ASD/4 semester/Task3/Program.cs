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
            string? sample = Console.ReadLine();

            if(sample == null || sample == "")
            {
                Console.WriteLine("Образец не найден");
                return;
            }

            int state = 0;
            for(int i = 0; i < text.Length; i++)
            {
                
                if (state < sample.Length && text[i] == sample[state])
                    ++state;
                else
                {
                    state = 0;
                    for(int o = 0; o < sample.Length; o++)
                    {
                        if (sample[sample.Length - 1 - o] == text[i - state])
                        {
                            state++;
                        }
                        else
                        {
                            state = 0;
                        }

                    }
                }
                if (state == sample.Length)
                {
                    Console.WriteLine("Подслово было найдено! Его позиция: " + (i - state + 1));
                }
            }
        }
    }

    
}
