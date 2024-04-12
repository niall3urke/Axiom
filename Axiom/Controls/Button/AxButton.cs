using Axiom.Core.Bases;
using Axiom.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Axiom.Controls
{
    [ToolboxItem(true)]
    public class AxButton : AxControlBase, IAxControl
    {

        // ============================
        // ===== Properties: browsable 
        // ============================

        [Category(Category), DisplayName("Color")]
        public AxColor Color
        {
            get => _logic.Color;
            set => _logic.Color = value;
        }

        [Category(Category), DisplayName("Shape")]
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

        [Category(Category), DisplayName("Static")]
        public bool IsStatic
        {
            get => _logic.IsStatic;
            set => _logic.IsStatic = value;
        }

        [Category(Category), DisplayName("Light")]
        public bool IsLight
        {
            get => _logic.IsLight;
            set => _logic.IsLight = value;
        }

        [Category(Category), DisplayName("Text")]
        public override string Text
        {
            get => _logic.Text;
            set => _logic.Text = value;
        }

        // ================================
        // ===== Properties: non-browsable
        // ================================

        [Browsable(false)]
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

        // =============
        // ===== Fields 
        // =============

        private readonly ButtonLogic _logic;

        // ===================
        // ===== Constructors
        // ===================

        public AxButton() : base()
        {
            _logic = new ButtonLogic(() => Invalidate())
            {
                Color = AxColor.Primary,
                Shape = AxShape.Normal
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

        protected override void OnClick(EventArgs e)
        {
            if (_logic.State != AxState.Loading)
            {
                this.Focus();
                base.OnClick(e);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _logic.HasFocus = this.Focused;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _logic.HasFocus = this.Focused;
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
            _logic.Draw(e.Graphics);

            Size = new Size(_logic.Width, _logic.Height);
        }


    }
}
