using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Axiom.Core.Bases
{
    [ToolboxItem(false)]
    public partial class AxUserControlBase : UserControl
    {

        // Fields

        protected const string Category = "Axiom";

        // Constructors

        public AxUserControlBase()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
        }

        // Methods

        protected void SetField<T>(ref T field, T value)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                Invalidate();
            }
        }

        protected bool ContainsCursor()
        {
            return ClientRectangle.Contains(PointToClient(Cursor.Position));
        }
    }
}
