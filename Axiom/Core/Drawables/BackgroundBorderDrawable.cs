using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;

namespace Axiom.Core.Drawables
{
    public class BackgroundBorderDrawable : DrawableBase
    {

        // Properties

        public bool DrawTopBorder { get; set; }

        public bool DrawBtmBorder { get; set; }

        public bool DrawLhsBorder { get; set; }

        public bool DrawRhsBorder { get; set; }


        public bool CurveTopLhs { get; set; }

        public bool CurveTopRhs { get; set; }

        public bool CurveBtmLhs { get; set; }

        public bool CurveBtmRhs { get; set; }


        public float Radius { get; set; }

        // Constructors

        public BackgroundBorderDrawable() : base()
        {
            DrawTopBorder = true;
            DrawBtmBorder = true;
            DrawLhsBorder = true;
            DrawRhsBorder = true;

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
            bool isCircle = Math.Abs(Width - Height) < 0.01;

            if (isCircle)
            {
                DrawCircle(g);
            }
            else
            {
                DrawRoundedRectangle(g);
            }
        }

        // Methods: protected overridable

        protected virtual void DrawCircle(Graphics g)
        {
            // +3 pixels offset for the border shadow that we 
            // draw whenever the drawable is in focus.
            float x = Location.X + 3;
            float y = Location.Y + 3;
            float r = Radius;

            using (Path = GetCircle(x, y, r))
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

            // Reduce the height and width by 1px due to a 
            // controls paintable region being 1px less the
            // width and height of the controls size.
            float h = Height - 1;
            float w = Width - 1;
            float r = Radius;

            var (Path, border) = GetRoundedRectangle(x, y, w, h, r);

            using (var b = new SolidBrush(BackgroundColor))
            {
                g.FillPath(b, Path);
            }

            using (border) // We can dispose of the border
            using (var p = new Pen(BorderColor))
            {
                g.DrawPath(p, border);
            }
        }

        // Methods: protected

        protected GraphicsPath GetCircle(float x0, float y0, float r)
        {
            var path = new GraphicsPath();

            path.AddEllipse(new RectangleF(x0, y0, r, r));

            return path;
        }

        protected (GraphicsPath, GraphicsPath) GetRoundedRectangle(float x0, float y0, float w, float h, float r)
        {
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

            var b = new[] { b0, b1, b2, b3 };
            var a = new[] { a0, a1, a2, a3 };
            var p = new[] { p0, p1, p2, p3, p4, p5, p6, p7 };

            var background = GetRoundedRectangleBackgroundPath(arc, b, a, p);

            var border = GetRoundedRectangleBorderPath(arc, b, a, p);

            return (background, border);
        }

        #region Border Paths

        protected GraphicsPath GetRoundedRectangleBorderPath(SizeF arc, PointF[] b, PointF[] a, PointF[] p)
        {
            bool isRounded = Radius >= Height / 2;

            // Can we get aware with drawing two semi-circles instead
            // of quarter circles at each end?
            if (isRounded)
            {
                return GetRoundedBorderPath(arc, b, a, p);
            }

            return GetRegularBorderPath(arc, b, a, p);
        }

        protected GraphicsPath GetRoundedBorderPath(SizeF arc, PointF[] b, PointF[] a, PointF[] p)
        {
            return GetRegularBackgroundPath(arc, b, a, p);
        }

        protected GraphicsPath GetRegularBorderPath(SizeF arc, PointF[] b, PointF[] a, PointF[] p)
        {

            // This approach allows 1 border to be excluded and maintain a continuous
            // path. If more than 1 border needs to be excluded, we'll need to create
            // several separate paths because we can't produce a continuous path.

            var path = new GraphicsPath();

            // Gather all the methods responsible for drawing the borders. Add them 
            // in the same order that we draw them (CW from lhs > top > rhs > btm)

            var methods = new List<Func<
                GraphicsPath, SizeF, PointF[], PointF[], PointF[], GraphicsPath>>
            {
                AddLhsBorder,
                AddTopBorder,
                AddRhsBorder,
                AddBtmBorder,
            };

            var borders = new List<bool>
            {
                DrawLhsBorder,
                DrawTopBorder,
                DrawRhsBorder,
                DrawBtmBorder
            };

            // Get the start offset - we want to draw the path in a continuous loop
            // starting after the first border that's not drawn (if any)

            int offset = borders.Any(x => !x)
                ? borders.FindIndex(x => !x) + 1 % methods.Count
                : 0;

            // Loop the borders, account for the start offset, and check whether we
            // actually want to draw that particular border. Always in CW fashion.

            for (int i = 0; i < borders.Count; i++)
            {
                int index = (i + offset) % methods.Count;

                if (borders[index])
                {
                    path = methods[index](path, arc, b, a, p);
                }
            }

            // Don't close the path in this case.

            return path;
        }

