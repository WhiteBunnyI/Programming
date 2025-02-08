namespace Practice_2
{
    public class ConsoleColor : IDisposable
    {
        private System.ConsoleColor m_prevColor;
        public ConsoleColor(System.ConsoleColor color)
        {
            m_prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }
        public void Dispose()
        {
            Console.ForegroundColor = m_prevColor;
        }
    }
    internal class Program
    {
        static void Main()
        {
            using(new ConsoleColor(System.ConsoleColor.DarkGreen))
            {
                using(new ConsoleColor(System.ConsoleColor.Blue))
                {
                    Console.WriteLine("Some other text");
                }
                Console.WriteLine("Hello, World!");
            }
            Console.WriteLine("End of program!");
        }
    }
}
