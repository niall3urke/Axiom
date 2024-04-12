using Axiom.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axiom.Controls.Label
{
    class GhostLabel
    {

        public static Color Get(AxState state, IAppearanceProperties p, Color transparent)
        {
            Color color = Color.White;

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

            return color;

        }

        private static Color GetNormal(IAppearanceProperties p, Color transparent)
        {
            Color color = Color.FromArgb(72, 95, 199);

            if (p.IsLight)
            {
                color = Color.FromArgb(179, 0, 0, 0);
            }

            return color;
        }

        private static Color GetHover(IAppearanceProperties p, Color transparent)
        {
            Color color = Color.FromArgb(72, 95, 199);

            if (p.IsLight)
            {
                color = Color.FromArgb(179, 0, 0, 0); // #3850b7
            }

            return color;
        }

        private static Color GetActive(IAppearanceProperties p, Color transparent)
        {
            Color color = Color.FromArgb(72, 95, 199);

            if (p.IsLight)
            {
                color = Color.FromArgb(179, 0, 0, 0); // #3850b7
            }

            return color;
        }

        private static Color GetDisabled(IAppearanceProperties p, Color transparent)
        {
            Color color = Color.FromArgb(128, 72, 95, 199);

            if (p.IsLight)
            {
                color = Color.FromArgb(90, 0, 0, 0);
            }

            return color;
        }

    }
}
