using Axiom.Core.Bases;
using Axiom.Core;
using System;
using System.Windows.Forms;

namespace Axiom.Controls.Select
{
    public class AxSelectBox : AxControlBase
    {

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

        public AxColor Color
        {
            get => _box.Color;
            set => _box.Color = value;
        }

        public bool CurveTopLhs
        {
            get => _box.CurveTopLhs;
            set => _box.CurveTopLhs = value;
        }

        public bool CurveTopRhs
        {
            get => _box.CurveTopRhs;
            set => _box.CurveTopRhs = value;
        }

        public bool CurveBtmLhs
        {
            get => _box.CurveBtmLhs;
            set => _box.CurveBtmLhs = value;
        }

        public bool CurveBtmRhs
        {
            get => _box.CurveBtmRhs;
            set => _box.CurveBtmRhs = value;
        }

        // =============
        // ===== Fields 
        // =============

        private readonly SelectBoxLogic _box;

        // ===================
        // ===== Constructors
        // ===================

        public AxSelectBox() : base()
        {
            _box = new SelectBoxLogic()
            {
                Color = AxColor.Default,
            };

            _box.PropertyChanged += (s, e) => Invalidate();

            Visible = false;
            Height = 50;
            Width = 200;
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

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            CheckSize();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            CheckSize();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            _box.Height = Height;
            _box.Width = Width;

            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _box.DesignMode = DesignMode;
            _box.Draw(e.Graphics);
        }

        // ==============
        // ===== Methods
        // ==============

        public void Toggle()
        {
            BringToFront();
            Visible = !Visible;
        }

        public void Open()
        {
            BringToFront();
            Visible = true;
        }

        public void Close()
        {
            Visible = false;
        }

        private void CheckSize()
        {
            int height = 16; // Top + btm padding

            int width = 0;

            for (int i = 0; i < Controls.Count; i++)
            {
                var item = Controls[i] as AxSelectItem;

                height = height + item.Height;

                if (width < item.MinWidth)
                {
                    width = item.MinWidth;
                }
            }

            // Set the minimum allowable width
            Width = width < 100 ? 100 : width;

            // Update all items to span the width of the box
            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Width = Width;
            }

            Height = height;

            Width = Width + 2; // +2 for 1px border thicknes lhs + rhs
        }


    }
}
