using System.Drawing.Drawing2D;
using System.Drawing;

namespace Axiom.Core.Drawables
{
    public class ShadowDrawable : DrawableBase
    {

        // Properties

        public float Depth
        {
            get => _shadowDepth;
            set => SetField(ref _shadowDepth, value);
        }

        // Fields 

        private float _shadowDepth;

        // Constructors

        public ShadowDrawable() : base()
        {
            Depth = 20f;
        }

        // Events 

        public override void EnabledChanged(bool enabled) { }

        public override bool MouseLeave(Point p) => !Path.IsVisible(p);

        public override bool MouseEnter(Point p) => Path.IsVisible(p);

        public override bool MouseDown(Point p) => Path.IsVisible(p);

        public override bool MouseUp(Point p) => Path.IsVisible(p);

        public override bool Click(Point p) => Path.IsVisible(p);

        // Methods

        public override void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float diameter = Depth;

            float radius = diameter * 0.5f;

            // Get the corner shadows

            using (var c = GetTopLhsCorner(diameter))
            using (var b = new PathGradientBrush(c))
            {
                b.CenterPoint = new PointF(radius, radius);
                b.SurroundColors = new Color[] { Color.White };
                b.CenterColor = Color.FromArgb(26, 10, 10, 10);
                g.FillPath(b, c);
            }

            using (var c = GetTopRhsCorner(diameter)) //20f))
            using (var b = new PathGradientBrush(c))
            {
                b.CenterPoint = new PointF(Width - radius, radius);
                b.SurroundColors = new Color[] { Color.White };
                b.CenterColor = Color.FromArgb(26, 10, 10, 10);
                g.FillPath(b, c);
            }

            using (var c = GetBtmLhsCorner(diameter)) //20f))
            using (var b = new PathGradientBrush(c))
            {
                b.CenterPoint = new PointF(radius, Height - radius);
                b.SurroundColors = new Color[] { Color.White };
                b.CenterColor = Color.FromArgb(26, 10, 10, 10);
                g.FillPath(b, c);
            }

            using (var c = GetBtmRhsCorner(diameter)) //20f))
            using (var b = new PathGradientBrush(c))
            {
                b.CenterPoint = new PointF(Width - radius, Height - radius);
                b.SurroundColors = new Color[] { Color.White };
                b.CenterColor = Color.FromArgb(26, 10, 10, 10);
                g.FillPath(b, c);
            }

            // Get the rectanglar shadows between the corners

            var tr = GetTopRectangle(radius, diameter, 0.25f);
            var lr = GetLhsRectangle(radius, diameter, 0.25f);
            var br = GetBtmRectangle(radius, diameter, 0.25f);
            var rr = GetRhsRectangle(radius, diameter, 0.25f);

            using (var brush = new LinearGradientBrush(tr, Color.White, Color.FromArgb(26, 10, 10, 10), 90f))
            {
                g.FillRectangle(brush, tr);
            }
            using (var brush = new LinearGradientBrush(lr, Color.White, Color.FromArgb(26, 10, 10, 10), 0f))
            {
                g.FillRectangle(brush, lr);
            }
            using (var brush = new LinearGradientBrush(br, Color.White, Color.FromArgb(26, 10, 10, 10), 270f))
            {
                g.FillRectangle(brush, br);
            }
            using (var brush = new LinearGradientBrush(rr, Color.White, Color.FromArgb(26, 10, 10, 10), 180f))
            {
                g.FillRectangle(brush, rr);
            }
        }

        // Methods - private

        private RectangleF GetTopRectangle(float radius, float diameter, float offset)
        {
            float x0 = radius - offset;
            float y0 = 0;
            float w = Width - diameter + offset;
            float h = radius;

            return new RectangleF(x0, y0, w, h);
        }

        private RectangleF GetBtmRectangle(float radius, float diameter, float offset)
        {
            float x0 = radius - offset;
            float y0 = Height - radius;
            float w = Width - diameter + offset;
            float h = radius;

            return new RectangleF(x0, y0, w, h);
        }

        private RectangleF GetLhsRectangle(float radius, float diameter, float offset)
        {
            float x0 = 0;
            float y0 = radius - offset;
            float w = radius;
            float h = Height - diameter + offset;

            return new RectangleF(x0, y0, w, h);
        }

        private RectangleF GetRhsRectangle(float radius, float diameter, float offset)
        {
            float x0 = Width - radius;
            float y0 = radius - offset;
            float w = radius;
            float h = Height - diameter + offset;

            return new RectangleF(x0, y0, w, h);
        }

        private GraphicsPath GetTopLhsCorner(float diameter)
        {
            var p = PointF.Empty;
            var s = new SizeF(diameter, diameter);
            var r = new RectangleF(p, s);

            float x0 = p.X;
            float y0 = p.Y;
            float x1 = x0 + diameter * 0.5f;
            float y1 = y0 + diameter * 0.5f;

            var p1 = new PointF(x1, y0);
            var p2 = new PointF(x1, y1);
            var p3 = new PointF(x0, y1);

            var path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(r, 180, 90);
            path.AddLine(p1, p2);
            path.AddLine(p2, p3);
            path.CloseFigure();

            return path;
        }

        private GraphicsPath GetBtmLhsCorner(float diameter)
        {
            var p = new PointF(0, Height - diameter);
            var s = new SizeF(diameter, diameter);
            var r = new RectangleF(p, s);

            float x0 = p.X;
            float y0 = p.Y;
            float x1 = x0 + diameter * 0.5f;
            float y1 = y0 + diameter * 0.5f;
            float y2 = y0 + diameter;

            var p1 = new PointF(x0, y1);
            var p2 = new PointF(x1, y1);
            var p3 = new PointF(x1, y2);

            var path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(r, 90, 90);
            path.AddLine(p1, p2);
            path.AddLine(p2, p3);
            path.CloseFigure();

            return path;
        }

        private GraphicsPath GetTopRhsCorner(float diameter)
        {
            var path = new GraphicsPath();
            var p = new PointF(Width - diameter, 0);
            var s = new SizeF(diameter, diameter);
            var r = new RectangleF(p, s);

            float x0 = p.X;
            float y0 = p.Y;
            float x1 = x0 + diameter * 0.5f;
            float y1 = y0 + diameter * 0.5f;
            float x2 = x0 + diameter;

            var p1 = new PointF(x1, y0);
            var p2 = new PointF(x1, y1);
            var p3 = new PointF(x2, y1);

            path.StartFigure();
            path.AddArc(r, 270, 90);
            path.AddLine(p1, p2);
            path.AddLine(p2, p3);
            path.CloseFigure();

            return path;
        }

        private GraphicsPath GetBtmRhsCorner(float diameter)
        {
            var path = new GraphicsPath();
            var p = new PointF(Width - diameter, Height - diameter);
            var s = new SizeF(diameter, diameter);
            var r = new RectangleF(p, s);

            float x0 = p.X;
            float y0 = p.Y;
            float x1 = x0 + diameter * 0.5f;
            float y1 = y0 + diameter * 0.5f;
            float x2 = x0 + diameter;
            float y2 = y0 + diameter;

            var p1 = new PointF(x1, y2);
            var p2 = new PointF(x1, y1);
            var p3 = new PointF(x2, y1);

            path.StartFigure();
            path.AddArc(r, 0, 90);
            path.AddLine(p1, p2);
            path.AddLine(p2, p3);
            path.CloseFigure();

            return path;
        }


    }
}
