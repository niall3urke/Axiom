using System.Drawing.Drawing2D;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Axiom.Core.Drawables
{
    public class ShadowDrawable : BackgroundDrawable
    {


        // Properties 

        public AxShadowDirection Direction
        {
            get => _direction;
            set => SetField(ref _direction, value);
        }

        public float Blur
        {
            get => _blur;
            set => SetField(ref _blur, value);
        }

        public int Depth
        {
            get => _depth; 
            set => SetField(ref _depth, value);
        }

        // Fields

        private AxShadowDirection _direction;

        private float _blur;

        private int _depth;

        // Constructors

        public ShadowDrawable() : base()
        {
            Direction = AxShadowDirection.BottomRight;
            Blur = 0.45f;
            Depth = 6;
        }

        // Methods

        // Methods - private

        public override void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            SetShadowLocation();

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

        // Methods: protected overridable

        protected override void DrawCircle(Graphics g)
        {
            float x = Location.X;
            float y = Location.Y;

            // We used to reduce the diameter by 1px to account for the 1px removed
            // from the control's width and height, but this should be account for
            // by the control itself, not the drawable... TODO!
            float d = Radius * 2 - 1;

            using (Path = GetCircle(x, y, d))
                DrawShadow(g, Path);
        }

        protected override void DrawRoundedRectangle(Graphics g)
        {
            float x = Location.X;
            float y = Location.Y;
            float h = Height;
            float w = Width;
            float r = Radius;

            using (Path = GetRoundedRectangle(x, y, w, h, r))
                DrawShadow(g, Path);
        }

        protected virtual void DrawShadow(Graphics g, GraphicsPath p)
        {
            using (var pgb = new PathGradientBrush(p))
            {
                // Set the center to the middle
                pgb.CenterPoint = new PointF(0.5f * Width, 0.5f * Height);

                // Fade out to the back color
                pgb.SurroundColors = new Color[] { Color.Transparent };

                // Set the blur scale in the x and y directions
                pgb.FocusScales = new PointF(Blur, Blur);

                // Set the base color for the shadow
                pgb.CenterColor = Color.Black;

                // Draw our shadow
                g.FillPath(pgb, p);
            }
        }

        private void SetShadowLocation()
        {
            if (Direction == AxShadowDirection.Centered)
            {
                Location = new PointF(Depth * 0.5f, Depth * 0.5f);
            }
            else if (Direction == AxShadowDirection.Top)
            {
                Location = new PointF(Depth * 0.5f, 0);
            }
            else if (Direction == AxShadowDirection.Right)
            {
                Location = new PointF(Depth, Depth * 0.5f);
            }
            else if (Direction == AxShadowDirection.Bottom)
            {
                Location = new PointF(Depth * 0.5f, Depth);
            }
            else if (Direction == AxShadowDirection.Left)
            {
                Location = new PointF(0, Depth * 0.5f);
            }
            else if (Direction == AxShadowDirection.TopLeft)
            {
                Location = new PointF(0, 0);
            }
            else if (Direction == AxShadowDirection.BottomLeft)
            {
                Location = new PointF(0, Depth);
            }
            else if (Direction == AxShadowDirection.BottomRight)
            {
                Location = new PointF(Depth, Depth);
            }
            else if (Direction == AxShadowDirection.TopRight)
            {
                Location = new PointF(Depth, 0);
            }
        }


    }
}
