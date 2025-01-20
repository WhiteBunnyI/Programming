using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asd
{
    public class Dot
    {
        public Vector2 startPos;

        public Dot(float x, float y)
        {
            startPos = new Vector2(x, y);
        }

        public virtual void Render(Form1 form)
        {
            form.g.DrawEllipse(form.pen, startPos.x - form.pen.Width / 2, startPos.y - form.pen.Width / 2, form.pen.Width, form.pen.Width);
        }

        protected bool isDragging = false;
        public virtual void OnLeftClickDown(Form1 form, MouseEventArgs e)
        {
            float x = startPos.x - e.X;
            float y = startPos.y - e.Y;
            float dist = (float)Math.Sqrt(x * x + y * y);
            if (dist <= form.pen.Width * 2f)
            {
                isDragging = true;
                return;
            }

        }

        public virtual void OnLeftClickDrag(Form1 form, MouseEventArgs e)
        {
            if(isDragging)
            {
                startPos.x = e.X;
                startPos.y = e.Y;
            }
        }

        public virtual void OnLeftClickUp(Form1 form, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
