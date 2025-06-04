using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3
{
    internal static class ColliderForm
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        //Алгоритм Джарвиса
        public static Stack<Dot> Calculate(List<Dot> points)
        {
            if (points.Count < 3) return null;

            Stack<Dot> result = new Stack<Dot>(points.Count);

            Dot start = points[0];

            //Ищем крайнюю точку, которая гарантировано входит в выпуклую оболочку
            foreach (Dot p in points)
            {
                if (start.startPoint.y <= p.startPoint.y)
                    start = p;
            }

            result.Push(start);

            Dot current = start;
            Dot next = null;
            double cos = -1;
            for (int i = 0; i < points.Count; i++)
            {
                if (current == points[i]) continue;

                float x = points[i].startPoint.x - current.startPoint.x;
                float y = points[i].startPoint.y - current.startPoint.y;
                double tempCos = x / Math.Sqrt(x * x + y * y);
                if (tempCos > cos)
                {
                    next = points[i];
                    cos = tempCos;
                }
            }
            current = next;

            while (current != start)
            {
                cos = -1;
                float x1 = current.startPoint.x - result.Peek().startPoint.x;
                float y1 = current.startPoint.y - result.Peek().startPoint.y;

                for (int i = 0; i < points.Count; i++)
                {
                    if (current == points[i]) continue;

                    float x2 = points[i].startPoint.x - current.startPoint.x;
                    float y2 = points[i].startPoint.y - current.startPoint.y;
                    double tempCos = (x1 * x2 + y1 * y2) / (Math.Sqrt(x1 * x1 + y1 * y1) * Math.Sqrt(x2 * x2 + y2 * y2));

                    if (tempCos > cos)
                    {
                        next = points[i];
                        cos = tempCos;
                    }
                }
                result.Push(current);

                if (result.Count > points.Count) return null;

                current = next;
            }

            return result;
        }

        public static void CheckCollide(Straight straight1, Straight straight2, ref List<Dot> result)
        {
            float c1 = straight1.startPoint.x * straight1.vector.y - straight1.startPoint.y * straight1.vector.x;
            float c2 = straight2.startPoint.x * straight2.vector.y - straight2.startPoint.y * straight2.vector.x;

            //Параллельны 
            if (straight1.vector.x * straight2.vector.y == straight1.vector.y * straight2.vector.x)
            {
                //Параллельны
                if ((c1 * straight2.vector.y - c2 * straight1.vector.y != 0) || (c1 * straight2.vector.x - c2 * straight1.vector.x != 0))
                {

                    return;
                }
                //Лежат на одной прямой
                result.Add(straight1);
                return;
            }
            Dot crossing = new Dot(0, 0);


            crossing.startPoint.x = (c1 * straight2.vector.x - c2 * straight1.vector.x) / 
                (straight1.vector.y * straight2.vector.x - straight2.vector.y * straight1.vector.x);

            if (straight1.vector.x != 0)
                crossing.startPoint.y = straight1.F(crossing.startPoint.x);
            else
                crossing.startPoint.y = straight2.F(crossing.startPoint.x);

            result.Add(crossing);
        }

        public static void CheckCollide(Straight straight, Circle circle, ref List<Dot> result)
        {
            float dx = (straight.startPoint.x - circle.startPoint.x);
            float dy = (straight.startPoint.y - circle.startPoint.y);
            float B = 2 * (straight.vector.x * dx + straight.vector.y * dy);
            float C = dx * dx + dy * dy - circle.radius * circle.radius;

            float D = (float)Math.Sqrt(B * B - 4 * C);
            float t1 = (-B + D) / 2f;
            float t2 = (-B - D) / 2f;

            Dot dot1 = new Dot(0,0);
            dot1.startPoint.x = straight.startPoint.x + t1 * straight.vector.x;
            dot1.startPoint.y = straight.startPoint.y + t1 * straight.vector.y;

            if(circle.F(dot1.startPoint.x) == dot1.startPoint.y ||
                circle.F1(dot1.startPoint.x) == dot1.startPoint.y)
                result.Add(dot1);

            Dot dot2 = new Dot(0, 0);
            dot2.startPoint.x = straight.startPoint.x + t2 * straight.vector.x;
            dot2.startPoint.y = straight.startPoint.y + t2 * straight.vector.y;

            if (circle.F(dot2.startPoint.x) == dot2.startPoint.y ||
                circle.F1(dot2.startPoint.x) == dot2.startPoint.y)
                result.Add(dot2);
        }

        public static void CheckCollide(Straight straight, Segment segment, ref List<Dot> result)
        {
            float y1 = straight.F(segment.startPoint.x);
            float y2 = straight.F(segment.endPoint.x);
            float vecx = segment.endPoint.x - segment.startPoint.x;
            float vecy = segment.endPoint.y - segment.startPoint.y;
            Straight straight1 = new Straight(segment.startPoint.x, segment.startPoint.y, vecx, vecy);
            if (straight.vector.x == 0)
            {
                if((straight.startPoint.x > segment.startPoint.x && straight.startPoint.x < segment.endPoint.x) ||
                    (straight.startPoint.x > segment.endPoint.x && straight.startPoint.x < segment.startPoint.x))
                {
                    CheckCollide(straight, straight1, ref result);
                }
            }
            else if((y1 < segment.startPoint.y && y2 > segment.endPoint.y) ||
                (y1 > segment.startPoint.y && y2 < segment.endPoint.y))
            {
                CheckCollide(straight, straight1, ref result);
            }
        }

        public static void CheckCollide(Segment segment, Circle circle, ref List<Dot> result)
        {
            float vecx = segment.endPoint.x - segment.startPoint.x;
            float vecy = segment.endPoint.y - segment.startPoint.y;
            Straight straight = new Straight(segment.startPoint.x, segment.startPoint.y, vecx, vecy);
            
            CheckCollide(straight, circle, ref result);
            for(int i = 0; i < result.Count; i++)
            {
                if ((result[i].startPoint.x < segment.startPoint.x && result[i].startPoint.x < segment.endPoint.x) ||
                    (result[i].startPoint.x > segment.startPoint.x && result[i].startPoint.x > segment.endPoint.x))
                {
                    result.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void CheckCollide(Segment segment1, Segment segment2, ref List<Dot> result)
        {

        }

        public static void CheckCollide(Circle circle1, Circle circle2, ref List<Dot> result)
        {

        }

        public static void CheckCollide(Circle circle, Straight straight, ref List<Dot> result)
        {
            CheckCollide(straight, circle, ref result);
        }

        public static void CheckCollide(Circle circle, Segment segment, ref List<Dot> result)
        {
            CheckCollide(segment, circle, ref result);
        }

        public static void CheckCollide(Segment segment, Straight straight, ref List<Dot> result)
        {
            CheckCollide(straight, segment, ref result);
        }
    }
}
