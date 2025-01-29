using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asd
{
    internal static class Program
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
            float c1 = straight1.startPos.x * straight1.vector.y - straight1.startPos.y * straight1.vector.x;
            float c2 = straight2.startPos.x * straight2.vector.y - straight2.startPos.y * straight2.vector.x;

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


            crossing.startPos.x = (c1 * straight2.vector.x - c2 * straight1.vector.x) / 
                (straight1.vector.y * straight2.vector.x - straight2.vector.y * straight1.vector.x);

            crossing.startPos.y = straight1.F(crossing.startPos.x);

            result.Add(crossing);
        }

        public static void CheckCollide(Straight straight, Circle circle, ref List<Dot> result)
        {
            float c = straight.startPos.x * straight.vector.y - straight.startPos.y * straight.vector.x;
            float d = Math.Abs((straight.vector.x * circle.startPos.x - straight.vector.y * circle.startPos.y + c)) / 
                (float)Math.Sqrt(straight.vector.x * straight.vector.x + straight.vector.y * straight.vector.y);

            //Расстояние до прямой больше радиуса => прямая не пересекает окружность
            if (circle.radius < d)
                return;


            //Прямая пересекает окружность
            float num1 = straight.vector.x * straight.vector.x;
            float num2 = 2 * circle.startPos.x * num1 + 2 * straight.vector.y * c - 2 * straight.vector.x * straight.vector.y * circle.startPos.y;
            float num3 = circle.startPos.x * circle.startPos.x + circle.startPos.y * circle.startPos.y - circle.radius * circle.radius;
            d = (float)Math.Sqrt(num2 * num2 - 4 * (c * c + 2 * c * straight.vector.x * circle.startPos.y + num1 * num3));
            Dot dot = new Dot((num2 + d) / 2, 0);
            dot.startPos.y = straight.F(dot.startPos.x);
            result.Add(dot);
            dot = new Dot((num2 - d) / 2, 0);
            dot.startPos.y = straight.F(dot.startPos.x);
            result.Add(dot);
        }

        public static void CheckCollide(Straight straight, Segment segment, ref List<Dot> result)
        {

        }

        public static void CheckCollide(Segment segment, Circle circle, ref List<Dot> result)
        {

        }

        public static void CheckCollide(Circle circle1, Circle circle2, ref List<Dot> result)
        {

        }

        public static void CheckCollide(Circle circle, Straight straight, ref List<Dot> result)
        {
            CheckCollide(straight, circle, ref result);
        }

        public static void CheckCollide(Segment segment, Straight straight, ref List<Dot> result)
        {
            CheckCollide(straight, segment, ref result);
        }

        public static void CheckCollide(Circle circle, Segment segment, ref List<Dot> result)
        {
            CheckCollide(segment, circle, ref result);
        }
    }
}
