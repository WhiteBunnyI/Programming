using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace asd
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

        private void calculate_Click(object sender, EventArgs e)
        {
            Render(false);
            Color prev = pen.Color;
            pen.Color = Color.Firebrick;
            for(int i = 0; i < figures.Count; i++)
            {
                for(int o = i + 1; o < figures.Count; o++)
                {
                    var collide = figures[i].CheckCollide(figures[o]);
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
            foreach(Dot figure in figures)
            {
                figure?.OnLeftClickDown(this, e);
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
            foreach(Dot figure in figures)
            {
                figure?.OnLeftClickUp(this, e);
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
                    //figures[index] = new Straight(20, 20, 1, 1);
                    break;
                case "Окружность":
                    figures[index] = new Circle(50, 50, 100);
                    break;

            }
            Render();
        }
    }
}
