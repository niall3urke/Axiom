using Axiom.Core;
using Axiom.Core.Bases;
using Axiom.Core.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace Axiom.Controls.Label
{
    [ToolboxItem(true)]
    public class AxLabel : AxLabelBase, IAxControl
    {

        // ============================
        // ===== Properties: browsable 
        // ============================

        [Category(Category), DisplayName("Clickable")]
        public bool IsClickable
        {
            get => _logic.IsClickable;
            set => _logic.IsClickable = value;
        }

        [Category(Category), DisplayName("Light")]
        public bool IsLight
        {
            get => _logic.IsLight;
            set => _logic.IsLight = value;
        }
        
        [Category(Category), DisplayName("Color")]
        public AxColor Color
        {
            get => _logic.Color;
            set => _logic.Color = value;
        }

        [TypeConverter(typeof(EnumDescriptionConverter))]
        [Category(Category), DisplayName("Size")]
        public AxLabelSize LabelSize
        {
            get => _logic.Size;
            set => _logic.Size = value;
        }

        [TypeConverter(typeof(EnumDescriptionConverter))]
        [Category(Category), DisplayName("Weight")]
        public AxFontWeight Weight
        {
            get => _logic.Weight;
            set => _logic.Weight = value;
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
        public Color BackgroundColor => _logic.BackgroundColor;

        [Browsable(false)]
        public bool IsInverted { get; set; }

        [Browsable(false)]
        public bool IsOutlined { get; set; }

        [Browsable(false)]
        public bool IsRounded { get; set; }

        [Browsable(false)]
        public AxShape Shape { get; set; }

        // =============
        // ===== Fields 
        // =============

        private readonly LabelLogic _logic;

        // ===================
        // ===== Constructors
        // ===================

        public AxLabel() : base()
        {
            _logic = new LabelLogic
            {
                Color = AxColor.Dark,
            };
            _logic.PropertyChanged += (s, e) => Invalidate();
        }

        // =============
        // ===== Events
        // =============

        protected override void OnClick(EventArgs e)
        {
            if (!_logic.IsClickable)
                return;

            if (_logic.State != AxState.Loading)
            {
                this.Focus();
                base.OnClick(e);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!_logic.IsClickable)
                return;

            if (_logic.State != AxState.Loading)
            {
                _logic.State = AxState.Active;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!_logic.IsClickable)
                return;

            if (_logic.State != AxState.Loading)
            {
                _logic.State = AxState.Hover;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!_logic.IsClickable)
                return;

            if (ContainsCursor() && _logic.State != AxState.Loading)
            {
                _logic.State = AxState.Hover;
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!_logic.IsClickable)
                return;

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
            if (Parent is IAxControl control)
            {
                BackColor = control.BackgroundColor;
            }

            // Set the logic properties for the font/forecolor
            _logic.Update();

            // Update the label properties
            ForeColor = _logic.ForeColor;
            Font = _logic.Font;

            // Paint the label
            base.OnPaint(e);
        }


    }
}
