using Axiom.Core.Drawables;
using Axiom.Core.Logic;
using Axiom.Core;
using System;
using System.Drawing;

namespace Axiom.Controls.Input
{
    class InputLogic : LogicBase
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

        public Point TextLocation { get; private set; }

        public Size TextSize { get; private set; }

        // ==================================
        // ===== Properties - backing fields
        // ==================================

        private bool _isRounded;

        // =============
        // ===== Fields
        // =============

        private readonly FocusableBackgroundDrawable _background;

        private readonly LoadingSpinnerDrawable _spinner;

        // ===================
        // ===== Constructors
        // ===================

        public InputLogic(Action loadingElapsed) : base()
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

            SetProperties();

            SetSpinnerProperties();

            SetBackgroundProperties();

            // Set the textbox property after the background properties
            // as we make use of the _background.Radius value for offsets

            SetTextboxProperties();

            _background.Draw(g);

            if (State == AxState.Loading && !DesignMode)
            {
                _spinner.Draw(g);
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

        private void SetSpinnerProperties()
        {
            if (State == AxState.Loading)
            {
                _spinner.ForegroundColor = ForegroundColor;

                _spinner.Height = (int)(Height * 0.4); ;

                _spinner.Width = _spinner.Height;

                _spinner.Location = new Point
                {
                    X = Width - _spinner.Width - GetRightPadding(),
                    Y = (Height - _spinner.Height) / 2
                };

                _spinner.IsLoading = true;
            }
            else
            {
                _spinner.IsLoading = false;
            }
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

            _background.Height = Height;

            _background.Width = Width;

            // For input, the focus is shown when it's active

            _background.HasFocus = State == AxState.Active;

        }

        private void SetTextboxProperties()
        {
            SetTextSize();
            SetTextLocation();
        }

        private void SetTextLocation()
        {
            int x = (int)(_background.Radius + _background.FocusBorderWidth);

            if (!IsRounded && Shape == AxShape.Small)
            {
                // The small shape without rounded sides has a very
                // small radius, hence * 2
                x = x * 2;
            }

            if (Shape == AxShape.Normal)
            {
                TextLocation = new Point(x, 13);
            }
            else if (Shape == AxShape.Small)
            {
                TextLocation = new Point(x, 11);
            }
            else if (Shape == AxShape.Medium)
            {
                TextLocation = new Point(x, 14);
            }
            else if (Shape == AxShape.Large)
            {
                TextLocation = new Point(x, 16);
            }
        }

        private void SetTextSize()
        {
            int x = (int)(_background.Radius + _background.FocusBorderWidth);

            if (!IsRounded && Shape == AxShape.Small)
            {
                // The small shape without rounded sides has a very
                // small radius, hence * 2
                x = x * 2;
            }

            // The Width property is for the overall control, but we want 
            // the width of the textbox within the drawn area, so x2
            x = x * 2;

            // If loading, reduce the width to show the loading spinner
            if (State == AxState.Loading)
            {
                x = x + _spinner.Width + GetRightPadding();
            }

            if (Shape == AxShape.Normal)
            {
                TextSize = new Size(Width - x, 22);
            }
            else if (Shape == AxShape.Small)
            {
                TextSize = new Size(Width - x, 16);
            }
            else if (Shape == AxShape.Medium)
            {
                TextSize = new Size(Width - x, 28);
            }
            else if (Shape == AxShape.Large)
            {
                TextSize = new Size(Width - x, 32);
            }
        }

        private void SetColors()
        {
            (BackgroundColor, ForegroundColor, BorderColor, FocusColor) =
                InputColorHelper.Get(Color, State, System.Drawing.Color.Transparent);
        }

        private void SetFont()
        {
            if (Shape == AxShape.Small)
            {
                Font = new Font("Segoe UI", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            else if (Shape == AxShape.Normal)
            {
                Font = new Font("Segoe UI", 16, FontStyle.Regular, GraphicsUnit.Pixel);
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

        private int GetRightPadding()
        {
            int x = (int)(_background.Radius + _background.FocusBorderWidth);

            if (!IsRounded)
            {
                x = x * 2;
            }

            return x;
        }


    }
}
