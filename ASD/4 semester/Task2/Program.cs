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

/*        public static bool IsCollide(Dot figure1,  Dot figure2, ref Straight crossing)
        {
            string t1 = figure1.GetType().Name;
            string t2 = figure2.GetType().Name;
            switch (t1)
            {
                case "Dot":
                    break;
                case "Straight":
                    break;
                case "Segment":
                    break;
                case "Circle":
                    break;
            }
            return false;
        }*/

        public static bool IsCollide(Straight straight1, Straight straight2, ref Straight crossing)
        {
            crossing = new Straight(-10, -10, 0, 0);
            //Параллельны
            if(straight1.vector == straight2.vector || straight1.vector == -straight2.vector)
            {
                //Лежат на одной прямой
                if(straight1.F(straight1.startPos.x) == straight2.F(straight1.startPos.x) &&
                    straight1.F(straight2.startPos.x) == straight2.F(straight2.startPos.x))
                {
                    crossing.Copy(straight1);
                    return true;
                }

                return false;
            }

            //x==0 y==0

            //Пересекаются
            float k = straight1.vector.y / straight1.vector.x;
            crossing.startPos.y = (straight2.startPos.x - straight1.startPos.x) * k + straight1.startPos.y;
            crossing.startPos.x = (crossing.startPos.y - straight1.startPos.y) / k + straight1.startPos.x;

            return true;
        }

        public static bool IsCollide(Straight straight, Circle circle, ref Straight crossing)
        {
            crossing = new Straight(-10, -10, 0, 0);
            


            return true;
        }
        public static bool IsCollide(Circle circle, Straight straight, ref Straight crossing)
        {
            return IsCollide(straight, circle, ref crossing);
        }
    }
}
