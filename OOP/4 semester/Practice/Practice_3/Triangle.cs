using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_3
{
    internal class Triangle : Figure
    {
        public Point center;

        public override Color color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override float thickness { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override float point_size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override float point_density { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Area()
        {
            throw new NotImplementedException();
        }

        public override bool Is_Inside(Point point)
        {
            throw new NotImplementedException();
        }

        public override void Perimeter()
        {
            throw new NotImplementedException();
        }

        public override List<Point> To_Points()
        {
            throw new NotImplementedException();
        }
    }
}
