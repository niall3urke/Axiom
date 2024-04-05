using System.Drawing.Drawing2D;
using System.Drawing;

namespace Axiom.Core.Drawables
{
    public class TickDrawable : DrawableBase
    {

        // Constructors

        public TickDrawable() { }

        // Methods: public

        public override void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (var path = GetPath())
            using (var pen = new Pen(ForegroundColor, 2f))
            {
                g.DrawPath(pen, path);
            }

            var r = new Rectangle(Location.X, Location.Y, Width, Height);
            using (var pen = new Pen(ForegroundColor, 1f))
            {
                //g.DrawRectangle(pen, r);
            }
        }

        // Methods: private

        private GraphicsPath GetPath()
        {
            float x0 = Location.X;
            float y0 = Location.Y;

            var p0 = new PointF
            {
                X = x0,
                Y = y0 + (Height / 2)
            };

            var p1 = new PointF
            {
                X = x0 + (Width * 0.33f),
                Y = y0 + Height
            };

            var p2 = new PointF
            {
                X = x0 + Width,
                Y = y0
            };

            var p = new GraphicsPath();

            p.AddLine(p0, p1);
            p.AddLine(p1, p2);

            return p;
        }


    }
}
