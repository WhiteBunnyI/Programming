using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace asd
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        Graphics g;
        Pen pen;

        decimal prevValue;

        List<PointForm> panels;

        public Form1()
        {
            InitializeComponent();

            pen = new Pen(Color.Black, 5);
            bitmap = new Bitmap(Size.Width, Size.Height);
            g = Graphics.FromImage(bitmap);

            prevValue = points.Value;
            panels = new List<PointForm>((int)points.Maximum);

            groupMain.Visible = false;

            windowResTextBox.Text = string.Format("Размер окна w:{0}, h:{1}", Size.Width, Size.Height);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //Уменьшилось
            if (prevValue > points.Value)
            {
                for (decimal i = prevValue - points.Value; i > 0; i--)
                {
                    PointForm f = panels.Last();
                    panels.Remove(f);
                    f.Dispose();
                }

            }

            //Увеличилось
            else if (prevValue < points.Value)
            {
                for (decimal i = points.Value - prevValue; i > 0; i--)
                {
                    panels.Add(new PointForm(groupMain, textBoxX, textBoxY, enterX, enterY, enter_TextChanged, KeyPressed));
                    panel.Controls.Add(panels.Last()._panel);
                    
                }
            }

            else if(prevValue == points.Value)
            {
                if (sender is MouseEventArgs mouse)
                {
                    panels.Last().Move(mouse.X, mouse.Y);
                }
            }

            Render();

            prevValue = points.Value;
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            var stack = Program.Calculate(panels);
            if (stack == null) return;

            Render(false);

            PointForm prev = stack.Pop();
            float t = pen.Width;
            pen.Width = t / 2;
            while (stack.Count != 0)
            {
                PointForm current = stack.Pop();
                g.DrawLine(pen, prev.X, prev.Y, current.X, current.Y);
                prev = current;
            }
            pen.Width = t;
            pictureBox1.Image = bitmap;
        }

        private void enter_TextChanged(object sender, EventArgs e)
        {
            Render();
        }

        private void Render(bool setBitmap = true)
        {
            g.Clear(DefaultBackColor);
            foreach (PointForm point in panels)
            {
                g.DrawEllipse(pen, point.X - pen.Width / 2, point.Y - pen.Width / 2, pen.Width, pen.Width);
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

        PointForm draggedPoint = null;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (PointForm point in panels)
            {
                float x = point.X - e.X;
                float y = point.Y - e.Y;
                double dist = Math.Sqrt(x * x + y * y);

                if (dist <= pen.Width * 2f)
                {
                    draggedPoint = point;
                    return;
                }
            }

            points.Value++;
            numericUpDown1_ValueChanged(e, null);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedPoint != null)
            {
                draggedPoint.Move(e.X, e.Y);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            draggedPoint = null;
        }
    }

    public class PointForm
    {
        public FlowLayoutPanel _panel { get; private set; }
        TextBox _textBoxX;
        TextBox _textBoxY;
        TextBox _x;
        TextBox _y;

        int _X;
        int _Y;

        public PointForm
            (
            FlowLayoutPanel groupMain,
            TextBox textBoxX,
            TextBox textBoxY,
            TextBox enterX,
            TextBox enterY,
            EventHandler onValueChanged,
            KeyPressEventHandler keyPressed
            )
        {
            _panel = new FlowLayoutPanel();

            _textBoxX = new TextBox();
            _textBoxY = new TextBox();
            _x = new TextBox();
            _y = new TextBox();

            _panel.Size = groupMain.Size;

            _textBoxX.Text = textBoxX.Text;
            _textBoxX.Size = textBoxX.Size;
            _textBoxX.ReadOnly = textBoxX.ReadOnly;

            _textBoxY.Text = textBoxY.Text;
            _textBoxY.Size = textBoxY.Size;
            _textBoxY.ReadOnly = textBoxY.ReadOnly;

            _x.Size = enterX.Size;
            _x.TextChanged += onChangedX;
            _x.TextChanged += onValueChanged;
            _x.KeyPress += keyPressed;
            _x.Text = "20";

            _y.Size = enterY.Size;
            _y.TextChanged += onChangedY;
            _y.TextChanged += onValueChanged;
            _y.KeyPress += keyPressed;
            _y.Text = "20";

            _panel.Controls.Add(_textBoxX);
            _panel.Controls.Add(_x);
            _panel.Controls.Add(_textBoxY);
            _panel.Controls.Add(_y);
        }

        public void Move(int x, int y)
        {
            _x.Text = x.ToString();
            _y.Text = y.ToString();
        }

        private void onChangedX(object sender, EventArgs e)
        {
            _X = (_x.Text == "") ? 0 : int.Parse(_x.Text);
        }

        private void onChangedY(object sender, EventArgs e)
        {
            _Y = (_y.Text == "") ? 0 : int.Parse(_y.Text);
        }

        public void Dispose()
        {
            _panel.Dispose();
        }


        public int X => _X;
        public int Y => _Y;
    }
}
