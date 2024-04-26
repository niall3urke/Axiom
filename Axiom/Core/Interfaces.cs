using System.ComponentModel;
using System.Drawing;

namespace Axiom.Core
{

    public interface IDrawable : INotifyPropertyChanged
    {

        // Properties 

        Color BackgroundColor { get; set; }

        Color ForegroundColor { get; set; }

        Color BorderColor { get; set; }

        Color FocusColor { get; set; }

        PointF Location { get; set; }

        int Height { get; set; }

        int Width { get; set; }

        // Methods/events

        void EnabledChanged(bool enabled);

        bool MouseLeave(Point p);

        bool MouseEnter(Point p);

        bool MouseDown(Point p);

        bool MouseUp(Point p);

        bool Click(Point p);

        // Methods

        void Draw(Graphics g);

    }

    public interface IAppearanceProperties
    {
        bool IsOutlined { get; set; }

        bool IsInverted { get; set; }

        bool IsLight { get; set; }
    }

    public interface ICanCastShadow
    {
        AxShadowDirection ShadowDirection { get; set; }

        float ShadowOpacity { get; set; }

        Color ShadowColor { get; set; }

        float ShadowBlur { get; set; }

        int ShadowSpread { get; set; }

        bool HasShadow { get; set; }
    }

    public interface IAxControl
    {
        Color BackgroundColor { get; }

        bool IsOutlined { get; set; }

        bool IsInverted { get; set; }

        bool IsRounded { get; set; }

        AxState State { get; set; }

        AxShape Shape { get; set; }

        AxColor Color { get; set; }

        bool IsLight { get; set; }
    }

    public interface IColorStateHelper
    {
        Color Default { get; }

        Color Hover { get; }

        Color Active { get; }

        Color Invert { get; }

        Color Light { get; }

        Color LightHover { get; }

        Color LightActive { get; }

        Color LightInvert { get; }

        Color Dark { get; }
    }
}
