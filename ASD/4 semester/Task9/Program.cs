namespace Task9
{
    internal class Program
    {
        static (int bestCost, List<int> bestPath) SolveDP(int[,] dist)
        {
            int n = dist.GetLength(0);

            int m = n - 1; //вершины от 1 до n-1 участвуют в маске
            int maxMask = 1 << m;

            const int INF = int.MaxValue / 2;
            int[,] dp = new int[maxMask, m];
            int[,] parent = new int[maxMask, m];

            //Инициализируем всё значением INF.
            for (int mask = 0; mask < maxMask; mask++)
            {
                for (int j = 0; j < m; j++)
                {
                    dp[mask, j] = INF;
                    parent[mask, j] = -1;
                }
            }

            //Рассматриваем базовые случаи: S = {j} - т.е. цикл из 1 вершины
            for (int j = 0; j < m; j++)
            {
                int bit = 1 << j;
                dp[bit, j] = dist[0, j + 1];
                parent[bit, j] = 0; // «пришли» из вершины 0
            }


            for (int mask = 1; mask < maxMask; mask++)
            {
                for (int j = 0; j < m; j++)
                {
                    int bitJ = 1 << j;
                    if ((mask & bitJ) == 0) // j не входит в mask → пропустить
                        continue;

                    int prevMask = mask ^ bitJ; // подмножество без j
                    if (prevMask == 0) // Это было базовое состояние, уже инициализировано
                        continue;

                    // Ищём, из какой предыдущей вершины k было выгоднее всего перейти в j
                    for (int k = 0; k < m; k++)
                    {
                        int bitK = 1 << k;
                        if ((prevMask & bitK) == 0)
                            continue;

                        int costCandidate = dp[prevMask, k];
                        if (costCandidate >= INF)
                            continue;

                        int newCost = costCandidate + dist[k + 1, j + 1];
                        if (newCost < dp[mask, j])
                        {
                            dp[mask, j] = newCost;
                            parent[mask, j] = k + 1;
                        }
                    }
                }
            }

            int fullMask = maxMask - 1; // маска, в которой все биты = 1
            int bestCost = INF;
            int lastVertex = -1;

            for (int j = 0; j < m; j++)
            {
                int costEndingHere = dp[fullMask, j] + dist[j + 1, 0];
                if (costEndingHere < bestCost)
                {
                    bestCost = costEndingHere;
                    lastVertex = j + 1;
                }
            }

            List<int> bestPath = [0];

            if (lastVertex == -1)
                return (0, bestPath);

            int currVertex = lastVertex;
            int currMask = fullMask;

            Stack<int> stack = new();
            stack.Push(currVertex);

            // Восстанавливаем маршрут обратно
            for (int i = 0; i < m - 1; i++)
            {
                int idx = currVertex - 1;
                int prevVertex = parent[currMask, idx];
                currMask ^= (1 << idx);

                if (prevVertex == 0)
                {
                    stack.Push(0);
                    break;
                }

                stack.Push(prevVertex);
                currVertex = prevVertex;
            }

            while (stack.Count > 0)
            {
                int v = stack.Pop();
                if (v != 0)
                    bestPath.Add(v);
            }

            bestPath.Add(0);

            return (bestCost, bestPath);
        }

        static void Main()
        {
            string tablePath = "../../../Path.csv";
            int[,] dist = GetMatrix(tablePath);
            var result = SolveDP(dist);

            Console.WriteLine($"Стоимость: {result.bestCost}");
            Console.Write("Цикл: ");
            Print(result.bestPath);
        }



        static (int bestPath, List<int> path) GetBestPath(string tablePath)
        {
            int[,] matrix = GetMatrix(tablePath);

            int n = matrix.GetLength(0);

            List<List<int>> cycles = [];
            for (int i = 1; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                    cycles.Add([0, i, j]);

            List<int> best = [];
            int path = int.MaxValue;

            while (true)
            {
                path = int.MaxValue;
                foreach (var cycle in cycles)
                {
                    int calculated = CalculatePath(matrix, cycle);
                    if (calculated < path)
                    {
                        best = cycle;
                        path = calculated;
                    }
                }

                if (best.Count == matrix.GetLength(0))
                    break;

                int minInt = GetMinUnused(best, n);
                cycles = [];
                for (int i = 1; i <= best.Count; i++)
                {
                    List<int> buffer = new(best);
                    buffer.Insert(i, minInt);
                    cycles.Add(buffer);
                }
            }

            best.Add(0);

            Console.Write($"Цикл: "); Print(best);
            Console.WriteLine($"Стоимость: {path}");

            return (path, best);
        }

        static int GetMinUnused(List<int> lst, int count)
        {
            List<int> all = [];
            for(int i = 0; i < count; i++)
                all.Add(i);

            foreach (var i in lst)
                all.Remove(i);

            return all[0];
        }

        static int CalculatePath(int[,] matrix, List<int> cycle)
        {
            int prev = 0;
            int result = 0;
            for (int i = 1; i < cycle.Count; i++)
            {
                result += matrix[prev, cycle[i]];
                prev = cycle[i];
            }
            result += matrix[prev, 0];

            return result;
        }

        static void Print<T>(IEnumerable<T> enumerable)
        {
            foreach (T item in enumerable)
                Console.Write($"{item} ");
            Console.WriteLine();
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
