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
            get => _button.Color;
            set => _button.Color = value;
        }

        [Category(Category), DisplayName("Shape")]
        public AxShape Shape
        {
            get => _button.Shape;
            set => _button.Shape = value;
        }

        [Category(Category), DisplayName("Outlined")]
        public bool IsOutlined
        {
            get => _button.IsOutlined;
            set => _button.IsOutlined = value;
        }

        [Category(Category), DisplayName("Inverted")]
        public bool IsInverted
        {
            get => _button.IsInverted;
            set => _button.IsInverted = value;
        }

        [Category(Category), DisplayName("Rounded")]
        public bool IsRounded
        {
            get => _button.IsRounded;
            set => _button.IsRounded = value;
        }

        [Category(Category), DisplayName("Static")]
        public bool IsStatic
        {
            get => _button.IsStatic;
            set => _button.IsStatic = value;
        }

        [Category(Category), DisplayName("Light")]
        public bool IsLight
        {
            get => _button.IsLight;
            set => _button.IsLight = value;
        }

        [Category(Category), DisplayName("Text")]
        public override string Text
        {
            get => _button.Text;
            set => _button.Text = value;
        }

        // ================================
        // ===== Properties: non-browsable
        // ================================

        [Browsable(false)]
        public AxState State
        {
            get => _button.State;
            set
            {
                if (!Enabled && value != AxState.Disabled)
                {
                    Enabled = true;
                }
                _button.State = value;
            }
        }

        [Browsable(false)]
        public bool CurveTopLhs
        {
            get => _button.CurveTopLhs;
            set => _button.CurveTopLhs = value;
        }

        [Browsable(false)]
        public bool CurveTopRhs
        {
            get => _button.CurveTopRhs;
            set => _button.CurveTopRhs = value;
        }

        [Browsable(false)]
        public bool CurveBtmLhs
        {
            get => _button.CurveBtmLhs;
            set => _button.CurveBtmLhs = value;
        }

        [Browsable(false)]
        public bool CurveBtmRhs
        {
            get => _button.CurveBtmRhs;
            set => _button.CurveBtmRhs = value;
        }

        // =============
        // ===== Fields 
        // =============

        private readonly ButtonLogic _button;

        // ===================
        // ===== Constructors
        // ===================

        public AxButton() : base()
        {
            _button = new ButtonLogic(() => Invalidate())
            {
                Color = AxColor.Primary,
                Shape = AxShape.Normal
            };

            _button.PropertyChanged += (s, e) => Invalidate();
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
            if (_button.State != AxState.Loading)
            {
                this.Focus();
                base.OnClick(e);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _button.HasFocus = this.Focused;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _button.HasFocus = this.Focused;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_button.State != AxState.Loading)
            {
                _button.State = AxState.Active;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_button.State != AxState.Loading)
            {
                _button.State = AxState.Hover;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (ContainsCursor() && _button.State != AxState.Loading)
            {
                _button.State = AxState.Hover;
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!ContainsCursor() && _button.State != AxState.Loading)
            {
                _button.State = AxState.Normal;
                Cursor = Cursors.Default;
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _button.State = Enabled ? AxState.Normal : AxState.Disabled;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _button.Draw(e.Graphics);

            Size = new Size(_button.Width, _button.Height);
        }


    }
}
