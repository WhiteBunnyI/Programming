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
            result.Add(dot1);

            Dot dot2 = new Dot(0, 0);
            dot2.startPoint.x = straight.startPoint.x + t2 * straight.vector.x;
            dot2.startPoint.y = straight.startPoint.y + t2 * straight.vector.y;
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
