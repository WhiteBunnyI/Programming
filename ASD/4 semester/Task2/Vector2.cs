using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3
{
    public class Vector2
    {
        public float x;
        public float y;
        public Vector2(float x, float y)
        {
            this.x = x; this.y = y;
        }

        public Vector2(Dot a, Dot b)
        {
            x = b.startPoint.x - a.startPoint.x;
            y = b.startPoint.y - a.startPoint.y;
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return (a.x == b.x && a.y == b.y);
        }
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a==b);
        }
        public static Vector2 operator -(Vector2 a)
        {
            a.x = -a.x;
            a.y = -a.y;
            return a;
        }

        public Dot GetDot()
        {
            return new Dot(x, y);
        }

        public float DotProduct(Vector2 other)
        {
            return x * other.x + y * other.y;
        }

        public Vector2 GetNormal()
        {
            return new Vector2(-y, x);
        }

        public Vector2 GetNormalized()
        {
            float len = (float)Math.Sqrt(x * x + y * y);
            return new Vector2(x / len, y / len);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 vector &&
                   x == vector.x &&
                   y == vector.y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }
    }
}
