using System.Runtime.InteropServices;
using ExtendedConsole.Enums;

namespace ExtendedConsole.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct InputRecord
    {
        [FieldOffset(0)] public EventType EventType;
        [FieldOffset(4)] public KeyEventRecord KeyEvent;
        [FieldOffset(4)] public MouseEventRecord MouseEvent;
        [FieldOffset(4)] public Coord BufferSizeEvent;
        [FieldOffset(4)] public MenuEventRecord MenuEvent;
        [FieldOffset(4)] public FocusEventRecord FocusEvent;
    };
}