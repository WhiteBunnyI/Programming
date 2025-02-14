using System.Runtime.CompilerServices;

namespace Practice_2
{
    public class ConsolePrint : IDisposable
    {
        public enum Size
        {
            One,
            Four,
            Eight,
        }
        private System.ConsoleColor m_prevColor;
        private Size m_size;
        public ConsolePrint(Size size = Size.One, System.ConsoleColor color)
        {
            m_prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            m_size = size;
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
            using(new ConsolePrint(System.ConsoleColor.DarkGreen))
            {
                using(new ConsolePrint(System.ConsoleColor.Blue))
                {
                    Console.WriteLine("Some other text");
                }
                Console.WriteLine("Hello, World!");
            }
            Console.WriteLine("End of program!");
        }
    }
}
