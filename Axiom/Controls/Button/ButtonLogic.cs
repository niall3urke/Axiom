using Axiom.Controls.ColorHelpers;
using Axiom.Core.Drawables;
using Axiom.Core.Logic;
using Axiom.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Axiom.Controls
{
    class ButtonLogic : LogicBase, IAppearanceProperties
    {

        // =================
        // ===== Properties
        // =================

        public bool IsOutlined
        {
            get => _isOutlined;
            set => SetField(ref _isOutlined, value);
        }

        public bool IsInverted
        {
            get => _isInverted;
            set => SetField(ref _isInverted, value);
        }

        public bool IsRounded
        {
            get => _isRounded;
            set => SetField(ref _isRounded, value);
        }

        public bool IsStatic
        {
            get => _isStatic;
            set => SetField(ref _isStatic, value);
        }

        public bool IsLight
        {
            get => _isLight;
            set => SetField(ref _isLight, value);
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

        private bool _isOutlined;

        private bool _isInverted;

        private bool _isRounded;

        private bool _isStatic;

        private bool _isLight;

        // =============
        // ===== Fields
        // =============

        private readonly FocusableBackgroundDrawable _background;

        private readonly LoadingSpinnerDrawable _spinner;

        // ===================
        // ===== Constructors
        // ===================

        public ButtonLogic(Action loadingElapsed) : base()
        {
            _background = new FocusableBackgroundDrawable();
            _spinner = new LoadingSpinnerDrawable(loadingElapsed);
        }

        // ======================
        // ===== Methods: public  
        // ======================

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            SetProperties(g);

            SetSpinnerProperties();

            SetBackgroundProperties();

            _background.Draw(g);

            if (State == AxState.Loading && !DesignMode)
            {
                _spinner.IsLoading = true;
                _spinner.Draw(g);
            }
            else if (!string.IsNullOrWhiteSpace(Text))
            {
                // Stop the spinner if its active
                _spinner.IsLoading = false;

                // Measure the text
                var size = g.MeasureString(Text, Font);

                // TextRenderer draws slightly high at lower sizes
                double yOffset = Shape < AxShape.Medium ? Math.Round(0.1 * size.Height) : 0;

                // Get the x and y for the text position
                int y = (int)((Height - size.Height) * 0.5 + yOffset);
                int x = (int)((Width - size.Width) * 0.5);

                // We render the background with a reduced height and width
                // because a controls paintable region is 1px less. Having 
                // the text render 1px lower actually looks better, but the  
                // width needs adjusting to make it look right.
                x += 1;

                // Draw the text
                TextRenderer.DrawText(g, Text, Font, new Point(x, y), ForegroundColor);
            }
        }

        // =======================
        // ===== Methods: private  
        // =======================

        private void SetProperties(Graphics g)
        {
            // Always do set font before set width which measures the text
            SetFont();
            SetColors();
            SetHeight();
            SetWidth(g);
        }

        private void SetSpinnerProperties()
        {
            int wh = (int)(Height * 0.4);

            _spinner.Height = wh;

            _spinner.Width = wh;

            _spinner.Location = new Point
            {
                X = (Width - wh) / 2,
                Y = (Height - wh) / 2
            };

            _spinner.ForegroundColor = ForegroundColor;
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
                ButtonColorHelper.Get(Color, State, this, System.Drawing.Color.Transparent);
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

            if (Color == AxColor.Text)
            {
                Font = new Font("Segoe UI", Font.Size, FontStyle.Underline, Font.Unit);
            }
            else if (Color == AxColor.Ghost && (State == AxState.Hover || State == AxState.Active))
            {
                Font = new Font("Segoe UI", Font.Size, FontStyle.Underline, Font.Unit);
            }
            else if (Color == AxColor.Ghost)
            {
                Font = new Font("Segoe UI", Font.Size, FontStyle.Regular, Font.Unit);
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

        private void SetWidth(Graphics g)
        {
            int textWidth = GetTextWidth(g);

            int padding = GetPadding();

            // The width needs to account for the 3px (left and right)
            // for the border shadow when the control is focused
            int borderShadowWidth = 6;

            Width = textWidth + padding + borderShadowWidth;
        }

        private int GetPadding()
        {
            int padding = 0;

            if (Shape == AxShape.Small)
            {
                padding = IsRounded ? 15 : 12;
            }
            else if (Shape == AxShape.Normal)
            {
                padding = _isRounded ? 20 : 16;
            }
            else if (Shape == AxShape.Medium)
            {
                padding = IsRounded ? 25 : 20;
            }
            else if (Shape == AxShape.Large)
            {
                padding = IsRounded ? 30 : 24;
            }

            return padding;
        }


    }
}
