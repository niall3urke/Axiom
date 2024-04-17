using System.Drawing;
using System.Drawing.Drawing2D;

namespace Axiom.Core.Drawables
{
    public class ShadowDrawable : BackgroundDrawable, ICanCastShadow
    {

        // Properties 
        
        public AxShadowDirection ShadowDirection
        {
            get => _shadowDirection;
            set => SetField(ref _shadowDirection, value);
        }

        public float ShadowOpacity
        {
            get => _shadowOpacity;
            set
            {
                if (value < 0)
                    value = 0;

                if (value > 1)
                    value = 1;

                SetField(ref _shadowOpacity, value);
            }
        }

        public Color ShadowColor
        {
            get => _shadowColor;
            set => SetField(ref _shadowColor, value);
        }

        public float ShadowBlur
        {
            get => _shadowBlur;
            set
            {
                if (value < 0)
                    value = 0;

                if (value > 1)
                    value = 1;

                SetField(ref _shadowBlur, value);
            }
        }

        public int ShadowSpread
        {
            get => _shadowSpread;
            set => SetField(ref _shadowSpread, value);
        }

        public bool HasShadow
        {
            get => _hasShadow;
            set => SetField(ref _hasShadow, value);
        }


        // Fields

        private AxShadowDirection _shadowDirection;

        private float _shadowOpacity;

        private Color _shadowColor;

        private float _shadowBlur;

        private int _shadowSpread;

        private bool _hasShadow;

        // Constructors

        public ShadowDrawable() : base()
        {
            _shadowDirection = AxShadowDirection.BottomRight;
            _shadowColor = Color.Black;
            _shadowOpacity = 1f;
            _shadowBlur = 0.45f;
            _shadowSpread = 6;
            _hasShadow = true;
        }

        // Methods

        // Methods - private

        public override void Draw(Graphics g)
        {
            if (!HasShadow)
                return;

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
                pgb.FocusScales = new PointF(ShadowBlur, ShadowBlur);

                // Set the base color for the shadow
                pgb.CenterColor = GetShadowColor();

                // Draw our shadow
                g.FillPath(pgb, p);
            }
        }

        private Color GetShadowColor() =>
            Color.FromArgb((int)(255 * ShadowOpacity), ShadowColor.R, ShadowColor.G, ShadowColor.B);

        private void SetShadowLocation()
        {
            if (ShadowDirection == AxShadowDirection.Centered)
            {
                Location = new PointF(ShadowSpread * 0.5f, ShadowSpread * 0.5f);
            }
            else if (ShadowDirection == AxShadowDirection.Top)
            {
                Location = new PointF(ShadowSpread * 0.5f, 0);
            }
            else if (ShadowDirection == AxShadowDirection.Right)
            {
                Location = new PointF(ShadowSpread, ShadowSpread * 0.5f);
            }
            else if (ShadowDirection == AxShadowDirection.Bottom)
            {
                Location = new PointF(ShadowSpread * 0.5f, ShadowSpread);
            }
            else if (ShadowDirection == AxShadowDirection.Left)
            {
                Location = new PointF(0, ShadowSpread * 0.5f);
            }
            else if (ShadowDirection == AxShadowDirection.TopLeft)
            {
                Location = new PointF(0, 0);
            }
            else if (ShadowDirection == AxShadowDirection.BottomLeft)
            {
                Location = new PointF(0, ShadowSpread);
            }
            else if (ShadowDirection == AxShadowDirection.BottomRight)
            {
                Location = new PointF(ShadowSpread, ShadowSpread);
            }
            else if (ShadowDirection == AxShadowDirection.TopRight)
            {
                Location = new PointF(ShadowSpread, 0);
            }
        }


    }
}
