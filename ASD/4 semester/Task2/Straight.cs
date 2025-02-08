using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace asd
{
    public class Straight : Dot
    {
        public Vector2 vector;

        public Straight(float startX, float startY, float vecX, float vecY) : base(startX, startY)
        {
            vector = new Vector2(vecX, vecY);

            float vecLen = (float)Math.Sqrt(vecX * vecX + vecY * vecY);
            if (vecLen == 0)
                return;
            vector.x /= vecLen;
            vector.y /= vecLen;
            
        }

        public void SetVector(float x, float y)
        {
            float vecLen = (float)Math.Sqrt(x * x + y * y);
            vector.x = x;
            vector.y = y;
            if (vecLen == 0)
                return;

            vector.x /= vecLen;
            vector.y /= vecLen;
        }

        public override float F(float x)
        {
            if(vector.x == 0f) return startPoint.y;

            return (x - startPoint.x) * vector.y / vector.x + startPoint.y;
        }

        public override List<Dot> CheckCollide(Dot other)
        {
            List<Dot> result = new List<Dot>(8);

            switch(other.GetType().Name)
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

        public void Copy(Straight other)
        {
            startPoint.x = other.startPoint.x;
            startPoint.y = other.startPoint.y;
            vector.x = other.vector.x;
            vector.y = other.vector.y;
        }

        public override void Render(Form1 form)
        {
            if (vector.x == 0)
            {
                form.g.DrawLine(form.pen, startPoint.x, -form.Size.Height, startPoint.x, form.Size.Height * 2);
            }
            else if (vector.y == 0)
            {
                form.g.DrawLine(form.pen, -form.Size.Width, startPoint.y, form.Size.Width * 2, startPoint.y);
            }
            else
            {
                float k = vector.y / vector.x;
                float y1 = (-form.Size.Width - startPoint.x) * k + startPoint.y;
                float x1 = (y1 - startPoint.y) / k + startPoint.x;
                float y2 = (form.Size.Width * 2 - startPoint.x) * k + startPoint.y;
                float x2 = (y2 - startPoint.y) / k + startPoint.x;
                form.g.DrawLine(form.pen, x1, y1, x2, y2);
            }
            Color c = form.pen.Color;
            form.pen.Color = Color.Silver;
            base.Render(form);
            form.pen.Color = c;

        }

        protected bool isDraggingVec = false;
        public override void OnLeftClickDown(Form1 form, MouseEventArgs e)
        {
            base.OnLeftClickDown(form, e);
            if (isDragging) return;

            if(Math.Abs(vector.x) > Math.Abs(vector.y))
            {
                float y = (e.X - startPoint.x) * vector.y / vector.x + startPoint.y;

                if (e.Y + form.pen.Width * 2 > y)
                {
                    isDraggingVec = e.Y - form.pen.Width * 2 < y;
                }
            }
            else
            {
                float x = (e.Y - startPoint.y) * vector.x / vector.y + startPoint.x;

                if(e.X + form.pen.Width * 2 > x)
                {
                    isDraggingVec = e.X - form.pen.Width * 2 < x;
                }
            }
            
        }

        public override void OnLeftClickDrag(Form1 form, MouseEventArgs e)
        {
            base.OnLeftClickDrag(form, e);
            if (isDraggingVec)
            {
                vector.x = e.X - startPoint.x;
                vector.y = e.Y - startPoint.y;
                float vecLen = (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y);
                if (vecLen == 0)
                    return;
                vector.x /= vecLen;
                vector.y /= vecLen;
            }
        }

        public override void OnLeftClickUp(Form1 form, MouseEventArgs e)
        {
            base.OnLeftClickUp(form, e);
            isDraggingVec = false;
        }
    }
}
