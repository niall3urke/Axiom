using System.Drawing;

namespace Axiom.Core.ColorHelpers
{
    public class Colors
    {

        // Properties

        public static ColorDefault Default { get; }

        public static ColorPrimary Primary { get; }

        public static ColorSuccess Success { get; }

        public static ColorWarning Warning { get; }

        public static ColorDanger Danger { get; }

        public static ColorGhost Ghost { get; }

        public static ColorLink Link { get; }

        public static ColorInfo Info { get; }


        public static ColorLight Light { get; }

        public static ColorDark Dark { get; }


        public static ColorWhite White { get; }

        public static ColorBlack Black { get; }


        // Methods

        public static Color GetColorInvert(Color c)
        {
            double luminance = GetColorLuminance(c);

            if (luminance > 0.55)
            {
                return Color.FromArgb(179, 0, 0, 0);
            }
            return Color.White;
        }

        private static double GetColorLuminance(Color c)
        {
            return 0.299 * c.R + 0.587 * c.G + 0.114 * c.B;
        }

    }

    public struct ColorDefault : IColorStateHelper
    {
        public Color Default => Color.White;

        public Color Hover => Color.White;

        public Color Active => Color.White;

        public Color Invert => Color.FromArgb(179, 0, 0, 0);


        public Color Light => Color.White; // TODO

        public Color LightHover => Color.White;

        public Color LightActive => Color.White;

        public Color LightInvert => Color.White;


        public Color Dark => Color.White;
    }

    public struct ColorPrimary : IColorStateHelper
    {
        public Color Default => Color.FromArgb(0, 209, 178);

        public Color Hover => Color.FromArgb(0, 196, 167);

        public Color Active => Color.FromArgb(0, 184, 156);

        public Color Invert => Color.White;


        public Color Light => Color.FromArgb(235, 255, 252);

        public Color LightHover => Color.FromArgb(222, 255, 250);

        public Color LightActive => Color.FromArgb(209, 255, 248);

        public Color LightInvert => Color.FromArgb(0, 148, 126);


        public Color Dark => Color.FromArgb(0, 148, 126);
    }

    public struct ColorLink : IColorStateHelper
    {
        public Color Default => Color.FromArgb(72, 95, 199);

        public Color Hover => Color.FromArgb(62, 86, 196);

        public Color Active => Color.FromArgb(58, 81, 187);

        public Color Invert => Color.White;


        public Color Light => Color.FromArgb(239, 241, 250);

        public Color LightHover => Color.FromArgb(230, 233, 247);

        public Color LightActive => Color.FromArgb(220, 224, 244);

        public Color LightInvert => Color.FromArgb(56, 80, 183);


        public Color Dark => Color.FromArgb(56, 80, 183);
    }

    public struct ColorInfo : IColorStateHelper
    {
        public Color Default => Color.FromArgb(62, 142, 208);

        public Color Hover => Color.FromArgb(52, 136, 206);

        public Color Active => Color.FromArgb(48, 130, 197);

        public Color Invert => Color.White;


        public Color Light => Color.FromArgb(239, 245, 251);

        public Color LightHover => Color.FromArgb(228, 239, 249);

        public Color LightActive => Color.FromArgb(218, 233, 246);

        public Color LightInvert => Color.FromArgb(41, 111, 168);


        public Color Dark => Color.FromArgb(56, 80, 183);
    }

    public struct ColorSuccess : IColorStateHelper
    {
        public Color Default => Color.FromArgb(72, 199, 142);

        public Color Hover => Color.FromArgb(62, 196, 135);

        public Color Active => Color.FromArgb(58, 187, 129);

        public Color Invert => Color.White;


        public Color Light => Color.FromArgb(239, 250, 245);

        public Color LightHover => Color.FromArgb(230, 247, 239);

        public Color LightActive => Color.FromArgb(220, 244, 233);

        public Color LightInvert => Color.FromArgb(37, 121, 83);


        public Color Dark => Color.FromArgb(37, 121, 83);
    }

    public struct ColorWarning : IColorStateHelper
    {
        public Color Default => Color.FromArgb(255, 224, 138);

        public Color Hover => Color.FromArgb(255, 220, 125);

