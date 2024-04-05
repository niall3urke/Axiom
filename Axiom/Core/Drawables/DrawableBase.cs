using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Axiom.Core.Drawables
{
    public abstract class DrawableBase : IDrawable
    {
        // Actions

        public event PropertyChangedEventHandler PropertyChanged;

        // Properties

        public Point Location
        {
            get => _location;
            set => SetField(ref _location, value);
        }

        public GraphicsPath Path { get; protected set; }

        public Color BackgroundColor { get; set; }

        public Color ForegroundColor { get; set; }

        public Color BorderColor { get; set; }

        public Color FocusColor { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        // Fields

        private Point _location;

        // Constructors

        public DrawableBase() => Location = Point.Empty;

        // Events

        public virtual void EnabledChanged(bool enabled) { }

        public virtual bool MouseLeave(Point p) => false;

        public virtual bool MouseEnter(Point p) => false;

        public virtual bool MouseDown(Point p) => false;

        public virtual bool MouseUp(Point p) => false;

        public abstract void Draw(Graphics g);

        public virtual bool Click(Point p) => false;

        // Methods

        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                NotifyPropertyChanged(propertyName);
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
