using Axiom.Core.Bases;
using Axiom.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Axiom.Controls.Select
{
    public class AxSelectButton : AxControlBase, IAxControl
    {

        // =================
        // ===== Properties
        // =================

        public AxState State
        {
            get => _logic.State;
            set
            {
                if (!Enabled && value != AxState.Disabled)
                {
                    Enabled = true;
                }
                _logic.State = value;
            }
        }

        public AxColor Color
        {
            get => _logic.Color;
            set => _logic.Color = value;
        }

        public AxShape Shape
        {
            get => _logic.Shape;
            set => _logic.Shape = value;
        }

        public bool IsRounded
        {
            get => _logic.IsRounded;
            set => _logic.IsRounded = value;
        }

        public override string Text
        {
            get => _logic.Text;
            set => _logic.Text = value;
        }

        public bool CurveTopLhs
        {
            get => _logic.CurveTopLhs;
            set => _logic.CurveTopLhs = value;
        }

        public bool CurveTopRhs
        {
            get => _logic.CurveTopRhs;
            set => _logic.CurveTopRhs = value;
        }

        public bool CurveBtmLhs
        {
            get => _logic.CurveBtmLhs;
            set => _logic.CurveBtmLhs = value;
        }

        public bool CurveBtmRhs
        {
            get => _logic.CurveBtmRhs;
            set => _logic.CurveBtmRhs = value;
        }

        public Color BackgroundColor => _logic.BackgroundColor;

        // TODO

        public bool IsOutlined { get; set; }

        public bool IsInverted { get; set; }

        public bool IsLight { get; set; }

        // =============
        // ===== Fields 
        // =============

        private readonly SelectButtonLogic _logic;

        // ===================
        // ===== Constructors
        // ===================

        public AxSelectButton() : base()
        {
            _logic = new SelectButtonLogic(() => Invalidate())
            {
                Text = "Select..."
            };

            _logic.PropertyChanged += (s, e) => Invalidate();

            Height = _logic.Height;
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
            _logic.Width = Width;
            Height = _logic.Height;
            base.OnSizeChanged(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (_logic.State != AxState.Loading)
            {
                Focus();

                _logic.Click(Cursor.Position);

                base.OnClick(e);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _logic.HasFocus = Focused;
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _logic.HasFocus = Focused;

            if (!_logic.HasFocus)
            {
                _logic.ClosedState();
            }

            base.OnLostFocus(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_logic.State != AxState.Loading)
            {
                _logic.State = AxState.Active;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_logic.State != AxState.Loading)
            {
                _logic.State = AxState.Hover;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (ContainsCursor() && _logic.State != AxState.Loading)
            {
                _logic.State = AxState.Hover;
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!ContainsCursor() && _logic.State != AxState.Loading)
            {
                _logic.State = AxState.Normal;
                Cursor = Cursors.Default;
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _logic.State = Enabled ? AxState.Normal : AxState.Disabled;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _logic.DesignMode = DesignMode;

            _logic.Draw(e.Graphics);

            Size = new Size(Width, _logic.Height);
        }

        // ==============
        // ===== Methods
        // ==============

        public void OpenState() => _logic.OpenState();

        public void ClosedState() => _logic.ClosedState();


    }
}
