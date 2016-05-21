using System.Runtime.InteropServices;
using ExtendedConsole.Enums;

namespace ExtendedConsole.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MouseEventRecord
    {
        [FieldOffset(0)] public Coord MousePosition;
        [FieldOffset(4)] public MouseButtonState ButtonState;
        [FieldOffset(8)] public ControlKeyState ControlKeyState;
        [FieldOffset(12)] public MouseEventFlags EventFlags;
    }
}