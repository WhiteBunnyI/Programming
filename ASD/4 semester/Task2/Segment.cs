using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace asd
{
    public class Segment : Dot
    {
        public Vector2 endPoint;

        public Segment(float x1, float y1, float x2, float y2) : base(x1, y1)
        {
            endPoint = new Vector2 (x2, y2);
        }

        public override float F(float x)
        {
            if(x < startPoint.x || x > endPoint.x)
            {
                return float.NaN;
            }
            return float.NaN;
        }

        public override List<Dot> CheckCollide(Dot other)
        {
            List<Dot> result = new List<Dot>(8);

            switch (other.GetType().Name)
            {
                case "Dot":
                    break;
                case "Straight":
                    Program.CheckCollide(this, other as Straight, ref result);
                    break;
                case "Segment":
                    Program.CheckCollide(this, other as Segment, ref result);
                    break;
                case "Circle":
                    Program.CheckCollide(this, other as Circle, ref result);
                    break;
            }

            return result;
        }

        bool isDraggingOther = false;
        public override void OnLeftClickDown(Form1 form, MouseEventArgs e)
        {
            base.OnLeftClickDown(form, e);
            float x = endPoint.x - e.X;
            float y = endPoint.y - e.Y;
            float dist = (float)Math.Sqrt(x * x + y * y);
            if (dist <= form.pen.Width * 2f)
            {
                isDraggingOther = true;
                return;
            }
        }

        public override void OnLeftClickDrag(Form1 form, MouseEventArgs e)
        {
            base.OnLeftClickDrag(form, e);
            if (isDraggingOther)
            {
                endPoint.x = e.X;
                endPoint.y = e.Y;
            }
        }

        public override void OnLeftClickUp(Form1 form, MouseEventArgs e)
        {
            base.OnLeftClickUp(form, e);
            isDraggingOther = false;
        }

        public override void Render(Form1 form)
        {
            base.Render(form);
            form.g.DrawEllipse(form.pen, endPoint.x - form.pen.Width / 2, endPoint.y - form.pen.Width / 2, form.pen.Width, form.pen.Width);
            form.g.DrawLine(form.pen, startPoint.x, startPoint.y, endPoint.x, endPoint.y);
        }
    }
}
