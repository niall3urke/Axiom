using Axiom.Core;
using Axiom.Core.Bases;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Axiom.Controls.Box
{
    [ToolboxItem(true)]
    public class AxBox : AxPanelBase, IAxControl
    {

        // ============================
        // ===== Properties: browsable 
        // ============================

        [Category(Category), DisplayName("Color")]
        public AxColor Color
        {
            get => _box.Color;
            set => _box.Color = value;
        }

        [Category(Category), DisplayName("Radius")]
        public AxShape Shape
        {
            get => _box.Shape;
            set => _box.Shape = value;
        }

        [Category(Category), DisplayName("Outlined")]
        public bool IsOutlined
        {
            get => _box.IsOutlined;
            set => _box.IsOutlined = value;
        }
        
        [Category(Category), DisplayName("Inverted")]
        public bool IsInverted
        {
            get => _box.IsInverted;
            set => _box.IsInverted = value;
        }

        [Category(Category), DisplayName("Rounded")]
        public bool IsRounded
        {
            get => _box.IsRounded;
            set => _box.IsRounded = value;
        }

        [Category(Category), DisplayName("Light")]
        public bool IsLight
        {
            get => _box.IsLight;
            set => _box.IsLight = value;
        }

        // ================================
        // ===== Properties: non-browsable
        // ================================

        [Browsable(false)]
        public AxState State
        {
            get => _box.State;
            set
            {
                if (!Enabled && value != AxState.Disabled)
                {
                    Enabled = true;
                }
                _box.State = value;
            }
        }

        [Browsable(false)]
        public bool CurveTopLhs
        {
            get => _box.CurveTopLhs;
            set => _box.CurveTopLhs = value;
        }

        [Browsable(false)]
        public bool CurveTopRhs
        {
            get => _box.CurveTopRhs;
            set => _box.CurveTopRhs = value;
        }

        [Browsable(false)]
        public bool CurveBtmLhs
        {
            get => _box.CurveBtmLhs;
            set => _box.CurveBtmLhs = value;
        }

        [Browsable(false)]
        public bool CurveBtmRhs
        {
            get => _box.CurveBtmRhs;
            set => _box.CurveBtmRhs = value;
        }

        // =============
        // ===== Fields 
        // =============

        private readonly BoxLogic _box;

        // ===================
        // ===== Constructors
        // ===================

        public AxBox() : base()
        {
            _box = new BoxLogic()
            {
                Color = AxColor.White,
                Height = Height,
                Width = Width,
            };

            _box.PropertyChanged += (s, e) => Invalidate();
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
            _box.Height = Height;
            _box.Width = Width;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _box.State = Enabled ? AxState.Normal : AxState.Disabled;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _box.Draw(e.Graphics);
        }

    }
}
