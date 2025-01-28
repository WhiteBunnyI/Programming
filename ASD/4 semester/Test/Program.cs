namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\t");
            for (int i = -4; i <= 4; i++)
            {
                Console.Write(i + "\t");
            }
            Console.WriteLine();
            for (int i = -4; i <= 4; i++)
            {
                Console.Write(i + "\t");
                for(int o = -4; o <= 4; o++)
                {
                    Console.Write(((Math.Abs(i + o) % 3 == 1) ? 1 : 0) + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
