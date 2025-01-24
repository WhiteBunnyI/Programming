using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asd
{
    using Pair = KeyValuePair<int, int>;

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

        static List<Color> colors = new List<Color>()
        {
            Color.Red,
            Color.Green, 
            Color.Blue,
            Color.Magenta,
            Color.Yellow,
            Color.DimGray,
            Color.DeepPink,
        };

        public static void Coloring(Graphics graphics, Pen pen, List<Dot> dots, List<Pair> links)
        {
            int[] linksCount = new int[dots.Count];
            int[] colors = new int[dots.Count];
            int biggest = 0;
            for(int i = 0; i < links.Count; i++)
            {
                int first = links[i].Key;
                int sec = links[i].Value;
                linksCount[first]++;
                linksCount[sec]++;
                if (linksCount[biggest] < linksCount[first])
                    biggest = first;
                if (linksCount[biggest] < linksCount[sec])
                    biggest = sec;
            }
            HashSet<int> _checked = new HashSet<int>(dots.Count);
            Queue<int> needToCheck = new Queue<int>(dots.Count);
            needToCheck.Enqueue(biggest);

            while(needToCheck.Count > 0)
            {
                int current = needToCheck.Dequeue();
                if (_checked.Contains(current))
                    continue;

                _checked.Add(current);
                for(int i = 0; i <  dots.Count; i++)
                {
                    if (links[i].Key == current)
                    {
                        if(!_checked.Contains(links[i].Value))
                        {
                            colors[links[i].Value]++;
                            needToCheck.Enqueue(links[i].Value);
                        }
                    }
                    else if (links[i].Value == current)
                    {
                        if (!_checked.Contains(links[i].Key))
                        {
                            colors[links[i].Key]++;
                            needToCheck.Enqueue(links[i].Key);
                        }
                    }
                }

            }
            

            Color c = pen.Color;
            for(int i = 0; i < linksCount.Length; i++)
            {
                pen.Color = Program.colors[colors[i]];
                graphics.DrawEllipse(pen, dots[i].position.x - pen.Width / 2, dots[i].position.y - pen.Width / 2, pen.Width, pen.Width);
            }
            pen.Color = c;

        }
    }
}
