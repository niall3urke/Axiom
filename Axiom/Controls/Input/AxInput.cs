using Axiom.Core.Bases;
using Axiom.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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
            get => _logic.Color;
            set => _logic.Color = value;
        }

        [Category(Category), DisplayName("Shape")]
        public AxShape Shape
        {
            get => _logic.Shape;
            set => _logic.Shape = value;
        }

        [Category(Category), DisplayName("Rounded")]
        public bool IsRounded
        {
            get => _logic.IsRounded;
            set => _logic.IsRounded = value;
        }

        [Category(Category), DisplayName("Placeholder")]
        public string Placeholder
        {
            get => _logic.Placeholder;
            set => _logic.Placeholder = value;
        }

        [Category(Category), DisplayName("Outlined")]
        public bool IsOutlined { get; set; }

        [Category(Category), DisplayName("Inverted")]
        public bool IsInverted { get; set; }

        [Category(Category), DisplayName("Light")]
        public bool IsLight { get; set; }

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

        [Browsable(false)]
        public Color BackgroundColor => _logic.BackgroundColor;

        // =============
        // ===== Fields 
        // =============

        private readonly InputLogic _logic;

        private readonly bool _initialized;

        // ===================
        // ===== Constructors
        // ===================

        public AxInput() : base()
        {
            InitializeComponent();

            _logic = new InputLogic(() => Invalidate())
            {
                Color = AxColor.Default,
                Shape = AxShape.Normal
            };

            _logic.PropertyChanged += (s, e) => Invalidate();

            _logic.SetTextboxTextToPlaceholder = () => Tb.Text = _logic.Placeholder;

            Tb.TextChanged += Tb_TextChanged;

            Tb.LostFocus += Tb_LostFocus;

            Tb.GotFocus += Tb_GotFocus;

            Tb.Enter += Tb_Enter;

            Tb.Leave += Tb_Leave;

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
            if (_logic.State != AxState.Loading)
            {
                Tb.Focus();
                base.OnClick(e);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (_initialized)
            {
                _logic.Width = Width;
            }
            base.OnSizeChanged(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (ContainsCursor() && !(_logic.State == AxState.Loading || _logic.State == AxState.Active))
            {
                _logic.State = AxState.Hover;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!ContainsCursor() && !(_logic.State == AxState.Loading || _logic.State == AxState.Active))
            {
                _logic.State = AxState.Normal;
            }
        }

        private void Tb_TextChanged(object sender, EventArgs e)
        {
            Text = Tb.Text;
        }

        private void Tb_GotFocus(object sender, EventArgs e)
        {
            if (_logic.State != AxState.Loading)
            {
                _logic.State = AxState.Active;
            }
        }

        private void Tb_LostFocus(object sender, EventArgs e)
        {
            if (_logic.State != AxState.Loading)
            {
                _logic.State = AxState.Normal;
            }
        }

        private void Tb_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tb.Text))
            {
                Tb.Text = _logic.Placeholder;
            }
        }

        private void Tb_Enter(object sender, EventArgs e)
        {
            if (Tb.Text == _logic.Placeholder)
            {
                Tb.Text = "";
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

            _logic.DesignMode = DesignMode;

            _logic.Draw(e.Graphics);

            Size = new Size(Width, _logic.Height);

            SetTextboxProperties();

            base.OnPaint(e);
        }


        private void SetTextboxProperties()
        {
            Tb.Enabled = !(_logic.State == AxState.Disabled || _logic.State == AxState.Loading);

            var backgroundColor = _logic.BackgroundColor;
            var foregroundColor = _logic.ForegroundColor;

            // If the textbox is disabled the colors will have alpha values 
            // set to 50% (128). The textbox doesn't handle transparency so
            // we need to convert from ARGB to RGB based on a % (0.97255%).
            bool isTransparent = backgroundColor.A != 255 || foregroundColor.A != 255;

            if (_logic.State == AxState.Disabled && isTransparent)
            {
                backgroundColor = GetDisabledColorAsRgbInsteadOfArgb(backgroundColor);
                foregroundColor = GetDisabledColorAsRgbInsteadOfArgb(foregroundColor);
            }
            else if (Tb.Text == _logic.Placeholder)
            {
                foregroundColor = GetPlaceholderTextForegroundColor(foregroundColor);
            }

            Tb.ForeColor = foregroundColor;

            Tb.BackColor = backgroundColor;

            Tb.Location = _logic.TextLocation;
            
            Tb.Size = _logic.TextSize;

            Tb.Font = _logic.Font;
        }

        private Color GetDisabledColorAsRgbInsteadOfArgb(Color c)
        {
            byte r = Convert.ToByte(c.R * 0.97255);
            byte g = Convert.ToByte(c.G * 0.97255);
            byte b = Convert.ToByte(c.B * 0.97255);
            return System.Drawing.Color.FromArgb(r, g, b);
        }

        private Color GetPlaceholderTextForegroundColor(Color c)
        {
            // Bulma values:HSLA( 46.05, 50.8, 61.05, 0.3)
            // Equivalent: RGB(190, 191, 195)
            return System.Drawing.Color.FromArgb(190, 191, 195);
        }



    }
}
