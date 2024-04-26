using Axiom.Controls.ColorHelpers;
using Axiom.Core;
using Axiom.Core.Drawables;
using Axiom.Core.Logic;
using Axiom.Core.Utils;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Axiom.Controls.Animated
{
    class HoverableBoxLogic : LogicBase, IAppearanceProperties, ICanCastShadow
    {

        // ==============
        // ===== Actions
        // ==============

        public Action<float, float> MoveIncrementChanged = delegate(float x, float y) { };
        
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
            _background.PropertyChanged += (s, e) => NotifyPropertyChanged();
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

        private (int, int) GetBackgroundHeightAndWidth() => 
            (Height - 2 * ShadowSpread - 1, Width - 2 * ShadowSpread - 1);

        private PointF GetBackgroundLocation() =>
                new PointF(ShadowSpread, ShadowSpread);

        // =========================
        // ===== Methods: animation
        // =========================

        private bool _animationInProgress;

        // Methods: private

        private void Animate()
        {
            if (_animationInProgress)
                return;

            _animationInProgress = true;

            if (State == AxState.Hover)
            {
                Run();
            }
            else
            {
                Run(-1);
            }
        }
        
        private void Run(int direction = 1)
        {

            bool rise = direction == 1;

            if (ShadowDirection == AxShadowDirection.Top)
            {
                // Move down
                Move(0, 1 * direction, rise); 
            }
            else if (ShadowDirection == AxShadowDirection.Right)
            {
                // Move left
                Move(-1 * direction, 0, rise);
            }
            else if (ShadowDirection == AxShadowDirection.Bottom)
            {
                // Move up
                Move(0, -1 * direction, rise); 
            }
            else if (ShadowDirection == AxShadowDirection.Left)
            {
                // Move right
                Move(1 * direction, 0, rise);
            }
            else if (ShadowDirection == AxShadowDirection.BottomRight)
            {
                // Move left and up
                Move(-1 * direction, -1 * direction, rise);
            }
            else if (ShadowDirection == AxShadowDirection.BottomLeft)
            {
                // Move right and up
                Move(1 * direction, -1 * direction, rise);
            }
            else if (ShadowDirection == AxShadowDirection.TopLeft)
            {
                // Move right and down
                Move(1 * direction, 1 * direction, rise);
            }
            else if (ShadowDirection == AxShadowDirection.TopRight)
            {
                // Move left and down
                Move(-1 * direction, 1 * direction, rise);
            }
        }

        private void Move(int xDirection, int yDirection, bool rise)
        {
            new Animation(0, ShadowSpread, 300)
            {
                OnChangeIncrement = (value) =>
                {
                    _shadow.ShadowOpacity += value * 0.1f * (rise ? -1 : 1);

                    float xIncrement = value * xDirection;

                    float yIncrement = value * yDirection;

                    _background.Location = new PointF
                    (
                        Math.Max(_background.Location.X + xIncrement, 0),
                        Math.Max(_background.Location.Y + yIncrement, 0)
                    );

                    MoveIncrementChanged(xIncrement, yIncrement);
                },
                OnComplete = () =>
                {
                    _animationInProgress = false;

                    // Check if the state has changed in between the animation
                    if (rise && State == AxState.Normal || !rise && State == AxState.Hover)
                    {
                        // Fire the animation again to ensure consistency
                        Animate();
                    }
                }
            }.Start();
        }


    }
}
