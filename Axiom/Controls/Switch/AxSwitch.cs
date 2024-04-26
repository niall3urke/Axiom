﻿using Axiom.Core.Bases;
using Axiom.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Axiom.Controls.Switch
{
    [ToolboxItem(true)]
    [DefaultEvent("CheckedChanged")]
    public class AxSwitch : AxControlBase, IAxControl
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
            get => _logic.Color;
            set => _logic.Color = value;
        }

        [Category(Category), DisplayName("Shape")]
        public AxShape Shape
        {
            get => _logic.Shape;
            set => _logic.Shape = value;
        }

        [Category(Category), DisplayName("Inverted")]
        public bool IsInverted
        {
            get => _logic.IsInverted;
            set => _logic.IsInverted = value;
        }

        [Category(Category), DisplayName("Outlined")]
        public bool IsOutlined
        {
            get => _logic.IsOutlined;
            set => _logic.IsOutlined = value;
        }

        [Category(Category), DisplayName("Rounded")]
        public bool IsRounded
        {
            get => _logic.IsRounded;
            set => _logic.IsRounded = value;
        }

        [Category(Category), DisplayName("Text")]
        public override string Text
        {
            get => _logic.Text;
            set => _logic.Text = value;
        }

        [Category(Category), DisplayName("Checked")]
        public bool Checked
        {
            get => _checked;
            set => SetField(ref _checked, value, OnCheckChanged);
        }

        [Category(Category), DisplayName("Light")]
        public bool IsLight
        {
            get => _logic.IsLight;
            set => _logic.IsLight = value;
        }

        [Category(Category), DisplayName("Thin")]
        public bool IsThin
        {
            get => _logic.IsThin;
            set => _logic.IsThin = value;
        }

        // ================================
        // ===== Properties: non-browsable
        // ================================

        [Browsable(false)]
        public Color BackgroundColor => _logic.BackgroundColor;

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

        // =============
        // ===== Fields 
        // =============

        private readonly SwitchLogic _logic;

        private bool _checked;

        // ===================
        // ===== Constructors
        // ===================

        public AxSwitch() : base()
        {
            _logic = new SwitchLogic()
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
                Checked = !Checked;
                Focus();
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _logic.HasFocus = Focused;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _logic.HasFocus = Focused;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (ContainsCursor() && _logic.State != AxState.Active)
            {
                _logic.State = AxState.Hover;
            }
            Cursor = Cursors.Hand;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!ContainsCursor() && _logic.State != AxState.Active)
            {
                _logic.State = AxState.Normal;
            }
            Cursor = Cursors.Default;
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

            _logic.Draw(e.Graphics);

            Size = new Size(_logic.Width, _logic.Height);
        }

        // =======================
        // ===== Methods: private
        // =======================

        private void OnCheckChanged()
        {
            if (Checked)
            {
                State = AxState.Active;
            }
            else
            {
                State = AxState.Normal;
            }

            CheckedChanged.Invoke(this, EventArgs.Empty);
        }


    }
}
