using Axiom.Core.Bases;
using Axiom.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Axiom.Controls
{
    [ToolboxItem(true)]
    [DefaultEvent("CheckedChanged")]
    class AxRadioButton : AxControlBase, IAxControl
    {

        // =============
        // ===== Events 
        // =============

        public event EventHandler CheckedChanged = delegate { };

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

        [Category(Category), DisplayName("Checked")]
        public bool Checked
        {
            get => _checked;
            set
            {
                SetField(ref _checked, value, OnCheckChanged);
            }
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

        // =============
        // ===== Fields 
        // =============

        private readonly RadioButtonLogic _button;

        private bool _checked;

        // ===================
        // ===== Constructors
        // ===================

        public AxRadioButton() : base()
        {
            _button = new RadioButtonLogic()
            {
                Color = AxColor.Default,
                Shape = AxShape.Normal,
                IsOutlined = true,
                IsRounded = true
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
            if (_button.State != AxState.Loading || _button.State != AxState.Disabled)
            {
                Checked = true;
                Focus();
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _button.HasFocus = Focused;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _button.HasFocus = Focused;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (ContainsCursor() && _button.State != AxState.Active)
            {
                _button.State = AxState.Hover;
            }
            Cursor = Cursors.Hand;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!ContainsCursor() && _button.State != AxState.Active)
            {
                _button.State = AxState.Normal;
            }
            Cursor = Cursors.Default;
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

        // =======================
        // ===== Methods: private
        // =======================

        private void OnCheckChanged()
        {
            if (Checked)
            {
                DeselectOtherRadios();

                State = AxState.Active;
            }
            else
            {
                State = AxState.Normal;
            }

            CheckedChanged.Invoke(this, EventArgs.Empty);
        }

        private void DeselectOtherRadios()
        {
            if (Parent == null)
                return;

            foreach (var radio in Parent.Controls.OfType<AxRadioButton>())
            {
                if (radio != this)
                {
                    radio.Checked = false;
                }
            }
        }


    }
}
