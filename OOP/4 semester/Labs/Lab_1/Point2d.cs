namespace Lab_1;

internal class Point2d
{
    private int m_x;
    private int m_y;

    public static int WIDTH;
    public static int HEIGHT;

    public Point2d(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static bool operator ==(Point2d a, Point2d b)
    {
        return a.m_x == b.m_x && a.m_y == b.m_y;
    }

    public static bool operator !=(Point2d a, Point2d b)
    {
        return !(a == b);
    }

    public override string ToString()
    {
        return $"Точка ({m_x}, {m_y})";
        //return string.Format("Точка ({0}, {1})", m_x, m_y);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Point2d other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return m_x.GetHashCode() ^ m_y.GetHashCode();
    }

    public int X
    {
        get { return m_x; }
        set
        {
            if (value >= 0 && value <= WIDTH) m_x = value;
            else throw new ArgumentOutOfRangeException(string.Format("Координата x: {0} находится за пределами экрана!", value));
        }
    }

    public int Y
    {
        get { return m_y; }
        set
        {
            if (value >= 0 && value <= HEIGHT) m_y = value;
            else throw new ArgumentOutOfRangeException(string.Format("Координата y: {0} находится за пределами экрана!", value));
        }
    }
}
