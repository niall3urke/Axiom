using Axiom.Core;
using System.Drawing;

namespace Axiom.Controls.Select
{
    public class SelectItemColorHelper
    {

        public static (Color, Color, Color) GetColors(AxColor color, AxState state, Color transparent)
        {
            switch (color)
            {
                case AxColor.Primary:
                    return GetDefault(state);

                case AxColor.Link:
                    return GetDefault(state);

                case AxColor.Info:
                    return GetDefault(state);

                case AxColor.Success:
                    return GetDefault(state);

                case AxColor.Warning:
                    return GetDefault(state);

                case AxColor.Danger:
                    return GetDefault(state);

                case AxColor.White:
                    return GetDefault(state);

                case AxColor.Light:
                    return GetDefault(state);

                case AxColor.Dark:
                    return GetDefault(state);

                case AxColor.Black:
                    return GetDefault(state);

                case AxColor.Text:
                    return GetDefault(state);

                case AxColor.Ghost:
                    return GetDefault(state);

                default:
                    return GetDefault(state);
            }
        }

        private static (Color, Color, Color) GetDefault(AxState state)
        {
            // Normal
            Color backgroundColor = Color.White;
            Color foregroundColor = Color.White;
            Color borderColor = backgroundColor;

            if (state == AxState.Normal)
            {
                backgroundColor = Color.White;
                foregroundColor = Color.FromArgb(74, 74, 74);
                borderColor = backgroundColor;
            }

            else if (state == AxState.Hover)
            {
                backgroundColor = Color.FromArgb(245, 245, 245);
                foregroundColor = Color.FromArgb(10, 10, 10);
                borderColor = backgroundColor;
            }

            else if (state == AxState.Active)
            {
                backgroundColor = Color.FromArgb(72, 95, 199);
                foregroundColor = Color.FromArgb(255, 255, 255);
                borderColor = backgroundColor;
            }


            return (backgroundColor, foregroundColor, borderColor);
        }


    }
}
