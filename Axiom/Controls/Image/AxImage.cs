using Axiom.Core;
using Axiom.Core.Bases;
using Axiom.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static Axiom.Controls.Image.AxImage;

namespace Axiom.Controls.Image
{
    [ToolboxItem(true)]
    public class AxImage : AxPictureBoxBase, IAxControl
    {

        // ============================
        // ===== Properties: browsable 
        // ============================

        [TypeConverter(typeof(EnumDescriptionConverter))]
        [Category(Category), DisplayName("Aspect Ratio")]
        public AspectRatio Aspect { get; set; }

        [Category(Category), DisplayName("Border")]
        public Color BorderColor
        {
            get => _image.BorderColor;
            set => _image.BorderColor = value;
        }

        [Category(Category), DisplayName("Radius")]
        public AxShape Shape
        {
            get => _image.Shape;
            set => _image.Shape = value;
        }

        [Category(Category), DisplayName("Rounded")]
        public bool IsRounded
        {
            get => _image.IsRounded;
            set => _image.IsRounded = value;
        }

        [Category(Category), DisplayName("Clickable")]
        public bool IsClickable
        {
            get => _image.IsClickable;
            set => _image.IsClickable = value;
        }

        [Category(Category), DisplayName("Circle")]
        public bool IsCircle
        {
            get => _image.IsCircle;
            set
            {
                _image.IsCircle = value;
                if (_image.IsCircle)
                {
                    int max = Math.Max(Height, Width);
                    Height = max;
                    Width = max;
                }
            }
        }

        [Category(CategoryAdvanced), DisplayName("Curve Top LHS")]
        public bool CurveTopLhs
        {
            get => _image.CurveTopLhs;
            set => _image.CurveTopLhs = value;
        }

        [Category(CategoryAdvanced), DisplayName("Curve Top RHS")]
        public bool CurveTopRhs
        {
            get => _image.CurveTopRhs;
            set => _image.CurveTopRhs = value;
        }

        [Category(CategoryAdvanced), DisplayName("Curve Bottom LHS")]
        public bool CurveBtmLhs
        {
            get => _image.CurveBtmLhs;
            set => _image.CurveBtmLhs = value;
        }

        [Category(CategoryAdvanced), DisplayName("Curve Bottom RHS")]
        public bool CurveBtmRhs
        {
            get => _image.CurveBtmRhs;
            set => _image.CurveBtmRhs = value;
        }

        // ================================
        // ===== Properties: non-browsable
        // ================================

        [Browsable(false)]
        public AxColor Color { get; set; }

        [Browsable(false)]
        public bool IsOutlined { get; set; }

        [Browsable(false)]
        public bool IsInverted { get; set; }

        [Browsable(false)]
        public bool IsLight { get; set; }

        [Browsable(false)]
        public AxState State
        {
            get => _image.State;
            set
            {
                if (!Enabled && value != AxState.Disabled)
                {
                    Enabled = true;
                }
                _image.State = value;
            }
        }

        // =============
        // ===== Fields 
        // =============

        private readonly ImageLogic _image;

        // ===================
        // ===== Constructors
        // ===================

        public AxImage() : base()
        {
            _image = new ImageLogic
            {
                BorderColor = BackColor,
                Height = Height,
                Width = Width
            };
            _image.PropertyChanged += (s, e) => Invalidate();
        }

        // =============
        // ===== Events
        // =============

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            // Redraw control once layout is set
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (_applyingAspectRatio)
                return;

            if (_image.IsCircle)
            {
                int max = Math.Max(Height, Width);
                Height = max;
                Width = max;
            }
            else if (Aspect != AspectRatio.Free)
            {
                ApplyAspectRatio();
            }

            _image.Height = Height;
            _image.Width = Width;
        }

        protected override void OnClick(EventArgs e)
        {
            if (_image.State != AxState.Loading)
            {
                this.Focus();
                base.OnClick(e);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!_image.IsClickable)
                return;

            if (_image.State != AxState.Loading)
            {
                _image.State = AxState.Active;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!_image.IsClickable)
                return;

            if (_image.State != AxState.Loading)
            {
                _image.State = AxState.Hover;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!_image.IsClickable)
                return;

            if (ContainsCursor() && _image.State != AxState.Loading)
            {
                _image.State = AxState.Hover;
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!_image.IsClickable)
                return;

            if (!ContainsCursor() && _image.State != AxState.Loading)
            {
                _image.State = AxState.Normal;
                Cursor = Cursors.Default;
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _image.State = Enabled ? AxState.Normal : AxState.Disabled;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Ensure this is always up to date
            _image.BorderColor = BackColor;

            // Crop the paintable region depending on user settings
            Region = _image.GetRegion();

            // Paint the picture
            base.OnPaint(e);

            // Paint the border to create smooth edges effect
            _image.Draw(e.Graphics);
        }

        // ==============
        // ===== Methods
        // ==============

        private bool _applyingAspectRatio;

        private void ApplyAspectRatio()
        {
            try
            {
                _applyingAspectRatio = true;

                double ratio = AspectRatios[Aspect];

                int w, h;

                if ((float)Width / Height > ratio)
                {
                    w = (int)(Height * ratio);
                    h = Height;
                }
                else
                {
                    w = Width;
                    h = (int)(Width / ratio);
                }

                Size = new Size(w, h);
            }
            finally
            {
                _applyingAspectRatio = false;
            }
        }

        // ============
        // ===== Enums
        // ============

        public enum AspectRatio
        {
            [Description("Free")]
            Free,
            [Description("1 by 1")]
            Ratio1By1,
            [Description("5 by 4")]
            Ratio5By4,
            [Description("4 by 3")]
            Ratio4By3,
            [Description("3 by 2")]
            Ratio3By2,
            [Description("5 by 3")]
            Ratio5By3,
            [Description("16 by 9")]
            Ratio16By9,
            [Description("2 by 1")]
            Ratio2By1,
            [Description("3 by 1")]
            Ratio3By1,
            [Description("4 by 5")]
            Ratio4By5,
            [Description("3 by 4")]
            Ratio3By4,
            [Description("2 by 3")]
            Ratio2By3,
            [Description("3 by 5")]
            Ratio3By5,
            [Description("9 by 16")]
            Ratio9By16,
            [Description("1 by 2")]
            Ratio1By2,
            [Description("1 by 3")]
            Ratio1By3,
        }

        public static Dictionary<AspectRatio, double> AspectRatios = new Dictionary<AspectRatio, double>()
        {
            { AspectRatio.Ratio16By9, 1.7778 },
            { AspectRatio.Ratio9By16, 0.5625 },
            { AspectRatio.Ratio2By3, 0.6666 },
            { AspectRatio.Ratio1By3, 0.3333 },
            { AspectRatio.Ratio1By2, 0.5000 },
            { AspectRatio.Ratio3By5, 0.6000 },
            { AspectRatio.Ratio3By4, 0.7500 },
            { AspectRatio.Ratio4By5, 0.8000 },
            { AspectRatio.Ratio1By1, 1.0000 },
            { AspectRatio.Ratio5By4, 1.2500 },
            { AspectRatio.Ratio4By3, 1.3333 },
            { AspectRatio.Ratio3By2, 1.5000 },
            { AspectRatio.Ratio5By3, 1.6667 },
            { AspectRatio.Ratio2By1, 2.0000 },
            { AspectRatio.Ratio3By1, 3.0000 },
        };

    }
}
