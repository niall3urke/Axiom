using Axiom.Core.Drawables;
using Axiom.Core.Logic;
using Axiom.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Axiom.Controls.Switch
{
    class SwitchLogic : LogicBase, IAppearanceProperties
    {

        // =================
        // ===== Properties
        // =================

        public Color TextColor
        {
            get => _textColor;
            set => SetField(ref _textColor, value);
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

        public bool IsOutlined
        {
            get => _isOutlined;
            set => SetField(ref _isOutlined, value);
        }

        public bool IsLight
        {
            get => _isLight;
            set => SetField(ref _isLight, value);
        }

        public bool IsThin
        {
            get => _isThin;
            set => SetField(ref _isThin, value);
        }

        // ==================================
        // ===== Properties - backing fields
        // ==================================

        private bool _isInverted;

        private bool _isOutlined;

        private bool _isRounded;

        private bool _isLight;

        private bool _isThin;

        public Color _textColor;

        // =============
        // ===== Fields
        // =============

        private readonly FocusableBackgroundDrawable _track;

        private readonly BackgroundDrawable _slider;

        // ===================
        // ===== Constructors
        // ===================

        public SwitchLogic() : base()
        {
            _track = new FocusableBackgroundDrawable();

            _slider = new BackgroundDrawable();

            _isRounded = true;

            _isThin = true;
        }

        // ======================
        // ===== Methods: public  
        // ======================

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            SetProperties(g);

            SetSliderProperties();

            SetTrackProperties();

            _track.Draw(g);

            _slider.Draw(g);

            if (!string.IsNullOrWhiteSpace(Text))
            {
                // Measure the text
                var size = g.MeasureString(Text, Font);

                // TextRenderer draws slightly high at lower sizes
                double yOffset = Shape < AxShape.Medium ? Math.Round(0.1 * size.Height) : 0;

                // Get the x and y for the text position
                int y = (int)((Height - size.Height) * 0.5 + yOffset);
                int x = _track.Width + 4; // 10px padding

                // We render the background with a reduced height and width
                // because a controls paintable region is 1px less. Having 
                // the text render 1px lower actually looks better, but the  
                // width needs adjusting to make it look right.
                x += 1;

                // Draw the text
                TextRenderer.DrawText(g, Text, Font, new Point(x, y), TextColor);
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

        // ===== Methods: control properties

        private void SetColors()
        {
            (BackgroundColor, ForegroundColor, BorderColor, FocusColor) =
                SwitchColorHelper.Get(Color, State, this, System.Drawing.Color.Transparent);

            _textColor = System.Drawing.Color.FromArgb(54, 54, 54);
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
            Height = Height + 2 * (int)_track.FocusBorderWidth + 1;
        }

        private void SetWidth(Graphics g)
        {
            int textWidth = GetTextWidth(g);

            // The width needs to account for the 3px (left and right)
            // for the border shadow when the control is focused
            int borderShadowWidth = 6;

            Width = textWidth + borderShadowWidth + GetTrackWidth();
        }

        // ===== Methods: background properties

        private void SetTrackProperties()
        {
            _track.BackgroundColor = BackgroundColor;

            _track.ForegroundColor = ForegroundColor;

            _track.BorderColor = BorderColor;

            _track.FocusColor = FocusColor;


            _track.Radius = GetTrackRadius();

            _track.Height = GetTrackHeight();

            _track.Width = GetTrackWidth();


            _track.HasFocus = HasFocus;


            _track.Location = new Point
            {
                X = 0,
                Y = (Height - _track.Height) / 2
            };
        }

        private int GetTrackWidth()
        {
            int width;

            if (Shape == AxShape.Normal)
            {
                width = 48;
            }
            else if (Shape == AxShape.Small)
            {
                width = 36;
            }
            else if (Shape == AxShape.Medium)
            {
                width = 60;
            }
            else
            {
                width = 72;
            }

            return width + 2 * (int)_track.FocusBorderWidth + 1; // Large
        }

        private int GetTrackHeight()
        {
            int height;

            if (Shape == AxShape.Normal)
            {
                height = IsThin ? 6 : 24;
            }
            else if (Shape == AxShape.Small)
            {
                height = IsThin ? 4 : 18;
            }
            else if (Shape == AxShape.Medium)
            {
                height = IsThin ? 8 : 30;
            }
            else
            {
                height = IsThin ? 10 : 36; // Large
            }

            return height + 2 * (int)_track.FocusBorderWidth;
        }

        private float GetTrackRadius()
        {
            float radius;

            if (IsRounded)
            {
                radius = (GetTrackHeight() - 2 * (int)_track.FocusBorderWidth) * 0.5f;
            }
            else if (IsThin)
            {
                radius = (GetTrackHeight() - 2 * (int)_track.FocusBorderWidth) * 0.5f;
            }
            else
            {
                radius = 4f; // Not rounded, not thin
            }

            return radius;
        }

        // Methods: slider properties

        private void SetSliderProperties()
        {
            _slider.BackgroundColor = ForegroundColor;

            _slider.ForegroundColor = ForegroundColor;

            _slider.BorderColor = BorderColor;


            _slider.Radius = GetSliderRadius();

            _slider.Height = GetSliderHeight();

            _slider.Width = _slider.Height;


            _slider.Location = new Point
            {
                X = GetSliderX(),
                Y = (Height - _slider.Height) / 2
            };
        }

        private int GetSliderHeight()
        {
            int height = 0;

            if (Shape == AxShape.Normal)
            {
                height = 16;
            }
            else if (Shape == AxShape.Small)
            {
                height = 10;
            }
            else if (Shape == AxShape.Medium)
            {
                height = 22;
            }
            else if (Shape == AxShape.Large)
            {
                height = 28;
            }

            return height;
        }

        private float GetSliderRadius()
        {
            float radius;

            if (Shape == AxShape.Normal)
            {
                radius = IsRounded ? 8f : 4f;
            }
            else if (Shape == AxShape.Small)
            {
                radius = IsRounded ? 5f : 4f;
            }
            else if (Shape == AxShape.Medium)
            {
                radius = IsRounded ? 11f : 4f;
            }
            else // Large
            {
                radius = IsRounded ? 14f : 4f;
            }

            return radius;
        }

        private int GetSliderX()
        {
            if (State == AxState.Active)
            {
                return GetTrackWidth() - GetSliderHeight() - 8;
            }

            return 8;
        }


    }
}
