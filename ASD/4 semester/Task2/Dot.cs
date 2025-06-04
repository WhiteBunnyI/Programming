using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3
{
    public class Dot
    {
        public Vector2 startPoint;

        public Dot(float x, float y)
        {
            startPoint = new Vector2(x, y);
        }

        public virtual float F(float x)
        {
            throw new NotImplementedException();
        }

        public virtual List<Dot> CheckCollide(Dot other)
        {
            throw new NotImplementedException();
        }

        public virtual void Render(Form1 form)
        {
            form.g.DrawEllipse(form.pen, startPoint.x - form.pen.Width / 2, startPoint.y - form.pen.Width / 2, form.pen.Width, form.pen.Width);
        }

        public static Dot operator +(Dot a, Dot b)
        {
            return new Dot(a.startPoint.x + b.startPoint.x, a.startPoint.y + b.startPoint.y);
        }

        public static Dot operator /(Dot dot, float num)
        {
            return new Dot(dot.startPoint.x / num, dot.startPoint.y / num);
        }

        protected bool isDragging = false;
        public virtual bool OnLeftClickDown(Form1 form, MouseEventArgs e)
        {
            float x = startPoint.x - e.X;
            float y = startPoint.y - e.Y;
            float dist = (float)Math.Sqrt(x * x + y * y);
            if (dist <= form.pen.Width * 2f)
            {
                isDragging = true;
            }

            return isDragging;
        }

        public virtual void OnLeftClickDrag(Form1 form, MouseEventArgs e)
        {
            if(isDragging)
            {
                startPoint.x = e.X;
                startPoint.y = e.Y;
            }
        }

        public virtual void OnLeftClickUp(Form1 form, MouseEventArgs e)
        {
            isDragging = false;
        }

    }
}
