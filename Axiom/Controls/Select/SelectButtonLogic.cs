using System;
using System.Drawing;
using System.Windows.Forms;
using Axiom.Controls.Input;
using Axiom.Core;
using Axiom.Core.Drawables;
using Axiom.Core.Logic;

namespace Axiom.Controls.Select
{

    public class SelectButtonLogic : LogicBase
    {

        // =================
        // ===== Properties
        // =================

        public bool IsRounded
        {
            get => _isRounded;
            set => SetField(ref _isRounded, value);
        }

        public bool CurveTopLhs
        {
            get => _background.CurveTopLhs;
            set => _background.CurveTopLhs = value;
        }

        public bool CurveTopRhs
        {
            get => _background.CurveTopRhs;
            set => _background.CurveTopRhs = value;
        }

        public bool CurveBtmLhs
        {
            get => _background.CurveBtmLhs;
            set => _background.CurveBtmLhs = value;
        }

        public bool CurveBtmRhs
        {
            get => _background.CurveBtmRhs;
            set => _background.CurveBtmRhs = value;
        }

        // ==================================
        // ===== Properties - backing fields
        // ==================================

        private bool _isRounded;

        private bool _arrowCw;

        // =============
        // ===== Fields
        // =============

        private readonly FocusableBackgroundDrawable _background;

        private readonly LoadingSpinnerDrawable _spinner;

        private readonly RotatingArrowDrawable _arrow;

        // ===================
        // ===== Constructors
        // ===================

        public SelectButtonLogic(Action timerElapsed) : base()
        {
            _spinner = new LoadingSpinnerDrawable(timerElapsed);

            _arrow = new RotatingArrowDrawable(timerElapsed);

            _background = new FocusableBackgroundDrawable();

            SetProperties();
        }

        // =============
        // ===== Events
        // =============

        public override bool Click(Point p)
        {
            ToggleArrow();
            return true;
        }

        // ======================
        // ===== Methods: public  
        // ======================

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            SetProperties();

            SetArrowProperties();

            SetSpinnerProperties();

            SetBackgroundProperties();

            _background.Draw(g);

            if (!string.IsNullOrWhiteSpace(Text))
            {
                var size = g.MeasureString(Text, Font);

                // TextRenderer draws slightly high at lower sizes
                double yOffset = Shape < AxShape.Medium ? Math.Round(0.1 * size.Height) : 0;

                int y = (int)((Height - size.Height) * 0.5 + yOffset);

                int x = GetTextX();

                var p = new Point(x, y);

                int w = Width - x - GetRightPadding() - _arrow.Width;

                string text = Text;

                if (size.Width > w)
                {
                    double widthPerChar = text.Length / size.Width;

                    int numChars = (int)(widthPerChar * w);

                    // Go to numChars - 1 to allow for the "..."
                    text = Text.Substring(0, numChars - 1) + "...";
                }

                TextRenderer.DrawText(g, text, Font, p, ForegroundColor);
            }

            if (State == AxState.Loading && !DesignMode)
            {
                _spinner.Draw(g);
            }
            else
            {
                _arrow.Draw(g);
            }
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

        private void SetArrowProperties()
        {
            if (State == AxState.Loading)
                return;

            _arrow.ForegroundColor = ForegroundColor;

            _arrow.BackgroundColor = BackgroundColor;

            _arrow.Height = (int)(Height * 0.12);

            _arrow.Width = (int)(Height * 0.24);

            int padding = GetRightPadding();

            _arrow.Location = new Point
            {
                X = Width - _arrow.Width - padding,
                Y = (Height - _arrow.Height) / 2
            };
        }

        private void SetSpinnerProperties()
        {
            if (State != AxState.Loading)
            {
                _spinner.IsLoading = false;
                return;
            }

            int padding = GetRightPadding();

            int wh = (int)(Height * 0.4);

            _spinner.Height = wh;

            _spinner.Width = wh;

            _spinner.Location = new Point
            {
                X = Width - wh - padding,
                Y = (Height - wh) / 2
            };

            _spinner.ForegroundColor = ForegroundColor;

            _spinner.IsLoading = true;
        }

        private void SetBackgroundProperties()
        {
            if (Shape == AxShape.Normal)
            {
                _background.Radius = IsRounded ? 20f : 6f;
            }
            else if (Shape == AxShape.Small)
            {
                _background.Radius = IsRounded ? 15f : 2f;
            }
            else if (Shape == AxShape.Medium)
            {
                _background.Radius = IsRounded ? 25f : 7.5f;
            }
            else if (Shape == AxShape.Large)
            {
                _background.Radius = IsRounded ? 30f : 9f;
            }

            _background.BackgroundColor = BackgroundColor;

            _background.ForegroundColor = ForegroundColor;

            _background.BorderColor = BorderColor;

            _background.FocusColor = FocusColor;

            _background.HasFocus = HasFocus;

            _background.Height = Height;

            _background.Width = Width;
        }

        private void SetColors()
        {
            (BackgroundColor, ForegroundColor, BorderColor, FocusColor) =
                InputColorHelper.Get(Color, State, System.Drawing.Color.Transparent); // TODO: Create select color helper
        }

        private void SetFont()
        {
            if (Shape == AxShape.Small)
            {
                Font = new Font("Segoe UI Semibold", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            else if (Shape == AxShape.Normal)
            {
                Font = new Font("Segoe UI Semibold", 16, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            else if (Shape == AxShape.Medium)
            {
                Font = new Font("Segoe UI", 20, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            else if (Shape == AxShape.Large)
            {
                Font = new Font("Segoe UI", 24, FontStyle.Regular, GraphicsUnit.Pixel);
            }
        }

        private void SetHeight()
        {
            if (Shape == AxShape.Small)
            {
                Height = 30;
            }
            else if (Shape == AxShape.Normal)
            {
                Height = 40;
            }
            else if (Shape == AxShape.Medium)
            {
                Height = 50;
            }
            else if (Shape == AxShape.Large)
            {
                Height = 60;
            }

            // The height needs to account for the 3px focus border
            // (top and bottom) plus the reduction in the controls
            // paintable area (1px less the height and width).
            Height = Height + (int)_background.FocusBorderWidth * 2 + 1;
        }

        private int GetTextX()
        {
            int x = (int)(_background.Radius + _background.FocusBorderWidth);

            if (!IsRounded && Shape == AxShape.Small)
            {
                // The small shape without rounded sides has a very
                // small radius, hence * 2
                x = x * 2;
            }

            return x;
        }

        private int GetTextY(Graphics g)
        {
            var size = g.MeasureString(Text, Font);

            // TextRenderer draws slightly high at lower sizes
            double yOffset = Shape < AxShape.Medium ? Math.Round(0.1 * size.Height) : 0;

            return (int)((Height - size.Height) * 0.5 + yOffset);

        }

        private int GetRightPadding()
        {
            int x = (int)(_background.Radius + _background.FocusBorderWidth);

            if (!IsRounded)
            {
                x = x * 2;
            }

            return x;
        }

        private void ToggleArrow()
        {
            double from = _arrowCw ? 180 : 0;

            double to = _arrowCw ? 0 : 180;

            _arrow.RotateBy(from, to);

            _arrowCw = !_arrowCw;

        }

        public void ClosedState()
        {
            _arrow.RotateBy(180, 0);

            _arrowCw = false;
        }

        public void OpenState()
        {
            _arrow.RotateBy(0, 180);

            _arrowCw = true;
        }


    }
}
