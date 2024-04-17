using System.Drawing;

namespace Axiom.Core.ColorHelpers
{
    public class ColorHelper
    {

        protected static IColorStateHelper GetColorHelper(AxColor c)
        {
            switch (c)
            {
                // Standard colors

                case AxColor.Primary:
                    return Colors.Primary;

                case AxColor.Link:
                    return Colors.Link;

                case AxColor.Info:
                    return Colors.Info;

                case AxColor.Success:
                    return Colors.Success;

                case AxColor.Warning:
                    return Colors.Warning;

                case AxColor.Danger:
                    return Colors.Danger;

                // Neutrals

                case AxColor.Black:
                    return Colors.Black;

                case AxColor.White:
                    return Colors.White;

                case AxColor.Dark:
                    return Colors.Dark;

                case AxColor.Light:
                    return Colors.Light;


                default:
                    return Colors.Default;
            }
        }

        protected static (Color, Color, Color, Color) GetDefault(AxState s)
        {
            Color background, foreground, border, focus;

            background = Color.White;
            focus = Color.FromArgb(72, 95, 199);

            if (s == AxState.Normal || s == AxState.Loading)
            {
                foreground = Color.FromArgb(54, 54, 54);
                border = Color.FromArgb(219, 219, 219);
            }

            else if (s == AxState.Hover)
            {
                foreground = Color.FromArgb(54, 54, 54);
                border = Color.FromArgb(181, 181, 181);
            }

            else if (s == AxState.Active)
            {
                foreground = Color.FromArgb(54, 54, 54);
                border = Color.FromArgb(74, 74, 74);
            }

            else
            {
                foreground = Color.FromArgb(128, 54, 54, 54);
                border = Color.FromArgb(128, 219, 219, 219);
            }

            return (background, foreground, border, focus);
        }

        protected static (Color, Color, Color, Color) GetDisabled(Color background, Color foreground, Color border, Color focus)
        {
            background = Color.FromArgb(128, background.R, background.G, background.B);

            foreground = Color.FromArgb(128, foreground.R, foreground.G, foreground.B);

            border = Color.FromArgb(128, border.R, border.G, border.B);

            focus = Color.FromArgb(128, focus.R, focus.G, focus.B);

            return (background, foreground, border, focus);
        }

        private static int Invert(int color, bool ignoreAlpha = true)
        {
            if (ignoreAlpha)
                return (int)(0xff000000 | ~color);

            return color ^ 0x00ffffff;
        }


    }
}
