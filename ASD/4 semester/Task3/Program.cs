namespace Task3;

public class Program
{
    public static List<Dictionary<char, int>> Calculate(ref List<char> unique_chars, string pattern)
    {
        List<Dictionary<char, int>> result = new(pattern.Length);

        for(int state = 0; state <= pattern.Length; state++)
        {
            result.Add(new Dictionary<char, int>(pattern.Length + 1));

            for (int i = 0; i < unique_chars.Count; i++)
            {
                char c = unique_chars[i];

                if (state != pattern.Length && pattern[state] == c)
                {
                    result[state].Add(c, state + 1);
                    continue;
                }

                int newState = 0;
                string newPattern = state == pattern.Length ? pattern + ' ' : pattern;
                for (int o = 0; o <= state; o++)
                {
                    if ((newState == 0 && c == newPattern[state - o]) ||
                        (newState != 0 && newPattern[state - newState] == newPattern[state - o]))
                    {
                        newState++;
                    }
                    else
                    {
                        newState = 0;
                    }
                }

                result[state].Add(c, newState);
            }
        }

        return result;
    }

    static void Main()
    {
        string filepath = "../../../Text.txt";
        if (!File.Exists(filepath))
        {
            var file = File.Create(filepath);
            file.Close();
        }

        string text = File.ReadAllText(filepath);

        Console.WriteLine("Введите образец для поиска: ");
        string? pattern = Console.ReadLine();

        if (pattern == null || pattern == "")
        {
            Console.WriteLine("Образец не найден");
            return;
        }


        List<char> chars = new(pattern.Length);

        for (int i = 0; i < pattern.Length; i++)
            if (!chars.Contains(pattern[i]))
                chars.Add(pattern[i]);

        int state = 0;
        var states = Calculate(ref chars, pattern);

/*        foreach (var i in states)
            foreach(var j in i)
                Console.WriteLine($"{j.Key}: {j.Value}");*/

        for(int i = 0; i < text.Length; i++)
        {
            if (!chars.Contains(text[i]))
            {
                state = 0;
                continue;
            }

            state = states[state][text[i]];

            if (state == pattern.Length)
            {
                Console.WriteLine("Подслово было найдено! Его позиция: " + (i - state + 1));
            }
        }
    }
}


