namespace Task8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> nominals = [1, 2, 5];
            int collect = 5;
            int test = RecursiveCalculateCount(nominals, collect);
            int result = CalculateCount(nominals, collect);
            Console.WriteLine((result == test) + " " + result.ToString());

            collect = 7;
            test = RecursiveCalculateCount(nominals, collect);
            result = CalculateCount(nominals, collect);
            Console.WriteLine((result == test) + " " + result.ToString());

            collect = 9;
            test = RecursiveCalculateCount(nominals, collect);
            result = CalculateCount(nominals, collect);
            Console.WriteLine((result == test) + " " + result.ToString());

            for(int i = 10; i < 50; i++)
            {
                //Console.WriteLine(i + "| R:" + RecursiveCalculateCount(nominals, i) + " D:" + CalculateCount(nominals, i));
            }

            collect = 100;
            //test = RecursiveCalculateCount(nominals, collect);
            result = CalculateCount(nominals, collect);
            Console.WriteLine(result);

            collect = 8;
            test = RecursiveCalculateCount(nominals, collect);
            result = CalculateCount(nominals, collect);
            Console.WriteLine(result + " " + test);
        }

        public static int CalculateCount(List<int> nominals, int needToCollect)
        {
            int[] cache = new int[needToCollect + 1];
            cache[0] = 1;

            for (int j = 0; j < nominals.Count; j++)
            {
                for (int i = 1; i <= needToCollect; i++)
                {
                    if (nominals[j] > i) continue;

                    cache[i] += cache[i - nominals[j]];
                }
                
            }

            return cache[needToCollect];
        }
        //Переделать
        public static int RecursiveCalculateCount(List<int> nominals, int needToCollect)
        {
            int func(ref List<int> n, int t, int c = 0)
            {
                if (c == t) return 1;
                if (c > t) return 0;

                int result = 0;
                for(int i = 0; i < n.Count; i++)
                {
                    result += func(ref n, t, c + n[i]);
                }
                return result;
            }

            return func(ref nominals, needToCollect);
        }
    }
}