        #endregion

        #region Background Paths

        protected GraphicsPath GetRoundedRectangleBackgroundPath(SizeF arc, PointF[] b, PointF[] a, PointF[] p)
        {
            bool isRounded = Radius >= Height / 2;

            // Can we get aware with drawing two semi-circles instead
            // of quarter circles at each end?
            if (isRounded)
            {
                return GetRoundedBackgroundPath(arc, b, a, p);
            }

            return GetRegularBackgroundPath(arc, b, a, p);
        }

        protected GraphicsPath GetRoundedBackgroundPath(SizeF arc, PointF[] b, PointF[] a, PointF[] p)
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

        protected GraphicsPath GetRegularBackgroundPath(SizeF arc, PointF[] b, PointF[] a, PointF[] p)
        {
            // Create the graphics path our rounded rectangle
            var path = new GraphicsPath();

            path = AddLhsBorder(path, arc, b, a, p);

            path = AddTopBorder(path, arc, b, a, p);

            path = AddRhsBorder(path, arc, b, a, p);

            path = AddBtmBorder(path, arc, b, a, p);

            // Close the path when it's a background
            path.CloseFigure();

            return path;
        }

        #endregion

        #region Forming paths

        private GraphicsPath AddLhsBorder(GraphicsPath path, SizeF arc, PointF[] b, PointF[] a, PointF[] p)
        {
            if (CurveBtmLhs)
            {
                path.AddArc(new RectangleF(a[3], arc), 90, 90);

                // LHS line
                if (DrawTopBorder && CurveTopLhs)
                {
                    path.AddLine(p[6], p[7]);
                }
                else
                {
                    path.AddLine(p[7], b[0]);
                }
            }
            else if (CurveTopLhs)
            {
                path.AddLine(b[3], p[7]); // Lhs btm border to lhs top arc
            }
            else
            {
                path.AddLine(b[3], b[0]);// Lhs btm border to lhs top border
            }

            return path;
        }

        private GraphicsPath AddTopBorder(GraphicsPath path, SizeF arc, PointF[] b, PointF[] a, PointF[] p)
        {
            if (CurveTopLhs)
            {
                path.AddArc(new RectangleF(a[0], arc), 180, 90);

                // Top line 
                if (DrawRhsBorder && CurveTopRhs)
                {
                    path.AddLine(p[0], p[1]);
                }
                else
                {
                    path.AddLine(p[0], b[1]);
                }
            }
            else if (CurveTopRhs)
            {
                path.AddLine(b[0], p[1]); // Top rhs border to top lhs arc
            }
            else
            {
                path.AddLine(b[0], b[1]);// Top rhs border to top lhs border
            }

            return path;
        }

        private GraphicsPath AddRhsBorder(GraphicsPath path, SizeF arc, PointF[] b, PointF[] a, PointF[] p)
        {
            if (CurveTopRhs)
            {
                path.AddArc(new RectangleF(a[1], arc), 270, 90);

                // RHS line
                if (DrawBtmBorder && CurveBtmRhs)
                {
                    path.AddLine(p[2], p[3]);
                }
                else
                {
                    path.AddLine(p[2], b[2]);
                }
            }
            else if (CurveBtmRhs)
            {
                path.AddLine(b[1], p[3]); // Top rhs border to btm arc
            }
            else
            {
                path.AddLine(b[1], b[2]);// Top rhs border to btm rhs border
            }
            return path;
        }

        private GraphicsPath AddBtmBorder(GraphicsPath path, SizeF arc, PointF[] b, PointF[] a, PointF[] p)
        {
            if (CurveBtmRhs)
            {
                path.AddArc(new RectangleF(a[2], arc), 0, 90);

                // Btm line
                if (DrawLhsBorder && CurveBtmLhs)
                {
                    path.AddLine(p[4], p[5]);
                }
                else
                {
                    path.AddLine(p[4], b[3]);
                }
            }
            else if (CurveBtmLhs)
            {
                path.AddLine(b[2], p[5]); // Rhs btm border to lhs arc 
            }
            else
            {
                path.AddLine(b[2], b[3]);// Rhs btm border to lhs border
            }

            return path;
        }

        #endregion


    }
}
