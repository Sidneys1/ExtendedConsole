using System;
using System.Runtime.InteropServices;

namespace ExtendedConsole.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        public CharInfo(char defaultChar = ' ',
            ConsoleColor foregroundColor = ConsoleColor.Gray,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            CharUnion = new CharUnion {UnicodeChar = defaultChar};
            Attributes = (short) ((int) foregroundColor | ((int) backgroundColor << 4));
        }

        [FieldOffset(0)] public CharUnion CharUnion;
        [FieldOffset(2)] public short Attributes;

        public ConsoleColor BackgroundColor
        {
            get { return (ConsoleColor) ((Attributes >> 4) & 0xF); }
            set { Attributes = (short) ((Attributes & 0xF) | ((int) value << 4)); }
        }

        public ConsoleColor ForegroundColor
        {
            get { return (ConsoleColor) (Attributes & 0xF); }
            set { Attributes = (short) ((Attributes & 0xF0) | (int) value); }
        }

        public char Char
        {
            get { return CharUnion.UnicodeChar; }
            set { CharUnion.UnicodeChar = value; }
        }
    }
}