using System;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Axiom.Core.Drawables
{
    public class BackgroundDrawable : DrawableBase
    {

        // Properties

        public bool CurveTopLhs { get; set; }

        public bool CurveTopRhs { get; set; }

        public bool CurveBtmLhs { get; set; }

        public bool CurveBtmRhs { get; set; }

        public float Radius { get; set; }

        // Constructors

        public BackgroundDrawable() : base()
        {
            CurveTopLhs = true;
            CurveTopRhs = true;
            CurveBtmRhs = true;
            CurveBtmLhs = true;
            Radius = 6;
        }

        // Events

        public override void EnabledChanged(bool enabled) { }

        public override bool MouseLeave(Point p) => !Path.IsVisible(p);

        public override bool MouseEnter(Point p) => Path.IsVisible(p);

        public override bool MouseDown(Point p) => Path.IsVisible(p);

        public override bool MouseUp(Point p) => Path.IsVisible(p);

        public override bool Click(Point p) => Path.IsVisible(p);

        // Methods: public

        public override void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // If it's a circle we can use an ellipse 
            // instead of the more complicated path.
            if (IsCircle())
            {
                DrawCircle(g);
            }
            else
            {
                DrawRoundedRectangle(g);
            }
        }

        public GraphicsPath GetPath()
        {
            float x = Location.X - 0.5f;
            float y = Location.Y - 0.5f;

            if (IsCircle())
            {
                float d = Radius * 2 - 1;
                return GetCircle(x, y, d);
            }

            float h = Height + 0.5f;
            float w = Width  + 0.5f;
            float r = Radius;
            return GetRoundedRectangle(x, y, w, h, r);
        }

        // Methods: protected overridable

        protected virtual void DrawCircle(Graphics g)
        {
            float x = Location.X;
            float y = Location.Y;

            // We used to reduce the diameter by 1px to account for the 1px removed
            // from the control's width and height, but this should be account for
            // by the control itself, not the drawable... TODO!
            float d = Radius * 2 - 1;

            using (Path = GetCircle(x, y, d))
            using (var b = new SolidBrush(BackgroundColor))
            using (var p = new Pen(BorderColor))
            {
                g.FillPath(b, Path);
                g.DrawPath(p, Path);
            }
        }

        protected virtual void DrawRoundedRectangle(Graphics g)
        {
            float x = Location.X;
            float y = Location.Y;
            float h = Height;
            float w = Width;
            float r = Radius;

            // The border pen has a width of 1f, hence -1 from w and h.
            //using (var borderPath = GetRoundedRectangle(x, y, w - 1, h - 1, r))
            Path = GetRoundedRectangle(x, y, w, h, r);
            using (var b = new SolidBrush(BackgroundColor))
            using (var p = new Pen(BorderColor))
            {
                g.FillPath(b, Path);
                g.DrawPath(p, Path);
            }
        }

        // Methods: protected

        protected GraphicsPath GetCircle(float x0, float y0, float d)
        {
            var path = new GraphicsPath();

            path.AddEllipse(new RectangleF(x0, y0, d, d));

            return path;
        }

        protected GraphicsPath GetRoundedRectangle(float x0, float y0, float w, float h, float r)
        {
            if (r == 0)
            {
                var gp = new GraphicsPath();
                gp.AddRectangle(new RectangleF(x0, y0, w, h));
                return gp;
            }

            // Diameter
            float d = r * 2;

            // Coordinates
            float x1 = x0 + w;
            float y1 = y0 + h;

            // Boundaries
            var b0 = new PointF(x0, y0);
            var b1 = new PointF(x1, y0);
            var b2 = new PointF(x1, y1);
            var b3 = new PointF(x0, y1);

            // Arcs
            var a0 = new PointF(b0.X, b0.Y);
            var a1 = new PointF(b1.X - d, b1.Y);
            var a2 = new PointF(b2.X - d, b2.Y - d);
            var a3 = new PointF(b3.X, b3.Y - d);

            // Arc rectangle/size
            var arc = new SizeF(d, d);

            // Get the line points
            var p0 = new PointF(b0.X + r, b0.Y); // Top line p1
            var p1 = new PointF(b1.X - r, b1.Y); // Top line p2
            var p2 = new PointF(b1.X, b1.Y + r); // Rhs line p1
            var p3 = new PointF(b2.X, b2.Y - r); // Rhs line p2
            var p4 = new PointF(b2.X - r, b2.Y); // Btm line p1
            var p5 = new PointF(b3.X + r, b3.Y); // Btm line p2
            var p6 = new PointF(b3.X, b3.Y - r); // Lhs line p1
            var p7 = new PointF(b0.X, b0.Y + r); // Lhs line p2

            return GetPathObject(arc,
                new[] { b0, b1, b2, b3 },
                new[] { a0, a1, a2, a3 },
                new[] { p0, p1, p2, p3, p4, p5, p6, p7 });
        }

        protected virtual bool IsRounded()
        {
            return Radius > Height / 2;
        }

        protected virtual bool IsCircle()
        {
            return
               Math.Abs(Width - 2 * Radius) < 0.01 &&
               Math.Abs(Height - 2 * Radius) < 0.01;
        }

        protected GraphicsPath GetPathObject(SizeF arc, PointF[] b, PointF[] a, PointF[] p)
        {
            // Can we get aware with drawing two semi-circles instead
            // of quarter circles at each end?
            if (IsRounded())
            {
                return GetRoundedPath(arc, b, a, p);
            }

            return GetRegularPath(arc, b, a, p);
        }

        protected GraphicsPath GetRoundedPath(SizeF arc, PointF[] b, PointF[] a, PointF[] p)
        {
            // Create the graphics path our rounded rectangle
            var path = new GraphicsPath();

            if (CurveTopLhs && CurveBtmLhs)
            {
                // lhs line
                path.AddArc(new RectangleF(a[0], arc), 90, 180);

                // top line
                path.AddLine(p[0], CurveTopRhs ? p[1] : b[1]);
            }
            else
            {
                // lhs line
                path.AddLine(b[3], b[0]);

                // top line
                path.AddLine(b[0], CurveTopRhs ? p[1] : b[1]);
            }

            // Rhs
            if (CurveTopRhs && CurveBtmRhs)
            {
                // rhs line
                path.AddArc(new RectangleF(a[1], arc), 270, 180);

                // btm line
                path.AddLine(p[4], CurveBtmLhs ? p[5] : b[3]);
            }
            else
            {
                // rhs line
                path.AddLine(b[1], b[2]);

                // btm line
                path.AddLine(b[2], CurveBtmLhs ? p[5] : b[3]);
            }

            return path;
        }

        protected GraphicsPath GetRegularPath(SizeF arc, PointF[] b, PointF[] a, PointF[] p)
        {
            // Create the graphics path our rounded rectangle
            var path = new GraphicsPath();

            // Top LHS arc
            if (CurveTopLhs)
            {
                path.AddArc(new RectangleF(a[0], arc), 180, 90);

                // Top line 
                path.AddLine(p[0], p[1]);
            }
            else if (CurveTopRhs)
            {
                path.AddLine(b[0], p[1]); // Top rhs border to top lhs arc
            }
            else
            {
                path.AddLine(b[0], b[1]);// Top rhs border to top lhs border
            }

            // Top RHS arc
            if (CurveTopRhs)
            {
                path.AddArc(new RectangleF(a[1], arc), 270, 90);

                // RHS line
                path.AddLine(p[2], p[3]);
            }
            else if (CurveBtmRhs)
            {
                path.AddLine(b[1], p[3]); // Top rhs border to btm arc
            }
            else
            {
                path.AddLine(b[1], b[2]);// Top rhs border to btm rhs border
            }

            // Btm RHS arc
            if (CurveBtmRhs)
            {
                path.AddArc(new RectangleF(a[2], arc), 0, 90);

                // Btm line
                path.AddLine(p[4], p[5]);
            }
            else if (CurveBtmLhs)
            {
                path.AddLine(b[2], p[5]); // Rhs btm border to lhs arc 
            }
            else
            {
                path.AddLine(b[2], b[3]);// Rhs btm border to lhs border
            }

            // Btm LHS arc
            if (CurveBtmLhs)
            {
                path.AddArc(new RectangleF(a[3], arc), 90, 90);

                // LHS line
                path.AddLine(p[6], p[7]);
            }
            else if (CurveTopLhs)
            {
                path.AddLine(b[3], p[7]); // Lhs btm border to lhs top arc
            }
            else
            {
                path.AddLine(b[3], b[0]);// Lhs btm border to lhs top border
            }

            // Close the path 
            path.CloseFigure();

            return path;
        }


    }
}
