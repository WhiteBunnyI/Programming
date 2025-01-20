using System;
using System.Drawing;
using System.Windows.Forms;

namespace asd
{
    public class Circle : Dot
    {
        float radius;

        public Circle(float x, float y, float radius) : base(x, y)
        {
            this.radius = radius;
        }

        public override void Render(Form1 form)
        {
            form.g.DrawEllipse(form.pen, startPos.x - radius / 4, startPos.y - radius / 4, radius / 2, radius / 2);
            
            Color c = form.pen.Color;
            form.pen.Color = Color.Aqua;

            form.g.DrawEllipse(form.pen, startPos.x - form.pen.Width / 2, startPos.y - form.pen.Width, form.pen.Width, form.pen.Width);
            form.pen.Color = c;
            
        }

        protected bool isDraggingRad = false;
        public override void OnLeftClickDown(Form1 form, MouseEventArgs e)
        {
            base.OnLeftClickDown(form, e);
            float x = e.X - startPos.x + radius / 4;
            float y = e.Y - startPos.y + radius / 4;
            float dist = (float)Math.Sqrt(x * x + y * y);
            if (dist > radius / 2 - form.pen.Width * 2 && dist < radius / 2 + form.pen.Width * 2)
            {
                isDraggingRad = true;
            }
        }

        public override void OnLeftClickDrag(Form1 form, MouseEventArgs e)
        {
            base.OnLeftClickDrag(form, e);
            if(isDraggingRad)
            {
                float x = e.X - startPos.x + radius / 4;
                float y = e.Y - startPos.y + radius / 4;
                radius = (float)Math.Sqrt(x * x + y * y);
            }
        }

        public override void OnLeftClickUp(Form1 form, MouseEventArgs e)
        {
            base.OnLeftClickUp(form, e);
            isDraggingRad = false;
        }
    }
}
