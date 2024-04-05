using System;
using System.Drawing;

namespace Axiom.Core.Drawables
{
    public class FocusableBackgroundDrawable : BackgroundDrawable
    {

        // Properties

        public float FocusBorderWidth { get; set; }

        public bool HasFocus { get; set; }

        // Constructors

        public FocusableBackgroundDrawable() : base()
        {
            FocusBorderWidth = 3;
        }

        // Events

        public override void EnabledChanged(bool enabled) { }

        public override bool MouseLeave(Point p) => !Path.IsVisible(p);

        public override bool MouseEnter(Point p) => Path.IsVisible(p);

        public override bool MouseDown(Point p) => Path.IsVisible(p);

        public override bool MouseUp(Point p) => Path.IsVisible(p);

        public override bool Click(Point p) => false;

        // Methods

        protected override bool IsRounded()
        {
            return Radius >= (Height - 2 * FocusBorderWidth) / 2;
        }

        protected override bool IsCircle()
        {
            return
               Math.Abs(Width - 2 * Radius - 2 * FocusBorderWidth) < 0.01 &&
               Math.Abs(Height - 2 * Radius - 2 * FocusBorderWidth) < 0.01;
        }

        protected override void DrawCircle(Graphics g)
        {
            // Get the line thickness of the focus pen
            float t = FocusBorderWidth;

            // Adjust the variables for a box shadow effect. Set
            // x and y by half the thickness because a pen draws 
            // along the centerline of the path
            float x = Location.X + (t / 2);
            float y = Location.Y + (t / 2);

            // Reduce the height and width by the thickness of 
            // the focus border. 
            float d = Width - t - 1; // diameter

            if (HasFocus)
            {
                // Create the border shadow for active state
                byte br = FocusColor.R;
                byte bg = FocusColor.G;
                byte bb = FocusColor.B;

                // Get the focus border at 25% opacity
                using (var c = GetCircle(x, y, d))
                using (var p = new Pen(Color.FromArgb(64, br, bg, bb), 2f))
                {
                    g.DrawPath(p, c);
                }
            }

            // Update the variables for the background and border
            x += t / 2;
            y += t / 2;
            d -= t;

            using (Path = GetCircle(x, y, d))
            using (var b = new SolidBrush(BackgroundColor))
            using (var p = new Pen(BorderColor))
            {
                g.FillPath(b, Path);
                g.DrawPath(p, Path);
            }
        }

        protected override void DrawRoundedRectangle(Graphics g)
        {
            // Get the line thickness of the focus pen
            float t = FocusBorderWidth;

            // Adjust the variables for a box shadow effect. Set
            // x and y by half the thickness because a pen draws 
            // along the centerline of the path
            float x = Location.X + (t / 2);
            float y = Location.Y + (t / 2);

            // Reduce the height and width by the thickness of 
            // the focus border. 
            float h = Height - t - 1;
            float w = Width - t - 1;
            float r = Radius + 1;

            if (HasFocus)
            {
                // Create the border shadow for active state
                byte br = FocusColor.R;
                byte bg = FocusColor.G;
                byte bb = FocusColor.B;

                // Get the focus border at 25% opacity
                using (var c = GetRoundedRectangle(x, y, w, h, r))
                using (var p = new Pen(Color.FromArgb(64, br, bg, bb), 2f))
                {
                    g.DrawPath(p, c);
                }
            }

            // Update the variables for the background and border
            x += t / 2;
            y += t / 2;
            h -= t;
            w -= t;
            r--;

            if (r * 2 > h)
            {
                r = h * 0.5f;
            }

            using (Path = GetRoundedRectangle(x, y, w, h, r))
            using (var b = new SolidBrush(BackgroundColor))
            using (var p = new Pen(BorderColor))
            {
                g.FillPath(b, Path);
                g.DrawPath(p, Path);
            }
        }


    }
}
