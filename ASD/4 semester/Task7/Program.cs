namespace Task7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = [-5, -3, -2];
            List<int> result = [-2];
            Console.WriteLine(IsEqual(Cadan(numbers), result));

            numbers = [-2, 1, -3, 4];
            result = [4];
            Console.WriteLine(IsEqual(Cadan(numbers), result));

            numbers = [2, -1, 3];
            result = [2, -1, 3];
            Console.WriteLine(IsEqual(Cadan(numbers), result));

        }

        public static bool IsEqual(List<int> a, List<int> b)
        {
            if(a.Count != b.Count) return false;

            for(int i = 0; i < a.Count; i++)
            {
                if (a[i] != b[i]) return false;
            }

            return true;
        }

        public static List<int> Cadan(List<int> numbers)
        {
            int l = 0;
            int r = 0;
            int lMax = 0;
            int rMax = 0;
            int maxSum = int.MinValue;
            int currentSum = 0;
            
            for (int i = 0; i < numbers.Count; i++)
            {
                currentSum += numbers[i];
                r = i;
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    lMax = l;
                    rMax = r;
                }

                if (currentSum < 0)
                {
                    l = i + 1;
                    currentSum = 0;
                }
                
            }

            return numbers.GetRange(lMax, rMax - lMax + 1);
        }
    }
}
