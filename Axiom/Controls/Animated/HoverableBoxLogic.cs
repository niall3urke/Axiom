using Axiom.Controls.ColorHelpers;
using Axiom.Core;
using Axiom.Core.Drawables;
using Axiom.Core.Logic;
using Axiom.Core.Utils;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Axiom.Controls.Animated
{
    class HoverableBoxLogic : LogicBase, IAppearanceProperties, ICanCastShadow
    {

        // =================
        // ===== Properties
        // =================

        // ==== Properties: ICanCastShadow

        public AxShadowDirection ShadowDirection
        {
            get => _shadow.ShadowDirection;
            set => _shadow.ShadowDirection = value;
        }

        public Color ShadowColor
        {
            get => _shadow.ShadowColor;
            set => _shadow.ShadowColor = value;
        }

        public float ShadowOpacity
        {
            get => _shadow.ShadowOpacity;
            set => _shadow.ShadowOpacity = value;
        }

        public float ShadowBlur
        {
            get => _shadow.ShadowBlur;
            set => _shadow.ShadowBlur = value;
        }

        public int ShadowSpread
        {
            get => _shadow.ShadowSpread;
            set => _shadow.ShadowSpread = value;
        }

        public bool HasShadow
        {
            get => _shadow.HasShadow;
            set => _shadow.HasShadow = value;
        }

        // ===== Properties: IAppearanceProperties

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

        // ===== Properties: other

        public bool IsClickable
        {
            get => _isClickable;
            set => SetField(ref _isClickable, value);
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

        public override AxState State
        {
            get => _state;
            set => SetField(ref _state, value, Animate);
        }

        // ==================================
        // ===== Properties - backing fields
        // ==================================

        // ===== Fields: IAppearanceProperties

        private bool _isOutlined;

        private bool _isInverted;

        private bool _isLight;

        // ===== Fields: other

        private bool _isClickable;

        private bool _isRounded;

        private AxState _state;

        // =============
        // ===== Fields
        // =============

        private readonly BackgroundDrawable _background;

        private readonly ShadowDrawable _shadow;

        // ===================
        // ===== Constructors
        // ===================

        public HoverableBoxLogic() : base()
        {
            _background = new BackgroundDrawable();
            _shadow = new ShadowDrawable();
            _shadow.PropertyChanged += (s, e) => NotifyPropertyChanged();
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

            int d = 2 * ShadowSpread;

            int s = ShadowSpread;

            if (ShadowDirection == AxShadowDirection.Centered)
                return new Padding(s);

            if (ShadowDirection == AxShadowDirection.Top)
                return new Padding(s, d, s, 0);

            if (ShadowDirection == AxShadowDirection.Right)
                return new Padding(0, s, d, s);

            if (ShadowDirection == AxShadowDirection.Bottom)
                return new Padding(s, 0, s, d);

            if (ShadowDirection == AxShadowDirection.Left)
                return new Padding(d, s, 0, s);

            if (ShadowDirection == AxShadowDirection.TopLeft)
                return new Padding(d, d, 0, 0);

            if (ShadowDirection == AxShadowDirection.BottomLeft)
                return new Padding(d, 0, 0, d);

            if (ShadowDirection == AxShadowDirection.BottomRight)
                return new Padding(s, s, s, s);

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
            _shadow.Radius = GetRadius();

            _shadow.Height = Height - ShadowSpread;

            _shadow.Width = Width - ShadowSpread;

            _shadow.ShadowDirection = ShadowDirection;

            _shadow.ShadowSpread = ShadowSpread;

            _shadow.ShadowBlur = ShadowBlur;
        }

        private void SetBackgroundProperties()
        {
            var (h, w) = GetBackgroundHeightAndWidth();

            if (State == AxState.Normal && !_animationInProgress)
            {
                _background.Location = GetBackgroundLocation();
            }

            _background.Height = h;

            _background.Width = w;

            _background.BackgroundColor = BackgroundColor;

            _background.ForegroundColor = ForegroundColor;

            _background.BorderColor = BorderColor;

            _background.Radius = GetRadius();
        }

        private float GetRadius()
        {
            if (!IsRounded)
                return 0f;

            if (Shape == AxShape.Normal)
                return 6f;

            if (Shape == AxShape.Small)
                return 3f;

            if (Shape == AxShape.Medium)
                return 12f;

            // Large
            return 18f;
        }

        private (int, int) GetBackgroundHeightAndWidth()
        {
            if (HasShadow)
            {
                return (Height - 2 * ShadowSpread - 1, Width - 2 * ShadowSpread - 1);
            }
            return (Height - 1, Width - 1);
        }

        private PointF GetBackgroundLocation()
        {
            if (!HasShadow)
                return PointF.Empty;

            return new PointF(ShadowSpread, ShadowSpread);

            if (ShadowDirection == AxShadowDirection.Centered)
                return new PointF(ShadowSpread, ShadowSpread);

            else if (ShadowDirection == AxShadowDirection.Top)
                return new PointF(ShadowSpread, ShadowSpread * 2);

            else if (ShadowDirection == AxShadowDirection.Right)
                return new PointF(0, ShadowSpread);

            else if (ShadowDirection == AxShadowDirection.Bottom)
                return new PointF(ShadowSpread, 0);

            else if (ShadowDirection == AxShadowDirection.Left)
                return new PointF(ShadowSpread * 2, ShadowSpread);

            else if (ShadowDirection == AxShadowDirection.TopLeft)
                return new PointF(ShadowSpread * 2, ShadowSpread * 2);

            else if (ShadowDirection == AxShadowDirection.BottomLeft)
                return new PointF(ShadowSpread * 2, 0);

            else if (ShadowDirection == AxShadowDirection.BottomRight)
                return new PointF(0, 0);

            // Top right
            return new PointF(0, ShadowSpread * 2);

        }

        // =========================
        // ===== Methods: animation
        // =========================

        private bool _animationInProgress;
        private void Animate()
        {
            if (_animationInProgress)
                return;

            _animationInProgress = true;

            if (State == AxState.Hover)
            {
                Rise();
            }
            else
            {
                Fall();
            }
        }

        private PointF _originalLocation;
        private float _originalOpacity;

        public void SetInitialLocationAndOpacity()
        {
            _originalLocation = _background.Location;
            _originalOpacity = _shadow.ShadowOpacity;
        }

        private void Rise()
        {
            new Animation((int)_background.Location.X, (int)_background.Location.X - ShadowSpread, 300)
            {
                OnChangeIncrement = (value) =>
                {
                    _shadow.ShadowOpacity += value * 0.1f;
                },
                OnChange = (value) =>
                {
                    _background.Location = new PointF(value, value);
                    NotifyPropertyChanged();
                },
                OnComplete = () =>
                {
                    _animationInProgress = false;
                }
            }.Start();
        }

        private void Fall()
        {
            new Animation((int)_background.Location.X, (int)_background.Location.X + ShadowSpread, 300)
            {
                OnChangeIncrement = (value) =>
                {
                    _shadow.ShadowOpacity += value * 0.1f;
                },
                OnChange = (value) =>
                {
                    _background.Location = new PointF(value, value);
                    NotifyPropertyChanged();
                },
                OnComplete = () =>
                {
                    _shadow.ShadowOpacity = _originalOpacity;
                    _background.Location = _originalLocation;
                    _animationInProgress = false;
                }
            }.Start();
        }


    }
}
