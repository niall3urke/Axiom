﻿using Axiom.Core.Bases;
using Axiom.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Axiom.Controls.Input
{
    [DefaultEvent("TextChanged")]
    [ToolboxItem(true)]
    public partial class AxInput : AxUserControlBase, IAxControl
    {

        // ============================
        // ===== Properties: browsable 
        // ============================

        [Category(Category), DisplayName("Color")]
        public AxColor Color
        {
            get => _input.Color;
            set => _input.Color = value;
        }

        [Category(Category), DisplayName("Shape")]
        public AxShape Shape
        {
            get => _input.Shape;
            set => _input.Shape = value;
        }

        [Category(Category), DisplayName("Rounded")]
        public bool IsRounded
        {
            get => _input.IsRounded;
            set => _input.IsRounded = value;
        }

        [Category(Category), DisplayName("Text")]
        public override string Text
        {
            get => Tb.Text;
            set => Tb.Text = value;
        }

        // TODO

        public bool IsOutlined { get; set; }

        public bool IsInverted { get; set; }

        public bool IsLight { get; set; }

        // ================================
        // ===== Properties: non-browsable
        // ================================

        [Browsable(false)]
        public AxState State
        {
            get => _input.State;
            set
            {
                if (!Enabled && value != AxState.Disabled)
                {
                    Enabled = true;
                }
                _input.State = value;
            }
        }

        [Browsable(false)]
        public bool CurveTopLhs
        {
            get => _input.CurveTopLhs;
            set => _input.CurveTopLhs = value;
        }

        [Browsable(false)]
        public bool CurveTopRhs
        {
            get => _input.CurveTopRhs;
            set => _input.CurveTopRhs = value;
        }

        [Browsable(false)]
        public bool CurveBtmLhs
        {
            get => _input.CurveBtmLhs;
            set => _input.CurveBtmLhs = value;
        }

        [Browsable(false)]
        public bool CurveBtmRhs
        {
            get => _input.CurveBtmRhs;
            set => _input.CurveBtmRhs = value;
        }

        // =============
        // ===== Fields 
        // =============

        private readonly InputLogic _input;

        private bool _initialized;

        // ===================
        // ===== Constructors
        // ===================

        public AxInput() : base()
        {
            InitializeComponent();

            _input = new InputLogic(() => Invalidate())
            {
                Color = AxColor.Default,
                Shape = AxShape.Normal
            };

            _input.PropertyChanged += (s, e) => Invalidate();

            Tb.LostFocus += Tb_LostFocus;

            Tb.GotFocus += Tb_GotFocus;

            _initialized = true;
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
            if (_input.State != AxState.Loading)
            {
                Tb.Focus();
                base.OnClick(e);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (_initialized)
            {
                _input.Width = Width;
            }
            base.OnSizeChanged(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (ContainsCursor() && !(_input.State == AxState.Loading || _input.State == AxState.Active))
            {
                _input.State = AxState.Hover;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!ContainsCursor() && !(_input.State == AxState.Loading || _input.State == AxState.Active))
            {
                _input.State = AxState.Normal;
            }
        }

        private void Tb_GotFocus(object sender, EventArgs e)
        {
            if (_input.State != AxState.Loading)
            {
                _input.State = AxState.Active;
            }
        }

        private void Tb_LostFocus(object sender, EventArgs e)
        {
            if (_input.State != AxState.Loading)
            {
                _input.State = AxState.Normal;
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            _input.State = Enabled ? AxState.Normal : AxState.Disabled;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _input.DesignMode = DesignMode;

            _input.Draw(e.Graphics);

            Size = new Size(Width, _input.Height);

            SetTextboxProperties();

            base.OnPaint(e);
        }

        private void SetTextboxProperties()
        {
            Tb.Enabled = !(_input.State == AxState.Disabled || _input.State == AxState.Loading);
            
            var backgroundColor = _input.BackgroundColor;
            var foregroundColor = _input.ForegroundColor;

            // If the textbox is disabled the colors will have alpha values 
            // set to 50% (128). The textbox doesn't handle transparency so
            // we need to convert from ARGB to RGB based on a % (0.97255%).
            if (_input.State == AxState.Disabled)
            {
                backgroundColor = GetDisabledColorAsRgbInsteadOfArgb(backgroundColor);
                foregroundColor = GetDisabledColorAsRgbInsteadOfArgb(foregroundColor);
            }

            Tb.ForeColor = foregroundColor;

            Tb.BackColor = backgroundColor;

            Tb.Location = _input.TextLocation;
            
            Tb.Size = _input.TextSize;

            Tb.Font = _input.Font;
        }

        private Color GetDisabledColorAsRgbInsteadOfArgb(Color c)
        {
            byte r = Convert.ToByte(c.R * 0.97255);
            byte g = Convert.ToByte(c.G * 0.97255);
            byte b = Convert.ToByte(c.B * 0.97255);
            return System.Drawing.Color.FromArgb(r, g, b);
        }



    }
}
