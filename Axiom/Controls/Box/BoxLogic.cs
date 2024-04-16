using Axiom.Controls.ColorHelpers;
using Axiom.Core;
using Axiom.Core.Drawables;
using Axiom.Core.Logic;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Axiom.Controls.Box
{
    class BoxLogic : LogicBase, IAppearanceProperties
    {

        // =================
        // ===== Properties
        // =================

        public bool HasShadow
        {
            get => _hasShadow;
            set => SetField(ref _hasShadow, value);
        }

        public int ShadowDepth
        {
            get => _shadowDepth;
            set => SetField(ref _shadowDepth, value);
        }

        public float ShadowBlur
        {
            get => _shadowBlur;
            set => SetField(ref _shadowBlur, value);
        }

        public AxShadowDirection ShadowDirection
        {
            get => _shadowDirection;
            set => SetField(ref _shadowDirection, value);
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

        private AxShadowDirection _shadowDirection;

        private float _shadowBlur;

        private int _shadowDepth;

        private bool _isOutlined;

        private bool _isInverted;

        private bool _isRounded;

        private bool _hasShadow;

        private bool _isLight;

        // =============
        // ===== Fields
        // =============

        private readonly BackgroundDrawable _background;

        private readonly ShadowDrawable _shadow;

        // ===================
        // ===== Constructors
        // ===================

        public BoxLogic() : base()
        {
            _background = new BackgroundDrawable();
            _shadow = new ShadowDrawable();

            _shadowDirection = AxShadowDirection.BottomRight;
            _shadowBlur = 0.45f;
            _shadowDepth = 6;
        }

        // ======================
        // ===== Methods: public
        // ======================

        public void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            SetProperties();

            SetBackgroundProperties();

            if (HasShadow)
            {
                SetShadowProperties();

                _shadow.Draw(g);
            }

            _background.Draw(g);
        }

        public Padding GetPadding()
        {
            if (!HasShadow)
                return new Padding();

            int r = (int)Math.Round(ShadowDepth * 0.5);
            int d = 2 * ShadowDepth;

            if (ShadowDirection == AxShadowDirection.Centered)
                return new Padding(ShadowDepth);
            
            if (ShadowDirection == AxShadowDirection.Top)
                return new Padding(ShadowDepth, d, ShadowDepth, 0);

            if (ShadowDirection == AxShadowDirection.Right)
                return new Padding(0, ShadowDepth, d, ShadowDepth);

            if (ShadowDirection == AxShadowDirection.Bottom)
                return new Padding(ShadowDepth, 0, ShadowDepth, d);

            if (ShadowDirection == AxShadowDirection.Left)
                return new Padding(d, ShadowDepth, 0, ShadowDepth);

            if (ShadowDirection == AxShadowDirection.TopLeft)
                return new Padding(d, d, 0, 0);

            if (ShadowDirection == AxShadowDirection.BottomLeft)
                return new Padding(d, 0, 0, d);

            if (ShadowDirection == AxShadowDirection.BottomRight)
                return new Padding(0, 0, d, d);

            // Top right
            return new Padding(0, d, d, 0);
        }
        // =======================
        // ===== Methods: private  
        // =======================

        private void SetProperties()
        {
            SetColors();
        }

        private void SetColors()
        {
            (BackgroundColor, ForegroundColor, BorderColor, FocusColor) =
                ButtonColorHelper.Get(Color, State, this, System.Drawing.Color.Transparent);
        }

        private void SetShadowProperties()
        {
            _shadow.Height = Height - ShadowDepth;

            _shadow.Width = Width - ShadowDepth;

            _shadow.Direction = ShadowDirection;

            _shadow.Depth = ShadowDepth;

            _shadow.Blur = ShadowBlur;

            if (IsRounded)
            {
                _shadow.Radius = 6f;

                if (Shape == AxShape.Small)
                    _shadow.Radius *= 0.5f;

                else if (Shape == AxShape.Medium)
                    _shadow.Radius *= 2f;

                else if (Shape == AxShape.Large)
                    _shadow.Radius *= 3f;
            }
            else
            {
                _shadow.Radius = 0f;
            }
        }

        private void SetBackgroundProperties()
        {
            if (HasShadow)
            {
                SetBackgroundPositionForShadow();

                _background.Height = Height -  2 * ShadowDepth  - 1;

                _background.Width  = Width -  2 * ShadowDepth - 1;
            }
            else
            {
                _background.Location = Point.Empty;

                _background.Height = Height - 1;

                _background.Width = Width - 1;
            }

            _background.BackgroundColor = BackgroundColor;

            _background.ForegroundColor = ForegroundColor;

            _background.BorderColor = BorderColor;

            if (IsRounded)
            {
                _background.Radius = 6f;

                if (Shape == AxShape.Small)
                    _background.Radius *= 0.5f;

                else if (Shape == AxShape.Medium)
                    _background.Radius *= 2f;

                else if (Shape == AxShape.Large)
                    _background.Radius *= 3f;
            }
            else
            {
                _background.Radius = 0f;
            }
        }

        private void SetBackgroundPositionForShadow()
        {
            if (ShadowDirection == AxShadowDirection.Centered )
            {
                _background.Location = new PointF(ShadowDepth, ShadowDepth);
            }
            else if (ShadowDirection == AxShadowDirection.Top)
            {
                _background.Location = new PointF(ShadowDepth, ShadowDepth * 2);
            }
            else if (ShadowDirection == AxShadowDirection.Right)
            {
                _background.Location = new PointF(0, ShadowDepth);
            }
            else if (ShadowDirection == AxShadowDirection.Bottom)
            {
                _background.Location = new PointF(ShadowDepth, 0);
            }
            else if (ShadowDirection == AxShadowDirection.Left)
            {
                _background.Location = new PointF(ShadowDepth * 2, ShadowDepth);
            }
            else if (ShadowDirection == AxShadowDirection.TopLeft)
            {
                _background.Location = new PointF(ShadowDepth * 2, ShadowDepth * 2);
            }
            else if (ShadowDirection == AxShadowDirection.BottomLeft)
            {
                _background.Location = new PointF(ShadowDepth * 2, 0);
            }
            else if (ShadowDirection == AxShadowDirection.BottomRight)
            {
                _background.Location = new PointF(0, 0);
            }
            else if (ShadowDirection == AxShadowDirection.TopRight)
            {
                _background.Location = new PointF(0, ShadowDepth * 2);
            }
        }


    }
}
