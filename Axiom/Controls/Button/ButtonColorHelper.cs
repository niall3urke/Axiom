using Axiom.Core.ColorHelpers;
using Axiom.Core;
using System.Drawing;

namespace Axiom.Controls.ColorHelpers
{
    class ButtonColorHelper : ColorHelper
    {

        public static (Color, Color, Color, Color) Get(AxColor c, AxState s, IAppearanceProperties p, Color transparent)
        {
            if (c == AxColor.Default)
            {
                return GetDefault(s);
            }

            else if (c == AxColor.Ghost)
            {
                return GhostButton.Get(s, p, transparent);
            }

            else if (c == AxColor.Text)
            {
                return TextButton.Get(s, p, transparent);
            }

            else
            {
                return GetColors(c, s, p, transparent);
            }
        }

        // =======================
        // ===== Methods: private
        // =======================

        private static (Color, Color, Color, Color) GetColors(AxColor c, AxState s, IAppearanceProperties p, Color transparent)
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

            var (background, foreground, border, focus) = GetNormal(helper, p, transparent);

            return GetDisabled(background, foreground, border, focus);
        }

        private static (Color, Color, Color, Color) GetNormal(IColorHelper helper, IAppearanceProperties p, Color transparent)
        {
            Color background, foreground, border, focus;

            background = helper.Default;
            foreground = helper.Invert;

            if (p.IsLight)
            {
                background = helper.Light;
                foreground = helper.LightInvert;
            }
            else if (p.IsOutlined && p.IsInverted)
            {
                background = transparent;
                foreground = Color.White;
            }
            else if (p.IsOutlined)
            {
                background = transparent;
                foreground = helper.Default;
            }
            else if (p.IsInverted)
            {
                background = transparent;
                foreground = helper.Default;
            }

            border = p.IsOutlined && !p.IsLight ? foreground : background;
            focus = border;

            return (background, foreground, border, focus);
        }

        private static (Color, Color, Color, Color) GetHover(IColorHelper helper, IAppearanceProperties p, Color transparent)
        {
            Color background, foreground, border, focus;

            background = helper.Hover;
            foreground = helper.Invert;

            if (p.IsLight)
            {
                background = helper.LightHover;
                foreground = helper.LightInvert;
            }
            else if (p.IsOutlined && p.IsInverted)
            {
                background = Color.White;
                foreground = helper.Default;

            }
            else if (p.IsOutlined)
            {
                background = helper.Default;
                foreground = Color.White;
            }
            else if (p.IsInverted)
            {
                background = Color.FromArgb(242, 242, 242);
                foreground = helper.Default;
            }

            border = background;
            focus = background;

            return (background, foreground, border, focus);
        }

        private static (Color, Color, Color, Color) GetActive(IColorHelper helper, IAppearanceProperties p, Color transparent)
        {
            Color background, foreground, border, focus;

            background = helper.Active;
            foreground = helper.Invert;

            if (p.IsLight)
            {
                background = helper.LightActive;
                foreground = helper.LightInvert;
            }
            else if (p.IsOutlined && p.IsInverted)
            {
                background = Color.White;
                foreground = helper.Default;
            }
            else if (p.IsOutlined)
            {
                background = helper.Default;
                foreground = Color.White;
            }
            else if (p.IsInverted)
            {
                background = Color.FromArgb(242, 242, 242);
                foreground = helper.Default;
            }

            border = background;
            focus = background;

            return (background, foreground, border, focus);
        }


    }
}
