using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_3
{
    internal class Ellipse : Figure
    {
        public Point center;
        public Point axis;

        float p;

        Color m_color;
        float m_thickness;
        float m_point_size;
        float m_point_density;

        public override Color color { get => m_color; set => m_color = value; }
        public override float thickness { get => m_thickness; set { if (value >= 0) m_thickness = value; } }
        public override float point_size { get => m_point_size; set { if (value >= 0) m_point_size = value; } }
        public override float point_density { get => m_point_density; set { if (value >= 0) m_point_density = value; } }

        public Ellipse(Point center, Point axis, Color color)
        {
            this.center = center;
            this.axis = axis;
            m_color = color;
            p = (axis.y * axis.y / axis.x);
        }

        public override void Area()
        {
            
        }

        public override bool Is_Inside(Point point)
        {
            throw new NotImplementedException();
        }

        public override void Perimeter()
        {
            
        }

        public override List<Point> To_Points()
        {
            throw new NotImplementedException();
        }
    }
}
