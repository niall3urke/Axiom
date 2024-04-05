using Axiom.Core.Bases;
using Axiom.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Axiom.Controls.Select
{
    public class AxSelectButton : AxControlBase
    {

        // =================
        // ===== Properties
        // =================

        public AxState State
        {
            get => _select.State;
            set
            {
                if (!Enabled && value != AxState.Disabled)
                {
                    Enabled = true;
                }
                _select.State = value;
            }
        }

        public AxColor Color
        {
            get => _select.Color;
            set => _select.Color = value;
        }

        public AxShape Shape
        {
            get => _select.Shape;
            set => _select.Shape = value;
        }

        public bool IsRounded
        {
            get => _select.IsRounded;
            set => _select.IsRounded = value;
        }

        public override string Text
        {
            get => _select.Text;
            set => _select.Text = value;
        }

        public bool CurveTopLhs
        {
            get => _select.CurveTopLhs;
            set => _select.CurveTopLhs = value;
        }

        public bool CurveTopRhs
        {
            get => _select.CurveTopRhs;
            set => _select.CurveTopRhs = value;
        }

        public bool CurveBtmLhs
        {
            get => _select.CurveBtmLhs;
            set => _select.CurveBtmLhs = value;
        }

        public bool CurveBtmRhs
        {
            get => _select.CurveBtmRhs;
            set => _select.CurveBtmRhs = value;
        }

        // =============
        // ===== Fields 
        // =============

        private readonly SelectButtonLogic _select;

        // ===================
        // ===== Constructors
        // ===================

        public AxSelectButton() : base()
        {
            _select = new SelectButtonLogic(() => Invalidate())
            {
                Text = "Select..."
            };

            _select.PropertyChanged += (s, e) => Invalidate();

            Height = _select.Height;
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
            _select.Width = Width;
            Height = _select.Height;
            base.OnSizeChanged(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (_select.State != AxState.Loading)
            {
                Focus();

                _select.Click(Cursor.Position);

                base.OnClick(e);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _select.HasFocus = Focused;
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _select.HasFocus = Focused;

            if (!_select.HasFocus)
            {
                _select.ClosedState();
            }

            base.OnLostFocus(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_select.State != AxState.Loading)
            {
                _select.State = AxState.Active;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_select.State != AxState.Loading)
            {
                _select.State = AxState.Hover;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (ContainsCursor() && _select.State != AxState.Loading)
            {
                _select.State = AxState.Hover;
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!ContainsCursor() && _select.State != AxState.Loading)
            {
                _select.State = AxState.Normal;
                Cursor = Cursors.Default;
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _select.State = Enabled ? AxState.Normal : AxState.Disabled;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _select.DesignMode = DesignMode;

            _select.Draw(e.Graphics);

            Size = new Size(Width, _select.Height);
        }

        // ==============
        // ===== Methods
        // ==============

        public void OpenState() => _select.OpenState();

        public void ClosedState() => _select.ClosedState();


    }
}
