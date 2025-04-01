namespace Lab_1
{
    internal class Point2d
    {
        private int x;
        private int y;

        public static int WIDTH;
        public static int HEIGHT;

        public Point2d(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
            set { if (value >= 0 && value <= WIDTH) x = value; }
        }

        public int Y
        {
            get { return y; }
            set { if (value >= 0 && value <= HEIGHT) y = value; }
        }


    }
}
