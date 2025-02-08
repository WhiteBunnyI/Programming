namespace oop
{
    internal class Program
    {
        public static int screenWidth = 768;
        public static int screenHeight = 1024;
        static void Main()
        {
            Dot dot = new Dot(30, 67);
            //dot.x = -5;
            Dot dot1 = new Dot(40, 80);
            Vector vec = new Vector(dot, dot1);
            Log("Получили вектор: " + vec.x + " " + vec.y);

            vec = new Vector(1, 2);
            Log("Получили вектор: " + vec.x + " " + vec.y);
        }

        public static void Log(string log)
        {
            Console.WriteLine(log);
        }
    }

    public class Vector
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector(Dot dot1, Dot dot2)
        {
            x = dot2.x - dot1.x;
            y = dot2.y - dot1.y;
        }
    }


    public class Dot
    {
        private int _x;
        private int _y;
        public Dot(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x
        {
            get => _x;
            set
            {
                if (value < 0 || value > Program.screenWidth)
                    throw new ArgumentOutOfRangeException("Координата x за границей экрана!");
                _x = value;
            }
        }

        public int y
        {
            get => _y;
            set
            {
                if (value < 0 || value > Program.screenHeight)
                    throw new ArgumentOutOfRangeException("Координата y за границей экрана!");
                _y = value;
            }
        }
    }
}
