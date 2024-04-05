using Axiom.Core;
using System.Drawing;

namespace Axiom.Controls.ColorHelpers
{
    class GhostButton
    {

        public static (Color, Color, Color, Color) Get(AxState state, IAppearanceProperties p, Color transparent)
        {
            Color backgroundColor = Color.White;
            Color foregroundColor = Color.White;
            Color borderColor = Color.White;
            Color focusColor = Color.White;

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

            return (backgroundColor, foregroundColor, borderColor, focusColor);
        }

        private static (Color, Color, Color, Color) GetNormal(IAppearanceProperties p, Color transparent)
        {
            Color background = transparent;
            Color foreground = Color.FromArgb(72, 95, 199);
            Color border = background;
            Color focus = foreground;

            if (p.IsLight)
            {
                background = Color.FromArgb(245, 245, 245);
                foreground = Color.FromArgb(179, 0, 0, 0);
                border = transparent;
            }

            return (background, foreground, border, focus);
        }

        private static (Color, Color, Color, Color) GetHover(IAppearanceProperties p, Color transparent)
        {
            Color background = transparent; // #3e56c4
            Color foreground = Color.FromArgb(72, 95, 199);
            Color border = background;
            Color focus = foreground;

            if (p.IsLight)
            {
                background = Color.FromArgb(238, 238, 238); // #e6e9f7
                foreground = Color.FromArgb(179, 0, 0, 0); // #3850b7
                border = transparent;
            }

            return (background, foreground, border, focus);
        }

        private static (Color, Color, Color, Color) GetActive(IAppearanceProperties p, Color transparent)
        {
            Color background = transparent;
            Color foreground = Color.FromArgb(72, 95, 199);
            Color border = background;
            Color focus = foreground;

            if (p.IsLight)
            {
                background = Color.FromArgb(232, 232, 232); // #e6e9f7
                foreground = Color.FromArgb(179, 0, 0, 0); // #3850b7
                border = transparent;
            }

            return (background, foreground, border, focus);
        }

        private static (Color, Color, Color, Color) GetDisabled(IAppearanceProperties p, Color transparent)
        {
            Color background = transparent;
            Color foreground = Color.FromArgb(128, 72, 95, 199);
            Color border = background;
            Color focus = foreground;

            if (p.IsLight)
            {
                background = Color.FromArgb(128, 245, 245, 245);
                foreground = Color.FromArgb(90, 0, 0, 0);
                border = transparent;
            }

            return (background, foreground, border, focus);
        }
    }
}
