﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Task3
{
    public class Circle : Dot
    {
        public float radius;

        public Circle(float x, float y, float radius) : base(x, y)
        {
            this.radius = radius;
        }

        public override List<Dot> CheckCollide(Dot other)
        {
            List<Dot> result = new List<Dot>(8);

            switch (other.GetType().Name)
            {
                case "Dot":
                    break;
                case "Straight":
                    ColliderForm.CheckCollide(this, other as Straight, ref result);
                    break;
                case "Segment":
                    ColliderForm.CheckCollide(this, other as Segment, ref result);
                    break;
                case "Circle":
                    ColliderForm.CheckCollide(this, other as Circle, ref result);
                    break;
            }

            return result;
        }

        public override float F(float x)
        {
            float _x = (x - startPoint.x);
            return startPoint.y + (float)Math.Sqrt(radius * radius - _x * _x);
        }

        public float F1(float x)
        {
            float _x = (x - startPoint.x);
            return startPoint.y - (float)Math.Sqrt(radius * radius - _x * _x);
        }

        public override void Render(Form1 form)
        {
            form.g.DrawEllipse(form.pen, startPoint.x - radius, startPoint.y - radius, radius * 2, radius * 2);
            
            Color c = form.pen.Color;
            form.pen.Color = Color.Aqua;

            form.g.DrawEllipse(form.pen, startPoint.x - form.pen.Width / 2, startPoint.y - form.pen.Width, form.pen.Width, form.pen.Width);
            form.pen.Color = c;
            
        }

        protected bool isDraggingRad = false;
        public override bool OnLeftClickDown(Form1 form, MouseEventArgs e)
        {
            base.OnLeftClickDown(form, e);
            float x = e.X - startPoint.x;
            float y = e.Y - startPoint.y;
            float dist = (float)Math.Sqrt(x * x + y * y);
            if(dist > radius - form.pen.Width && dist < radius + form.pen.Width)
            {
                isDraggingRad = true;
            }

            return isDraggingRad || isDragging;
        }

        public override void OnLeftClickDrag(Form1 form, MouseEventArgs e)
        {
            base.OnLeftClickDrag(form, e);
            if(isDraggingRad)
            {
                float x = e.X - startPoint.x;
                float y = e.Y - startPoint.y;
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
