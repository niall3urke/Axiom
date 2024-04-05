using Axiom.Core;
using System;
using System.Linq;
using System.Windows.Forms;

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
            foreach (var control in Controls.OfType<IAxControl>())
            {
                control.IsRounded = !control.IsRounded;
            }
        }

        private void TogOutline_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var control in Controls.OfType<IAxControl>())
            {
                control.IsOutlined = !control.IsOutlined;
            }
        }

        private void TogLight_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var control in Controls.OfType<IAxControl>())
            {
                control.IsLight = !control.IsLight;
            }
        }

        private void TogInvert_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var control in Controls.OfType<IAxControl>())
            {
                control.IsInverted = !control.IsInverted;
            }
        }

        private void TogEnable_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control control in Controls.OfType<IAxControl>())
            {
                control.Enabled = !control.Enabled;
            }
        }

        private void TogLoading_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var control in Controls.OfType<IAxControl>())
            {
                if (control.State == AxState.Loading)
                {
                    control.State = AxState.Normal;
                }
                else
                {
                    control.State = AxState.Loading;
                }
            }
        }

        private void CmbShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var button in Controls.OfType<IAxControl>())
            {
                button.Shape = (AxShape)CmbShape.SelectedIndex;
            }
        }

        private void CmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var control in Controls.OfType<IAxControl>())
            {
                control.Color = (AxColor)CmbColor.SelectedIndex;
            }
        }


    }
}
