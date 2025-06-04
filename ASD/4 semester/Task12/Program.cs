namespace Task12
{
    internal class Program
    {
        static void Print<T>(IEnumerable<T> lst)
        {
            foreach (var item in lst)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }

        static void Main()
        {
            const int BACKPACK_WEIGHT = 15;

            List<(int weight, int value)> items = new()
            {
                (2, 10),
                (5, 20),
                (10, 30),
            };

            int[,] dp = new int[items.Count + 1, BACKPACK_WEIGHT + 1];

            for (int w = 1; w <= BACKPACK_WEIGHT; w++)
            {
                for (int i = 1; i <= items.Count; i++)
                {
                    var item = items[i - 1];
                    if (item.weight > w)
                    {
                        dp[i, w] = dp[i - 1, w];
                        continue;
                    }

                    int prevValue = dp[i - 1, w];
                    int maxValuePlusCurrent = dp[i - 1, w - item.weight] + item.value;

                    dp[i, w] = Math.Max(prevValue, maxValuePlusCurrent);
                }
            }

            for (int i = 0; i <= items.Count; i++)
            {
                for (int w = 0; w <= BACKPACK_WEIGHT; w++)
                {
                    Console.Write($"{dp[i, w]},\t");
                }
                Console.WriteLine();
            }

            int index = items.Count;
            int weight = BACKPACK_WEIGHT;

            List<int> selectedItems = new();
            int backpackValue = 0;
            while (index > 0 && weight > 0)
            {
                if (dp[index, weight] != dp[index - 1, weight])
                {
                    selectedItems.Add(index - 1);
                    var item = items[index - 1];
                    weight -= item.weight;
                    backpackValue += item.value;
                }
                index--;
            }
            selectedItems.Reverse();

            Console.WriteLine();
            Console.Write($"Взятые предметы: "); Print(selectedItems);
            Console.WriteLine($"Ценность рюкзака: {backpackValue}");
        }
    }
}
