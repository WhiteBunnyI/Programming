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
            Form1 form = new Form1();
            Application.Run(form);
        }

        //Алгоритм Джарвиса
        public static Stack<PointForm> Calculate(List<PointForm> points)
        {
            if(points.Count < 3) return null;

            Stack<PointForm> result = new Stack<PointForm>(points.Count);

            PointForm start = points[0];

            //Ищем крайнюю точку, которая гарантировано входит в выпуклую оболочку
            foreach(PointForm p in points)
            {
                if (start.Y > p.Y)
                    start = p;
            }

            result.Push(start);

            PointForm current = start;
            PointForm next = null;
            double cos = -1;
            for(int i = 0; i < points.Count; i++)
            {
                if (current == points[i]) continue;

                float x = points[i].X - current.X;
                float y = points[i].Y - current.Y;
                double tempCos = x / Math.Sqrt(x * x + y * y);
                if(tempCos > cos)
                {
                    next = points[i];
                    cos = tempCos;
                }
            }
            current = next;

            while(current != start)
            {
                cos = -1;
                for (int i = 0; i < points.Count; i++)
                {
                    if (current == points[i]) continue;

                    float x1 = current.X - result.Peek().X;
                    float y1 = current.Y - result.Peek().Y;
                    float x2 = points[i].X - current.X;
                    float y2 = points[i].Y - current.Y;
                    double tempCos = (x1 * x2 + y1 * y2) / (Math.Sqrt(x1 * x1 + y1 * y1) * Math.Sqrt(x2 * x2 + y2 * y2));
                    if (tempCos > cos)
                    {
                        next = points[i];
                        cos = tempCos;
                    }
                }
                result.Push(current);

                current = next;
            }
            result.Push(current);   //Необходим для корректного отображения линий

            return result;
        }
    }
}
