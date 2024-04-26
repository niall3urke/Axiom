using Axiom.Controls.Select.Designer;
using Axiom.Core.Bases;
using Axiom.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

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

        // TODO

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
        public Color BackgroundColor => BackColor;

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

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            // Ensure the backcolor is up to date. Calling invalidate on
            // AxSelect doesn't seem to fire the OnPaint event. Hence we 
            // catch it here.
            if (Parent is IAxControl control)
            {
                BackColor = control.BackgroundColor;
                _selectButton.BackColor = control.BackgroundColor;
                _selectBox.BackColor = control.BackgroundColor;
            }

            // Invalidate/redraw the child controls too
            _selectButton.Invalidate();
            _selectBox.Invalidate();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            CalculateSelectBoxLocation();
            UpdateItems();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
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

            if (Parent is IAxControl control)
            {
                BackColor = control.BackgroundColor;
            }

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

        private void CalculateSelectBoxLocation()
        {
            // Always has to be a parent, it's hosted control
            Control parent = Parent;

            // Keep going until we get the root
            while (parent.Parent != null)
                parent = parent.Parent;

            // Add the select to the root so it's displayed over everything else
            parent.Controls.Add(_selectBox);

            // Get the location as coordinates of the screen
            var abs = PointToScreen(Location);

            // Now get the location relative to the parent 
            var rel = parent.PointToClient(abs);

            // Determine the location of the select box in the parent
            _selectBox.Location = new Point
            {
                X = rel.X - Location.X + 3,
                Y = rel.Y - Location.Y + _selectButton.Height + 3
            };
        }

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
