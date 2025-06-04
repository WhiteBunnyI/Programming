using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Task3
{
    public partial class Form1 : Form
    {
        public Graphics g { get; private set; }
        public Pen pen { get; private set; }
        Bitmap bitmap;

        decimal prevValue;

        List<Dot> figures;
        List<ComboBox> boxs;

        public Form1()
        {
            InitializeComponent();

            pen = new Pen(Color.Black, 5);
            bitmap = new Bitmap(Size.Width, Size.Height);
            g = Graphics.FromImage(bitmap);

            prevValue = points.Value;
            figures = new List<Dot>((int)points.Maximum);
            boxs = new List<ComboBox>((int)points.Maximum);

            windowResTextBox.Text = string.Format("Размер окна w:{0}, h:{1}", Size.Width, Size.Height);

            type.Visible = false;
        }

        private void points_ValueChanged(object sender, EventArgs e)
        {
            //Уменьшилось
            if(prevValue > points.Value)
            {
                for(decimal i = prevValue - points.Value; i > 0; i--)
                {
                    figures.RemoveAt(figures.Count - 1);

                    ComboBox b = boxs.Last();
                    boxs.Remove(b);
                    b.Dispose();
                }
            }

            //Увеличилось
            if (prevValue < points.Value)
            {
                for (decimal i = points.Value - prevValue; i > 0; i--)
                {
                    ComboBox b = new ComboBox();
                    boxs.Add(b);
                    panel.Controls.Add(b);
                    b.SelectedIndexChanged += type_SelectedIndexChanged;
                    foreach(var item in type.Items)
                    {
                        b.Items.Add(item);
                    }
                    b.Size = type.Size;
                    b.RightToLeft = type.RightToLeft;
                    figures.Add(null);
                }
            }


            Render();

            prevValue = points.Value;
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void check_Clicked(object sender, EventArgs e)
        {
            Render(false);
            Color prev = pen.Color;
            pen.Color = Color.Firebrick;
            for(int i = 0; i < figures.Count; i++)
            {
                for(int o = i + 1; o < figures.Count; o++)
                {
                    if (figures[o] == null)
                        continue;

                    var collide = figures[i]?.CheckCollide(figures[o]);
                    foreach(var dot in collide)
                    {
                        dot.Render(this);
                    }
                }
            }
            pen.Color = prev;
            pictureBox1.Image = bitmap;
        }

        private void Render(bool setBitmap = true)
        {
            g.Clear(DefaultBackColor);
            foreach (Dot figure in figures)
            {
                figure?.Render(this);
            }
            if (setBitmap)
                pictureBox1.Image = bitmap;
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Size = Size;
            windowResTextBox.Text = string.Format("Размер окна w:{0}, h:{1}", Size.Width, Size.Height);

            bitmap = new Bitmap(Size.Width, Size.Height);
            g = Graphics.FromImage(bitmap);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            bool isClickedOnFigure = false;

            if (e.Button == MouseButtons.Left)
            {
                foreach (Dot figure in figures)
                {
                    if (figure == null)
                        continue;

                    isClickedOnFigure = figure.OnLeftClickDown(this, e) || isClickedOnFigure;
                }

                if (!isClickedOnFigure)
                {
                    Dot dot = new Dot(e.X, e.Y);
                    figures.Add(dot);
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            foreach(Dot figure in figures)
            {
                figure?.OnLeftClickDrag(this, e);
            }
            Render();
            mouseText.Text = "Позиция мыши: " + e.X + " " + e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (Dot figure in figures)
                {
                    figure?.OnLeftClickUp(this, e);
                }
            }
        }

        private void type_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            int index = boxs.IndexOf(box);
            switch(box.Text)
            {
                case "Точка":
                    figures[index] = new Dot(20, 20);
                    break;
                case "Прямая":
                    figures[index] = new Straight(20, 20, 1, 0);
                    break;
                case "Отрезок":
                    figures[index] = new Segment(10, 10, 30, 10);
                    break;
                case "Окружность":
                    figures[index] = new Circle(50, 50, 100);
                    break;

            }
            Render();
        }

        private void calculate_Clicked(object sender, EventArgs e)
        {
            List<Dot> dots = new List<Dot>();

            foreach (Dot figure in figures)
            {
                if (figure.GetType() == typeof(Dot))
                    dots.Add(figure);
            }

            figures.Clear();
            figures = new List<Dot>(dots);

            var fullOutline = ColliderForm.Calculate(dots)?.ToList();

            //Точек внутри области меньше 2
            if (fullOutline == null || dots.Count - fullOutline.Count < 3)
            {
                result.Text = "Нет вложенных треугольников";
                return;
            }

            var inlineDots = new List<Dot>();
            foreach (Dot dot in dots)
            {
                if(!fullOutline.Contains(dot))
                    inlineDots.Add(dot);
            }

            //Перебираем тройки выпуклой оболочки и выбираем ту, внутри которой больше или равно 3 точки
            for (int i = 0; i < fullOutline.Count - 2; i++)
            {
                for (int j = i + 1; j < fullOutline.Count - 1; j++)
                {
                    for (int k = j + 1; k < fullOutline.Count; k++)
                    {
                        List<Dot> outlineTriangle = new List<Dot>() { fullOutline[i], fullOutline[j], fullOutline[k] };

                        var funcResult = IsHasInsideTriangle(outlineTriangle, inlineDots);
                        if (funcResult.hasTriangle)
                        {
                            result.Text = "Есть вложенные треугольники";

                            RenderTriangle(outlineTriangle);
                            RenderTriangle(funcResult.insideDots);

                            pen.Color = Color.Red;
                            outlineTriangle[0].Render(this);
                            pen.Color = Color.Black;
                            return;
                        }
                    }
                }
            }

            result.Text = "Нет вложенных треугольников";
        }

        private void RenderTriangle(List<Dot> triangle)
        {
            /*for (int i = 0; i < triangle.Count - 1; i++)
            {
                for (int j = i + 1; j < triangle.Count; j++)
                {
                    figures.Add(new Segment(triangle[i], triangle[j]));
                }
            }*/

            figures.Add(new Segment(triangle[0], triangle[1]));
            figures.Add(new Segment(triangle[1], triangle[2]));
            figures.Add(new Segment(triangle[2], triangle[0]));
            Render();
        }
        
        private (bool hasTriangle, List<Dot> insideDots) IsHasInsideTriangle(List<Dot> outlineTriangle, List<Dot> inlineDots)
        {
            List<Dot> inside = new List<Dot>();

            Vector2 normal_1 = new Vector2(outlineTriangle[0], outlineTriangle[1]).GetNormal();
            Vector2 normal_2 = new Vector2(outlineTriangle[1], outlineTriangle[2]).GetNormal();
            Vector2 normal_3 = new Vector2(outlineTriangle[2], outlineTriangle[0]).GetNormal();

            Dot center_1 = (outlineTriangle[0] + outlineTriangle[1]) / 2;
            Dot center_2 = (outlineTriangle[1] + outlineTriangle[2]) / 2;
            Dot center_3 = (outlineTriangle[2] + outlineTriangle[0]) / 2;

            foreach (Dot dot in inlineDots)
            {
                float dot_1 = normal_1.DotProduct(new Vector2(center_1, dot));
                float dot_2 = normal_2.DotProduct(new Vector2(center_2, dot));
                float dot_3 = normal_3.DotProduct(new Vector2(center_3, dot));

                if (dot_1 > 0f && dot_3 > 0f && dot_2 > 0f)
                {
                    inside.Add(dot);
                }
            }

            if (inside.Count < 3)
                return (false, inlineDots);

            //Проверка лежат ли точки на одной прямой
            Straight straight = new Straight(inside[0], new Vector2(inside[0], inside[1]));
            for (int i = 2; i < inside.Count; i++)
            {
                Dot dot = inside[i];
                Circle circle = new Circle(dot.startPoint.x, dot.startPoint.y, 15f);
                var collisions = straight.CheckCollide(circle);
                if (collisions.Count == 0)
                {
                    List<Dot> result = new List<Dot>() { inside[0], inside[1], dot };
                    foreach (var d in inlineDots)
                        if (!result.Contains(d))
                            result.Add(d);
                    return (true, result);
                }
            }


            return (false, inlineDots);
        }

    }
}
