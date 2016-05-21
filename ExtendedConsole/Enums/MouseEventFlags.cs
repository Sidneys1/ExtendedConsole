using System;

namespace ExtendedConsole.Enums
{
    [Flags]
    public enum MouseEventFlags
    {
        MouseMoved = 1,
        DoubleClick = 2,
        VerticalWheel = 4,
        HorizontalWheel = 8,
    }
}