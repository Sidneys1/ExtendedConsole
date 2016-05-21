using System;

namespace ExtendedConsole.Enums
{
    [Flags]
    public enum ControlKeyState
    {
        Capslock = 0x0080,
        EnhancedKey = 0x0100,
        LeftAlt = 0x0002,
        LeftCtrl = 0x0008,
        Numlock = 0x0020,
        RightAlt = 0x0001,
        RightCtrl = 0x0004,
        ScrollLock = 0x0040,
        Shift = 0x0010
    }
}