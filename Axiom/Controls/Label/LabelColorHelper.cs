using Axiom.Core;
using Axiom.Core.ColorHelpers;
using System.Drawing;

namespace Axiom.Controls.Label
{
    class LabelColorHelper : ColorHelper
    {

        public static Color Get(AxColor c, AxState s, IAppearanceProperties p, Color transparent)
        {
            if (c == AxColor.Default)
            {
                return GetDefault(s).Item2; //ForegroundColor
            }

            else if (c == AxColor.Ghost)
            {
                return GhostLabel.Get(s, p, transparent);
            }

            else if (c == AxColor.Text)
            {
                return TextLabel.Get(s, p, transparent);
            }

            else
            {
                return GetColors(c, s, p, transparent);
            }
        }

        private static Color GetColors(AxColor c, AxState s, IAppearanceProperties p, Color transparent)
        {
            var helper = GetColorHelper(c);

            if (s == AxState.Normal || s == AxState.Loading)
            {
                return GetNormal(helper, p, transparent);
            }

            else if (s == AxState.Hover)
            {
                return GetHover(helper, p, transparent);
            }

            else if (s == AxState.Active)
            {
                return GetActive(helper, p, transparent);
            }

            // AxState.Disabled

            var color = GetNormal(helper, p, transparent);

            return Color.FromArgb(128, color.R, color.G, color.B);
        }

        private static Color GetNormal(IColorHelper helper, IAppearanceProperties p, Color transparent)
        {
            Color foreColor;

            foreColor = helper.Default;

            if (p.IsLight)
            {
                foreColor = helper.Light;
            }
            else if (p.IsOutlined && p.IsInverted)
            {
                foreColor = transparent;
            }
            else if (p.IsOutlined)
            {
                foreColor = transparent;
            }
            else if (p.IsInverted)
            {
                foreColor = transparent;
            }

            return foreColor;
        }

        private static Color GetHover(IColorHelper helper, IAppearanceProperties p, Color transparent)
        {
            Color foreColor;

            foreColor = helper.Hover;

            if (p.IsLight)
            {
                foreColor = helper.LightHover;
            }
            else if (p.IsOutlined && p.IsInverted)
            {
                foreColor = Color.White;

            }
            else if (p.IsOutlined)
            {
                foreColor = helper.Default;
            }
            else if (p.IsInverted)
            {
                foreColor = Color.FromArgb(242, 242, 242);
            }

            return foreColor;
        }

        private static Color GetActive(IColorHelper helper, IAppearanceProperties p, Color transparent)
        {
            Color foreColor;

            foreColor = helper.Active;

            if (p.IsLight)
            {
                foreColor = helper.LightActive;
            }
            else if (p.IsOutlined && p.IsInverted)
            {
                foreColor = Color.White;
            }
            else if (p.IsOutlined)
            {
                foreColor = helper.Default;
            }
            else if (p.IsInverted)
            {
                foreColor = Color.FromArgb(242, 242, 242);
            }

            return foreColor;
        }


    }
}
