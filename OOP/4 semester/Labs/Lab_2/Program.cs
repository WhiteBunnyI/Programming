namespace Lab_2
{
    public class ConsolePrint : IDisposable
    {
        public enum Size
        {
            One = 1,
            Five = 5,
            Seven = 7,
        }

        public enum Color
        {
            Default,
            Red,
            Green,
            Blue,
        }

        private static int ALPHABET_COUNT = 32;
        private static char SYMBOL_GRAPHIC = '#';

        private static Dictionary<Size, string> SIZE_FILE_PATH = new Dictionary<Size, string>()
        {
            { Size.Five, "./../../../5px.txt" },
            { Size.Seven, "./../../../7px.txt" },
        };

        private static Dictionary<Color, string> COLOR_ANSI_COMMANDS = new Dictionary<Color, string>()
        {
            { Color.Default, "\u001b[37m" },
            { Color.Red, "\x1B[31m" },
            { Color.Green, "\x1B[32m" },
            { Color.Blue, "\x1B[34m" },
        };

        private Color m_prevColor;
        private Size m_prevSize;
        private char m_prevSymbol;

        private static Color m_color;
        private static Size m_size;
        private static char m_symbol;
        public ConsolePrint(Color color, Size size = Size.One, char symbol = '#')
        {
            m_prevColor = m_color;
            m_color = color;

            m_prevSize = m_size;
            m_size = size;

            m_prevSymbol = m_symbol;
            m_symbol = symbol;
        }
        public void Dispose()
        {
            m_color = m_prevColor;
            m_size = m_prevSize;
            m_symbol = m_prevSymbol;
        }

        public static void Print(string text, int pos_x = -1, int pos_y = -1)
        {
            if (text.Length == 0) return;

            text = text.ToUpper();
            bool isNeedTransfer = text[text.Length - 1] == '\n' || true;

            if (pos_x != -1)
                Console.CursorLeft = pos_x;
            if (pos_y != -1)
                Console.CursorTop = pos_y;

            Console.Write(COLOR_ANSI_COMMANDS[m_color]);

            if (m_size == Size.One)
            {
                Console.Write(text);
                if (isNeedTransfer) Console.Write('\n');
                Console.Write(COLOR_ANSI_COMMANDS[Color.Default]);
                return;
            }

            FileStream stream = File.OpenRead(SIZE_FILE_PATH[m_size]) ?? throw new DirectoryNotFoundException();

            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            int size = (int)m_size;

            foreach (char c in text)
            {
                PrintChar(stream, c, size);
            }

            if (isNeedTransfer)
            {
                Console.CursorLeft = 0;
                Console.CursorTop = y + 1 + size;
            }

            Console.Write(COLOR_ANSI_COMMANDS[Color.Default]);

            stream.Dispose();
        }

        private static void PrintChar(FileStream stream, char chr, int size)
        {
            if (chr == '\n') return;

            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            int index = chr - 1040;
            int pos = index * (size + 1);

            for (int i = 0; i < size; i++)
            {
                byte[] buffer = new byte[size + 1];
                stream.Position = pos + i * (ALPHABET_COUNT * (size + 1) + 1);
                stream.Read(buffer, 0, size + 1);
                foreach (char c in buffer)
                {
                    if (c == SYMBOL_GRAPHIC)
                        Console.Write(m_symbol);
                    else Console.Write(' ');
                }
                Console.CursorLeft = x;
                Console.CursorTop = y + i + 1;
                
            }

            Console.CursorLeft = x + size + 1;
            Console.CursorTop = y;
        }


    }

    internal class Program
    {
        static void Main()
        {
            string chr = "привет";

            using (new ConsolePrint(ConsolePrint.Color.Blue, ConsolePrint.Size.Five))
            {
                ConsolePrint.Print(chr, 8, 2);
                using(new ConsolePrint(ConsolePrint.Color.Red, ConsolePrint.Size.Seven, '█'))
                {
                    using (new ConsolePrint(ConsolePrint.Color.Green, ConsolePrint.Size.One))
                    {
                        ConsolePrint.Print(chr);
                    }
                    ConsolePrint.Print(chr, pos_x: 10, pos_y: 4);
                }
            }
        }
    }
}
