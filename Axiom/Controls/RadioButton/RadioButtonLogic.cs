using Axiom.Core.Drawables;
using Axiom.Core.Logic;
using Axiom.Core;
using System;
using System.Drawing;
using System.Windows.Forms;
using Axiom.Controls.Checkbox;

namespace Axiom.Controls
{
    class RadioButtonLogic : LogicBase, IAppearanceProperties
    {

        // =================
        // ===== Properties
        // =================

        public Color TextColor
        {
            get => _textColor;
            set => SetField(ref _textColor, value);
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

        public bool IsInverted
        {
            get => _isInverted;
            set => SetField(ref _isInverted, value);
        }

        public bool IsLight
        {
            get => _isLight;
            set => SetField(ref _isLight, value);
        }

        // ==================================
        // ===== Properties - backing fields
        // ==================================

        private bool _isOutlined;

        private bool _isInverted;

        private bool _isRounded;

        private bool _isLight;

        public Color _textColor;

        // =============
        // ===== Fields
        // =============

        private readonly FocusableBackgroundDrawable _box;

        private readonly BackgroundDrawable _check;

        // ===================
        // ===== Constructors
        // ===================

        public RadioButtonLogic() : base()
        {
            _box = new FocusableBackgroundDrawable();

            _check = new BackgroundDrawable();
        }

        // ======================
        // ===== Methods: public  
        // ======================

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            SetProperties(g);

            SetBackgroundProperties();

            SetCheckProperties();

            _box.Draw(g);

            if (State == AxState.Active)
            {
                _check.Draw(g);
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                // Measure the text
                var size = g.MeasureString(Text, Font);

                // TextRenderer draws slightly high at lower sizes
                double yOffset = Shape < AxShape.Medium ? Math.Round(0.1 * size.Height) : 0;

                // Get the x and y for the text position
                int y = (int)((Height - size.Height) * 0.5 + yOffset);
                int x = _box.Width + 4; // 10px padding

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
                CheckboxColorHelper.Get(Color, State, this, System.Drawing.Color.Transparent);

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
            Height = Height + (int)_box.FocusBorderWidth * 2 + 1;
        }

        private void SetWidth(Graphics g)
        {
            int textWidth = GetTextWidth(g);

            // The width needs to account for the 3px (left and right)
            // for the border shadow when the control is focused
            int borderShadowWidth = (int)_box.FocusBorderWidth * 2 + _box.Width;

            Width = textWidth + borderShadowWidth;
        }

        private void SetCheckProperties()
        {
            _check.BackgroundColor = ForegroundColor;

            _check.Height = GetCheckHeight();

            _check.Width = _check.Height;

            _check.Radius = GetCheckRadius();

            _check.Location = new Point
            {
                X = (_box.Width - _check.Width) / 2,
                Y = (Height - _check.Height) / 2
            };
        }

        private int GetCheckHeight()
        {
            int height;

            if (Shape == AxShape.Normal)
            {
                height = 8;
            }
            else if (Shape == AxShape.Small)
            {
                height = 6;
            }
            else if (Shape == AxShape.Medium)
            {
                height = 12;
            }
            else
            {
                height = 18; // Large
            }

            return height;
        }

        private float GetCheckRadius()
        {
            float radius;

            if (IsRounded)
            {
                radius = _check.Height / 2;
            }
            else
            {
                if (Shape == AxShape.Normal)
                {
                    radius = 1f;
                }
                else if (Shape == AxShape.Small)
                {
                    radius = 1f;
                }
                else if (Shape == AxShape.Medium)
                {
                    radius = 2f;
                }
                else // Large
                {
                    radius = 3f;
                }
            }

            return radius;
        }

        private void SetBackgroundProperties()
        {
            _box.BackgroundColor = BackgroundColor;

            _box.ForegroundColor = ForegroundColor;

            _box.BorderColor = BorderColor;

            _box.FocusColor = FocusColor;


            _box.Radius = GetBoxRadius();

            _box.Height = GetBoxHeight();

            _box.Width = _box.Height;


            _box.HasFocus = HasFocus;


            _box.Location = new Point
            {
                X = 0,
                Y = (Height - _box.Height) / 2
            };
        }

        private int GetBoxHeight()
        {
            int height;

            if (Shape == AxShape.Normal)
            {
                height = 16;
            }
            else if (Shape == AxShape.Small)
            {
                height = 14;
            }
            else if (Shape == AxShape.Medium)
            {
                height = 22;
            }
            else
            {
                height = 28; // Large
            }

            return height + (2 * (int)_box.FocusBorderWidth);
        }

        private float GetBoxRadius()
        {
            float radius;

            if (Shape == AxShape.Normal)
            {
                radius = IsRounded ? 8f : 4f;
            }
            else if (Shape == AxShape.Small)
            {
                radius = IsRounded ? 7f : 4f;
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


    }
}
