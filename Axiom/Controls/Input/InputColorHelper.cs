using Axiom.Core;
using Axiom.Core.ColorHelpers;
using System.Drawing;

namespace Axiom.Controls.Input
{
    class InputColorHelper : ColorHelper
    {

        public static (Color, Color, Color, Color) Get(AxColor c, AxState s, Color transparent)
        {
            if (c == AxColor.Default)
            {
                return GetDefault(s);
            }

            return GetColors(c, s, transparent);
        }

        // =======================
        // ===== Methods: private
        // =======================

        private new static (Color, Color, Color, Color) GetDefault(AxState state)
        {
            Color background, foreground, border, focus;

            background = Color.FromArgb(255, 255, 255);
            foreground = Color.FromArgb(54, 54, 54);

            border = Color.White;
            focus = Color.FromArgb(72, 95, 199);

            if (state == AxState.Normal || state == AxState.Loading)
            {
                border = Color.FromArgb(219, 219, 219);
            }

            else if (state == AxState.Hover)
            {
                border = Color.FromArgb(181, 181, 181);
            }

            else if (state == AxState.Active)
            {
                border = Color.FromArgb(72, 95, 199);
            }

            else if (state == AxState.Disabled)
            {
                background = Color.FromArgb(245, 245, 245);
                foreground = Color.FromArgb(122, 122, 122);
                border = Color.FromArgb(245, 245, 245);
            }

            return (background, foreground, border, focus);
        }

        private static (Color, Color, Color, Color) GetColors(AxColor c, AxState s, Color transparent)
        {
            var helper = GetColorHelper(c);

            if (s == AxState.Normal || s == AxState.Loading)
            {
                return GetNormal(helper, transparent);
            }

            else if (s == AxState.Hover)
            {
                return GetHover(helper, transparent);
            }

            else if (s == AxState.Active)
            {
                return GetActive(helper, transparent);
            }

            var (background, foreground, border, focus) = GetNormal(helper, transparent);

            return GetDisabled(background, foreground, border, focus);
        }

        private static (Color, Color, Color, Color) GetNormal(IColorStateHelper helper, Color transparent)
        {
            Color background, foreground, border, focus;

            foreground = Color.FromArgb(54, 54, 54);
            background = Color.White;
            border = helper.Default;
            focus = border;

            return (background, foreground, border, focus);
        }

        private static (Color, Color, Color, Color) GetHover(IColorStateHelper helper, Color transparent)
        {
            Color background, foreground, border, focus;

            foreground = Color.FromArgb(54, 54, 54);
            background = Color.White;
            border = helper.Hover;
            focus = border;

            return (background, foreground, border, focus);
        }

        private static (Color, Color, Color, Color) GetActive(IColorStateHelper helper, Color transparent)
        {
            Color background, foreground, border, focus;

            foreground = Color.FromArgb(54, 54, 54);
            background = Color.White;
            border = helper.Active;
            focus = border;

            return (background, foreground, border, focus);
        }


    }
}
