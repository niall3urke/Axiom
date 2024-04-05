using System.Drawing;
using System.Windows.Forms;
using Axiom.Core.Logic;
using Axiom.Core.Drawables;
using Axiom.Core;

namespace Axiom.Controls.Select
{
    public class SelectItemLogic : LogicBase
    {

        // =================
        // ===== Properties
        // =================

        public int MinWidth { get; set; }

        // =============
        // ===== Fields
        // =============

        private readonly BackgroundDrawable _background;

        // ===================
        // ===== Constructors
        // ===================

        public SelectItemLogic() : base()
        {
            _background = new BackgroundDrawable
            {
                CurveBtmLhs = false,
                CurveBtmRhs = false,
                CurveTopLhs = false,
                CurveTopRhs = false
            };

            SetProperties();

            SetBackgroundProperties();
        }

        // ======================
        // ===== Methods: public  
        // ======================

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            SetProperties();

            SetBackgroundProperties();

            _background.Draw(g);

            if (!string.IsNullOrWhiteSpace(Text))
            {
                var size = g.MeasureString(Text, Font);

                int x = GetPadding();

                int y = Location.Y + (int)((Height - size.Height) * 0.5);

                TextRenderer.DrawText(g, Text, Font, new Point(x, y), ForegroundColor);
            }
        }

        public int GetMinWidth(Graphics g)
        {
            MinWidth = (int)g.MeasureString(Text, Font).Width + (GetPadding() * 2);
            return MinWidth;
        }

        // =======================
        // ===== Methods: private  
        // =======================

        private void SetProperties()
        {
            // Always do set font before set width which measures the text
            SetFont();
            SetColors();
            SetHeight();
        }

        private void SetBackgroundProperties()
        {
            _background.BackgroundColor = BackgroundColor;

            _background.ForegroundColor = ForegroundColor;

            _background.BorderColor = BorderColor;

            _background.Location = Location;

            _background.Height = Height;

            _background.Width = Width;
        }

        private void SetColors()
        {
            (BackgroundColor, ForegroundColor, BorderColor) =
                SelectItemColorHelper.GetColors(Color, State, System.Drawing.Color.Transparent);
        }

        private void SetFont()
        {
            if (Shape == AxShape.Small)
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            else if (Shape == AxShape.Normal)
            {
                Font = new Font("Segoe UI", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            else if (Shape == AxShape.Medium)
            {
                Font = new Font("Segoe UI", 18, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            else if (Shape == AxShape.Large)
            {
                Font = new Font("Segoe UI", 22, FontStyle.Regular, GraphicsUnit.Pixel);
            }
        }

        private void SetHeight()
        {
            if (Shape == AxShape.Small)
            {
                Height = 23;
            }
            else if (Shape == AxShape.Normal)
            {
                Height = 33;
            }
            else if (Shape == AxShape.Medium)
            {
                Height = 43;
            }
            else if (Shape == AxShape.Large)
            {
                Height = 53;
            }
        }

        private int GetPadding()
        {
            int padding = 0;

            if (Shape == AxShape.Small)
            {
                padding = 10;
            }
            else if (Shape == AxShape.Normal)
            {
                padding = 12;
            }
            else if (Shape == AxShape.Medium)
            {
                padding = 14;
            }
            else if (Shape == AxShape.Large)
            {
                padding = 16;
            }

            return padding;
        }


    }
}
