using System.Windows.Forms;

namespace asd
{
    public class Segment : Dot
    {
        public Vector2 endPoint;

        public Segment(int x1, int y1, int x2, int y2) : base(x1, y1)
        {
            endPoint = new Vector2 (x2, y2);
        }
    }
}
