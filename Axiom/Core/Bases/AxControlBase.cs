using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Axiom.Core.Bases
{
    [ToolboxItem(false)]
    public abstract class AxControlBase : Control
    {
        
        // Consts

        protected const string Category = "Axiom";

        // Constructors 

        public AxControlBase()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
        }

        // Methods

        protected void SetField<T>(ref T field, T value, Action todo = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;

                todo?.Invoke();

                Invalidate();
            }
        }

        protected bool ContainsCursor()
        {
            return ClientRectangle.Contains(PointToClient(Cursor.Position));
        }

    }
}
