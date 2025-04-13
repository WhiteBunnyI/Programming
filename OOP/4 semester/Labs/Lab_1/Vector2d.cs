using System;
using System.Collections;

namespace Lab_1
{
    internal class Vector2d : IEnumerable<int>
    {
        private int m_x;
        private int m_y;

        public Vector2d(int x, int y)
        {
            X = x; Y = y;
        }

        public Vector2d(Point2d start, Point2d end)
        {
            X = end.X - start.X;
            Y = end.Y - start.Y;
        }

        public static Vector2d operator +(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.m_x + b.m_x, a.m_y + b.m_y);
        }

        public static Vector2d operator -(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.m_x - b.m_x, a.m_y - b.m_y);
        }

        public static Vector2d operator *(Vector2d vec, int num)
        {
            return new Vector2d(vec.m_x * num, vec.m_y * num);
        }

        public static Vector2d operator /(Vector2d vec, int num)
        {
            return new Vector2d(vec.m_x / num, vec.m_y / num);
        }

        public static bool operator ==(Vector2d a, Vector2d b)
        {
            return a.m_x == b.m_x && a.m_y == b.m_y;
        }

        public static bool operator !=(Vector2d a, Vector2d b)
        {
            return !(a == b);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Vector2d other)
            {
                return this == other;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return m_x.GetHashCode() ^ m_y.GetHashCode();
        }

        public override string ToString()
        {
            return "Вектор (" + m_x + ", " + m_y + ")";
        }

        public int this[int i]
        {
            get => GetComponent(i);
            set => SetComponent(i, value);
        }

        private int GetComponent(int index)
        {
            if (index == 0) return X;
            else if (index == 1) return Y;
            else throw new IndexOutOfRangeException();
        }

        private void SetComponent(int index, int value)
        {
            if (index == 0) X = value;
            else if (index == 1) Y = value;
            else throw new IndexOutOfRangeException();
        }

        public static int DotProduct(Vector2d a, Vector2d b)
        {
            return a.DotProduct(b);
        }

        public int DotProduct(Vector2d other)
        {
            return m_x * other.m_x + m_y * other.m_y;
        }

        public static int CrossProduct(Vector2d a, Vector2d b)
        {
            return a.CrossProduct(b);
        }

        public int CrossProduct(Vector2d other)
        {
            return m_x * other.m_y - m_y * other.m_x;
        }

        public int ScalarTripleProduct(Vector2d other)
        {
            return 0; //В 2д пространстве не определено смешанное произведение
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < 2; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public float Abs
        {
            get
            {
                return (float)Math.Sqrt(m_x * m_x + m_y * m_y);
            }
        }

        public int X
        {
            get { return m_x; }
            set { m_x = value; }
        }

        public int Y
        {
            get { return m_y; }
            set { m_y = value; }
        }

    }
}
