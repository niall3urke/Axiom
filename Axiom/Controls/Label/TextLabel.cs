using Axiom.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axiom.Controls.Label
{
    class TextLabel
    {

        public static Color Get(AxState state, IAppearanceProperties p, Color transparent)
        {
            Color foregroundColor = Color.White;

            if (state == AxState.Normal || state == AxState.Loading)
            {
                return GetNormal(p, transparent);
            }

            else if (state == AxState.Hover)
            {
                return GetHover(p, transparent);
            }

            else if (state == AxState.Active)
            {
                return GetActive(p, transparent);
            }

            else if (state == AxState.Disabled)
            {
                return GetDisabled(p, transparent);
            }

            return foregroundColor;
        }

        private static Color GetNormal(IAppearanceProperties p, Color transparent)
        {
            Color foreground = Color.FromArgb(74, 74, 74);

            if (p.IsLight)
            {
                foreground = Color.FromArgb(179, 0, 0, 0);
            }

            return foreground;
        }

        private static Color GetHover(IAppearanceProperties p, Color transparent)
        {
            Color foreground = Color.FromArgb(54, 54, 54);

            if (p.IsLight)
            {
                foreground = Color.FromArgb(179, 0, 0, 0); // #3850b7
            }

            return (foreground);
        }

        private static Color GetActive(IAppearanceProperties p, Color transparent)
        {
            Color foreground = Color.FromArgb(54, 54, 54);

            if (p.IsLight)
            {
                foreground = Color.FromArgb(179, 0, 0, 0); // #3850b7
            }

            return (foreground);
        }

        private static Color GetDisabled(IAppearanceProperties p, Color transparent)
        {
            Color foreground = Color.FromArgb(128, 74, 74, 74);

            if (p.IsLight)
            {
                foreground = Color.FromArgb(90, 0, 0, 0);
            }

            return (foreground);
        }
    }
}
