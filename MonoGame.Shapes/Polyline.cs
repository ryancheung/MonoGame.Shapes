using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace MonoGame.Shapes
{
    public class Polyline
    {
        public Polyline(IEnumerable<Vector2> points)
        {
            Points = points;
        }

        public IEnumerable<Vector2> Points { get; private set; }
        public float Left { get { return Points.Min(p => p.X); } }
        public float Top { get { return Points.Min(p => p.Y); } }
        public float Right { get { return Points.Max(p => p.X); } }
        public float Bottom { get { return Points.Max(p => p.Y); } }

        public Rectangle BoundingRectangle
        {
            get
            {
                var minX = (int)Left;
                var minY = (int)Top;
                var maxX = (int)Right;
                var maxY = (int)Bottom;
                return new Rectangle(minX, minY, maxX - minX, maxY - minY);
            }
        }

        public bool Contains(float x, float y)
        {
            return false;
        }

        public bool Contains(Vector2 point)
        {
            return Contains(point.X, point.Y);
        }
    }
}