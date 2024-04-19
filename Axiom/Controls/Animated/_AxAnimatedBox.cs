//using Axiom.Controls.Animated.Designer;
//using Axiom.Controls.Box;
//using Axiom.Core;
//using Axiom.Core.Bases;
//using Axiom.Core.Drawables;
//using Axiom.Core.Utils;
//using System;
//using System.ComponentModel;
//using System.Drawing;
//using System.Windows.Forms;

//namespace Axiom.Controls.Animated
//{
//    [ToolboxItem(true)]
//    public partial class AxAnimatedBox : AxBox
//    {
        
//        // ===== Properties: IAxControl

//        [Browsable(false)]
//        public override AxState State
//        {
//            get => Box.State;
//            set
//            {
//                if (!Enabled && value != AxState.Disabled)
//                {
//                    Enabled = true;
//                }
//                Box.State = value;

//                Animate();
//            }
//        }

//        private bool _animationInProgress;
//        private void Animate()
//        {
//            if (_animationInProgress)
//                return;

//            _animationInProgress = true;

//            Animation a;
//            if (Box.State == AxState.Hover)
//            {
//                a = new Animation(Box.Location.X, Box.Location.X - ShadowSpread, 300);
//                a.OnChange += (value) =>
//                {
//                    Box.Location = new Point(value, value);
//                    Invalidate();
//                };
//                a.OnComplete += () => _animationInProgress = false;
//            }
//            else
//            {
//                a = new Animation(Box.Location.X, Box.Location.X + ShadowSpread, 300);
//                a.OnChange += (value) =>
//                {
//                    Box.Location = new Point(value, value);
//                    Invalidate();
//                };
//                a.OnComplete += () => _animationInProgress = false;
//            }
//        }

//        // ===== Properties: ICanCastShadow

//        [Browsable(false)]
//        public int ShadowSpread
//        {
//            get => _shadow.ShadowSpread;
//            set => _shadow.ShadowSpread = value;
//        }

//        [Browsable(false)]
//        public float ShadowOpacity
//        {
//            get => _shadow.ShadowOpacity;
//            set => _shadow.ShadowOpacity = value;
//        }

//        [Browsable(false)]
//        public float ShadowBlur
//        {
//            get => _shadow.ShadowBlur;
//            set => _shadow.ShadowBlur = value;
//        }

//        [Browsable(false)]
//        public Color ShadowColor
//        {
//            get => _shadow.ShadowColor;
//            set => _shadow.ShadowColor = value;
//        }

//        [Browsable(false)]
//        public AxShadowDirection ShadowDirection
//        {
//            get => _shadow.ShadowDirection;
//            set => _shadow.ShadowDirection = value;
//        }

//        // =================================
//        // ===== Fields: backing properties
//        // =================================

//        private bool _curveBtmRhs;

//        private bool _curveBtmLhs;

//        private bool _curveTopRhs;

//        private bool _curveTopLhs;

//        private bool _isRounded;

//        private AxShape _shape;

//        // =============
//        // ===== Fields
//        // =============

//        private readonly ShadowDrawable _shadow;

//        // ===================
//        // ===== Constructors
//        // ===================

//        public AxAnimatedBox() : base()
//        {
//            InitializeComponent();
//            _shadow = new ShadowDrawable();
//            _shadow.PropertyChanged += (s, e) => Invalidate();

//            _curveBtmLhs = true;
//            _curveTopRhs = true;
//            _curveTopLhs = true;
//            _curveBtmRhs = true;
//            _isRounded = true;
//        }

//        // =============
//        // ===== Events
//        // =============

//        protected override void OnMouseEnter(EventArgs e)
//        {
//            if (Box.ClientRectangle.Contains(Box.PointToClient(Cursor.Position)) && State != AxState.Loading)
//            {
//                State = AxState.Hover;
//                Cursor = Cursors.Hand;
//            }
//        }

//        protected override void OnMouseLeave(EventArgs e)
//        {
//            if (!Box.ClientRectangle.Contains(Box.PointToClient(Cursor.Position)) && State != AxState.Loading)
//            {
//                State = AxState.Hover;
//                Cursor = Cursors.Default;
//            }
//        }

//        protected override void OnEnabledChanged(EventArgs e)
//        {
//            base.OnEnabledChanged(e);
//            State = Enabled ? AxState.Normal : AxState.Disabled;
//        }

//        protected override void OnPaint(PaintEventArgs e)
//        {
//            var g = e.Graphics;

//            // Never draw the shadow for the box
//            Box.HasShadow = false;

//            SetBoxProperties();

//            if (HasShadow)
//            {
//                SetShadowProperties();

//                _shadow.Draw(g);
//            }

//            //Box.Invalidate();
//        }

//        private void SetShadowProperties()
//        {
//            _shadow.CurveBtmLhs = CurveBtmLhs;

//            _shadow.CurveBtmRhs = CurveBtmRhs;

//            _shadow.CurveTopLhs = CurveTopLhs;

//            _shadow.CurveTopRhs = CurveBtmRhs;


//            _shadow.Height = Height - ShadowSpread;

//            _shadow.Width = Width - ShadowSpread;

//            _shadow.Radius = GetRadius();
//        }

//        private void SetBoxProperties()
//        {
//            Box.CurveBtmLhs = CurveBtmLhs;

//            Box.CurveBtmRhs = CurveBtmRhs;

//            Box.CurveTopLhs = CurveTopLhs;

//            Box.CurveTopRhs = CurveBtmRhs;

//            var (h, w) = GetBoxHeightAndWidth();

//            if (State != AxState.Hover)
//            {
//                Box.Location = GetBoxLocation();
//            }

//            Box.IsRounded = IsRounded;

//            Box.Shape = Shape;

//            Box.Height = h;

//            Box.Width = w;

//        }

//        private float GetRadius()
//        {
//            if (!IsRounded)
//                return 0f;

//            if (Shape == AxShape.Normal)
//                return 6f;

//            if (Shape == AxShape.Small)
//                return 3f;

//            if (Shape == AxShape.Medium)
//                return 12f;

//            // Large
//            return 18f;
//        }

//        private (int, int) GetBoxHeightAndWidth()
//        {
//            if (HasShadow)
//            {
//                return (Height - 2 * ShadowSpread - 1, Width - 2 * ShadowSpread - 1);
//            }
//            return (Height - 1, Width - 1);
//        }

//        private Point GetBoxLocation()
//        {
//            if (!HasShadow)
//                return Point.Empty;

//            return new Point(ShadowSpread, ShadowSpread);

//            if (ShadowDirection == AxShadowDirection.Centered)
//                return new Point(ShadowSpread, ShadowSpread);

//            else if (ShadowDirection == AxShadowDirection.Top)
//                return new Point(ShadowSpread, ShadowSpread * 2);

//            else if (ShadowDirection == AxShadowDirection.Right)
//                return new Point(0, ShadowSpread);

//            else if (ShadowDirection == AxShadowDirection.Bottom)
//                return new Point(ShadowSpread, 0);

//            else if (ShadowDirection == AxShadowDirection.Left)
//                return new Point(ShadowSpread * 2, ShadowSpread);

//            else if (ShadowDirection == AxShadowDirection.TopLeft)
//                return new Point(ShadowSpread * 2, ShadowSpread * 2);

//            else if (ShadowDirection == AxShadowDirection.BottomLeft)
//                return new Point(ShadowSpread * 2, 0);

//            else if (ShadowDirection == AxShadowDirection.BottomRight)
//                return new Point(0, 0);

//            // Top right
//            return new Point(0, ShadowSpread * 2);

//        }


//    }
//}
