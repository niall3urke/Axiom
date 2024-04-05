using Axiom.Core.Bases;
using Axiom.Core;
using System;
using System.Windows.Forms;

namespace Axiom.Controls.Select
{
    public class AxSelectItem : AxControlBase
    {

        // =================
        // ===== Properties
        // =================

        public AxState State
        {
            get => _selectItem.State;
            set
            {
                if (!Enabled && value != AxState.Disabled)
                {
                    Enabled = true;
                }
                _selectItem.State = value;
            }
        }

        public AxColor Color
        {
            get => _selectItem.Color;
            set => _selectItem.Color = value;
        }

        public AxShape Shape
        {
            get => _selectItem.Shape;
            set => _selectItem.Shape = value;
        }

        public override string Text
        {
            get => _selectItem.Text;
            set => _selectItem.Text = value;
        }

        public int Index
        {
            get => _index;
            set => SetField(ref _index, Index);
        }

        public int MinWidth => _selectItem.GetMinWidth(CreateGraphics());

        // =================================
        // ===== Fields: backing properties
        // =================================

        private int _index;

        // =============
        // ===== Fields 
        // =============

        private readonly SelectItemLogic _selectItem;

        // ===================
        // ===== Constructors
        // ===================

        public AxSelectItem() : base()
        {
            _selectItem = new SelectItemLogic()
            {
                Color = AxColor.Default,
                Shape = AxShape.Normal
            };

            _selectItem.PropertyChanged += (s, e) => Invalidate();

            Height = _selectItem.Height;
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
            _selectItem.Width = Width;

            base.OnSizeChanged(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (_selectItem.State != AxState.Loading)
            {
                base.OnClick(e);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (ContainsCursor() && _selectItem.State != AxState.Active)
            {
                _selectItem.State = AxState.Hover;
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!ContainsCursor() && _selectItem.State != AxState.Active)
            {
                _selectItem.State = AxState.Normal;
                Cursor = Cursors.Default;
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _selectItem.State = Enabled ? AxState.Normal : AxState.Disabled;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _selectItem.Draw(e.Graphics);
            Height = _selectItem.Height;
        }


    }
}
