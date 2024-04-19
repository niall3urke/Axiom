using Axiom.Core;
using Axiom.Core.Bases;
using Axiom.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Axiom.Controls.Animated
{
    [ToolboxItem(true)]
    public class AxHoverableBox : AxPanelBase, IAxControl, ICanCastShadow
    {

        // ============================
        // ===== Properties: browsable 
        // ============================

        [Category(Category), DisplayName("Shadow")]
        public bool HasShadow
        {
            get => _logic.HasShadow;
            set => _logic.HasShadow = value;
        }

        [Category(Category), DisplayName("Clickable")]
        public bool IsClickable
        {
            get => _logic.IsClickable;
            set => _logic.IsClickable = value;
        }

        [Category(Category), DisplayName("Color")]
        public AxColor Color
        {
            get => _logic.Color;
            set => _logic.Color = value;
        }

        [Category(Category), DisplayName("Radius")]
        public AxShape Shape
        {
            get => _logic.Shape;
            set => _logic.Shape = value;
        }

        [Category(Category), DisplayName("Outlined")]
        public bool IsOutlined
        {
            get => _logic.IsOutlined;
            set => _logic.IsOutlined = value;
        }
        
        [Category(Category), DisplayName("Inverted")]
        public bool IsInverted
        {
            get => _logic.IsInverted;
            set => _logic.IsInverted = value;
        }

        [Category(Category), DisplayName("Rounded")]
        public bool IsRounded
        {
            get => _logic.IsRounded;
            set => _logic.IsRounded = value;
        }

        [Category(Category), DisplayName("Light")]
        public bool IsLight
        {
            get => _logic.IsLight;
            set => _logic.IsLight = value;
        }

        // ================================
        // ===== Properties: non-browsable
        // ================================

        [Browsable(false)]
        public bool CurveTopLhs
        {
            get => _logic.CurveTopLhs;
            set => _logic.CurveTopLhs = value;
        }

        [Browsable(false)]
        public bool CurveTopRhs
        {
            get => _logic.CurveTopRhs;
            set => _logic.CurveTopRhs = value;
        }

        [Browsable(false)]
        public bool CurveBtmLhs
        {
            get => _logic.CurveBtmLhs;
            set => _logic.CurveBtmLhs = value;
        }

        [Browsable(false)]
        public bool CurveBtmRhs
        {
            get => _logic.CurveBtmRhs;
            set => _logic.CurveBtmRhs = value;
        }

        // ===== Properties: IAxControl

        [Browsable(false)]
        public virtual AxState State
        {
            get => _logic.State;
            set
            {
                if (!Enabled && value != AxState.Disabled)
                {
                    Enabled = true;
                }
                _lastState = _logic.State;
                _logic.State = value;
                Animate();
            }
        }

        // ===== Properties: ICanCastShadow

        [Browsable(false)]
        public int ShadowSpread
        {
            get => _logic.ShadowSpread;
            set => _logic.ShadowSpread = value;
        }

        [Browsable(false)]
        public float ShadowOpacity
        {
            get => _logic.ShadowOpacity;
            set => _logic.ShadowOpacity = value;
        }

        [Browsable(false)]
        public float ShadowBlur
        {
            get => _logic.ShadowBlur;
            set => _logic.ShadowBlur = value;
        }

        [Browsable(false)]
        public Color ShadowColor
        {
            get=> _logic.ShadowColor;
            set => _logic.ShadowColor = value;
        }

        [Browsable(false)]
        public AxShadowDirection ShadowDirection
        {
            get => _logic.ShadowDirection;
            set => _logic.ShadowDirection = value;
        }

        // =============
        // ===== Fields 
        // =============

        private readonly HoverableBoxLogic _logic;

        // ===================
        // ===== Constructors
        // ===================

        public AxHoverableBox() : base()
        {
            _logic = new HoverableBoxLogic()
            {
                Color = AxColor.White,
                Height = Height,
                Width = Width,
            };

            _logic.PropertyChanged += (s, e) => Invalidate();
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

            _logic.Height = Height;
            _logic.Width = Width;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (!_logic.IsClickable)
                return;

            if (ContainsCursor() && State != AxState.Loading)
            {
                State = AxState.Hover;
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (!_logic.IsClickable)
                return;

            if (!ContainsCursor() && State != AxState.Loading)
            {
                State = AxState.Normal;
                Cursor = Cursors.Default;
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            InitalizeAnimationItems();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            State = Enabled ? AxState.Normal : AxState.Disabled;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Padding = _logic.GetPadding();
            _logic.Draw(e.Graphics);
        }

        // =========================
        // ===== Methods: animation
        // =========================

        // ===== Fields

        private Dictionary<Control, Point> _originalLocations;

        private Dictionary<Control, Point> _targetLocations;

        private AxState _lastState;

        private bool _animationInProgress;

        // ===== Methods

        private void InitalizeAnimationItems()
        {
            // Initialize the original and target location dictionaries
            
            _originalLocations = new Dictionary<Control, Point>();

            _targetLocations = new Dictionary<Control, Point>();

            // Loop all the controls

            foreach (Control c in Controls)
            {
                // Get the original location of the control
                if (!_originalLocations.ContainsKey(c))
                    _originalLocations.Add(c, c.Location);

                // Determine the target, on-hover, location of the control
                if (!_targetLocations.ContainsKey(c))
                    _targetLocations.Add(c, new Point(c.Left - ShadowSpread, c.Top - ShadowSpread));

                // Hook up events for animation on hover/leave
                c.MouseEnter += (s, x) => OnMouseEnter(x);
                c.MouseLeave += (s, x) => OnMouseLeave(x);
            }

            // Get the initalize values for the logic too
            _logic.SetInitialLocationAndOpacity();
        }

        private void Animate()
        {
            if (_lastState == State)
                return;

            if (_animationInProgress)
                return;

            _animationInProgress = true;

            if (State == AxState.Hover && _lastState != AxState.Hover)
            {
                Rise();
            }
            else if (_lastState == AxState.Hover && State == AxState.Normal)
            {
                Fall();
            }
        }

        private void Rise()
        {
            new Animation(0, ShadowSpread, 300)
            {
                OnChangeIncrement = (value) =>
                {
                    foreach (Control c in Controls)
                    {
                        c.Left -= (int)Math.Round(value);
                        c.Top -= (int)Math.Round(value);
                    }
                    Invalidate();
                },
                OnComplete = () =>
                {
                    // Ensure the control locations are where we expect them
                    foreach (Control c in Controls)
                    {
                        c.Location = _targetLocations[c];
                    }

                    // Allow animations to be processed again
                    _animationInProgress = false;

                    // Has the state changed during the animation?
                    if (State == AxState.Normal)
                    {
                        Animate();
                    }
                }
            }.Start();
        }

        private void Fall()
        {
            new Animation(0, ShadowSpread, 300)
            {
                OnChangeIncrement = (value) =>
                {
                    foreach (Control c in Controls)
                    {
                        c.Left += (int)Math.Round(value);
                        c.Top += (int)Math.Round(value);
                    }
                    Invalidate();
                },
                OnComplete = () =>
                {
                    // Ensure the control locations are where we expect them
                    foreach (Control c in Controls)
                    {
                        c.Location = _originalLocations[c];
                    }

                    // Allow animations to be processed again
                    _animationInProgress = false;

                    // Has the state changed during the animation?
                    if (State == AxState.Hover)
                    {
                        Animate();
                    }
                }
            }.Start();
        }


    }
}

