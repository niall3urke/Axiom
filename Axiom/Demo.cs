using Axiom.Core;
using System;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Axiom
{
    public partial class Demo : Form
    {
        public Demo()
        {
            InitializeComponent();
        }

        private void TogRound_CheckedChanged(object sender, EventArgs e)
        {
            RecursivelyUpdate(PnlControls, "IsRounded", TogRound.Checked);
        }

        private void TogOutline_CheckedChanged(object sender, EventArgs e)
        {
            RecursivelyUpdate(PnlControls, "IsOutlined", TogOutline.Checked);
        }

        private void TogLight_CheckedChanged(object sender, EventArgs e)
        {
            RecursivelyUpdate(PnlControls, "IsLight", TogLight.Checked);
        }

        private void TogInvert_CheckedChanged(object sender, EventArgs e)
        {
            RecursivelyUpdate(PnlControls, "IsInverted", TogInvert.Checked);
        }

        private void TogEnable_CheckedChanged(object sender, EventArgs e)
        {
            RecursivelyUpdate(PnlControls, "Enabled", TogEnable.Checked);
        }

        private void TogLoading_CheckedChanged(object sender, EventArgs e)
        {
            var state = TogLoading.Checked ? AxState.Loading : AxState.Normal;
            RecursivelyUpdate(PnlControls, "State", state);
        }

        private void CmbShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecursivelyUpdate(PnlControls, "Shape", (AxShape)CmbShape.SelectedIndex);
        }

        private void CmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecursivelyUpdate(PnlControls, "Color", (AxColor)CmbColor.SelectedIndex);
        }

        private void RecursivelyUpdate<T>(Control control, string propertyName, T value)
        {
            foreach (Control child in control.Controls)
            {

                if (child is IAxControl axControl)
                {
                    var pi = typeof(IAxControl).GetProperty(propertyName);

                    if (pi != null && pi.CanWrite)
                    {
                        pi.SetValue(axControl, value);
                    }
                }

                RecursivelyUpdate(child, propertyName, value);
            }
        }


    }
}
