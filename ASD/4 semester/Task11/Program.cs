using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3
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
            //Key - dot index, Value - dot color
            Dictionary<int, int> dotsColor = new Dictionary<int, int>();
            List<int> checkedDots = new List<int>();

            int index = 0;
            while (checkedDots.Count != dots.Count)
            {
                List<int> occupiedColors = new List<int>();

                var neighbords = GetNeighbords(index, links);
                foreach (var link in neighbords.links)
                {
                    int otherDot = link.Key == index ? link.Value : link.Key;
                    if (dotsColor.TryGetValue(otherDot, out int otherColor))
                        occupiedColors.Add(otherColor);
                }

                int color = 0;
                for (; color < colors.Count; color++)
                {
                    if (!occupiedColors.Contains(color))
                        break;
                }

                //Нет подходящего цвета
                if (color == colors.Count)
                    throw new Exception();

                dotsColor.Add(index, color);
                checkedDots.Add(index);

                bool isFound = false;
                //Берем соседнюю точку
                foreach (var dotIndex in neighbords.dots)
                {
                    if (!checkedDots.Contains(dotIndex))
                    {
                        index = dotIndex;
                        isFound = true;
                        break;
                    }
                }

                if (isFound) continue;

                //В случае, если соседних нет, ищем любую другую
                for (int i = 0; i < dots.Count; i++)
                {
                    if (!checkedDots.Contains(i))
                    {
                        index = i;
                        break;
                    }
                }

            }

            Color c = pen.Color;
            for (int i = 0; i < dotsColor.Count; i++)
            {
                pen.Color = colors[dotsColor[i]];
                graphics.DrawEllipse(pen, dots[i].position.x - pen.Width / 2, dots[i].position.y - pen.Width / 2, pen.Width, pen.Width);
            }
            pen.Color = c;
        }

        private static (List<Pair> links, List<int> dots) GetNeighbords(int index, List<Pair> links)
        {
            List<Pair> resultLinks = new List<Pair>();
            List<int> resultDots = new List<int>();

            foreach (var link in links)
            {
                if (link.Key == index || link.Value == index)
                {
                    int otherDot = link.Key == index ? link.Value : link.Key;
                    resultLinks.Add(link);
                    resultDots.Add(otherDot);
                }
            }

            return (resultLinks, resultDots);
        }

/*        public static void Coloring(Graphics graphics, Pen pen, List<Dot> dots, List<Pair> links)
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

        }*/
    }
}
