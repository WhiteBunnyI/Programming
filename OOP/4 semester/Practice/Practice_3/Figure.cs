using System.Collections.Generic;
using System.Drawing;

namespace Practice_3
{
    internal abstract class Figure
    {
        public abstract Color color { get; set; }
        public abstract float thickness { get; set; }
        public abstract float point_size { get; set; }
        public abstract float point_density { get; set; }
        public abstract void Area();
        public abstract void Perimeter();
        public abstract List<Point> To_Points();
        public abstract bool Is_Inside(Point point);

    }
}
