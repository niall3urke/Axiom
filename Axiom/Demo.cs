using Axiom.Core;
using System;
using System.Windows.Forms;

namespace Axiom
{
    public partial class Demo : Form
    {
        public Demo()
        {
            InitializeComponent();
        }

        // IAxControl control events

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
            foreach (Control control in PnlControls.Controls)
            {
                control.Enabled = !control.Enabled;
            }
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

        private void Demo_Load(object sender, EventArgs e)
        {

        }

        // ICanCastShadow control events

        private void TogShadow_CheckedChanged(object sender, EventArgs e)
        {
            RecursivelyUpdate(PnlControls, "HasShadow", TogShadow.Checked);
        }

        private void TxtShadowSpread_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(TxtShadowSpread.Text, out int value))
                RecursivelyUpdate(PnlControls, "ShadowSpread", value);
        }

        private void TxtShadowOpacity_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(TxtShadowOpacity.Text, out float value))
                RecursivelyUpdate(PnlControls, "ShadowOpacity", value);
        }

        private void TxtShadowBlur_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(TxtShadowBlur.Text, out float value))
                RecursivelyUpdate(PnlControls, "ShadowBlur", value);
        }

        private void CmbShadow_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecursivelyUpdate(PnlControls, "ShadowDirection", (AxShadowDirection)CmbShadowDirection.SelectedIndex);
        }

        // Methods

        private void RecursivelyUpdate<T>(Control control, string propertyName, T value)
        {
            foreach (Control child in control.Controls)
            {
                if (child is ICanCastShadow shadow)
                {
                    var pi = typeof(ICanCastShadow).GetProperty(propertyName);

                    if (pi != null && pi.CanWrite)
                    {
                        pi.SetValue(shadow, value);
                    }
                }
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
