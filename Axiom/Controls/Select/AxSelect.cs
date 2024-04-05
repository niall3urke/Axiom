using Axiom.Controls.Select.Designer;
using Axiom.Core.Bases;
using Axiom.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms;

namespace Axiom.Controls.Select
{
    [ToolboxItem(true)]
    [DefaultEvent("SelectedIndexChanged")]
    [Designer(typeof(SelectDesigner))]
    public class AxSelect : AxControlBase, IAxControl
    {

        // =====================
        // ===== Event handlers 
        // =====================

        public event EventHandler SelectedIndexChanged = delegate { };

        // ============================
        // ===== Properties: browsable 
        // ============================

        [Editor("System.Windows.Forms.Design.StringCollectionEditor", typeof(UITypeEditor))]
        [Category(Category), DisplayName("Items")]
        public List<string> Items
        {
            get => _items;
            set => SetField(ref _items, value);
        }

        [Category(Category), DisplayName("Color")]
        public AxColor Color
        {
            get => _color;
            set => SetField(ref _color, value, SetControlProperties);
        }

        [Category(Category), DisplayName("Shape")]
        public AxShape Shape
        {
            get => _shape;
            set => SetField(ref _shape, value, SetControlProperties);
        }

        [Category(Category), DisplayName("Rounded")]
        public bool IsRounded
        {
            get => _isRounded;
            set => SetField(ref _isRounded, value, SetControlProperties);
        }

        // TODO;

        public bool IsOutlined { get; set; }

        public bool IsInverted { get; set; }

        public bool IsLight { get; set; }

        // ================================
        // ===== Properties: non-browsable
        // ================================

        [Browsable(false)]
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetField(ref _selectedIndex, value, SetSelectedIndex);
        }

        [Browsable(false)]
        public AxSelectItem SelectedItem
        {
            get => _selectedItem;
            set => SetField(ref _selectedItem, value, SetSelectedItem);
        }

        [Browsable(false)]
        public AxState State
        {
            get => _state;
            set
            {
                if (!Enabled && value != AxState.Disabled)
                {
                    Enabled = true;
                }
                SetField(ref _state, value, SetControlProperties);
            }
        }

        // =================================
        // ===== Fields: backing properties
        // =================================

        private AxSelectItem _selectedItem;

        private List<string> _items;

        private int _selectedIndex;

        private bool _isRounded;

        private AxState _state;

        private AxColor _color;

        private AxShape _shape;

        // =============
        // ===== Fields 
        // =============

        private readonly AxSelectButton _selectButton;

        private readonly AxSelectBox _selectBox;

        // ===================
        // ===== Constructors
        // ===================

        public AxSelect() : base()
        {

            // Objects

            _selectButton = new AxSelectButton();

            _selectBox = new AxSelectBox();

            _items = new List<string>();

            // Events

            _selectButton.LostFocus += SelectButton_LostFocus;

            _selectButton.Click += SelectButton_Click;

            // Collections

            Controls.Add(_selectButton);

            _selectButton.Location = Point.Empty;

            Width = 180;

        }

        // =============
        // ===== Events
        // =============

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            Invalidate();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Parent != null)
            {
                Parent.Controls.Add(_selectBox);
            }

            UpdateItems();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            _selectBox.Location = new Point
            {
                X = Location.X + 3,
                Y = Location.Y + _selectButton.Height + 3
            };

            base.OnLocationChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Height = _selectButton.Height;

            _selectButton.Width = Width;

            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Height = _selectButton.Height;
        }

        // ===============
        // ===== Handlers
        // ===============

        private void SelectButton_Click(object sender, EventArgs e)
        {
            _selectBox.Toggle();
        }

        private void SelectButton_LostFocus(object sender, EventArgs e)
        {
            _selectBox.Close();
        }

        private void SelectItem_Click(object sender, EventArgs e)
        {
            var selectedItem = sender as AxSelectItem;

            for (int i = 0; i < _selectBox.Controls.Count; i++)
            {
                var currentItem = _selectBox.Controls[i] as AxSelectItem;

                if (currentItem == selectedItem)
                {
                    _selectedItem = currentItem;
                    _selectedItem.State = AxState.Active;
                    _selectedIndex = i;
                }
                else
                {
                    currentItem.State = AxState.Normal;
                }
            }

            SetTextAndCloseDropdown();

            SelectedIndexChanged(this, e);
        }

        // ==============
        // ===== Methods
        // ==============

        private void SetControlProperties()
        {
            _selectButton.IsRounded = _isRounded;
            _selectButton.State = _state;
            _selectButton.Shape = _shape;
            _selectButton.Color = _color;

            _selectBox.State = _state;
            _selectBox.Color = _color;
        }

        private void UpdateItems()
        {
            _selectBox.Controls.Clear();

            for (int i = 0; i < _items.Count; i++)
            {
                var selectItem = new AxSelectItem()
                {
                    Text = _items[i],
                    Index = i
                };

                selectItem.Location = new Point
                {
                    X = 1,
                    Y = i * selectItem.Height + 8
                };

                selectItem.Click += SelectItem_Click;

                _selectBox.Controls.Add(selectItem);
            }

            Invalidate();
        }

        private void SetSelectedItem()
        {
            // Loop the items for a matching object and update the selected
            // index, and invoke the selected index changed event
            for (int i = 0; i < _selectBox.Controls.Count; i++)
            {
                var currentItem = _selectBox.Controls[i] as AxSelectItem;

                if (currentItem == _selectedItem)
                {
                    _selectedItem.State = AxState.Active;
                    _selectedIndex = i;
                }
                else
                {
                    currentItem.State = AxState.Normal;
                }
            }

            SetTextAndCloseDropdown();

            SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void SetSelectedIndex()
        {
            for (int i = 0; i < _selectBox.Controls.Count; i++)
            {
                var currentItem = _selectBox.Controls[i] as AxSelectItem;

                if (i == _selectedIndex)
                {
                    _selectedItem = currentItem;
                    _selectedItem.State = AxState.Active;
                }
                else
                {
                    currentItem.State = AxState.Normal;
                }
            }

            SetTextAndCloseDropdown();

            SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void SetTextAndCloseDropdown()
        {
            _selectButton.Text = _selectedItem.Text;
            _selectButton.ClosedState();
            _selectBox.Close();

        }


    }
}
