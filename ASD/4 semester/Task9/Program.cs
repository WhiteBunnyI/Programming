namespace Task9
{
    internal class Program
    {
        static void Main()
        {
            int[,] matrix = GetMatrix("../../../Path.csv");

            int n = matrix.GetLength(0);
            int INF = int.MaxValue;

            int[,] dp = new int[1 << n, n];
            int[,] prev = new int[1 << n, n];
            for (int mask = 0; mask < (1 << n); mask++)
            {
                for (int i = 0; i < n; i++)
                {
                    dp[mask, i] = INF;
                    prev[mask, i] = -1;
                }
            }
            dp[1, 0] = 0; // Старт в городе 0

            // Заполнение DP и prev
            for (int mask = 1; mask < (1 << n); mask++)
            {
                for (int i = 0; i < n; i++)
                {
                    if ((mask & (1 << i)) == 0)
                        continue;

                    int prevMask = mask ^ (1 << i);
                    for (int j = 0; j < n; j++)
                    {
                        if (j == i || (prevMask & (1 << j)) == 0 || matrix[j, i] == INF || dp[prevMask, j] == INF)
                            continue;

                        int newCost = dp[prevMask, j] + matrix[j, i];
                        if (newCost < dp[mask, i])
                        {
                            dp[mask, i] = newCost;
                            prev[mask, i] = j; // Запоминаем предыдущий город
                        }
                    }
                }
            }

            // Поиск минимальной стоимости и конечного города
            int minCost = INF;
            int lastCity = -1;
            for (int i = 0; i < n; i++)
            {
                if (i == 0 || matrix[i, 0] == INF || dp[1 << n - 1, i] == INF)
                    continue;

                int totalCost = dp[1 << n - 1, i] + matrix[i, 0];
                if (totalCost < minCost)
                {
                    minCost = totalCost;
                    lastCity = i;
                }
            }

            if (minCost == INF)
            {
                Console.WriteLine($"Путь не был найден!");
                return;
            }

            // Восстановление пути
            List<int> path = [];
            int currentCity = lastCity;
            int currentMask = 1 << n - 1;

            while (currentCity != -1)
            {
                path.Add(currentCity);
                int previousCity = prev[currentMask, currentCity];
                if (previousCity == -1)
                    break;

                currentMask ^= (1 << currentCity); // Удаляем текущий город из маски
                currentCity = previousCity;
            }

            path.Reverse();
            path.Insert(0, 0); // Добавляем стартовый город в начало
            path.Add(0);       // Замыкаем цикл

            Console.WriteLine($"Стоимость: {minCost}, путь: {string.Join(" → ", path)}");
        }

        static int[,] GetMatrix(string filepath)
        {
            string[] text = GetLines(filepath);
            int[,] matrix = new int[text.Length, text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                var nums = text[i].Split(';');
                for (int j = 0; j < nums.Length; j++)
                {
                    int num = Convert.ToInt32(nums[j]);
                    matrix[i, j] = num;
                    
                    if(num == 0) matrix[i, j] = int.MaxValue;
                }
            }

            return matrix;
        }

        static string[] GetLines(string filepath)
        {
            if (!File.Exists(filepath))
            {
                var file = File.Create(filepath);
                file.Close();
            }

            return File.ReadAllLines(filepath);
        }
    }
}
