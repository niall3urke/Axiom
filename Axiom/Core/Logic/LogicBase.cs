using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Axiom.Core.Logic
{
    public class LogicBase : INotifyPropertyChanged
    {

        // ==============
        // ===== Actions
        // ==============

        public event PropertyChangedEventHandler PropertyChanged;

        // =================
        // ===== Properties
        // =================

        public virtual AxState State
        {
            get => _state;
            set => SetField(ref _state, value);
        }

        public AxColor Color
        {
            get => _color;
            set => SetField(ref _color, value);
        }

        public AxShape Shape
        {
            get => _shape;
            set => SetField(ref _shape, value);
        }

        public string Text
        {
            get => _text;
            set => SetField(ref _text, value);
        }

        public bool HasFocus
        {
            get => _hasFocus;
            set => SetField(ref _hasFocus, value);
        }

        public Color BackgroundColor { get; set; }

        public Color ForegroundColor { get; set; }

        public Color BorderColor { get; set; }

        public Color FocusColor { get; set; }

        public bool DesignMode { get; set; }

        public Point Location { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public Font Font { get; set; }

        // =============
        // ===== Fields
        // =============

        private bool _hasFocus;

        private AxState _state;

        private AxColor _color;

        private AxShape _shape;

        private string _text;

        // ===================
        // ===== Constructors
        // ===================

        public LogicBase()
        {
            Font = new Font("Segoe UI", 16, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        // =============
        // ===== Events
        // =============

        public virtual void EnabledChanged(bool enabled) =>
            State = enabled ? AxState.Normal : AxState.Disabled;

        public virtual bool MouseLeave(Point p) => false;

        public virtual bool MouseEnter(Point p) => false;

        public virtual bool MouseDown(Point p) => false;

        public virtual bool MouseMove(Point p) => false;

        public virtual bool MouseUp(Point p) => false;

        public virtual bool Click(Point p) => false;

        // ==============
        // ===== Methods
        // ==============

        protected virtual void OnStateChanged() { }

        protected void SetField<T>(ref T field, T value, Action todo = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;

                todo?.Invoke();

                NotifyPropertyChanged();
            }
        }

        protected void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected int GetTextWidth(Graphics g)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                return 0;
            }
            return (int)Math.Round(g.MeasureString(Text, Font).Width);
        }

        public virtual bool ContainsPoint(Point p)
        {
            return new Rectangle(
                Location, new Size(Width, Height)).Contains(p);
        }


    }
}