        public Color Active => Color.FromArgb(255, 217, 112);

        public Color Invert => Color.FromArgb(179, 0, 0, 0);


        public Color Light => Color.FromArgb(255, 250, 235);

        public Color LightHover => Color.FromArgb(255, 246, 222);

        public Color LightActive => Color.FromArgb(255, 243, 209);

        public Color LightInvert => Color.FromArgb(148, 108, 0);


        public Color Dark => Color.FromArgb(148, 108, 0);
    }

    public struct ColorDanger : IColorStateHelper
    {
        public Color Default => Color.FromArgb(241, 70, 104);

        public Color Hover => Color.FromArgb(240, 58, 95);

        public Color Active => Color.FromArgb(239, 46, 85);

        public Color Invert => Color.White;


        public Color Light => Color.FromArgb(252, 212, 220);

        public Color LightHover => Color.FromArgb(253, 224, 230);

        public Color LightActive => Color.FromArgb(252, 212, 220);

        public Color LightInvert => Color.FromArgb(204, 15, 53);


        public Color Dark => Color.FromArgb(204, 15, 53);
    }

    public struct ColorDark : IColorStateHelper
    {
        public Color Default => Color.FromArgb(54, 54, 54);

        public Color Hover => Color.FromArgb(47, 47, 47);

        public Color Active => Color.FromArgb(41, 41, 41);

        public Color Invert => Color.White;


        public Color Light => Color.FromArgb(54, 54, 54); // TODO;

        public Color LightHover => Color.FromArgb(47, 47, 47);

        public Color LightActive => Color.FromArgb(41, 41, 41);

        public Color LightInvert => Color.White;


        public Color Dark => Color.White;
    }

    public struct ColorLight : IColorStateHelper
    {
        public Color Default => Color.FromArgb(245, 245, 245);

        public Color Hover => Color.FromArgb(238, 238, 238);

        public Color Active => Color.FromArgb(232, 232, 232);

        public Color Invert => Color.FromArgb(179, 0, 0, 0);


        public Color Light => Color.FromArgb(245, 245, 245); // TODO;

        public Color LightHover => Color.FromArgb(238, 238, 238);

        public Color LightActive => Color.FromArgb(232, 232, 232);

        public Color LightInvert => Color.FromArgb(179, 0, 0, 0);


        public Color Dark => Color.FromArgb(179, 0, 0, 0);
    }

    public struct ColorBlack : IColorStateHelper
    {
        public Color Default => Color.FromArgb(10, 10, 10);

        public Color Hover => Color.FromArgb(4, 4, 4);

        public Color Active => Color.FromArgb(0, 0, 0);

        public Color Invert => Color.White;


        public Color Light => Color.FromArgb(10, 10, 10); // TODO;

        public Color LightHover => Color.FromArgb(4, 4, 4);

        public Color LightActive => Color.FromArgb(0, 0, 0);

        public Color LightInvert => Color.White;


        public Color Dark => Color.White;
    }

    public struct ColorWhite : IColorStateHelper
    {
        public Color Default => Color.FromArgb(255, 255, 255);

        public Color Hover => Color.FromArgb(249, 249, 249);

        public Color Active => Color.FromArgb(242, 242, 242);

        public Color Invert => Color.FromArgb(179, 0, 0, 0);


        public Color Light => Color.FromArgb(255, 255, 255); // TODO;

        public Color LightHover => Color.FromArgb(249, 249, 249);

        public Color LightActive => Color.FromArgb(242, 242, 242);

        public Color LightInvert => Color.FromArgb(179, 0, 0, 0);


        public Color Dark => Color.FromArgb(179, 0, 0, 0);
    }

    public struct ColorGhost : IColorStateHelper
    {
        public Color Default => Color.Transparent;

        public Color Hover => Color.Transparent;

        public Color Active => Color.Transparent;

        public Color Invert => Color.FromArgb(72, 95, 199);


        public Color Light => Color.FromArgb(245, 245, 245);

        public Color LightHover => Color.FromArgb(238, 238, 238);

        public Color LightActive => Color.FromArgb(232, 232, 232);

        public Color LightInvert => Color.FromArgb(179, 0, 0, 0);


        public Color Dark => Color.White;
    }
}
