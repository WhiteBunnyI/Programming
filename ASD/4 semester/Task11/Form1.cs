using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace asd
{
    using Pair = KeyValuePair<int, int>;
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        Graphics graphics;
        Pen pen;

        List<Dot> dots;
        List<Pair> links;
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(Size.Width, Size.Height);
            graphics = Graphics.FromImage(bitmap);
            pen = new Pen(Color.Black);
            pen.Width = 5;

            dots = new List<Dot>();
            links = new List<Pair>();
        }

        int currentDragging = -1;
        int firstLink = -1;
        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dot = GetDot(e.X, e.Y, pen.Width);
                if (dot == -1)
                {
                    currentDragging = CreateDot(e.X, e.Y);
                    return;
                }

                currentDragging = dot;
            }

            if (e.Button == MouseButtons.Right)
            {
                firstLink = GetDot(e.X, e.Y, pen.Width);
            }
        }

        private int CreateDot(int x = 20, int y = 20)
        {
            Dot dot = new Dot() { position = new Vector2(x, y) };
            dots.Add(dot);
            points.Value = dots.Count;
            return dots.Count - 1;
        }

        private int GetDot(int x, int y, float width)
        {
            for (int i = 0; i < dots.Count; i++)
            {
                float x_delta = x - dots[i].position.x;
                float y_delta = y - dots[i].position.y;
                float dist = (float)Math.Sqrt(x_delta * x_delta + y_delta * y_delta);

                if (dist <= width)
                    return i;
            }

            return -1;
        }

        private void picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None) return;
            graphics.Clear(DefaultBackColor);
            if (currentDragging != -1)
            {
                dots[currentDragging].position.x = e.X;
                dots[currentDragging].position.y = e.Y;
            }
            if (firstLink != -1)
            {
                graphics.DrawLine(pen, dots[firstLink].position.x, dots[firstLink].position.y, e.X, e.Y);
            }
            Render(false);
            picture.Image = bitmap;
        }

        private void Render(bool setBitmap = true)
        {
            if (setBitmap)
                graphics.Clear(DefaultBackColor);

            for (int i = 0; i < dots.Count; i++)
            {
                for (int o = 0; o < links.Count; o++)
                {
                    Dot first = dots[links[o].Key];
                    Dot second = dots[links[o].Value];
                    graphics.DrawLine(pen, first.position.x, first.position.y, second.position.x, second.position.y);
                }
                graphics.DrawEllipse(pen, dots[i].position.x - pen.Width / 2, dots[i].position.y - pen.Width / 2, pen.Width, pen.Width);
            }

            if (setBitmap)
                picture.Image = bitmap;
        }

        private void picture_MouseUp(object sender, MouseEventArgs e)
        {
            currentDragging = -1;
            if (e.Button == MouseButtons.Right && firstLink != -1)
            {
                int second = GetDot(e.X, e.Y, pen.Width);
                AddLink(firstLink, second);
            }
            firstLink = -1;
            Render();
        }

        private void AddLink(int first, int second)
        {
            if (second == -1 || firstLink == second) return;

            Pair p;

            if (first < second) p = new Pair(first, second);
            else p = new Pair(second, first);

            if (!links.Contains(p))
            {
                links.Add(p);
            }
        }

        decimal prevPointValue;
        private void points_ValueChanged(object sender, EventArgs e)
        {
            if (prevPointValue > points.Value)
            {
                for (decimal delta = prevPointValue - points.Value; delta > 0; delta--)
                {
                    dots.RemoveAt(dots.Count - 1);
                }
            }
            if (prevPointValue < points.Value)
            {

            }
            prevPointValue = points.Value;
            Render();
        }

        decimal prevLinesValue;
        private void lines_ValueChanged(object sender, EventArgs e)
        {
            if (prevLinesValue > lines.Value)
            {
                for(decimal delta = prevLinesValue - lines.Value; delta > 0; delta--)
                {
                    links.RemoveAt(links.Count - 1);
                }
            }
            if (prevLinesValue < lines.Value)
            {
                lines.Value = links.Count;
            }
            prevLinesValue = lines.Value;
            Render();
        }

        private void button_Click(object sender, EventArgs e)
        {
            graphics.Clear(DefaultBackColor);
            Render(false);
            Program.Coloring(graphics, pen, dots, links);
            picture.Image = bitmap;
        }
    }
}
